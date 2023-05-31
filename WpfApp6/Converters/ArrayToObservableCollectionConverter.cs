using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Go.Models;

namespace Go.Converters
{
    public class ArrayToObservableCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int[,] array)
                return null;

            ObservableCollection <ObservableCollection<string>> collection = new();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                ObservableCollection<string> collection2 = new();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    collection2.Add(array[i, j].ToString());
                }
                collection.Add(collection2);
            }
            return collection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Обратное преобразование не поддерживается");
        }
    }
}
