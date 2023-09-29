using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W1_P.Models;

namespace U5_W1_P.Controllers
{
    public class VerbaleController : Controller
    {
        private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString);
        
        // GET: Verbale
        public ActionResult Index()
        {
            try
            {
                conn.Open();

                string query = "SELECT * FROM VERBALE";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                var verbali = new List<Verbale>();
                foreach (DataRow row in dataTable.Rows)
                {
                    verbali.Add(new Verbale
                    {
                        idverbale = Convert.ToInt32(row["idverbale"]),
                        DataViolazione = Convert.ToDateTime(row["DataViolazione"]),
                        IndirizzoViolazione = row["IndirizzoViolazione"].ToString(),
                        Nominativo_Agente = row["Nominativo_Agente"].ToString(),
                        DataTrascrizioneVerbale = Convert.ToDateTime(row["DataTrascrizioneVerbale"]),
                        Importo = Convert.ToDecimal(row["Importo"]),
                        DecurtamentoPunti = Convert.ToInt32(row["DecurtamentoPunti"]),
                        idanagrafica = Convert.ToInt32(row["idanagrafica"]),
                        idviolazione = Convert.ToInt32(row["idviolazione"])
                    });
                }

                return View(verbali);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Errore durante l'accesso al database: " + ex.Message;
                return View(new List<Verbale>());
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult CreaVerbale()
        {
            return View(new Verbale());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaVerbale(Verbale verbale)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    conn.Open();

                    string query = "INSERT INTO VERBALE (DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, idanagrafica, idviolazione) " +
                   "VALUES (@DataViolazione, @IndirizzoViolazione, @Nominativo_Agente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @idanagrafica, @idviolazione)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                    cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                    cmd.Parameters.AddWithValue("@Nominativo_Agente", verbale.Nominativo_Agente);
                    cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                    cmd.Parameters.AddWithValue("@Importo", verbale.Importo);
                    cmd.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                    cmd.Parameters.AddWithValue("@idanagrafica", verbale.idanagrafica);
                    cmd.Parameters.AddWithValue("@idviolazione", verbale.idviolazione);

                    cmd.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }

                return View(verbale);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Errore durante l'accesso al database: " + ex.Message;
                return View(verbale);
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpGet]
        public ActionResult NewCreate()
        {
            return View();
        }
    }
}