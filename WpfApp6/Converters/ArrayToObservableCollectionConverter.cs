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
            if (value is not State[,] array)
                return null;

            ObservableCollection <ObservableCollection<State>> collection = new();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                ObservableCollection<State> collection2 = new();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    collection2.Add(array[i, j]);
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
