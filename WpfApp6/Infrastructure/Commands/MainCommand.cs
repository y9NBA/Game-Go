using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Go.Infrastructure.Commands.Base;
using Go.Models;
using Go.ViewModels;

namespace Go.Infrastructure.Commands
{
    class MainCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            Field field = parameters[0] as Field;
            MainWindowViewModel mwvm = parameters[1] as MainWindowViewModel;

            if (field.State == "" && mwvm.IsValidMove(field, mwvm.CurrentPlayer.Color) == false)
            {
                field.State = mwvm.CurrentPlayer.Color;
                mwvm.CurrentPlayer.Color = mwvm.CurrentPlayer.Color == "Black" ? "White" : "Black";
                mwvm.CheckBoard(field);
            }
            //mwvm.MakeMove(field);
            //int x = 
            //int y = 
            //mwvm.MakeMove(x, y);

            //if (mwvm.Game.Step % 2 == 0 && mwvm.Field)
            //{
            //    mwvm.Game.Move = mwvm.Player1.Color;
            //    field.Field = "0";
            //    mwvm.NextStep = mwvm.Player2.Color;
            //    mwvm.Game.Step++;
            //}
            //else if (mwvm.Game.Step % 2 != 0 && field.Field == "")
            //{
            //    mwvm.Game.Move = mwvm.Player2.Color;
            //    field.Field = "1";
            //    mwvm.NextStep = mwvm.Player1.Color;
            //    mwvm.Game.Step++;
            //}
        }
    }
}
