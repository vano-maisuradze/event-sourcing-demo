namespace App.Core
{
    public class CommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }


        public static CommandResult Success(string message)
        {
            return new CommandResult() { IsSuccess = true, Message = message };
        }

        public static CommandResult Error(string message)
        {
            return new CommandResult() { IsSuccess = false, Message = message };
        }

        public static CommandResult Success()
        {
            return Success("");
        }

        public static CommandResult Error()
        {
            return Error("");
        }
    }
}
