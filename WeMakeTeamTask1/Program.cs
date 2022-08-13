using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WeMakeTeamTask1;
using WeMakeTeamTask1.Domain;
using WeMakeTeamTask1.Utils;

while (true)
{
    String inputtedCommand = InputCommand();
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
    string? inputtedCommand = Console.ReadLine();
    if (inputtedCommand == null) inputtedCommand = "";
    return inputtedCommand.ToLower();
}

void ExecuteCommand(Commands cmd)
{
    switch (cmd)
    {
        case Commands.add:
            Add();
            break;
        case Commands.get:
            Get();
            break;
        default:
            throw new NotSupportedException($"Комманда {cmd} не поддерживается!");
    }
}

void Add()
{
    var transaction = InputFields.InputTransaction();
    using var context = new AppDbContext();
    var transactionRepository = new TransactionRepository(context);
    try
    {
        transactionRepository.InsertTransaction(transaction);
    }
    catch (DbUpdateException ex)
    {
        if (ex.InnerException is SqliteException sqlException && sqlException.SqliteErrorCode == 19
            && sqlException.SqliteExtendedErrorCode == 1555)
        {
            // можно было создать свой тип exception
            throw new ApplicationException($"Транзакция с Id = {transaction.Id} уже существует!");
        }
        else
            throw;
    }
}

void Get()
{
    int id = InputFields.Id();
    using var context = new AppDbContext();
    var transactionRepository = new TransactionRepository(context);
    var transaction = transactionRepository.GetTransactionById(id);
    string json = Json.CreateJson(transaction);
    Console.WriteLine(json);
}

