using Microsoft.AspNetCore.Http;

namespace Recipe.Service.Business.Utils
{
    public class ByteArrayHelper
    {
        public static (string? fileType, byte[]? archiveData) ImageToByteArray(IFormFile imageData)
        {
            byte[]? bytes = null;
            string? fileName = null;
            if (imageData?.Length > 0)
            {
                fileName = imageData.FileName;
                using (var fs = imageData.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    bytes = ms.ToArray();
                }
            }
            return (fileName, bytes);
        }
    }
}
