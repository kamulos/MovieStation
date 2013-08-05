using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MovieStation.Converters
{
    public class LanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            LanguageField lang = value as LanguageField;
            string img;

            if (lang.English && !lang.German)
            {
                img = "en";
            }
            else if (!lang.English && lang.German)
            {
                img = "de";
            }
            else if (lang.English && lang.German)
            {
                img = "en_de";
            }
            else
            {
                img = "none";
            }

            return new BitmapImage(new Uri("/MovieStation;component/Images/" + img + ".png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
