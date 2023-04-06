using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.Lab04.Yakovenko.Tools
{
    internal class ExcEmail : Exception
    {
        public ExcEmail() : base("You have entered an incorrect email!")
        {
        }
    }
}
