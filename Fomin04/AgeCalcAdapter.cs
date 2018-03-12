using System;

namespace Fomin04

{
    internal static class AgeCalcAdapter
    {
        internal static Person CreateUser(string firstName, string lastName, string email, DateTime date)
        {
            return new Person(firstName, lastName, email, date);
        }
    }
}