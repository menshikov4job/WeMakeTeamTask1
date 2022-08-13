using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeMakeTeamTask1.Domain;

namespace WeMakeTeamTask1.Utils
{
    internal static class InputFields
    {
        internal static Transaction InputTransaction()
        {
            Transaction transaction = new();
            transaction.Id = Id();
            transaction.TransactionDate = Date();
            transaction.Amount = Amount();
            return transaction;
        }

        internal static int Id()
        {
            while (true)
            {
                Console.Write("Введите ID:");
                string? idInput = Console.ReadLine();
                if (int.TryParse(idInput, out int id))
                {
                    return id;
                }
                else
                {
                    Console.WriteLine("Введенное значение не является целым числом");
                }
            }
        }

        internal static DateTime Date()
        {
            while (true)
            {
                string format = "dd.MM.yyyy";
                Console.Write($"Введите дату в формате {format}:");
                string? dateInput = Console.ReadLine();
                if (dateInput == null) dateInput = "";
                if (DateTime.TryParseExact(dateInput, format, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
                else
                {
                    Console.WriteLine("Введенное значение не является датой");
                }
            }
        }

        internal static decimal Amount()
        {
            while (true)
            {
                Console.Write("Введите сумму:");
                string? amountInput = Console.ReadLine();
                if (decimal.TryParse(amountInput, NumberStyles.AllowDecimalPoint |
                    NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal amount)
                    && Math.Round(amount, 2) == amount)
                {
                    // нужна ли проверка на отрицательное или нулевое значение суммы?
                    return amount;

                }
                else
                {
                    Console.WriteLine("Введенное значение не является суммой");
                }
            }
        }
    }
}