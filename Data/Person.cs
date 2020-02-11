using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDbContext.Data
{
    public class Person
    {
        public Person()
        {
        }

        public Person(string firstName, string surName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            SurName = surName;
            DateOfBirth = dateOfBirth;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
