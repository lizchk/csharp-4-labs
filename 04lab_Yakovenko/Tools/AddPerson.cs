using KMA.Lab04.Yakovenko.Models;
using System.Threading.Tasks;

namespace KMA.Lab04.Yakovenko.Tools
{
    internal class AddPerson
    {
        private static Serializer _serializer = new Serializer();

        public async void Add(Person person)
        {
            await _serializer.SerializePerson(person);
        }
    }
}
