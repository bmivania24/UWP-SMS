using ApiSMS.Data;
using ApiSMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SolutionApiSMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsgResponseTwController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public MsgResponseTwController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        MessageData data = new MessageData();
        [HttpGet]

        public JsonResult Get()
        {
            return new JsonResult(data.listResponses());
        }

        [HttpPost]
        public JsonResult Post(MsgResponseTwModel model)
        {
            return new JsonResult(data.InsertMsgResoponse (model));
        }
    }
}
