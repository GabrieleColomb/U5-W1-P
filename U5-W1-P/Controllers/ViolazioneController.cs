using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W1_P.Models;

namespace U5_W1_P.Controllers
{
    public class ViolazioneController : Controller
    {
        private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString);

        public ActionResult Index()
        {
            try
            {
                conn.Open();

                string query = "SELECT idviolazione, Descrizione FROM Tipo_Violazione";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                var tipologieViolazioni = new List<Violazione>();
                foreach (DataRow row in dataTable.Rows)
                {
                    tipologieViolazioni.Add(new Violazione
                    {
                        idviolazione = Convert.ToInt32(row["idviolazione"]),
                        Descrizione = row["Descrizione"].ToString()
                    });
                }

                return View(tipologieViolazioni);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Errore durante l'accesso al database: " + ex.Message;
                return View(new List<Violazione>()); 
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult Create()
        {
            return View(new Violazione());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Violazione Violazione)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    conn.Open();

                    string query = "INSERT INTO Tipo_Violazione (Descrizione) VALUES (@Descrizione)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Descrizione", Violazione.Descrizione);
                    cmd.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }

                return View(Violazione);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Errore durante l'accesso al database: " + ex.Message;
                return View(Violazione);
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Violazione Violazione)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    conn.Open();

                    string query = "UPDATE Tipo_Violazione SET Descrizione = @Descrizione WHERE idviolazione = @idviolazione";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idviolazione", Violazione.idviolazione);
                    cmd.Parameters.AddWithValue("@Descrizione", Violazione.Descrizione);
                    cmd.ExecuteNonQuery();

                    return RedirectToAction("Index");
                }

                return View(Violazione);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Errore durante l'accesso al database: " + ex.Message;
                return View(Violazione);
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                conn.Open();

                string query = "DELETE FROM Tipo_Violazione WHERE idviolazione = @idviolazione";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idviolazione", id);
                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Errore durante l'accesso al database: " + ex.Message;
                return View();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}