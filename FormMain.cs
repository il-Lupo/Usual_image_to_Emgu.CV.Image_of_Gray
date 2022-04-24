using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Usual_image_to_Emgu.CV.Image_of_Gray
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// I use a ‘List of FileInfo’ instead of a ‘List of String’ so that I can sort by the time the images were created. Otherwise, the images are sorted by file name or creation time, but in any case the wrong way round (this is done by the OpenFileDialog). The fact that the images have different names is also a disadvantage.
        /// </summary>
        private List<System.IO.FileInfo> ListOfFileInfoOfThePhotos = new List<System.IO.FileInfo>();

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog OFD = new CommonOpenFileDialog())
            {
                OFD.Title = "Bilder auswählen";
                OFD.Filters.Add(new CommonFileDialogFilter("", ".jpg;.jpeg;.bmp;.png"));
                OFD.Multiselect = true;
                if (System.IO.Directory.Exists(@"C:\Users\Birger\source\repos\Cs\Usual image to Emgu.CV.Image of Gray"))
                {
                    OFD.InitialDirectory = @"C:\Users\Birger\source\repos\Cs\Usual image to Emgu.CV.Image of Gray";
                }
                else
                {
                    OFD.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";  // Arbeitsplatz
                }
                if (OFD.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    foreach (string filename in OFD.FileNames)
                    {
                        ListOfFileInfoOfThePhotos.Add(new System.IO.FileInfo(filename));
                    }
                }
                else
                {
                    return;
                }
            }

            ListOfFileInfoOfThePhotos = ListOfFileInfoOfThePhotos.OrderBy(f => f.CreationTime).ToList();

            string Folderpath = null;

            using (CommonOpenFileDialog FBD = new CommonOpenFileDialog())
            {
                FBD.Title = "Wo speichern?";
                FBD.IsFolderPicker = true;
                if (System.IO.Directory.Exists(@"C:\Users\Birger\source\repos\Cs\Usual image to Emgu.CV.Image of Gray\konvertiert"))
                {
                    FBD.InitialDirectory = @"C:\Users\Birger\source\repos\Cs\Usual image to Emgu.CV.Image of Gray\konvertiert";
                }
                else
                {
                    FBD.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";  // Arbeitsplatz
                }
                if (FBD.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    Folderpath = FBD.FileName;
                }
                else
                {
                    return;
                }
            }

            ButtonStart.BackColor = Color.FromArgb(255, 255, 0); // yellow

            await Task.Run(() => Converter.ToGray(this.ListOfFileInfoOfThePhotos, Folderpath));

            ListOfFileInfoOfThePhotos.Clear();

            ButtonStart.BackColor = Color.FromArgb(0, 200, 23); // Green colour with a tinge of blue.
        }
    }
}