using System.Collections.Generic;
using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Usual_image_to_Emgu.CV.Image_of_Gray
{
    public sealed class Converter
    {
        private static readonly System.Globalization.CultureInfo Deu = new System.Globalization.CultureInfo("de-DE");
        public static void ToGray(List<System.IO.FileInfo> FInfos, string Folderpath)
        {
            if (FInfos is null || FInfos.Count == 0)
            {
                return ;
            }

            for (int i = 0; i < FInfos.Count; i++)
            {
                using (System.Drawing.Bitmap _bitmap = new System.Drawing.Bitmap(FInfos[i].FullName))
                {
                    using (Image<Bgr, byte> bgrImg = _bitmap.ToImage<Bgr, byte>())
                    {
                        using (Image<Gray, byte> GrayImg = bgrImg.Convert<Gray, byte>())
                        {
                            GrayImg.Save($"{Folderpath}\\{DateTime.Now.Year.ToString(Deu).PadLeft(4, '0')}-{DateTime.Now.Month.ToString(Deu).PadLeft(2, '0')}-{DateTime.Now.Day.ToString(Deu).PadLeft(2, '0')} {DateTime.Now.Hour.ToString(Deu).PadLeft(2, '0')}-{DateTime.Now.Minute.ToString(Deu).PadLeft(2, '0')}-{DateTime.Now.Second.ToString(Deu).PadLeft(2, '0')} {(i + 1).ToString(Deu).PadLeft(3, '0')}.bmp");
                        }      
                    }
                }
            }//for
            return;
        }
    }
}