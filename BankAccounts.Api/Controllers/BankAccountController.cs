using App.Core;
using BankAccounts.Commands;
using BankAccounts.Commands.Processor;
using Microsoft.AspNetCore.Mvc;

namespace BankAccounts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        public BankAccountController()
        {
        }

        [HttpPost("create")]
        public CommandResult Create([FromBody] CreateBankAccount command)
        {
            return CommandProcessor.Execute(command);
        }

        [HttpPost("credit")]
        public CommandResult Credit([FromBody] CreditMoney command)
        {
           return CommandProcessor.Execute(command);
        }

        [HttpPost("debit")]
        public CommandResult Debit([FromBody] DebitMoney command)
        {
           return CommandProcessor.Execute(command);
        }

    }
}