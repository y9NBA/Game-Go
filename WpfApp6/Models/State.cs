using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go.ViewModels.Base;

namespace Go.Models
{
    public class State : ViewModel
    {
        private int currentState = 0;
        public int CurrentState
        {
            get => currentState;
            set => Set(ref currentState, value);
        }
        public int I { get; set; }
        public int J { get; set; }

    }
}
