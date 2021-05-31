using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ark.net.util
{
    [Area("ark")]
    [ApiController]
    public class UtilController : ControllerBase
    {
        [HttpPost]
        [Route("api/upload")]
        public dynamic UploadFile()
        {
            return new Logic().PersistImage(Request.Form.Files);
        }
    }
}
