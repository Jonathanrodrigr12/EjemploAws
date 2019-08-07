using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Service
{
    public interface IS3Service
    {
        Task<S3Response> CreateBuskcetAsync(string bucketName);

        Task UploadFileAsync(string bucketName);
    }
}
