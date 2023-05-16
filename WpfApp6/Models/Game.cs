using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp6.ViewModels.Base;

namespace WpfApp6.Models
{
    class Game : ViewModel
    {
        private string move = "B";
        public string Move
        {
            get => move;
            set => Set(ref move, value);
        }
    }
}
