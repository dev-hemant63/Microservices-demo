using VMart.Services.ProductAPI.Models.Dto;

namespace VMart.Services.ProductAPI.Services.IServices
{
    public interface IFileUploadService
    {
        ResponseDto Upload(FileUploadDto request);
    }
}
