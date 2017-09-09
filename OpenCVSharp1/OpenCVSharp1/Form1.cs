using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OpenCVSharp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        Mat src = new Mat();
        Mat dstrev = new Mat();
        private Mat dstlog;
        private Mat dstgama;

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG |All files (*.*)|*.*";
            dialog.InitialDirectory = @"D:\OCV\";
            dialog.Title = "Please select an image file.";

            if (dialog.ShowDialog() == DialogResult.OK){
                pictureBox1.ImageLocation = dialog.FileName;
            }
            else
                return;

            src = Cv2.ImRead(dialog.FileName, LoadMode.GrayScale);
            dstrev = src.EmptyClone();
            dstlog = src.EmptyClone();
            dstgama = src.EmptyClone();

        }//button1_click end.
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.GetSelected(0)){
                pictureBox2.Image = BitmapConverter.ToBitmap(src);
            }
            if (listBox1.GetSelected(1)){
                for (int y=0;y<src.Rows;y++){
                    for (int x =0;x<src.Cols;x++){
                        byte sum = src.Get<byte>(y, x);
                        dstrev.Set<byte>(y, x, (byte)(255 - sum));
                    }
                }
                pictureBox2.Image = BitmapConverter.ToBitmap(dstrev);
            }
            if (listBox1.GetSelected(2)){
                for (int y = 0; y < src.Rows; y++){
                    for (int x = 0; x < src.Cols; x++){
                        byte sum = src.Get<byte>(y, x);
                        dstlog.Set<byte>(y, x, (byte)(75 * Math.Log10(1.0 + (double)sum)));
                    }
                }
                pictureBox2.Image = BitmapConverter.ToBitmap(dstlog);
            }
            if (listBox1.GetSelected(3)){
                for (int y = 0; y < src.Rows; y++){
                    for (int x = 0; x < src.Cols; x++){
                        byte sum = src.Get<byte>(y, x);
                        dstgama.Set<byte>(y, x, (byte)(20.0 * Math.Pow((double)sum,4.0)));
                    }
                }
                pictureBox2.Image = BitmapConverter.ToBitmap(dstgama);
            }
        }
    }//partial class end.
}//namespace end.
