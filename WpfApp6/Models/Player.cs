using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go.ViewModels.Base;

namespace Go.Models
{
    public class Player : ViewModel
    {
        private int color;
        private int score = 0;

        public int Color
        {
            get => color;
            set => Set(ref color, value);
        }
        public int Score
        {
            get => score;
            set => Set(ref score, value);
        }
    }
}
