using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private field
        private readonly List<Country> _countries;

        //Constructor
        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>() {
                new Country()
                {
                    CountryID = Guid.Parse("FF5843FB-BD88-4D4E-8CC9-525F38DC12AD"),
                    CountryName = "USA"
                },
                new Country()
                {
                    CountryID = Guid.Parse("A1677045-BFE0-42DA-98E1-1864BB5CD1E2"),
                    CountryName = "Canada"
                },
                new Country()
                {
                    CountryID = Guid.Parse("C46DAB65-A64A-4DFD-80C9-AFD8450B05F7"),
                    CountryName = "UK"
                },
                new Country()
                {
                    CountryID = Guid.Parse("B6F8C4C7-F07B-4A4D-8B7F-9271F0D98AB5"),
                    CountryName = "India"
                },

                new Country()
                {
                    CountryID = Guid.Parse("7410CC33-B336-434D-828A-BB8679357E73"),
                    CountryName = "Australia"
                }
                });
            }
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

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if(countryID == null)
            {
                return null;
            }
            
           Country? matched_country = _countries.FirstOrDefault(country => country.CountryID == countryID);
            if(matched_country == null)
            {
                return null;
            }
            return matched_country.ToCountryResponse();
        }
    }
}