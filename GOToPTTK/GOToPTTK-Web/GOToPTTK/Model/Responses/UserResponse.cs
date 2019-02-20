using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public static UserResponse BuildFromModel(Uzytkownik user)
        {
            var u_res = new UserResponse()
            {
                Id = user.Id,
                Name = user.Imie,
                Surname = user.Nazwisko
            };

            return u_res;
        }
    }
}
