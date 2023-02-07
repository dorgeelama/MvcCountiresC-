using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity
    /// </summary>
    public interface ICountriesService
    {
        // takes in country add request and return a country response
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to be added</param>
        /// <returns>Returns the country object after adding it (Including newly generated country id)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

    }
}