using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace U5_W1_P.Models
{
    public class Trasgressore
    {
        [Key]
        public int idanagrafica { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        public string Indirizzo { get; set; }

        public string Città { get; set; }

        public string CAP { get; set; }

        [Required]
        public string Cod_Fisc { get; set; }
    }
}