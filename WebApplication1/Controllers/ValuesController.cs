using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IS3Service _service;

        public ValuesController(IS3Service service)
        {
            _service = service;
        }

        [HttpGet]
        public string numero()
        {
            return "123";
        }
        [HttpPost("{bucketName}")]
        public async Task<IActionResult> CreateBucket([FromRoute] string bucketName)
        {
            var response = await _service.CreateBuskcetAsync(bucketName);
            return Ok(response);
        }

        [HttpPost("AddFile/{bucketName}")]  
        public async Task<IActionResult> AddFile([FromRoute] string bucketName)
        {
            await _service.UploadFileAsync(bucketName);

            return Ok();
        }
    }
}
