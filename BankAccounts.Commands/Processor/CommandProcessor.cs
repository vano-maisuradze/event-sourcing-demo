using App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BankAccounts.Commands.Processor
{
    public static class CommandProcessor
    {
        private static List<Type> _handlers;
        private static IServiceProvider _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider, IEnumerable<Type> handlers)
        {
            _handlers = new List<Type>();
            _handlers.AddRange(handlers);
            _serviceProvider = serviceProvider;
        }

        public static CommandResult Execute(ICommand command)
        {
            if (command == null) return CommandResult.Error("Command is null!");

            try
            {
                var handledBy = new List<string>();

                foreach (var handlerType in _handlers)
                {
                    bool canHandle = handlerType.GetInterfaces()
                        .Any(x => x.GetTypeInfo().IsGenericType
                            && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                            && x.GenericTypeArguments[0] == command.GetType());

                    if (canHandle)
                    {
                        var ctor = handlerType.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
                        var parameters = ctor.GetParameters();

                        var handlerArgs = new object[parameters.Length];
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            handlerArgs[i] = _serviceProvider.GetService(parameters[i].ParameterType);
                        }

                        dynamic handler = Activator.CreateInstance(handlerType, handlerArgs);
                        handler.Handle((dynamic)command);
                        handledBy.Add(handlerType.Name);
                    }
                }

                return CommandResult.Success($"Command executed successfully! Command was handled by: {string.Join(',', handledBy)}");
            }
            catch (Exception ex)
            {
                return CommandResult.Error(ex.Message);
            }
        }

    }
}
