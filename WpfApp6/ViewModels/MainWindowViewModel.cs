using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Go.Infrastructure.Commands;
using Go.ViewModels.Base;
using Go.Infrastructure.Commands.Base;
using System.Reflection.Metadata;
using Go.Models;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace Go.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            //board = new Board();
            currentPlayer = new Player() { Color = 2 };
        }

        
        public RellayCommand SaveCommand 
        { 
            get => new(SaveCommandRelBack); 
        }
        private void SaveCommandRelBack()
        {
        
        }

        private Board board = new();
        public string Board
        {
            get
            {
                for (int i = 0; i < board.Field.GetLength(0); i++)
                {
                    for (int j = 0; j < board.Field.GetLength(1); j++)
                    {
                        return board.Field[i, j].CurrentState.ToString();
                    }
                }
            }
        }
        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get => currentPlayer;
        }

        public void MakeMove(int x, int y)
        {
            board.MakeMove(currentPlayer, x, y);
            currentPlayer = currentPlayer.Color == 0 ? new Player { Color = 2 } : new Player { Color = 1 };

            OnPropertyChanged(nameof(Board));
            OnPropertyChanged(nameof(CurrentPlayer));
        }
        
        //public IEnumerable<int> BoardField
        //{
        //    get
        //    {
        //        for(int i = 0; i < 5; i++)
        //        {
        //            for(int j = 0; j < 5; j++)
        //            {
        //                yield return board.Field[i,j].CurrentState;
        //            }
        //        }
        //    }
        //}

        //private void Click_On_Board(object sender, EventArgs e)
        //{
        //    var button = sender as Button;
        //    var state = button.Tag as State;
        //    int x = state.I;
        //    int y = state.J;
        //    MakeMove(x, y);
        //}

        //private Game game;
        //public Game Game
        //{
        //    get => game;
        //    set => Set(ref game, value);
        //}

        //private Player player1 = new() { Color = 0 };
        //public Player Player1
        //{
        //    get => player1;
        //    set => Set(ref player1, value);
        //}

        //private Player player2 = new() { Color = 1 };
        //public Player Player2
        //{
        //    get => player2;
        //    set => Set(ref player2, value);
        //}

        //private string[,] field;
        //public string[,] Field
        //{
        //    get => field;
        //    set => Set( ref field, value);
        //}

        //private int size= 30;
        //public string Size
        //{
        //    get => size.ToString();
        //    set
        //    {
        //        if (int.TryParse(value, out int sizefield))
        //        {
        //            Set(ref size, sizefield);

        //            string[,] newfields = new string[sizefield, sizefield];
        //            for (int i = 0; i < sizefield; i++)
        //            {
        //                for (int j = 0; j < sizefield; j++)
        //                {
        //                    newfields[i, j] = "";
        //                }
        //            }
        //            Field = newfields;
        //        }
        //    }
        //}

    }
}
