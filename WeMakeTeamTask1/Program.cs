using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WeMakeTeamTask1;
using WeMakeTeamTask1.Domain;
using WeMakeTeamTask1.Utils;

while (true)
{
    var inputtedCommand = InputCommand();
    // проверяем наличие введенной комманды (TryParse цифры преобразовывал в комманды!)
    if (Enum.IsDefined(typeof(Commands), inputtedCommand))
    {
        Commands command = Enum.Parse<Commands>(inputtedCommand);
        if (command == Commands.exit) break;
        try
        {
            ExecuteCommand(command);
            Console.WriteLine("[OK]");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"При выполнении комманды {command.Description()} произошла ошибка:\n{ex.Message}");
        }
    }
    else
    {
        Console.WriteLine($"Указана неизвестная комманда - {inputtedCommand} !\nДопустимые комманды:");
        foreach (var cmd in Enum.GetValues<Commands>())
        {
            Console.WriteLine(cmd.Description());
        }
    }
    Console.WriteLine();
}

string InputCommand()
{
    Console.Write("Введите комманду:");
    string inputtedCommand = Console.ReadLine() ?? "";
    return inputtedCommand.ToLower();
}

void ExecuteCommand(Commands cmd)
{
    switch (cmd)
    {
        case Commands.add:
            AddTransaction();
            break;
        case Commands.get:
            GetTransaction();
            break;
        default:
            throw new NotSupportedException($"Комманда {cmd} не поддерживается!");
    }
}

void AddTransaction()
{
    var transaction = InputData.InputTransaction();
    // Новая конструкция using без скобок.
    using var context = new AppDbContext();
    var transactionRepository = new TransactionRepository(context);
    try
    {        
        transactionRepository.Insert(transaction);
    }
    catch (DbUpdateException ex)
    {
        if (ex.InnerException is SqliteException sqlException && sqlException.SqliteErrorCode == 19
            && sqlException.SqliteExtendedErrorCode == 1555)
        {
            // Можно было создать свой тип exception.
            throw new ApplicationException($"Транзакция с Id = {transaction.Id} уже существует!", ex);
        }
        else
            throw;
    }
}

void GetTransaction()
{
    var id = InputData.Id();
    // Новая конструкция using без скобок.
    using var context = new AppDbContext();
    var transactionRepository = new TransactionRepository(context);
    var transaction = transactionRepository.Get(id);
    var json = Json.CreateJson(transaction);
    Console.WriteLine(json);
}
