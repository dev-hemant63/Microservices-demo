using Azure;
using System.Text;
using System;
using VMart.Services.ProductAPI.Services.IServices;
using VMart.Services.ProductAPI.Models.Dto;
using System.Net.Http.Headers;

namespace VMart.Services.ProductAPI.Services
{
    public class FileUploadService: IFileUploadService
    {
        private readonly IRequestInfo _rinfo;
        public FileUploadService(IRequestInfo requestInfo)
        {
            _rinfo = requestInfo;
        }
        public ResponseDto Upload(FileUploadDto request)
        {
            var response = new ResponseDto
            {
                Message = "Failed to upload file.",
            };
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(request.FilePath);
                if (!Directory.Exists(sb.ToString()))
                {
                    Directory.CreateDirectory(sb.ToString());
                }
                var filename = ContentDispositionHeaderValue.Parse(request.file.ContentDisposition).FileName.Trim('"');
                string originalExt = Path.GetExtension(filename).ToLower();
                string[] Extensions = { ".png", ".jpeg", ".jpg", ".webp", ".pdf" };
                if (!Extensions.Contains(originalExt))
                {
                    response.Message = "You can only upload JPEG, JPG, and PNG files.";
                    return response;
                }
                if (string.IsNullOrEmpty(request.FileName))
                {
                    request.FileName = filename;
                }
                sb.Append($"{request.FileName}{originalExt}");
                using (FileStream fs = File.Create(sb.ToString()))
                {
                    request.file.CopyTo(fs);
                    fs.Flush();
                }
                response.IsSuccess = true;
                response.Message = "Success";
                response.Result = $"{_rinfo.GetDomain()}/{request.FilePath.Replace("wwwroot/", "")}{request.FileName}{originalExt}";
            }
            catch (Exception ex)
            {
                response.Message = "Error in file uploading. Try after sometime...";
            }
            return response;
        }
    }
}
