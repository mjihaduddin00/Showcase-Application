using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NM.Models
{
    public class Trivia
    {
        public int Id { get; set; }

        public string TriviaQuestion { get; set; }

        public string TriviaAnswer { get; set; }

        public Trivia()
        {

        }
    }
}
