using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtistAgencyWeb.DTOs;
using ArtistAgencyWeb.Models;
using ArtistAgencyWeb.Services;

namespace ArtistAgencyWeb.Controllers
{
    public class ConcertsController : Controller
    {
        private agencydb2Entities db = new agencydb2Entities();
        ConcertServices concertServices = new ConcertServices();
        MusicianServices musicianServices = new MusicianServices();

        // GET: Concerts
        public ActionResult Index()
        {
            List<ConcertDTO> cList = concertServices.LoadConcertDTOList();
            return View(cList);
        }

        // GET: Concerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ConcertDTO concertDTO = concertServices.getConcertDTObyId(id);
            if (concertDTO == null)
            {
                return HttpNotFound();
            }
            return View(concertDTO);
        }

        // GET: Concerts/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Concerts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConcertDTO concertDTO)
        {
            Concert concertDB = concertServices.mapConcertDBfromDTO(concertDTO);
            if (ModelState.IsValid)
            {
                concertServices.AddConcert(concertDTO);
                return RedirectToAction("Index");
            }

            return View(concertDTO);
        }

        // GET: Concerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ConcertDTO concertDTO = concertServices.getConcertDTObyId(id);

            if (concertDTO == null)
            {
                return HttpNotFound();
            }
           //ViewBag.id = new SelectList(db.Job, "id", "id", concert.id);
           //ViewBag.idMusician = new SelectList(db.Musician, "id", "id", concert.idMusician);
            return View(concertDTO);
        }

        // POST: Concerts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConcertDTO concertDTO)
        {
            Concert concertDB = concertServices.mapConcertDBfromDTOedit(concertDTO);
            if (ModelState.IsValid)
                
            {
                concertServices.EditConcert(concertDTO);
                return RedirectToAction("Index");
            }
            //ViewBag.id = new SelectList(db.Job, "id", "id", concert.id);
            //ViewBag.idMusician = new SelectList(db.Musician, "id", "id", concert.idMusician);
            return View();
        }

        // GET: Concerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConcertDTO concertDTO = concertServices.getConcertDTObyId(id);

            if (concertDTO == null)
            {
                return HttpNotFound();
            }
            return View(concertDTO);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            concertServices.Remove(id);
            return RedirectToAction("Index");
        }

        // GET: Concerts/Assign/5
        public ActionResult Assign(int? id)
        {
            ViewBag.MusicianId = new SelectList(musicianServices.loadMusicianDTOList(), "Id", "CompleteName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ConcertDTO concertDTO = concertServices.getConcertDTObyId(id);

            if (concertDTO == null)
            {
                return HttpNotFound();
            }
            //ViewBag.idActor = new SelectList(db.Actor, "id", "id", movie.idActor);
            //ViewBag.id = new SelectList(db.Job, "id", "id", movie.id);
            return View(concertDTO);
        }

        // POST: Concerts/Assign/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(ConcertDTO concertDTO, int musicianId)
        {

            if (ModelState.IsValid)
            {
                //db.Entry(movie).State = EntityState.Modified;
                concertServices.AssignConcert(concertDTO, musicianId);

                return RedirectToAction("Index");
            }
            ViewBag.ActorId = new SelectList(musicianServices.loadMusicianDTOList(), "Id", "CompleteName");
            //ViewBag.idActor = new SelectList(db.Actor, "id", "id", movie.idActor);
            //ViewBag.id = new SelectList(db.Job, "id", "id", movie.id);
            return View();
        }

        public ActionResult FilterByText(string text)
        {
            //List<MovieDTO> movieFilteredList = movieServices.LoadMovieNameFilterDTOList(text);
            //return View(movieFilteredList.Count); 
            List<ConcertDTO> concertFilteredList = concertServices.LoadConcertNameFilterDTOList(text);
            return View(concertFilteredList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
