﻿using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        PrintInto();
        MailMenu();
    }

    static void MailMenu()
    {
        while (true)
        {
            Console.WriteLine("Введіть номер опції:");
            Console.WriteLine("1. Роздрукувати дані про всіх фізичних осіб");
            Console.WriteLine("2. Додати дані про фізичну особу");
            Console.WriteLine("3. Редагувати дані про фізичну особу");
            Console.WriteLine("4. Видалити дані про фізичну особу");
            Console.WriteLine("0. Вийти");
            Console.WriteLine();

            var input = Console.ReadLine();

            if (int.TryParse(input, out var option))
            {
                switch (option)
                {
                    case 0:
                        PrintOutro();
                        return;
                    case 1:
                        PrintIndividualsData();
                        continue;
                    case 2:
                        AddIndividualData();
                        continue;
                    case 3:
                        EditIndividualData();
                        continue;
                    case 4:
                        DeleteIndividualData();
                        continue;
                }
            }

            PrintRedMessage("Некоректно вибрана опція. Повторіть введення.");
        }
    }

    static void PrintInto()
    {
        Console.WriteLine("Дана програма призначена для роботи з даними про фізичних осіб.\n");
    }

    static void PrintOutro()
    {
        Console.WriteLine("Дякуємо за користування програмою.");
    }

    static void PrintIndividualsData()
    {
        using var individualDao = new IndividualDao();
        var individuals = individualDao.GetAll();

        PrintIndividualsData(individuals, includeSummary: true);
    }

    static void PrintIndividualsData(IEnumerable<Individual> individuals, bool includeSummary = false)
    {
        Console.WriteLine();

        Console.WriteLine("                                                                               ФІЗИЧНІ ОСОБИ                                                                               ");

        Console.WriteLine();

        var horizontalLine = "|----|------------------|------------------|------------------|-----------------|-------|------------------------------------------|----------------|----------------|------------|";

        Console.WriteLine(horizontalLine);

        Console.WriteLine("| ID |     Прізвище     |       Імʼя       |   По батькові    | Дата народження | Стать |            Адреса реєстрації             | Номер телефону | Номер паспорта |    ІПН     |");

        Console.WriteLine(horizontalLine);

        foreach (var individual in individuals)
        {
            var dateOfBirth = Individual.FormatDateOfBirth(individual.DateOfBirth);
            var gender = Individual.GenderToLetter(individual.Gender);
            var phoneNumber = !string.IsNullOrEmpty(individual.PhoneNumber) ? individual.PhoneNumber : "-";
            var passportNumber = !string.IsNullOrEmpty(individual.PassportNumber) ? individual.PassportNumber : "-";
            var taxNumber = !string.IsNullOrEmpty(individual.TaxNumber) ? individual.TaxNumber : "-";

            Console.WriteLine("| {0,2} | {1,-16} | {2,-16} | {3,-16} | {4,15} |   {5,-2}  | {6,-40} | {7,14} | {8,-14} | {9,10} |",
                individual.Id,
                individual.LastName,
                individual.FirstName,
                individual.MiddleName,
                dateOfBirth,
                gender,
                individual.Address,
                phoneNumber,
                passportNumber,
                taxNumber);

            Console.WriteLine(horizontalLine);
        }

        if (includeSummary)
        {
            Console.WriteLine("|                                                                                                                                  | {0,31} | {1,10} |",
                "ВСЬОГО ФІЗИЧНИХ ОСІБ:",
                individuals.Count());

            Console.WriteLine(horizontalLine);
        }

        Console.WriteLine();
    }

    static void AddIndividualData()
    {
        var lastName = InputReader.ReadLastName("Введіть прізвище:");
        var firstName = InputReader.ReadFirstName("Введіть імʼя:");
        var middleName = InputReader.ReadMiddleName("Введіть по батькові:");
        var dateOfBirth = InputReader.ReadDateOfBirth("Введіть дату народження у форматі ДД.ММ.РРРР:");
        var gender = InputReader.ReadGender("Введіть стать (\"ч\" або \"ж\"):");
        var address = InputReader.ReadAddress("Введіть адресу реєстрації:");
        var phoneNumber = InputReader.ReadPhoneNumber("Введіть номер телефону у форматі +380xxxxxxxxx (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var passportNumber = InputReader.ReadPassportNumber("Введіть серію та номер паспорта у форматі XX XXXXXX (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var taxNumber = InputReader.ReadTaxNumber("Введіть ІПН у форматі XXXXXXXXXX (або натисніть Enter, щоб пропустити):", allowEmpty: true);

        var individual = new Individual()
        {
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            DateOfBirth = (DateOnly)dateOfBirth,
            Gender = (Gender)gender,
            Address = address,
            PhoneNumber = phoneNumber,
            PassportNumber = passportNumber,
            TaxNumber = taxNumber,
        };

        using var individualDao = new IndividualDao();

        try
        {
            individualDao.Add(individual);

            Console.WriteLine("Фізичну особу успішно додано.\n");
        }
        catch (ArgumentException exception)
        {
            PrintRedMessage($"{exception.Message} Повторіть введення.");
        }
    }

    static void EditIndividualData()
    {
        using var individualDao = new IndividualDao();

        var individuals = individualDao.GetAll();

        if (!individuals.Any())
        {
            Console.WriteLine("Немає фізичнних осіб.\n");
            return;
        }

        PrintIndividualsData(individuals);

        var id = InputReader.ReadId("Введіть ID фізичної особи для редагування (або натисніть Enter, щоб повернутися назад):", allowEmpty: true);

        if (id == null)
        {
            return;
        }

        var individual = individualDao.GetById((int)id);

        var newLastName = InputReader.ReadLastName("Введіть нове прізвище (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var newFirstName = InputReader.ReadFirstName("Введіть нове імʼя (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var newMiddleName = InputReader.ReadMiddleName("Введіть нове по батькові (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var newDateOfBirth = InputReader.ReadDateOfBirth("Введіть нову дату народження у форматі ДД.ММ.РРРР (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var newGender = InputReader.ReadGender("Введіть нову стать (\"ч\" або \"ж\") (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var newAddress = InputReader.ReadAddress("Введіть нову адресу реєстрації (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var newPhoneNumber = InputReader.ReadPhoneNumber("Введіть новий номер телефону у форматі +380xxxxxxxxx (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var newPassportNumber = InputReader.ReadPassportNumber("Введіть нові серію та номер паспорта у форматі XX XXXXXX (або натисніть Enter, щоб пропустити):", allowEmpty: true);
        var newTaxNumber = InputReader.ReadTaxNumber("Введіть новий ІПН у форматі XXXXXXXXXX (або натисніть Enter, щоб пропустити):", allowEmpty: true);

        if (newLastName != null) individual.LastName = newLastName;
        if (newFirstName != null) individual.FirstName = newFirstName;
        if (newMiddleName != null) individual.MiddleName = newMiddleName;
        if (newDateOfBirth != null) individual.DateOfBirth = (DateOnly)newDateOfBirth;
        if (newGender != null) individual.Gender = (Gender)newGender;
        if (newAddress != null) individual.Address = newAddress;
        if (newPhoneNumber != null) individual.PhoneNumber = newPhoneNumber;
        if (newPassportNumber != null) individual.PassportNumber = newPassportNumber;
        if (newTaxNumber != null) individual.TaxNumber = newTaxNumber;

        try
        {
            individualDao.Update(individual);

            Console.WriteLine("Дані про фізичну особу успішно оновлено.\n");
        }
        catch (ArgumentException exception)
        {
            PrintRedMessage($"{exception.Message} Повторіть введення.");
        }
    }

    static void DeleteIndividualData()
    {
        using var individualDao = new IndividualDao();

        var individuals = individualDao.GetAll();

        if (!individuals.Any())
        {
            Console.WriteLine("Немає фізичнних осіб.\n");
            return;
        }

        PrintIndividualsData(individuals);

        var id = InputReader.ReadId("Введіть ID фізичної особи для видалення (або натисніть Enter, щоб повернутися назад):", allowEmpty: true);

        if (id == null)
        {
            return;
        }

        if (InputReader.ReadConfirmation("Дані про фізичну особу будуть втрачені. Ви впевнені? Введіть \"т\", щоб підтвердити, або \"н\", щоб скасувати:"))
        {
            individualDao.RemoveById((int)id);
            Console.WriteLine("Фізичну особу успішно видалено.\n");
        }
    }

    static void PrintRedMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}