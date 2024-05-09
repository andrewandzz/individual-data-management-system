using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class IndividualDao : IDisposable
{
    private readonly IndividualDataManagementContext _context = new IndividualDataManagementContext();

    public bool Exists(int id)
    {
        return _context.Individuals.Any(individual => individual.Id == id);
    }

    public bool Any()
    {
        return _context.Individuals.Any();
    }

    public IEnumerable<Individual> GetAll()
    {
        return _context.Individuals.AsEnumerable();
    }

    public Individual GetById(int id)
    {
        return _context.Individuals.Find(id);
    }

    public void Add(Individual individual)
    {
        try
        {
            _context.Individuals.Add(individual);
            _context.SaveChanges();
        }
        catch (DbUpdateException exception)
        {
            if (((SqliteException)exception.InnerException).SqliteErrorCode == 19) // UNIQUE constraint
            {
                throw new ArgumentException("Фізична особа з таким номером телефона / номером паспорта / ІПН вже існує.");
            }
        }
    }

    public void Update(Individual individual)
    {
        try
        {
            _context.Update(individual);
            _context.SaveChanges();
        }
        catch (DbUpdateException exception)
        {
            if (((SqliteException)exception.InnerException).SqliteErrorCode == 19) // UNIQUE constraint
            {
                throw new ArgumentException("Фізична особа з таким номером телефона / номером паспорта / ІПН вже існує.");
            }
        }
    }

    public void RemoveById(int id)
    {
        var individual = _context.Individuals.Find(id);
        _context.Remove(individual);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}