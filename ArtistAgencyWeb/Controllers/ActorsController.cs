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
    public class ActorsController : Controller
    {
        ActorServices actorServices = new ActorServices();
        MovieServices movieServices = new MovieServices();

        // GET: Actors
        public ActionResult Index()
        {
            List<ActorDTO> aList = actorServices.loadActorDTOList();
            return View(aList);
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ActorDTO actorDTO = actorServices.getActorDTObyId(id);
            //Actor actor = db.Actor.Find(id);
            if (actorDTO == null)
            {
                return HttpNotFound();
            }
            return View(actorDTO);
        }

        // GET: Actors/Create
        public ActionResult Create()
        {
            //ViewBag.id = new SelectList(db.Client, "id", "id");
            return View();
        }

        // POST: Actors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActorDTO actorDTO)
        {
            if (ModelState.IsValid)
            {
                actorServices.AddActor(actorDTO);
                return RedirectToAction("Index");
            }

            //ViewBag.id = new SelectList(db.Client, "id", "id", actor.id);
            return View();
        }

        // GET: Actors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActorDTO actorDTO = actorServices.getActorDTObyId(id);
            if (actorDTO == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id = new SelectList(db.Client, "id", "id", actor.id);
            return View(actorDTO);
        }

        // POST: Actors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ActorDTO actorDTO)
        {
            if (ModelState.IsValid)
            {
                actorServices.EditActor(actorDTO);
                return RedirectToAction("Index");
            }

            //ViewBag.id = new SelectList(db.Client, "id", "id", actor.id);
            return View(actorDTO);
        }

        // GET: Actors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActorDTO actorDTO = actorServices.getActorDTObyId(id);
            if (actorDTO == null)
            {
                return HttpNotFound();
            }
            return View(actorDTO);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            actorServices.Remove(id);
            return RedirectToAction("Index");
        }

        // GET: Actors/Perform/5
        public ActionResult Perform(int? movieId,int? actorId)
        {
            if (movieId == null || actorId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = actorServices.getActorDBbyId(actorId);
            ActorDTO actorDTO = actorServices.mapActorDTOfromDB(actor);
            Movie movie = movieServices.getMovieDBbyId(movieId);
            if (actor == null || movie == null)
            {
                return HttpNotFound();
            }
            return View(actorDTO);
        }

        // POST: Actors/Perform/5
        [HttpPost, ActionName("Perform")]
        [ValidateAntiForgeryToken]
        public ActionResult PerformConfirmed(int? movieId, int? actorId)
        {
            actorServices.Perform(actorId, movieId);
            return RedirectToAction("Index");
        }

        public ActionResult FilterByText(string text)
        {

            List<ActorDTO> actorFilteredList = actorServices.LoadActorNameFilterDTOList(text);
            return View(actorFilteredList);
        }

        // GET: Actors/Quit/5
        public ActionResult Quit(int? movieId, int? actorId)
        {
            if (movieId == null || actorId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = actorServices.getActorDBbyId(actorId);
            ActorDTO actorDTO = actorServices.mapActorDTOfromDB(actor);
            Movie movie = movieServices.getMovieDBbyId(movieId);
            if (actor == null || movie == null)
            {
                return HttpNotFound();
            }
            return View(actorDTO);
        }

        // POST: Actors/Quit/5
        [HttpPost, ActionName("Quit")]
        [ValidateAntiForgeryToken]
        public ActionResult QuitConfirmed(int? movieId, int? actorId)
        {
            actorServices.Quit(actorId, movieId);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        actorServices.db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
