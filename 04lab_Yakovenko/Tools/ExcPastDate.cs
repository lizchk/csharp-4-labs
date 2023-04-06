using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.Lab04.Yakovenko.Tools
{
    internal class ExcPastDate : Exception
    {
        public ExcPastDate() : base("You entered an incorrect past date! You cannot be over 135 years old!")
        {
        }


    }
}
