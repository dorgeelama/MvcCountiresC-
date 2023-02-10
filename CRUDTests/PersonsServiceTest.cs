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
        private readonly IPersonsService _personsSerivce;

        //constructor
        public PersonsServiceTest()
        {
            _personsSerivce = new PersonsService();
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
                _personsSerivce.AddPerson(personAddRequest);
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
                _personsSerivce.AddPerson(personAddRequest);
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
            PersonResponse person_response_from_add = _personsSerivce.AddPerson(personAddRequest);
            List<PersonResponse> person_list = _personsSerivce.GetAllPersons();

            //Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);

            Assert.Contains(person_response_from_add, person_list);

        }


        #endregion
    }
}
