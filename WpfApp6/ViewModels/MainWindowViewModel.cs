﻿using System;
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
            currentPlayer = new Player() { Color = 2 };
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
            State[,] field = new State[5, 5];
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = new State { I = i, J = j, Color = 0 };
                }
            }
            board = field;
        }
        public State[,] board;
        public State[,] Board
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
        //            State[,] field = new State[newSize, newSize];
        //            for (int i = 0; i < field.GetLength(0); i++)
        //            {
        //                for (int j = 0; j < field.GetLength(1); j++)
        //                {
        //                    field[i, j] = new State { I = i, J = j, Color = 0 };
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
        }

        public void MakeMove(State field)
        {
            if (currentPlayer == null)
            {
                MakeMove(currentPlayer, field.I, field.J);
                currentPlayer = currentPlayer.Color == 1 ? new Player { Color = 2 } : new Player { Color = 0 };

                OnPropertyChanged(nameof(field));
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }
        public void MakeMove(Player player, int x, int y)
        {
            if (!IsValidMove(x, y, player.Color))
            {
                return;
            }
            InputState(x, y, player.Color);
            if (IsCapture(x, y, player.Color))
            {
                Capture(x, y, player.Color);
            }
        }

        private bool IsValidMove(int x, int y, int color)
        {
            if (board[x, y].Color != 1) return false;
            bool captured = false;
            foreach (Point n in GetNeighbors(x, y))
            {
                if (board[n.X, n.Y].Color == OppositeColor(color) && IsCapture(n.X, n.Y, color))
                {
                    captured = true; break;
                }
            }
            if (!captured)
            {
                InputState(x, y, color);
                bool connected = false;
                foreach (Point n in GetNeighbors(x, y))
                {
                    if (GetState(x, y) == color && IsConnected(n.X, n.Y))
                    {
                        connected = true; break;
                    }
                }
                if (!connected)
                {
                    InputState(x, y, 0);
                    return false;
                }
            }
            return true;
        }

        private bool IsConnected(int x, int y)
        {
            if (GetState(x, y) == 0) return false;
            else
            {
                int groupNumber = GetState(x, y);
                foreach (Point n in GetNeighbors(x, y))
                {
                    if (GetState(n.X, n.Y) == groupNumber) return true;
                }
            }
            return false;
        }

        private int OppositeColor(int color)
        {
            if (color == 1) return 2;
            else if (color == 2) return 1;
            else return 0;
        }

        private List<Point> GetNeighbors(int x, int y)
        {
            List<Point> neihbors = new List<Point>();
            if (x > 0 && GetState(x - 1, y) == 0)
                neihbors.Add(new Point(x - 1, y));
            if (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == 0)
                neihbors.Add(new Point(x + 1, y));
            if (y > 0 && GetState(x, y - 1) == 0)
                neihbors.Add(new Point(x, y - 1));
            if (y < Board.GetLength(1) - 1 && GetState(x, y) == 0)
                neihbors.Add(new Point(x, y + 1));
            return neihbors;
        }
        public bool HasLiberties(int x, int y)
        {
            int color = GetState(x, y);

            if ((x > 0 && GetState(x - 1, y) == 0) ||
                (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == 0) ||
                (y > 0 && GetState(x, y - 1) == 0) ||
                (y < Board.GetLength(1) - 1 && GetState(x, y + 1) == 0))
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


        private bool IsCapture(int x, int y, int color)
        {
            if (x > 0 && GetState(x - 1, y) == -color && !HasLiberties(x - 1, y))
            {
                InputState(x - 1, y, 0);
                return true;
            }
            if (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == -color && !HasLiberties(x + 1, y))
            {
                InputState(x + 1, y, 0);
                return true;
            }
            if (y > 0 && GetState(x, y - 1) == -color && !HasLiberties(x, y - 1))
            {
                InputState(x, y - 1, 0);
                return true;
            }
            if (y < Board.GetLength(1) - 1 && GetState(x, y + 1) == -color && !HasLiberties(x, y + 1))
            {
                InputState(x, y + 1, 0);
                return true;
            }

            return false;
        }

        private void Capture(int x, int y, int color)
        {
            {
                if (IsCapture(x, y, color))
                {
                    if (x > 0 && GetState(x - 1, y) == 0) HasLiberties(x - 1, y);
                    if (x < Board.GetLength(0) - 1 && GetState(x + 1, y) == 0) HasLiberties(x + 1, y);
                    if (y > 0 && GetState(x, y - 1) == 0) HasLiberties(x, y - 1);
                    if (y < Board.GetLength(1) - 1 && GetState(x, y + 1) == 0) HasLiberties(x, y + 1);
                }
            }
        }
        public int GetState(int x, int y)
        {
            return board[x, y].Color;
        }
        public void InputState(int x, int y, int color)
        {
            board[x, y].Color = color;
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
