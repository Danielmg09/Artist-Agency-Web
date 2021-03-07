using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.DTOs
{
    public class UserDTO
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Removed { get; set; }

        public string CompleteName { get
            {
                return this.FirstName + " " + this.LastName;
            } }


        public UserDTO()
        {
            Removed = false;
        }
    }
}