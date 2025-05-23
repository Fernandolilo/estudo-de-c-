﻿using System.ComponentModel;

static class Program
{
    static void Main()
    {
        //extendendo metodos 

        // modificado para um metodo de extends de string 
        "Test".WriteLine(ConsoleColor.Yellow);

    }

    //posso criar metodos static e reaproveitar da forma que for necessario, em diversas parte do codigo
    static void WriteLine(this string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}

class FileLogger : ILogger
{
    private readonly string filePath;

    public FileLogger(string filePath)
    {
        this.filePath = filePath;
    }
    public void Log(string message)
    {
        File.AppendAllText(filePath, $"{message}{Environment.NewLine}");
    }
}

class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"LOGGER: {message}");
    }
}

interface ILogger
{
    void Log(string message);
}

class BankAcount
{
    private string name;
    private readonly ILogger logger;
    private decimal balance;

    public decimal Balance
    {
        get { return balance; }

    }

    public string Name
    {
        get { return name; }

    }



    public BankAcount(string name, decimal balance, ILogger logger)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Nome invalido.", nameof(name));
        }
        if (balance <= 0)
        {
            throw new Exception("Saldo não pode ser negativo.");
        }
        this.name = name;
        this.logger = logger;
        this.balance = balance;
    }

    public void Deposito(decimal amount)
    {
        if (amount <= 0)
        {
            logger.Log($"Não é possível depositar {amount} na conta de {name}");
            return;
        }
        balance += amount;
    }



}