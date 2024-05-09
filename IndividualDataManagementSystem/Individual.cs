using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

[Table("individuals")]
public class Individual
{
    private const int NameMinLength = 2;
    private const int NameMaxLength = 16;
    private const int AddressMinLength = 2;
    private const int AddressMaxLength = 40;

    [Key]
    [Required]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MinLength(NameMinLength)]
    [MaxLength(NameMaxLength)]
    [Column("last_name")]
    public string LastName { get; set; }

    [Required]
    [MinLength(NameMinLength)]
    [MaxLength(NameMaxLength)]
    [Column("first_name")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(NameMinLength)]
    [MaxLength(NameMaxLength)]
    [Column("middle_name")]
    public string MiddleName { get; set; }

    [Required]
    [StringLength(10)]
    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [Column("gender")]
    public Gender Gender { get; set; }

    [Required]
    [MinLength(AddressMinLength)]
    [MaxLength(AddressMaxLength)]
    [Column("address")]
    public string Address { get; set; }

    [StringLength(13)]
    [Column("phone_number")]
    public string PhoneNumber { get; set; }

    [StringLength(9)]
    [Column("passport_number")]
    public string PassportNumber { get; set; }

    [StringLength(10)]
    [Column("tax_number")]
    public string TaxNumber { get; set; }

    public static int ValidateId(string input)
    {
        if (!int.TryParse(input, out var id) || id < 1)
        {
            throw new ArgumentException("Некоректний ID.");
        }

        return id;
    }

    public static string ValidateLastName(string input)
    {
        if (input.Length < NameMinLength || input.Length > NameMaxLength)
        {
            throw new ArgumentException($"Прізвище має бути від {NameMinLength} до {NameMaxLength} символів.");
        }

        return input;
    }

    public static string ValidateFirstName(string input)
    {
        if (input.Length < NameMinLength || input.Length > NameMaxLength)
        {
            throw new ArgumentException($"Імʼя має бути від {NameMinLength} до {NameMaxLength} символів.");
        }

        return input;
    }

    public static string ValidateMiddleName(string input)
    {
        if (input.Length < NameMinLength || input.Length > NameMaxLength)
        {
            throw new ArgumentException($"По батькові має бути від {NameMinLength} до {NameMaxLength} символів.");
        }

        return input;
    }

    public static DateOnly ValidateDateOfBirth(string input)
    {
        var pattern = @"^\d{2}\.\d{2}\.\d{4}$";

        if (!Regex.IsMatch(input, pattern))
        {
            throw new ArgumentException("Некоректний формат дати.");
        }

        var tokens = input.Split('.');
        var (date, month, year) = (tokens[0], tokens[1], tokens[2]);

        if (!DateOnly.TryParse($"{year}-{month}-{date}", out var dateOfBirth) || dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Некоректна дата.");
        }

        return dateOfBirth;
    }

    public static string FormatDateOfBirth(DateOnly dateOfBirth)
    {
        return dateOfBirth.ToString("dd.MM.yyyy");
    }

    public static Gender ValidateGender(string input)
    {
        var pattern = @"^(ч|ж)$";

        if (!Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
        {
            throw new ArgumentException("Некоректна стать.");
        }

        return LetterToGender(input);
    }

    public static Gender LetterToGender(string letter)
    {
        return letter.ToLower() switch
        {
            "ч" => Gender.Male,
            "ж" => Gender.Female,
            _ => Gender.Unknown,
        };
    }

    public static string GenderToLetter(Gender gender)
    {
        return gender switch
        {
            Gender.Male => "ч",
            Gender.Female => "ж",
            _ => "-"
        };
    }

    public static string ValidateAddress(string input)
    {
        if (input.Length < AddressMinLength || input.Length > AddressMaxLength)
        {
            throw new ArgumentException($"Адреса має бути від {AddressMinLength} до {AddressMaxLength} символів.");
        }

        return input;
    }

    public static string ValidatePhoneNumber(string input)
    {
        var pattern = @"^\+380\d{9}$";

        if (!Regex.IsMatch(input, pattern))
        {
            throw new ArgumentException("Некоректний формат номера телефону.");
        }

        return input;
    }

    public static string ValidatePassportNumber(string input)
    {
        var pattern = @"^[A-Z]{2}\s\d{6}$";

        if (!Regex.IsMatch(input, pattern))
        {
            throw new ArgumentException("Некоректний формат серії та номера паспорта.");
        }

        return input;
    }

    public static string ValidateTaxNumber(string input)
    {
        var pattern = @"^\d{10}$";

        if (!Regex.IsMatch(input, pattern))
        {
            throw new ArgumentException("Некоректний формат ІПН.");
        }

        return input;
    }
}