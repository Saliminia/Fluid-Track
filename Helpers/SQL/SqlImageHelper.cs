using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DMR
{
    public class SqlImageHelper
    {

        //-------------------------------------------------------
        public static byte[] ImageToByteArray(Image img)
        {
            MemoryStream ms = new MemoryStream();

            img.Save(ms, ImageFormat.Png);
            byte[] byteArr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(byteArr, 0, byteArr.Length);
            //ms.Dispose(); : do not dispose stream to avoid "A generic error occurred in GDI+."

            return byteArr;
        }
        //-------------------------------------------------------
        public static Image ByteArrayToImage(byte[] arr)
        {
            MemoryStream ms = new MemoryStream(arr);
            Image img = Image.FromStream(ms);
            //ms.Dispose(); : do not dispose stream to avoid "A generic error occurred in GDI+."
            return img;
        }
    }
}
