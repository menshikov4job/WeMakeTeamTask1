using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeMakeTeamTask1.Domain
{
    // Список доступных комманд
    public enum Commands { add, get, exit }

    public static class CommandsExtention
    {
        public static string Description(this Commands cmd)
        {
            // Использовал extention, но можно было использовать [атрибут].
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