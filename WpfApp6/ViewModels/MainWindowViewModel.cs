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
using Point = System.Drawing.Point;

namespace Go.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            CreateField();
            currentPlayer = new Player() { Color = "Black" };
            //Size = "5";
        }

        //public RellayCommand SaveCommand 
        //{ 
        //    get => new(SaveCommandRelBack); 
        //}
        //private void SaveCommandRelBack()
        //{

        //}
        public void CreateField()
        {
            Field[,] field = new Field[5, 5];
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = new Field { I = i, J = j, State = "" };
                }
            }
            board = field;
        }
        public Field[,] board;
        public Field[,] Board
        {
            get => board;
            set => Set(ref board, value);
        }
        //private int size = 5;
        //public string Size
        //{
        //    get => size.ToString();
        //    set
        //    {
        //        if (int.TryParse(value, out int newSize))
        //        {
        //            Set(ref size, newSize);
        //            Field[,] field = new Field[newSize, newSize];
        //            for (int i = 0; i < field.GetLength(0); i++)
        //            {
        //                for (int j = 0; j < field.GetLength(1); j++)
        //                {
        //                    field[i, j] = new Field { I = i, J = j, Color = 0 };
        //                }
        //            }
        //            Array.Clear(board);
        //            Board = field;
        //        }
        //    }
        //}

        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get => currentPlayer;
            set => Set(ref currentPlayer, value);
        }

        public void CheckBoard(Field field)
        {
            if (currentPlayer == null)
            {
                Move(currentPlayer, field.I, field.J, field);

                OnPropertyChanged(nameof(Board));
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }
        public void Move(Player player, int x, int y, Field field)
        {
            //InputState(field, player.Color);
            if (IsCapture(x, y, player.Color))
            {
                Capture(x, y, player.Color);
            }
        }

        public bool IsValidMove(Field field, string color)
        {
            //if (field.State != "") return false;
            bool captured = false;
            foreach (Point n in GetNeighbors(field))
            {
                if (board[n.X, n.Y].State == OppositeColor(color) && IsCapture(n.X, n.Y, color))
                {
                    captured = true; break;
                }
            }
            if (!captured)
            {
                InputState(field, color);
                bool connected = false;
                foreach (Point n in GetNeighbors(field))
                {
                    if (GetState(field) == color && IsConnected(n.X, n.Y))
                    {
                        connected = true; break;
                    }
                }
                if (!connected)
                {
                    InputState(field, "");
                    return false;
                }
            }
            return true;
        }

        private bool IsConnected(int x, int y)
        {
            if (GetState(x, y) == "") return false;
            else
            {
                string groupNumber = GetState(x, y);
                foreach (Point n in GetNeighbors(x, y))
                {
                    if (GetState(n.X, n.Y) == groupNumber) return true;
                }
            }
            return false;
        }

        private string OppositeColor(string color)
        {
            if (color == "White") return "Black";
            else if (color == "Black") return "White";
            else return "";
        }

        private List<Point> GetNeighbors(int x, int y)
        {
            List<Point> neihbors = new List<Point>();
            if (x > 0 && GetState(x - 1, y) == "")
                neihbors.Add(new Point(x - 1, y));
            if (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == "")
                neihbors.Add(new Point(x + 1, y));
            if (y > 0 && GetState(x, y - 1) == "")
                neihbors.Add(new Point(x, y - 1));
            if (y < Board.GetLength(1) - 1 && GetState(x, y) == "")
                neihbors.Add(new Point(x, y + 1));
            return neihbors;
        }
        private List<Point> GetNeighbors(Field field)
        {
            List<Point> neihbors = new List<Point>();
            if (field.I > 0 && GetState(field.I - 1, field.J) == "")
                neihbors.Add(new Point(field.I - 1, field.J));
            if (field.I < Board.GetLength(0) - 1 && GetState(field.I + 1, field.J) == "")
                neihbors.Add(new Point(field.I + 1, field.J));
            if (field.J > 0 && GetState(field.I, field.J - 1) == "")
                neihbors.Add(new Point(field.I, field.J - 1));
            if (field.J < Board.GetLength(1) - 1 && GetState(field) == "")
                neihbors.Add(new Point(field.I, field.J + 1));
            return neihbors;
        }
        public bool HasLiberties(int x, int y)
        {
            string color = GetState(x, y);

            if ((x > 0 && GetState(x - 1, y) == "") ||
                (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == "") ||
                (y > 0 && GetState(x, y - 1) == "") ||
                (y < Board.GetLength(1) - 1 && GetState(x, y + 1) == ""))
            {
                return true;
            }

            if ((x > 0 && GetState(x - 1, y) == color && HasLiberties(x - 1, y)) ||
                (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == color && HasLiberties(x + 1, y)) ||
                (y > 0 && GetState(x, y - 1) == color && HasLiberties(x, y - 1)) ||
                (y < Board.GetLength(1) - 1 && GetState(x, y + 1) == color && HasLiberties(x, y + 1)))
            {
                return true;
            }

            return false;
        }


        private bool IsCapture(int x, int y, string color)
        {
            if (x > 0 && GetState(x - 1, y) == color && !HasLiberties(x - 1, y))
            {
                InputState(x - 1, y, "");
                return true;
            }
            if (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == color && !HasLiberties(x + 1, y))
            {
                InputState(x + 1, y, "");
                return true;
            }
            if (y > 0 && GetState(x, y - 1) == color && !HasLiberties(x, y - 1))
            {
                InputState(x, y - 1, "");
                return true;
            }
            if (y < Board.GetLength(1) - 1 && GetState(x, y + 1) == color && !HasLiberties(x, y + 1))
            {
                InputState(x, y + 1, "");
                return true;
            }

            return false;
        }

        private void Capture(int x, int y, string color)
        {
            {
                if (IsCapture(x, y, color))
                {
                    if (x > 0 && GetState(x - 1, y) == "") HasLiberties(x - 1, y);
                    if (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == "") HasLiberties(x + 1, y);
                    if (y > 0 && GetState(x, y - 1) == "") HasLiberties(x, y - 1);
                    if (y < Board.GetLength(1) - 1 && GetState(x, y + 1) == "") HasLiberties(x, y + 1);
                }
            }
        }
        public string GetState(int x, int y)
        {
            return board[x, y].State;
        }
        public string GetState(Field field)
        {
            return field.State;
        }
        public void InputState(int x, int y, string color)
        {
            board[x,y].State = color;
        }
        public void InputState(Field field, string color)
        {
            field.State = color;
        }

        //public IEnumerable<int> BoardField
        //{
        //    get
        //    {
        //        for (int i = 0; i < 5; i++)
        //        {
        //            for (int j = 0; j < 5; j++)
        //            {
        //                yield return board.Field[i, j];
        //            }
        //        }
        //    }
        //}

        //private void Click_On_Board(object sender, EventArgs e)
        //{
        //    var button = sender as Button;
        //    var state = button.Tag as Field;
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
