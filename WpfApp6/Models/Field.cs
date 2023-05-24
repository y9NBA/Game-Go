using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp6.Infrastructure.Commands;
using WpfApp6.Infrastructure.Commands.Base;
using WpfApp6.ViewModels.Base;

namespace WpfApp6.Models
{
    internal class Field: ViewModel
    {
        private string state = "";
        public string State
        {
            get => state;
            set => Set(ref state, value);
        }
        public int I { get; set; }
        public int J { get; set; }
    }
}
