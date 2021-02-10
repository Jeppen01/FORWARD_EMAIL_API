using FORWARD_EMAIL_API.CODE.COMMON;
using FORWARD_EMAIL_API.CODE.FORWARD_EMAIL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FORWARD_EMAIL_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class FORWARD_API : Controller
    {
        [HttpGet]
        public string getAliasDetails(string AliasName)
        {
            return FORWARD_ALIAS.getAliasDetails(AliasName).ToString();
        }

        [HttpGet]
        public bool createAlias(string AliasName, string recipients)
        {
            return FORWARD_ALIAS.createAlias(AliasName, recipients);
        }

        [HttpGet]
        public bool updateAlias(string OldAliasName, string newAliasName, string recipients)
        {
            return FORWARD_ALIAS.updateAlias(OldAliasName, newAliasName, recipients);
        }

        [HttpGet]
        public bool deleteAlias(string AliasName)
        {
            return FORWARD_ALIAS.deleteAlias(AliasName);
        }
    }
}
