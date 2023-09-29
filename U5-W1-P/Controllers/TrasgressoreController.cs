using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using U5_W1_P.Models;

namespace U5_W1_P.Controllers
{
    public class TrasgressoreController : Controller
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString());

        // GET: Trasgressori
        public ActionResult index()
        {
            List<Trasgressore> trasgressori = new List<Trasgressore>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM ANAGRAFICA";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Trasgressore trasgressore = new Trasgressore
                    {
                        idanagrafica = Convert.ToInt32(reader["idanagrafica"]),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        Indirizzo = reader["Indirizzo"].ToString(),
                        Città = reader["Città"].ToString(),
                        CAP = reader["CAP"].ToString(),
                        Cod_Fisc = reader["Cod_Fisc"].ToString(),
                    };

                    trasgressori.Add(trasgressore);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Si è verificato un errore durante l'accesso al database:" + ex.Message;
            }

            return View(trasgressori);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trasgressore trasgressore)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString))
                    {
                        conn.Open();

                        string query = "INSERT INTO ANAGRAFICA (Nome, Cognome, Indirizzo, Città, CAP, Cod_Fisc) VALUES (@Nome, @Cognome, @Indirizzo, @Città, @CAP, @Cod_Fisc)";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@Nome", trasgressore.Nome);
                        cmd.Parameters.AddWithValue("@Cognome", trasgressore.Cognome);
                        cmd.Parameters.AddWithValue("@Indirizzo", trasgressore.Indirizzo);
                        cmd.Parameters.AddWithValue("@Città", trasgressore.Città);
                        cmd.Parameters.AddWithValue("@CAP", trasgressore.CAP);
                        cmd.Parameters.AddWithValue("@Cod_Fisc", trasgressore.Cod_Fisc);

                        cmd.ExecuteNonQuery();
                    }

                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Si è verificato un errore durante l'inserimento dei dati:" + ex.Message;
                }
            }

            return View(trasgressore);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                conn.Open();

                string query = "SELECT * FROM ANAGRAFICA WHERE idanagrafica = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Trasgressore trasgressore = new Trasgressore
                    {
                        idanagrafica = Convert.ToInt32(reader["idanagrafica"]),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        Indirizzo = reader["Indirizzo"].ToString(),
                        Città = reader["Città"].ToString(),
                        CAP = reader["CAP"].ToString(),
                        Cod_Fisc = reader["Cod_Fisc"].ToString(),
                    };

                    conn.Close();

                    return View(trasgressore);
                }
                else
                {
                    conn.Close();

                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Si è verificato un errore durante l'accesso al database: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trasgressore trasgressore)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE ANAGRAFICA SET Nome = @Nome, Cognome = @Cognome, Indirizzo = @Indirizzo, Città = @Città, CAP = @CAP, Cod_Fisc = @Cod_Fisc WHERE idanagrafica = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Id", trasgressore.idanagrafica);
                    cmd.Parameters.AddWithValue("@Nome", trasgressore.Nome);
                    cmd.Parameters.AddWithValue("@Cognome", trasgressore.Cognome);
                    cmd.Parameters.AddWithValue("@Indirizzo", trasgressore.Indirizzo);
                    cmd.Parameters.AddWithValue("@Città", trasgressore.Città);
                    cmd.Parameters.AddWithValue("@CAP", trasgressore.CAP);
                    cmd.Parameters.AddWithValue("@Cod_Fisc", trasgressore.Cod_Fisc);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Si è verificato un errore durante l'aggiornamento dei dati: " + ex.Message;
                }
            }

            return View(trasgressore);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                conn.Open();

                string query = "SELECT * FROM ANAGRAFICA WHERE idanagrafica = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Trasgressore trasgressore = new Trasgressore
                    {
                        idanagrafica = Convert.ToInt32(reader["idanagrafica"]),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        Indirizzo = reader["Indirizzo"].ToString(),
                        Città = reader["Città"].ToString(),
                        CAP = reader["CAP"].ToString(),
                        Cod_Fisc = reader["Cod_Fisc"].ToString(),
                    };

                    conn.Close();

                    return View(trasgressore);
                }
                else
                {
                    conn.Close();

                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Si è verificato un errore durante l'accesso al database: " + ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                conn.Open();

                string query = "DELETE FROM ANAGRAFICA WHERE idanagrafica = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();

                conn.Close();

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Si è verificato un errore durante l'eliminazione del trasgressore:" + ex.Message;
                return View();
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                conn.Open();

                string query = "SELECT * FROM ANAGRAFICA WHERE idanagrafica = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Trasgressore trasgressore = new Trasgressore
                    {
                        idanagrafica = Convert.ToInt32(reader["idanagrafica"]),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        Indirizzo = reader["Indirizzo"].ToString(),
                        Città = reader["Città"].ToString(),
                        CAP = reader["CAP"].ToString(),
                        Cod_Fisc = reader["Cod_Fisc"].ToString(),
                    };

                    conn.Close();

                    return View(trasgressore);
                }
                else
                {
                    conn.Close();

                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Si è verificato un errore durante l'accesso al database:" + ex.Message;
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                conn.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}