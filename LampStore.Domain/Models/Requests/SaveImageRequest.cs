using System;

namespace LampStore.Domain.Models.Requests
{
    public class SaveImageRequest
    {
        public int ImageId { get; set; }
        public string Base64Image { get; set; }
        public byte[] File => GetImageBytes();
        private byte[] GetImageBytes()
        {
            var fixedImage = Base64Image.Replace("data:image/png;base64,", string.Empty);

            return string.IsNullOrEmpty(Base64Image) ? null : Convert.FromBase64String(fixedImage);
        }
    }
}