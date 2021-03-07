using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }
        public decimal? Salary { get; set; }
        public bool? Completed { get; set; }
        public bool? Assigned { get; set; }
        public bool? Removed { get; set; }

        public JobDTO()
        {
            Id = 0;
            Assigned = false;
            Completed = false;
            Salary = 0;
            Removed = false;
        }
    }
}