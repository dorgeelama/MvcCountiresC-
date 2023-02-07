﻿using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private field
        private readonly List<Country> _countries;

        //Constructor
        public CountriesService()
        {
            _countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            //Validation: countryAddRequest parameter can't be null
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            //Validation: CountryAddRequest countryName can't be null
            if(countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            //Validation CountryName can't be duplicate
            if(_countries.Where(country => country.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given countryName already exists");
            }

            // Covert the country request to an country object
            Country country =  countryAddRequest.ToCountry();

            //generate CountryID
            country.CountryID = Guid.NewGuid();

            //Add country object to list of countries
            _countries.Add(country);
            return country.ToCountryResponse();
        }
    }
}