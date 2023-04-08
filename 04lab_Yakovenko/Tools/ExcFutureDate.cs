using System;

namespace KMA.Lab04.Yakovenko.Tools
{
    internal class ExcFutureDate : Exception
    {
        public ExcFutureDate() : base("You entered an incorrect future date! Date of birth must be valid!")
        {
        }
    }
}
