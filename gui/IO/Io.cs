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
    }
}
