using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go.Infrastructure.Commands.Base;

namespace Go.Infrastructure.Commands
{
    public class RellayCommand : Command
    {
        private readonly Action execute;

        public RellayCommand(Action execute)
        {
            this.execute = execute;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            execute();
        }
    }
}
