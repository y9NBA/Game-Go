using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WpfApp6.Infrastructure.Commands.Base;
using WpfApp6.Models;
using WpfApp6.ViewModels;

namespace WpfApp6.Infrastructure.Commands
{
    class FuckCommand : Command
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

            if (mwvm.Game.Step % 2 == 0 && field.State == "")
            {
                mwvm.Game.Color = field.State = mwvm.Game.Move = "Black";
                mwvm.Game.Step++;
            }
            else if (mwvm.Game.Step % 2 != 0 && field.State == "")
            {
                mwvm.Game.Color = field.State = mwvm.Game.Move = "White";
                mwvm.Game.Step++;
            }
        }
    }
}
