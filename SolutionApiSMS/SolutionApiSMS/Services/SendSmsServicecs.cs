using ApiSMS.Models;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SolutionApiSMS.Services
{
    public class SendSmsService  
    {
        public async Task<MessageResource> SendSmsAsync(MessageModel  Msg)
        {
            var result = await MessageResource.CreateAsync(
                from: new PhoneNumber(Msg.FromNumber),
                    to: new PhoneNumber(Msg.ToNumber),
                    body: Msg.Message);
            return result;

        }
    }
}
