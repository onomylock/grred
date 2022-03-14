using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

using iTextSharp;
using iTextSharp.text.pdf;


using GrRed;

namespace GrRed.IO
{
    public class Io
    {
        public string Output_json(IFigure obj)
        {
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            };
            var str = JsonConvert.SerializeObject(obj, settings);
            return str;
        }

        public static IFigure Input_json(string str)
        {
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            };

            var tmp = JsonConvert.DeserializeObject(str, settings);
            var obj = tmp as IFigure;
            return obj;
        }

        public static void CanvToPNG(Canvas canvas, string filename)
        {
            canvas.LayoutTransform = null;

            //качество изображения
            double dpi = 300;
            double scale = dpi / 96;

            Size size = canvas.RenderSize;
            RenderTargetBitmap image = new RenderTargetBitmap((int)(size.Width * scale), (int)(size.Height * scale), dpi, dpi, PixelFormats.Pbgra32);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            image.Render(canvas);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
           
            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }

        private void CanvasToPDF(Canvas canvas)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Canvas"; 
            dlg.DefaultExt = ".pdf"; 
            dlg.Filter = "Text documents (.pdf)|*.pdf"; 
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight + 125, 96d, 96d, PixelFormats.Pbgra32);
                renderBitmap.Render(canvas);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                byte[] bytes;
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }

                var document = new iTextSharp.text.Document(new iTextSharp.text.Rectangle((float)canvas.ActualWidth, (float)canvas.ActualHeight), 0, 0, 0, 0);
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bytes);
                image.SetAbsolutePosition(0, 0);

                FileStream file = File.Create(dlg.FileName);
                PdfWriter.GetInstance(document, file);
                document.Open();
                document.Add(image);
                document.Close();
            }

        }
    }
}
