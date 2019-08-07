using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Service
{
    public class S3Service: IS3Service
    {
        private readonly IAmazonS3 _cliente;

        public S3Service(IAmazonS3 cliente)
        {
            _cliente = cliente;
        }

        public async Task<S3Response> CreateBuskcetAsync(string bucketName)
        {
            try
            {
                if(await AmazonS3Util.DoesS3BucketExistAsync(_cliente,bucketName)==false)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    var response = await _cliente.PutBucketAsync(putBucketRequest);
                    return new S3Response
                    {
                        Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode
                    };
                }
            }
            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    Status = e.StatusCode,
                    Message = e.Message
                };
            }
            catch(Exception e)
            {
                return new S3Response
                {
                    Status = System.Net.HttpStatusCode.InternalServerError,
                    Message = e.Message
                };
            }

            return new S3Response
            {
                Status = System.Net.HttpStatusCode.InternalServerError,
                Message = "Error Inesperado"
            };
        }


        private const string FilePath = "C:\\Users\\jonathan\\AWS\\S3BUCKET\\ejemploSemtelco.pdf";///Ruta donde se sube el archvio verificar esta parte donde guardar el archivo en el mvil
        private const string UploadWithKeyName = "UploadWithKeyName";
        private const string FileStreamUpload = "FileStreamUpload";
        private const string AdvanceUpload = "AdvancedUpload";


        public async Task UploadFileAsync(string bucketName)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_cliente);

                using (var fileToupload = new FileStream(FilePath,FileMode.Open,FileAccess.Read))
                {
                    await fileTransferUtility.UploadAsync(fileToupload, bucketName, FileStreamUpload);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
