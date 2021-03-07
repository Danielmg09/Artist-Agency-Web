using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.DTOs
{
    public class ActorDTO : ClientDTO
    {
        public List<MovieDTO> MovieList { get; set; }

        public ActorDTO()
        {
            MovieList = new List<MovieDTO>();
        }
    }
}