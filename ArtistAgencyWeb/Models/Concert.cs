//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArtistAgencyWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Concert
    {
        public int id { get; set; }
        public string venue { get; set; }
        public string city { get; set; }
        public Nullable<int> idMusician { get; set; }
    
        public virtual Job Job { get; set; }
        public virtual Musician Musician { get; set; }
    }
}
