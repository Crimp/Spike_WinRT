using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;

namespace TestClientApplication.Converters {
    //[ValueConversionAttribute(typeof(DateTime), typeof(String))]
    public class BytesToImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            byte[] bytearr = value as byte[];
            if(bytearr != null) {
                BitmapImage image = new BitmapImage();

                InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
                ms.AsStreamForWrite().Write(bytearr, 0, bytearr.Length);
                ms.Seek(0);

                image.SetSource(ms);
                ImageSource src = image;

                return src;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
