using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App
{
	public class ConverterBase64ImageSource : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var base64Image = value as string;

			if (base64Image == null)
				return null;

			var imageBytes = System.Convert.FromBase64String(base64Image);

			//Assume FromStream dispose the stream
			return ImageSource.FromStream(() => new MemoryStream(imageBytes));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Not implemented as we do not convert back
			throw new NotSupportedException();
		}
	}
}
