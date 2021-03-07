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
    public class MusiciansController : Controller
    {
        private agencydb2Entities db = new agencydb2Entities();
        MusicianServices musicianServices = new MusicianServices();
        ConcertServices concertServices = new ConcertServices();

        // GET: Musicians
        public ActionResult Index()
        {
            List<MusicianDTO> mList = musicianServices.loadMusicianDTOList();
            return View(mList);
        }

        // GET: Musicians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //List<MusicianDTO> mList = musicianServices.loadMusicianDTOList();
            //MusicianDTO musicianDTO = mList.Find(x => x.Id == id);
            MusicianDTO musicianDTO = musicianServices.getMusicianDTObyId(id);
            if (musicianDTO == null)
            {
                return HttpNotFound();
            }
            return View(musicianDTO);
        }

        // GET: Musicians/Create
        public ActionResult Create()
        {
            //ViewBag.id = new SelectList(db.Client, "id", "id");
            return View();
        }

        // POST: Musicians/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MusicianDTO musicianDTO)
        {
            if (ModelState.IsValid)
            {
                musicianServices.AddMusician(musicianDTO);
                return RedirectToAction("Index");
            }

            //ViewBag.id = new SelectList(db.Client, "id", "id", musician.id);
            return View(musicianDTO);
        }

        // GET: Musicians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicianDTO musicianDTO = musicianServices.getMusicianDTObyId(id);
            if (musicianDTO == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id = new SelectList(db.Client, "id", "id", musician.id);
            return View(musicianDTO);
        }

        // POST: Musicians/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MusicianDTO musicianDTO)
        {
            if (ModelState.IsValid)
            {
                musicianServices.EditMusician(musicianDTO);
                return RedirectToAction("Index");
            }
            //ViewBag.id = new SelectList(db.Client, "id", "id", musician.id);
            return View(musicianDTO);
        }

        // GET: Musicians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicianDTO musicianDTO = musicianServices.getMusicianDTObyId(id);
            if (musicianDTO == null)
            {
                return HttpNotFound();
            }
            return View(musicianDTO);
        }

        // POST: Musicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            musicianServices.Remove(id);
            return RedirectToAction("Index");
        }

        // GET: Musicians/Perform/5
        public ActionResult Perform(int? concertId, int? musicianId)
        {
            if (concertId == null || musicianId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musician musician = musicianServices.getMusicianDBbyId(musicianId);
            MusicianDTO musicianDTO = musicianServices.mapMusicianDTOfromDB(musician);
            Concert concert = concertServices.getConcertDBbyId(concertId);
            if (musician == null || concert == null)
            {
                return HttpNotFound();
            }
            return View(musicianDTO);
        }

        // POST: Musicians/Perform/5
        [HttpPost, ActionName("Perform")]
        [ValidateAntiForgeryToken]
        public ActionResult PerformConfirmed(int? concertId, int? musicianId)
        {
            musicianServices.Perform(musicianId, concertId);
            return RedirectToAction("Index");
        }

        public ActionResult FilterByText(string text)
        {
             
            List<MusicianDTO> musicianFilteredList = musicianServices.LoadMusicianNameFilterDTOList(text);
            return View(musicianFilteredList);
        }

        // GET: Musicians/Quit/5
        public ActionResult Quit(int? concertId, int? musicianId)
        {
            if (concertId == null || musicianId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musician musician = musicianServices.getMusicianDBbyId(musicianId);
            MusicianDTO musicianDTO = musicianServices.mapMusicianDTOfromDB(musician);
            Concert concert = concertServices.getConcertDBbyId(concertId);
            if (concert == null || musician == null)
            {
                return HttpNotFound();
            }
            return View(musicianDTO);
        }

        // POST: Musicians/Quit/5
        [HttpPost, ActionName("Quit")]
        [ValidateAntiForgeryToken]
        public ActionResult QuitConfirmed(int? concertId, int? musicianId)
        {
            musicianServices.Quit(musicianId, concertId);
            return RedirectToAction("Index");
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
