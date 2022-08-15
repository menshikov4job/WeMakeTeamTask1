using System;
using System.Buffers.Text;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeMakeTeamTask1.Utils
{
    internal static class Json
    {
        internal static string CreateJson<T>(T? obj) where T : class
        {
            if (obj == null) return "{}";
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                
            };
            options.Converters.Add(new DateTimeConverter4Json('O'));
            return JsonSerializer.Serialize<T>(obj, options);
        }
    }

    public class DateTimeConverter4Json : JsonConverter<DateTime>
    {
        readonly char _standartFormat;
        public DateTimeConverter4Json(char standartFormat)
        {
            _standartFormat = standartFormat;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {           
            if (Utf8Parser.TryParse(reader.ValueSpan, out DateTime value, out _, _standartFormat))
            {
                return value;
            }

            throw new FormatException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Для каждого формата свое кол-во байт 33 для O,
            // нужна доработка!
            if(_standartFormat != 'O') throw new FormatException();
            Span<byte> utf8Date = new byte[33];

            if (Utf8Formatter.TryFormat(value, utf8Date, out _, new StandardFormat(_standartFormat)))
            {
                writer.WriteStringValue(utf8Date);
            }
            else
                throw new FormatException();            
        }
    }
}
