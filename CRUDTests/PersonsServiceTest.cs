﻿using System;
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

        #region GetAllPerson

        //The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> persons_from_get = _personService.GetAllPersons();

            //Assert
            Assert.Empty(persons_from_get);

        }

            //First, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added
            [Fact]
            public void GetAllPersons_AddFewPersons()
            {
                //Arrange
                CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
                CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "CANADA" };
                CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
                CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

                PersonAddRequest person_request_1 = new PersonAddRequest()
                {
                    PersonName = "Smith",
                    Email = "smith@example.com",
                    Gender = GenderOptions.Male,
                    Address = "address of smith",
                    CountryID = country_response_1.CountryID,
                    DateOfBirth = DateTime.Parse("2002-05-06"),
                    ReceiveNewsLetter = true,
                };

                PersonAddRequest person_request_2 = new PersonAddRequest()
                {
                    PersonName = "Mary",
                    Email = "mary@example.com",
                    Gender = GenderOptions.Female,
                    Address = "address of mary",
                    CountryID = country_response_2.CountryID,
                    DateOfBirth = DateTime.Parse("1990-05-06"),
                    ReceiveNewsLetter = false,
                };


                PersonAddRequest person_request_3 = new PersonAddRequest()
                {
                    PersonName = "dcmbeg",
                    Email = "dcmbeg@example.com",
                    Gender = GenderOptions.Female,
                    Address = "address of dcmbeg",
                    CountryID = country_response_2.CountryID,
                    DateOfBirth = DateTime.Parse("2000-05-06"),
                    ReceiveNewsLetter = false,
                };

                List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
                {
                    person_request_1,
                    person_request_2,
                    person_request_3,
                };

                List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

                foreach (PersonAddRequest person_request in person_requests)
                {
                  PersonResponse person_response =  _personService.AddPerson(person_request);
                  person_response_list_from_add.Add(person_response);
                }

                //Act
               List<PersonResponse> persons_list_from_get = _personService.GetAllPersons();

             foreach (PersonResponse person_response_from_add in person_response_list_from_add)
                {
                    Assert.Contains(person_response_from_add, persons_list_from_get);
                }
        }

        #endregion
    }
}
