using ServiceContracts;
using System;
using System.Collections.Generic;
using Entities;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        [Fact]
        //When CountryAddRequest is null, it should throw ArgumentNullException
        public void AddCountry_NullCountry()
        {
            //arrange
            CountryAddRequest? request = null;

            //Assert  while executing any of the statments in the lambda expression
            // if it throws any Arguemnt Null Exception this test case will be passed
            Assert.Throws<ArgumentNullException>(() => {
                //Act
                _countriesService.AddCountry(request);
            });
                                              
        }


       


        [Fact]
        //When the CountryName is null,it should throw Argument Exception
        public void AddCountry_CountryNameIsNull()
        {
            //arrange
            CountryAddRequest? request = new CountryAddRequest() 
            { 
                CountryName  = null 
            };
            //Assert  while executing any of the statments in the lambda expression
            // if it throws any Argument Exception this test case will be passed
            Assert.Throws<ArgumentException>(() => {
                //Act
                _countriesService.AddCountry(request);
            });

        }

        [Fact]
        //When the CountryName is duplicate, it should throw Argument Exception
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange 
            CountryAddRequest request = new CountryAddRequest()
            {
                CountryName = "Japan"
            };

            CountryAddRequest request2 = new CountryAddRequest()
            {
                CountryName = "Japan"
            };

            //Assert
            Assert.Throws<ArgumentException>(() => {
                //Act
                _countriesService.AddCountry(request);
                _countriesService.AddCountry(request2);
            });
        }


       
        [Fact]
        // When you supply proper country nanme, it should insert(add) the country to the existing list of countries
        public void AddCountry_ProperCountryDetails()
        {
            //arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "Nepal"
            };
           
           
           //Act
           CountryResponse response = _countriesService.AddCountry(request);

           //Assert
           Assert.True(response.CountryID != Guid.Empty);
          

        }
    }
}
