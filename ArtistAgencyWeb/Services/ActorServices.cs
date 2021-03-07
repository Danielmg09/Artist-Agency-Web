using ArtistAgencyWeb.DTOs;
using ArtistAgencyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.Services
{
    public class ActorServices
    {
        public agencydb2Entities db = new agencydb2Entities();

        MovieServices movieServices = new MovieServices();

        public ActorDTO mapActorDTOfromDB(Actor actorDB)
        {
            ActorDTO actorDTO = new ActorDTO();

            actorDTO.Id = actorDB.Client.User.id;
            actorDTO.FirstName = actorDB.Client.User.firstName;
            actorDTO.LastName = actorDB.Client.User.lastName;
            actorDTO.Income = actorDB.Client.income ?? 0;
            actorDTO.MovieList = getMovieListbyActor(actorDB);
            actorDTO.Removed = actorDB.Client.User.removed ?? false;

            return actorDTO;
        }
        public Actor mapActorDBfromDTO(ActorDTO actorDTO)
        {
            Actor actorDB = new Actor();
            actorDB.Client = new Client();
            actorDB.Client.User = new User();

            actorDB.Client.User.firstName = actorDTO.FirstName;
            actorDB.Client.User.lastName = actorDTO.LastName;
            actorDB.Client.income = actorDTO.Income ?? 0;
            actorDB.Client.User.removed = actorDTO.Removed;

            return actorDB;
        }

        public Actor mapActorDBfromDTOedit(ActorDTO actorDTO)
        {
            Actor actorDB = db.Actor.FirstOrDefault(x => x.id == actorDTO.Id);

            actorDB.Client.User.firstName = actorDTO.FirstName;
            actorDB.Client.User.lastName = actorDTO.LastName;
            actorDB.Client.income = actorDTO.Income;

            return actorDB;
        }



        public List<MovieDTO> getMovieListbyActor(Actor actorDB)
        {
            List<MovieDTO> movies = new List<MovieDTO>();

            foreach (Movie movie in db.Movie)
            {
                if (movie.idActor == actorDB.id)
                {
                    MovieDTO movieDTO = movieServices.MapMovieDTOfromDB(movie);
                    movies.Add(movieDTO);
                }
            }

            return movies;
        }
        public List<ActorDTO> loadActorDTOList()
        {
            List<ActorDTO> aList = new List<ActorDTO>();

            foreach (Actor actor in db.Actor)
            {
                ActorDTO aDTO = mapActorDTOfromDB(actor);
                aList.Add(aDTO);
            }

            return aList;
        }

        public List<ActorDTO> LoadActorNameFilterDTOList(string text)
        {

            List<ActorDTO> aList = new List<ActorDTO>(); 

            foreach (Actor actor in db.Actor)
            {
                ActorDTO aDTO = mapActorDTOfromDB(actor);
                if (aDTO.Removed == false && aDTO.FirstName == text || aDTO.LastName == text)
                {
                    aList.Add(aDTO);
                }
            }

            return aList;
        }

        public Actor getActorDBbyId(int? id)
        {
            return db.Actor.FirstOrDefault(x => x.id == id);
        }
        public ActorDTO getActorDTObyId(int? id)
        {
            List<ActorDTO> aList = loadActorDTOList();
            return aList.Find(x => x.Id == id);
        }
        public void AddActor(ActorDTO actorDTO)
        {
            Actor actorDB = mapActorDBfromDTO(actorDTO);
            db.Actor.Add(actorDB);
            db.SaveChanges();
        }
        
        public void EditActor(ActorDTO actorDTO)
        {
            Actor actorDB = mapActorDBfromDTOedit(actorDTO);
            db.SaveChanges();
        }
        
        public void Perform(int? actorId, int? movieId)
        {
            Movie movie = db.Movie.FirstOrDefault(x => x.id == movieId);
            movie.Job.completed = true;
            Actor actor = db.Actor.FirstOrDefault(x => x.id == actorId);
            actor.Client.income += movie.Job.salary;
            db.SaveChanges();
        }

        public void Quit(int? actorId, int? movieId)
        {
            Movie movie = db.Movie.FirstOrDefault(x => x.id == movieId);
            movie.Job.assigned = false;
            movie.idActor = null;
            db.SaveChanges();
        }
        public void Remove(int id)
        {
            Actor actorDB = getActorDBbyId(id);
            actorDB.Client.User.removed = true;
            db.SaveChanges();
        }
    }
}