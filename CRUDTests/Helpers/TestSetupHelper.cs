using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests.Helpers
{
    public class TestSetupHelper
    {
        private readonly ICountriesService _countriesService;

        public TestSetupHelper()
        {
            _countriesService = new CountriesService();
        }
        internal static void countries_persons_setup()
        {
           

        }

    }
}
