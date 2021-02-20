using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Authorize(Roles = "BankCustomer")]
    [ApiController]
    [Route("[controller]")]
    public class BankCustomerController : ControllerBase
    {
        private readonly Database _database;

        public BankCustomerController(Database database)
        {
            _database = database;
        }

        [HttpGet]
        public string Get()
        {
            return _database.Accounts.SingleOrDefault()?.Amount.ToString() ?? "";
        }
    }
}