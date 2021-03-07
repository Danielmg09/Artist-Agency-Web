using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.DTOs
{
    public class MusicianDTO : ClientDTO
    {
        public List<ConcertDTO> ConcertList { get; set; }

        public MusicianDTO()
        {
            ConcertList = new List<ConcertDTO>();
        }
    }
}