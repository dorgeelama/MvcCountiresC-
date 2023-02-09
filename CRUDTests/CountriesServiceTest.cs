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

        //constructor
        #region AddCountry
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
            Assert.Throws<ArgumentNullException>(() =>
            {
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
                CountryName = null
            };
            //Assert  while executing any of the statments in the lambda expression
            // if it throws any Argument Exception this test case will be passed
            Assert.Throws<ArgumentException>(() =>
            {
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
            Assert.Throws<ArgumentException>(() =>
            {
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
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries();

            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);


        }
        #endregion

        #region GetAllCountries
        [Fact]
        // The list of countries should be empty by default (before adding any countries)
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> acutal_country_response_list = _countriesService.GetAllCountries();

            Assert.Empty(acutal_country_response_list);
        }

        [Fact]
        // The list should contain a few countries
        public void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName = "USA"},
                 new CountryAddRequest() { CountryName = "Thailand"},
                  new CountryAddRequest() { CountryName = "Japah"},
            };
            //Act
            List<CountryResponse> countries_response_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_response_list_from_add_country.Add(_countriesService.AddCountry(country_request)); 
            }

           List<CountryResponse> actual_countries_list =  _countriesService.GetAllCountries();

            //read each element from countires_list_from_add_country
            foreach(CountryResponse country_response in actual_countries_list)
            {
                Assert.Contains(country_response, actual_countries_list);
            }
        }
        #endregion

        #region GetCountryByCountryID
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            // Arrange
            Guid? countryID = null;
            //Act
            CountryResponse? country_response_from_get_method = _countriesService.GetCountryByCountryID(countryID);
            //Assert
            Assert.Null(country_response_from_get_method);    
        }

        [Fact]
        public void GetCountryByCountryID_ValidCountryID()
        {
            CountryAddRequest test_country = new CountryAddRequest()
            {
                CountryName = "Brazil"
            };
            CountryResponse test_country_response = _countriesService.AddCountry(test_country);
            CountryResponse? actual_country_response = _countriesService.GetCountryByCountryID(test_country_response.CountryID);

            Assert.Equal(test_country_response, actual_country_response);
        }


        #endregion

    }
}

