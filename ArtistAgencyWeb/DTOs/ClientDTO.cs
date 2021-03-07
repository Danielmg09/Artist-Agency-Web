using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.DTOs
{
    public class ClientDTO : UserDTO
    {
        public decimal? Income { get; set; }

        public ClientDTO()
        {
        }
    }
}