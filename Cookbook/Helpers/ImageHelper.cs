using System.Drawing;
using System.IO;

namespace Cookbook.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
            //ImageConverter imgCon = new ImageConverter();
            //return (byte[])imgCon.ConvertTo(img, typeof(byte[]));
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                var returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }
    }
}
