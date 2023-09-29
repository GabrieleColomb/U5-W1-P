using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace U5_W1_P.Models
{
    public class Violazione
    {
        public int idviolazione { get; set; }

        [Required]
        public string Descrizione { get; set; }
    }
}