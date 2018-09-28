using App.Core;
using System;

namespace BankAccounts.Domain.Models
{
    public class Iban : ValueObject
    {
        public string Value { get; private set; }

        private Iban()
        {

        }

        public Iban(string value)
        {
            Validate(value);
            Value = value;
        }
        
        public static implicit operator Iban(string value)
        {
            Validate(value);
            return new Iban(value);
        }

        public static implicit operator string(Iban iban)
        {
            return iban.Value;
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"IBAN value is not valid!");
            }
        }

    }
}
