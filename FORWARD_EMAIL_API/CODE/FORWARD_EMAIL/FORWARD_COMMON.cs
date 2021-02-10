using FORWARD_EMAIL_API.CODE.COMMON;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace FORWARD_EMAIL_API.CODE.FORWARD_EMAIL
{
    public class FORWARD_COMMON
    {
        public static RestClient getRestClient()
        {
            RestClient myclient = new RestClient(Startup.g_AppSettings.FORWARD_API_URL);
            myclient.Authenticator = new HttpBasicAuthenticator(Startup.g_AppSettings.api_user, "");

            return myclient;
        }

        public static string Aliases
        {
            get { return "/v1/domains/" + Startup.g_AppSettings.DomainName + "/aliases"; }
        }

        public static List<Attachment> convertAttachments(JArray attachments)
        {
            try
            {
                List<Attachment> result_files = new List<Attachment>();

                foreach (JToken attachment in attachments)
                {
                    //get the content
                    Stream content = new MemoryStream(attachment["content"]["data"].ToObject<byte[]>());

                    //create attachment
                    Attachment mail_attachment = new Attachment(content, attachment["filename"].ToString(), attachment["contentType"].ToString());

                    result_files.Add(mail_attachment);
                }

                return result_files;
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError("FORWARD_COMMON", MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());
                return null;
            }
        }

        public static List<string> sentToToList(JArray distribution_list)
        {
            try
            {
                List<string> distribution_result = new List<string>();

                foreach (JToken distribution in distribution_list)
                {
                    distribution_result.Add(distribution["address"].ToString());
                }

                return distribution_result;
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError("FORWARD_COMMON", MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());
                return null;
            }
        }
    }
}
