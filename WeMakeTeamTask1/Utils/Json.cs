using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeMakeTeamTask1.Utils
{
    internal class Json
    {
        internal static string CreateJson<T>(T? obj) where T : class
        {
            if (obj == null) return "{}";
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                // true каждое поле на отдельной строке, false в одну строку
                WriteIndented = false
            };
            return JsonSerializer.Serialize<T>(obj, options);
        }
    }
}
