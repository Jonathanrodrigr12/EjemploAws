using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Service
{
    public class S3Response
    {
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
