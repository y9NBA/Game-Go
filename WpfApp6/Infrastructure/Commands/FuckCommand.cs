using System;
using System.Collections.Generic;
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
            //object[] parameters = parameter as object[];
            //Field field = parameters[0] as Field;
            //return field.State == "";
            return true;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            Field field = parameters[0]  as Field;
            MainWindowViewModel mwvm = parameters[1] as MainWindowViewModel;
            if (mwvm.Game.Step % 2 == 0 && field.State == "")
                field.State = mwvm.Game.Move = "B";
            else if (Convert.ToDecimal(mwvm.Game.Step) % 2 != 0 && field.State == "")
                field.State = mwvm.Game.Move = "W";
            mwvm.Game.Step++;
            
        }
    }
}
