using ApiSMS.Data;
using ApiSMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SolutionApiSMS.Services;
using System;
using System.Threading.Tasks;

namespace SolutionApiSMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesssageController : ControllerBase
    {
        private readonly SendSmsService _smsService;

        public MesssageController()
        {
            this._smsService = new SendSmsService();
        }
       
        
        MessageData data = new MessageData();
        [HttpGet]

        public JsonResult Get()
        {
            return new JsonResult( data.list());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageModel objMsg )
        {
            string[] numbers = objMsg.ToNumber.Split(',');

            MessageModel _objMsg;
            MessageData  cn = new MessageData();
            MsgResponseTwModel _objMsgResponse;

            int  resultResponse = 0;

            cn = new MessageData();

            foreach (var num in numbers)
            {
                _objMsg = new MessageModel();
                _objMsg.ToNumber = num;
                _objMsg.FromNumber = objMsg.FromNumber;
                _objMsg.Message = objMsg.Message;

                var result = await _smsService.SendSmsAsync(_objMsg);

                object id = cn.InsertMsg(_objMsg);

                _objMsgResponse = new MsgResponseTwModel();

                _objMsgResponse.confirmationCode = result.Sid;
                _objMsgResponse.IdMessage = Convert.ToInt32(id);
                _objMsgResponse.DateSent  = Convert.ToString(result.DateSent);


                if ((Int32)id != 0)
                {
                    resultResponse  = cn.InsertMsgResoponse(_objMsgResponse);

                }

            }

            if (resultResponse!= 0)
            {
                return new JsonResult(data.listResponses());
            }
            else
            { 
                return null; 
            };
        }
    }
}
