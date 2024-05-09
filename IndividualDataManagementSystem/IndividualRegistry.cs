using System;
using System.Linq;

public class IndividualRegistry : IDisposable
{
    private readonly IndividualDao _individualDao = new IndividualDao();

    public Individual GetById(int id)
    {
        return _individualDao.GetById(id);
    }

    public bool Any()
    {
        return _individualDao.Any();
    }

    public void Add(Individual individual)
    {
        _individualDao.Add(individual);
    }

    public void Update(Individual individual)
    {
        _individualDao.Update(individual);
    }

    public void RemoveById(int id)
    {
        _individualDao.RemoveById(id);
    }

    public void PrintTable(bool includeSummary = false)
    {
        var individuals = _individualDao.GetAll();

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

    public void Dispose()
    {
        _individualDao.Dispose();
    }
}