using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.DTOs
{
    public class ConcertDTO : JobDTO
    {
        public string Venue { get; set; }
        public string City { get; set; }

        public int MusicianId { get; set; }

        public ConcertDTO()
        {

        }
    }
}