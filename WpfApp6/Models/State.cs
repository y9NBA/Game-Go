using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go.ViewModels.Base;

namespace Go.Models
{
    public class Field : ViewModel
    {
        private string state = "";
        public string State
        {
            get => state.ToString();
            set => Set(ref state, value);
        }
        public int I { get; set; }
        public int J { get; set; }

    }
}
