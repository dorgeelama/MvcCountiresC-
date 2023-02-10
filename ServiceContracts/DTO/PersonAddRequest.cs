using System;
using System.Collections.Generic;
using ServiceContracts.Enums;
using Entities;


namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding new person
    /// </summary>
    public class PersonAddRequest
    {
        public string? PersonName { get; set; }

        public string? Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderOptions? Gender { get; set; }

        public Guid? CountryID { get; set; }

        public string? Address { get; set; }

        public bool ReceiveNewsLetter { get; set; }

        /// <summary>
        /// Converts the current object of PersonAddRequest to Person object
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetter,
            };
        }
    }
}
