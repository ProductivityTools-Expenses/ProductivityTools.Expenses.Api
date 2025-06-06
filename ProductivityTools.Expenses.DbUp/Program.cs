﻿using DbUp;
using System.Reflection;


var connectionString =
    args.FirstOrDefault()
    ?? "Server=.\\sql2022; Database=PTExpenses;Integrated Security=True;TrustServerCertificate=True";
EnsureDatabase.For.SqlDatabase(connectionString);
Console.WriteLine(connectionString);
Console.WriteLine("pawel");

var upgrader =
    DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .WithExecutionTimeout(TimeSpan.FromSeconds(3600))
        .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return 0;
