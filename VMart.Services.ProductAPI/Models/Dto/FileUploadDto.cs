﻿namespace VMart.Services.ProductAPI.Models.Dto
{
    public class FileUploadDto
    {
        public IFormFile file { get; set; }
        public List<IFormFile> Files { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Id { get; set; }
        public bool IsThumbnailRequired { get; set; }
    }
}
