using System;
using System.Collections.Generic;
using Entities;
namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding new country
    /// </summary>
    public class CountryAddRequest
    {
        // we do not include id here becuase when the user makes a request 
        // they will only send country name
        // only after adding country to the list should the id be generated
        public string? CountryName { get; set; }

        // after validating this country name if it is correct 
        // we have to convert the same into a country object inorder to add it to the acutal data source
        // have to convert from country add request to country object

        public Country ToCountry()
        {
            return new Country()
            {
                CountryName = CountryName
            };
        }
    }
}
