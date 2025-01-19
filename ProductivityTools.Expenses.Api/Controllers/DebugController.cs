using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Database;

namespace ProductivityTools.Expenses.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : Controller
    {
        private readonly ExpensesContext ExpensesContext;

        public DebugController(ExpensesContext context)
        {
            this.ExpensesContext = context;
        }

        [HttpGet]
        [Route("Date")]
        public string Date()
        {
            return DateTime.Now.ToString();
        }

        [HttpGet]
        [Route("AppName")]
        public string AppName()
        {
            return "PTExpenses";
        }

        [HttpGet]
        [Route("Hello")]
        public string Hello(string name)
        {
            return string.Concat($"Hello {name.ToString()} Current date:{DateTime.Now}");
        }

        [HttpGet]
        [Route("ServerName")]
        public string ServerName()
        {
            string server = this.ExpensesContext.Database.SqlQuery<string>($"select @@SERVERNAME as value").Single();
            return server;
        }
    }
}
