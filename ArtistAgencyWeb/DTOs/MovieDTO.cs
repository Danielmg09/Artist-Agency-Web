using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.DTOs
{
    public class MovieDTO : JobDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }

        public int ActorId { get; set; }

        public MovieDTO()
        {
        }
    }
}