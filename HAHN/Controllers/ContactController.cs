using HAHN.Application.Contacts;
using HAHN.Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HAHN.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [Route("api/Contact/GetList")]
        
        public async Task<IActionResult> GetList()
        {
            var resp = await new ContactService().GetList();
            return new ObjectResult(resp.Data);
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [Route("api/Contact/Get")]
        public async Task<IActionResult> Get(int id)
        {
            var resp = await new ContactService().GetItem(id);
            return new ObjectResult(resp);
        }

        [HttpPut]
        [EnableCors("AllowOrigin")]
        [Route("api/Contact/Put")]
        public async Task<IActionResult> Put(ContactModel model)
        {
            var resp = await new ContactService().PutItem(model);
            return new ObjectResult(resp);
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Route("api/Contact/Post")]
        public async Task<IActionResult> Post(ContactModel model)
        {
            var resp = await new ContactService().PostItem(model);
            return new ObjectResult(resp);
        }

        [HttpDelete]
        [EnableCors("AllowOrigin")]
        [Route("api/Contact/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await new ContactService().DeleteItem(id);
            return new ObjectResult(resp);
        }
    }
}
