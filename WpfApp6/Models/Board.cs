using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Go.Infrastructure.Commands;
using Go.Infrastructure.Commands.Base;
using Go.ViewModels.Base;

namespace Go.Models
{
    public class Board: ViewModel
    {
        private int[,] field;
        public int[,] Field
        {
            get => field; 
            set => Set(ref field, value);
        }
        //public void MakeMove(Player player, int x, int y)
        //{
        //    if(!IsValidMove(x,y,player.Color))
        //    {
        //        throw new ArgumentException("Invalid move");
        //    }
        //    InputState(x, y, player.Color);
        //    if (IsCapture(x, y, player.Color))
        //    {
        //        Capture(x, y, player.Color);
        //    }
        //}
        //private bool IsValidMove(int x, int y, int color)
        //{
        //    if (field[x, y] != 0) return false;
        //    bool captured = false;
        //    foreach(Point n in GetNeighbors(x, y))
        //    {
        //        if (field[n.X,n.Y] == OppositeColor(color) && IsCapture(n.X,n.Y, color))
        //        {
        //            captured = true; break;
        //        }
        //    }
        //    if (!captured)
        //    {
        //        field[x, y]  = color;
        //        bool connected = false;
        //        foreach(Point n in GetNeighbors(x, y))
        //        {
        //            if (GetState(x, y) == color && IsConnected(n.X, n.Y))
        //            {
        //                connected = true; break;
        //            }
        //        }
        //        if (!connected)
        //        {
        //            InputState(x, y, 0);
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //private bool IsConnected(int x, int y)
        //{
        //    if (GetState(x, y) == 0) return false;
        //    else
        //    {
        //        int groupNumber = GetState(x, y);
        //        foreach(Point n in GetNeighbors(x, y))
        //        {
        //            if (GetState(n.X, n.Y) == groupNumber) return true;
        //        }
        //    }
        //    return false;
        //}

        //private int OppositeColor(int color)
        //{
        //    if (color == 1) return 2;
        //    else if (color == 2) return 1;
        //    else return 0;
        //}

        //private List<Point> GetNeighbors(int x, int y)
        //{
        //    List<Point> neihbors = new List<Point>();
        //    if (x > 0 && GetState(x - 1, y) == 0)
        //        neihbors.Add(new Point(x-1, y));
        //    if (x < field.GetLength(0)-1 && GetState(x + 1, y) == 0)
        //        neihbors.Add(new Point(x + 1, y));
        //    if (y > 0 && GetState(x, y - 1)  == 0)
        //        neihbors.Add(new Point(x, y - 1));
        //    if (y < field.GetLength(1)-1 && GetState(x, y) == 0)
        //        neihbors.Add(new Point(x, y + 1));
        //    return neihbors;
        //}
        //public bool HasLiberties(int x, int y)
        //{
        //    int color = GetState(x, y);

        //    if ((x > 0 && GetState(x - 1, y) == 0) ||
        //        (x < field.GetLength(0) - 1 && GetState(x + 1, y) == 0) ||
        //        (y > 0 && GetState(x, y - 1) == 0) ||
        //        (y < field.GetLength(1) - 1 && GetState(x, y + 1) == 0))
        //    {
        //        return true;
        //    }

        //    if ((x > 0 && GetState(x - 1, y) == color && HasLiberties(x - 1, y)) ||
        //        (x < field.GetLength(0) - 1 && GetState(x + 1, y) == color && HasLiberties(x + 1, y)) ||
        //        (y > 0 && GetState(x, y - 1) == color && HasLiberties(x, y - 1)) ||
        //        (y < field.GetLength(1) - 1 && GetState(x, y + 1) == color && HasLiberties(x, y + 1)))
        //    {
        //        return true;
        //    }

        //    return false;
        //}


        //private bool IsCapture(int x, int y, int color)
        //{
        //    if (x > 0 && GetState(x - 1, y) == -color && !HasLiberties(x - 1, y))
        //    {
        //        InputState(x - 1, y, 0);
        //        return true;
        //    }
        //    if (x < field.GetLength(0) - 1 && GetState(x + 1, y) == -color && !HasLiberties(x + 1, y))
        //    {
        //        InputState(x + 1, y, 0);
        //        return true;
        //    }
        //    if (y > 0 && GetState(x, y - 1) == -color && !HasLiberties(x, y - 1))
        //    {
        //        InputState(x, y - 1, 0);
        //        return true;
        //    }
        //    if (y < field.GetLength(1) - 1 && GetState(x, y + 1) == -color && !HasLiberties(x, y + 1))
        //    {
        //        InputState(x, y + 1, 0);
        //        return true;
        //    }

        //    return false;
        //}

        //private void Capture(int x, int y, int color)
        //{
        //    {
        //        if (IsCapture(x, y, color))
        //        {
        //            if (x > 0 && GetState(x - 1, y) == 0) HasLiberties(x - 1, y);       
        //            if (x < field.GetLength(0) - 1 && GetState(x + 1, y) == 0) HasLiberties(x + 1, y);       
        //            if (y > 0 && GetState(x, y - 1) == 0) HasLiberties(x, y - 1);       
        //            if (y < field.GetLength(1) - 1 && GetState(x, y + 1) == 0) HasLiberties(x, y + 1);   
        //        }
        //    }
        //}   
        //public int GetState(int x, int y)
        //{
        //    return field[x, y];
        //}
        //public void InputState(int x, int y, int color)
        //{
        //    field[x, y] = color;
        //}
    }
}
