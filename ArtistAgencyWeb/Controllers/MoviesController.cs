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
    public class MoviesController : Controller
    {
        MovieServices movieServices = new MovieServices();
        ActorServices actorServices = new ActorServices();

        // GET: Movies
        public ActionResult Index()
        {
            List<MovieDTO> mList = movieServices.LoadMovieDTOList();
            return View(mList);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MovieDTO movieDTO = movieServices.getMovieDTObyId(id);

            if (movieDTO == null)
            {
                return HttpNotFound();
            }
            return View(movieDTO);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            //ViewBag.idActor = new SelectList(db.Actor, "id", "id");
            //ViewBag.id = new SelectList(db.Job, "id", "id");
            return View();
        }

        // POST: Movies/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieDTO movieDTO)
        {
            Movie movieDB = movieServices.MapMovieDBfromDTO(movieDTO);
            if (ModelState.IsValid)
            {
                movieServices.AddMovie(movieDTO);
                
                return RedirectToAction("Index");
            }

            //ViewBag.idActor = new SelectList(db.Actor, "id", "id", movie.idActor);
            //ViewBag.id = new SelectList(db.Job, "id", "id", movie.id);
            return View(movieDTO);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            MovieDTO movieDTO = movieServices.getMovieDTObyId(id);
            if (movieDTO == null)
            {
                return HttpNotFound();
            }
            //ViewBag.idActor = new SelectList(db.Actor, "id", "id", movie.idActor);
            //ViewBag.id = new SelectList(db.Job, "id", "id", movie.id);
            return View(movieDTO);
        }

        // POST: Movies/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieDTO movieDTO)
        {
            Movie movieDB = movieServices.MapMovieDBfromDTOedit(movieDTO);
            if (ModelState.IsValid)
            {
                movieServices.EditMovie(movieDTO);
                
                return RedirectToAction("Index");
            }
            //ViewBag.idActor = new SelectList(db.Actor, "id", "id", movie.idActor);
            //ViewBag.id = new SelectList(db.Job, "id", "id", movie.id);
            return View();
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDTO movieDTO = movieServices.getMovieDTObyId(id);
            if (movieDTO == null)
            {
                return HttpNotFound();
            }
            return View(movieDTO);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            movieServices.Remove(id);
            return RedirectToAction("Index");
        }

        
        // GET: Movies/Assign/5
        public ActionResult Assign(int? id)
        {
            ViewBag.ActorId = new SelectList(actorServices.loadActorDTOList(), "Id", "CompleteName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MovieDTO movieDTO = movieServices.getMovieDTObyId(id);

            if (movieDTO == null)
            {
                return HttpNotFound();
            }
            //ViewBag.idActor = new SelectList(db.Actor, "id", "id", movie.idActor);
            //ViewBag.id = new SelectList(db.Job, "id", "id", movie.id);
            return View(movieDTO);
        }

        // POST: Movies/Assign/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(MovieDTO movieDTO,int actorId)
        {
       
            if (ModelState.IsValid)
            {
                //db.Entry(movie).State = EntityState.Modified;
                movieServices.AssignMovie(movieDTO, actorId);
                
                return RedirectToAction("Index");
            }
            ViewBag.ActorId = new SelectList(actorServices.loadActorDTOList(), "Id", "CompleteName");
            //ViewBag.idActor = new SelectList(db.Actor, "id", "id", movie.idActor);
            //ViewBag.id = new SelectList(db.Job, "id", "id", movie.id);
            return View();
        }

        public ActionResult FilterByText(string text)
        {
            //List<MovieDTO> movieFilteredList = movieServices.LoadMovieNameFilterDTOList(text);
            //return View(movieFilteredList.Count); 
            List<MovieDTO> movieFilteredList = movieServices.LoadMovieNameFilterDTOList(text);
            return View(movieFilteredList);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
