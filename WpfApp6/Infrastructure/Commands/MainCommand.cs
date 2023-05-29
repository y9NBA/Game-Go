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
            Board board = parameters[0] as Board;
            MainWindowViewModel mwvm = parameters[1] as MainWindowViewModel;
            EventArgs e = parameters[2] as EventArgs;
            object sender = parameters[3] as object;
            Button button = sender as Button;
            State state = button.Tag as State;


            int x = state.I;
            int y = state.J;
            mwvm.MakeMove(x, y);

                //if (mwvm.Game.Step % 2 == 0 && mwvm.Field)
                //{
                //    mwvm.Game.Move = mwvm.Player1.Color;
                //    field.State = "0";
                //    mwvm.NextStep = mwvm.Player2.Color;
                //    mwvm.Game.Step++;
                //}
                //else if (mwvm.Game.Step % 2 != 0 && field.State == "")
                //{
                //    mwvm.Game.Move = mwvm.Player2.Color;
                //    field.State = "1";
                //    mwvm.NextStep = mwvm.Player1.Color;
                //    mwvm.Game.Step++;
                //}
            }
    }
}
