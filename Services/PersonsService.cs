using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //check if PersonAddRequest is not null
           if(personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

           // validate PersonName
           if(string.IsNullOrEmpty(personAddRequest.PersonName))
            {
                throw new ArgumentException("PersonName can't be blank");
            }
           // to do
            return;
        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
