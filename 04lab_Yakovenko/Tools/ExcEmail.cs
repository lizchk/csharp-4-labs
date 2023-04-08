using System;

namespace KMA.Lab04.Yakovenko.Tools
{
    internal class ExcEmail : Exception
    {
        public ExcEmail() : base("You have entered an incorrect email!")
        {
        }
    }
}
