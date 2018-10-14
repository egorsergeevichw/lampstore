using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace LampStore.Domain.Utils
{
    public static class FilesUtils
    {
        public static FilePath UploadImage(byte[] fileData)
        {
            var fileId = Guid.NewGuid();

            var filePath = new FilePath
            {
                FileId = fileId,
                Url = $"AppServer/lampstore/files/{fileId}.png"
            };

            var client = new WebClient();

            client.UploadData(filePath.Url, "PUT", fileData);

            return filePath;
        }

        public static FilePath SaveImage(byte[] fileData)
        {
            var fileId = Guid.NewGuid();

            var ms = new MemoryStream(fileData, 0, fileData.Length);
            ms.Write(fileData, 0, fileData.Length);
            var image = Image.FromStream(ms, true);

            var filePath = new FilePath
            {
                FileId = fileId,
                Url = $"D:\\lampstore\\SourceTree\\LampStore\\LampStore.WebUI\\Files\\{fileId}.png"
            };

            image.Save(filePath.Url, ImageFormat.Png);

            return filePath;
        }
    }

    public class FilePath
    {
        public Guid FileId { get; set; }
        public string Url { get; set; }
    }
}
