﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Task3.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            var userName = RequestContext.Principal.Identity.Name;
            return String.Format("Hello, {0}.", userName);
        }
    }
}
