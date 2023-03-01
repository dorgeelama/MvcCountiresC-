﻿using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
namespace CRUDExample.Controllers
{
    public class PersonsController : Controller
    {
        //private fields
        private readonly IPersonsService _personService;

        public PersonsController(IPersonsService personsService)
        {
            _personService = personsService;
        }

        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
      {
        { nameof(PersonResponse.PersonName), "Person Name" },
        { nameof(PersonResponse.Email), "Email" },
        { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
        { nameof(PersonResponse.Gender), "Gender" },
        { nameof(PersonResponse.CountryID), "Country" },
        { nameof(PersonResponse.Address), "Address" }
      };



            List<PersonResponse> persons = _personService.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;
            return View(persons);
        }
    }
}
