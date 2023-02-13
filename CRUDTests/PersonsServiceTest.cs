using System;
using System.Collections.Generic;
using Xunit;
using ServiceContracts;
using Entities;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        // private fields
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonsServiceTest()
        {
            _personService = new PersonsService();
            _countriesService = new CountriesService(); 
        }

        #region AddPerson

        [Fact]
        // When we supply null value as PersonAddRequest it should throw ArgumentNullException
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _personService.AddPerson(personAddRequest);
            });

        }

        [Fact]
        // When we supply null value as PersonName, it should throw ArgumentException
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personService.AddPerson(personAddRequest);
            });

        }


        [Fact]
        // When we supply proper person details, it 
        // should insert the person into the persons list;
        // and it should return an object of PersonResponse, which includes the newly generated person id
        public void AddPerson_ProperDetailsl()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Person name...",
                Email = "person@example.com",
                Address = "sample address",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetter = true,
            };

            //Act
            PersonResponse person_response_from_add = _personService.AddPerson(personAddRequest);
            List<PersonResponse> person_list = _personService.GetAllPersons();

            //Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);

            Assert.Contains(person_response_from_add, person_list);

        }


        #endregion


        #region GetPersonPersonID

        //If we supply null as PersonID, it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonID_NullID()
        {
            //Arrange
            Guid? personID = null;
            //Act
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(personID);
            //Assert
            Assert.Null(person_response_from_get);
        }

        //If we supply a valid person id, it should return the valid person details and PersonResponse object
        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            //Arange
            CountryAddRequest country_request = new CountryAddRequest() { CountryName = "Canada" };
            CountryResponse country_response = _countriesService.AddCountry(country_request);

            PersonAddRequest person_request = new PersonAddRequest()
            { 
                PersonName = "person name...", 
                Email = "email@sample.com", 
                Address = "address", 
                CountryID = country_response.CountryID, 
                DateOfBirth = DateTime.Parse("2000-01-01"), 
                Gender = GenderOptions.Male,
                ReceiveNewsLetter = false 
            };

            PersonResponse person_response_from_add = _personService.AddPerson(person_request);

            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(person_response_from_add.PersonID);

            //Assert
            Assert.Equal(person_response_from_add, person_response_from_get);

        }
        #endregion  
    }
}
