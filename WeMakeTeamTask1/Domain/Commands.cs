using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeMakeTeamTask1.Domain
{
    // Список доступных комманд
    internal enum Commands { add, get, exit }

    internal static class CommandsExtention
    {
        internal static string Description(this Commands cmd)
        {
            // использовал extention, но можно было использовать [атрибут]
            switch (cmd)
            {
                case Commands.add:
                    return GenerateDescription(cmd, "Ввод данных");
                case Commands.get:
                    return GenerateDescription(cmd, "Получение данных");
                case Commands.exit:
                    return GenerateDescription(cmd, "Выход из приложения");
                default:
                    return GenerateDescription(cmd, "Неизвестная комманда");
            }
        }

        private static string GenerateDescription(Commands cmd, string descripton)
        {
            return $"{cmd} - {descripton}";
        }
    }
}