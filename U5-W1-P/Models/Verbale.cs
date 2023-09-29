using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace U5_W1_P.Models
{
    public class Verbale
    {
            public int idverbale { get; set; }

            [Required]
            public DateTime DataViolazione { get; set; }

            [Required]
            public string IndirizzoViolazione { get; set; }

            [Required]
            public string Nominativo_Agente { get; set; }

            [Required]
            public DateTime DataTrascrizioneVerbale { get; set; }

            [Required]
            public decimal Importo { get; set; }

            [Required]
            public int DecurtamentoPunti { get; set; }

            [Required]
            public int idanagrafica { get; set; }

            [Required]
            public int idviolazione { get; set; }
        }
    }