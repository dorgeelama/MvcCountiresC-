using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        //private field
       private readonly List<Person> _persons;
       private readonly ICountriesService _countriesService;

       //constructor
       public PersonsService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //check if PersonAddRequest is not null
           if(personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            // Model Validations
            // stores reference to model object
            ValidationContext validationContext = new ValidationContext(personAddRequest); 
            List<ValidationResult> validationResults = new List<ValidationResult>();
            // validates entire model object
            bool isValid = Validator.TryValidateObject(personAddRequest, validationContext, validationResults, true);
            // if this is false that means there is an error could be one or more
            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }

            //Model validation
            ValidationHelper.ModelValidation(personAddRequest);


            // convert  personAddRequest into Person type
            Person person = personAddRequest.ToPerson();

            //generate PersonID
            person.PersonID = Guid.NewGuid();

            _persons.Add(person);

            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
