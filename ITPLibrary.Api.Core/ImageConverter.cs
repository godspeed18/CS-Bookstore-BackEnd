using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace ITPLibrary.Api.Core
{
    public static class ImageConverter
    {
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
            catch
            {
                return null;
            }
        }

        public static byte[] ImagePathToByteArray(string imagePath)
        {
            byte[] data = null;

            FileInfo fInfo = new FileInfo(imagePath);
            long numBytes = fInfo.Length;

            FileStream fStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        public static async Task<byte[]> FormFileToByteArray(IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
