using KMA.Lab04.Yakovenko.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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
        public Serializer()
        {
            if (!Directory.Exists(_dateBase))
            {
                Directory.CreateDirectory(_dateBase);
            }
        }
    }
}
