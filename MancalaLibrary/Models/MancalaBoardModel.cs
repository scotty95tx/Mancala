using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaLibrary.Models
{
    public class MancalaBoardModel
    {
        public List<PitModel> Pits { get; set; } = new List<PitModel>();

        public int player1Store { get; set; }

        public int player2Store { get; set; }   
    }
}
