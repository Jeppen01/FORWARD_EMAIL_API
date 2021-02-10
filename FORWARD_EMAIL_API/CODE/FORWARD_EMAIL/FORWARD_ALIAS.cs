using FORWARD_EMAIL_API.CODE.COMMON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FORWARD_EMAIL_API.CODE.FORWARD_EMAIL
{
    public class FORWARD_ALIAS
    {
        public static JArray getAliasDetails(string name = "", string recipients = "")
        {
            try
            {
                var restClient = FORWARD_COMMON.getRestClient();

                //the request
                var request = new RestRequest(Method.GET);
                request.Resource = FORWARD_COMMON.Aliases;

                request.AddParameter("name", name);
                request.AddParameter("recipients", recipients);

                IRestResponse response = restClient.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<JArray>(response.Content);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError("FORWARD_ALIAS", MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());

                return null;
            }
        }

        public static bool createAlias(string name, string recipients, string description = "", string labels = "", bool is_enabled = true)
        {
            try
            {
                var restClient = FORWARD_COMMON.getRestClient();

                //the request
                var request = new RestRequest(Method.POST);
                request.Resource = FORWARD_COMMON.Aliases;

                request.AddParameter("name", name);
                request.AddParameter("recipients", recipients);
                request.AddParameter("description", description);
                request.AddParameter("labels", labels);
                request.AddParameter("is_enabled", is_enabled);

                IRestResponse response = restClient.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError("FORWARD_ALIAS", MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());

                return false;
            }
        }

        public static bool updateAlias(string old_name, string new_name, string recipients, string description = "", string labels = "", bool is_enabled = true)
        {
            try
            {
                //get the detailks
                JArray temp_alias = getAliasDetails(old_name);
                //failed to retrieve
                if (temp_alias == null)
                {
                    return false;
                }

                string id_to_del = temp_alias[0]["id"].ToString();

                var restClient = FORWARD_COMMON.getRestClient();

                //the request
                var request = new RestRequest(Method.PUT);
                request.Resource = FORWARD_COMMON.Aliases + "/" + id_to_del;

                request.AddParameter("name", new_name);
                request.AddParameter("recipients", recipients);
                request.AddParameter("description", description);
                request.AddParameter("labels", labels);
                request.AddParameter("is_enabled", is_enabled);

                IRestResponse response = restClient.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError("FORWARD_ALIAS", MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());

                return false;
            }
        }

        public static bool deleteAlias(string name)
        {
            try
            {
                //get the detailks
                JArray alias_to_delete = getAliasDetails(name);
                //failed to retrieve
                if (alias_to_delete == null)
                {
                    return false;
                }

                string id_to_del = alias_to_delete[0]["id"].ToString();

                var restClient = FORWARD_COMMON.getRestClient();

                //the request
                var request = new RestRequest(Method.DELETE);
                request.Resource = FORWARD_COMMON.Aliases + "/" + id_to_del;

                IRestResponse response = restClient.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError("FORWARD_ALIAS", MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());

                return false;
            }
        }
    }
}
