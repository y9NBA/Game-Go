using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp6.Infrastructure.Commands;
using WpfApp6.ViewModels.Base;
using WpfApp6.Infrastructure.Commands.Base;
using System.Reflection.Metadata;
using WpfApp6.Models;

namespace WpfApp6.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private Game game;
        public Game Game 
        {
            get => game;
            set => Set(ref game, value);
        }

        public MainWindowViewModel()
        {
            game = new Game();
            Size = "10";
            while (game.Color != null) Color = game.Color;
        }

        ObservableCollection<ObservableCollection<Field>> f = new();
        public ObservableCollection<ObservableCollection<Field>> F
        {
            get => f;
            set => Set(ref f, value);
        }

        DataTable field;
        public DataTable Field
        {
            get => field;
            set => Set(ref field, value);            
        }

        string color;
        public string Color
        { 
            get => color;
            set => Set(ref color, value);
        }

        int size;
        public string Size
        {
            get => size.ToString();
            set 
            {
                if (int.TryParse(value, out int newSize))
                {
                    Set(ref size, newSize);

                    DataTable newField = new();
                    for (int i = 0; i < newSize; i++)
                        newField.Columns.Add();
                    
                    for (int i = 0; i < newSize; i++)
                    {
                        object[] row = new object[newSize];
                        for (int j = 0; j < size; j++)
                            row[j] = i * 10 + j;
                        newField.Rows.Add(row);
                    }
                    Field = newField;


                    f.Clear();                    
                    for (int i = 0; i < newSize; i++)
                    {
                        ObservableCollection<Field> fRow = new();
                        for (int j = 0; j < size; j++)
                            fRow.Add(new Field() {I = i, J = j });
                        f.Add(fRow);
                    }
                }
            }
        }
    }
}
