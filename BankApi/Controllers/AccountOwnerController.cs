using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Authorize(Roles = "AccountOwner")]
    [ApiController]
    [Route("[controller]")]
    public class AccountOwnerController : ControllerBase
    {
        private readonly Database _database;

        public AccountOwnerController(Database database)
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