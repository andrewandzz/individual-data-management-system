using System;

public static class InputReader
{
    public static int ReadId(string message)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            try
            {
                var id = Individual.ValidateId(input);

                using var individualDao = new IndividualDao();

                if (!individualDao.Exists(id))
                {
                    throw new ArgumentException("Фізичної особи зі вказаним ID не існує.");
                }

                return id;
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static string ReadLastName(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidateLastName(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static string ReadFirstName(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidateFirstName(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static string ReadMiddleName(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidateMiddleName(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static DateOnly? ReadDateOfBirth(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidateDateOfBirth(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static Gender? ReadGender(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidateGender(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static string ReadAddress(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidateAddress(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static string ReadPhoneNumber(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidatePhoneNumber(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static string ReadPassportNumber(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidatePassportNumber(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    public static string ReadTaxNumber(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (allowEmpty && string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return Individual.ValidateTaxNumber(input);
            }
            catch (ArgumentException exception)
            {
                PrintErrorMessage($"{exception.Message} Спробуйте ще раз.");
            }
        }
    }

    private static void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}