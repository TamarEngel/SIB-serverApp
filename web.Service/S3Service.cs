using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Services;

namespace web.Service
{
    public class S3Service:IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            //var awsOptions = configuration.GetSection("AWS");
            //var accessKey = awsOptions["AccessKey"];
            //var secretKey = awsOptions["SecretKey"];
            //var region = awsOptions["Region"];
            //_bucketName = awsOptions["BucketName"];

            //_s3Client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(region));

            var awsOptions = configuration.GetSection("AWS");
            //var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
            var accessKey = awsOptions["AccessKey"];
            //var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var secretKey = awsOptions["SecretKey"];
            //var region = Environment.GetEnvironmentVariable("AWS_REGION");
            var region = awsOptions["Region"];
            //_bucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET");
            _bucketName = awsOptions["BucketName"];
            //Console.WriteLine($"AWS_ACCESS_KEY: {Environment.GetEnvironmentVariable("AWS_ACCESS_KEY")}");
            //Console.WriteLine($"AWS_SECRET_KEY: {Environment.GetEnvironmentVariable("AWS_SECRET_KEY")}");
            //Console.WriteLine($"AWS_REGION: {Environment.GetEnvironmentVariable("AWS_REGION")}");

            _s3Client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(region));

        }
        public async Task<string> GeneratePresignedUrlAsync(string fileName, string contentType)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(10),
                ContentType = contentType
            };
            var url = _s3Client.GetPreSignedURL(request);

            return url;
        }
        public async Task<string> GetDownloadUrlAsync(string fileName)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(30) // תוקף של 30 דקות
            };

            return _s3Client.GetPreSignedURL(request);
        }
    }
}
