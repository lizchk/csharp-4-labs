using KMA.Lab04.Yakovenko.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace KMA.Lab04.Yakovenko.Tools
{
    internal class Serializer
    {

        private static string _dateBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab04Users");
        public async Task SerializePerson(Person obj)
        {
            String personObj = JsonSerializer.Serialize(obj);
            using (StreamWriter s = new StreamWriter(Path.Combine(_dateBase, obj.Surname), false))
            {
                await s.WriteAsync(personObj);
            }
        }
        public List<Person> ShowPersons()
        {
            List<Person> persons = new List<Person>();
            foreach (String file in Directory.EnumerateFiles(_dateBase))
            {
                string personObj = "";
                using (StreamReader r = new StreamReader(file))
                {
                    personObj = r.ReadToEnd();
                }
                persons.Add(JsonSerializer.Deserialize<Person>(personObj));
            }
            return persons;
        }
        public async Task<Person> ShowPerson(String Surname)
        {
            string file = Path.Combine(_dateBase, Surname);
            if (!File.Exists(file))
            {
                return null;
            }
            string personObj = "";
            using (StreamReader r = new StreamReader(file))
            {
                personObj = await r.ReadToEndAsync();
            }
            return JsonSerializer.Deserialize<Person>(personObj);
        }

        public Serializer()
        {
            if (!Directory.Exists(_dateBase))
            {
                Directory.CreateDirectory(_dateBase);
            }
        }
    }
}
