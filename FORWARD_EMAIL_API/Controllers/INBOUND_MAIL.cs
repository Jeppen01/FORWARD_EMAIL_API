using FORWARD_EMAIL_API.CODE.COMMON;
using FORWARD_EMAIL_API.CODE.FORWARD_EMAIL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace FORWARD_EMAIL_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class INBOUND_MAIL : Controller
    {
        [HttpPost]
        public IActionResult RetrieveMail([FromBody] JsonElement mail)
        {
            try
            {
                if (mail.ToString() == null)
                {
                    return NotFound();
                }

                var mail_obj = JsonConvert.DeserializeObject<JObject>(mail.ToString());

                //Send mail to group
                List<string> sent_to = FORWARD_COMMON.sentToToList(JArray.Parse(mail_obj["to"]["value"].ToString()));
                string sent_from = mail_obj["from"]["value"][0]["address"].ToString();
                DateTime sent_on = Convert.ToDateTime(mail_obj["date"]);

                string subject = mail_obj["subject"].ToString();
                string mail_body = mail_obj["from"]["value"][0]["address"].ToString();

                //has attachements
                List<Attachment> attachments = new List<Attachment>();

                if (JArray.Parse(mail_obj["attachments"].ToString()).Count > 0)
                {
                    attachments = FORWARD_COMMON.convertAttachments(JArray.Parse(mail_obj["attachments"].ToString()));
                }              

                return Ok();
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError(this.GetType().Name, MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());
                return NotFound();
            }
        }

        
    }
}
