using ArtistAgencyWeb.DTOs;
using ArtistAgencyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.Services
{
    public class MovieServices
    {
        private agencydb2Entities db = new agencydb2Entities();

        public MovieDTO MapMovieDTOfromDB(Movie movieDB)
        {
            MovieDTO movieDTO = new MovieDTO();

            movieDTO.Id = movieDB.Job.id;
            movieDTO.Salary = movieDB.Job.salary ?? 0;
            movieDTO.Title = movieDB.title;
            movieDTO.Genre = movieDB.genre;
            movieDTO.Assigned = movieDB.Job.assigned ?? false;
            movieDTO.Completed = movieDB.Job.completed ?? false;
            movieDTO.Removed = movieDB.Job.removed ?? false;

            return movieDTO;

        }

        public Movie MapMovieDBfromDTO(MovieDTO movieDTO)
        {

            Movie movieDB = new Movie();
            movieDB.Job = new Job();
            

            movieDB.title = movieDTO.Title;
            movieDB.genre = movieDTO.Genre;
            movieDB.Job.assigned = movieDTO.Assigned;
            movieDB.Job.salary = movieDTO.Salary;
            movieDB.Job.completed = movieDTO.Completed;
            movieDB.Job.removed = movieDTO.Removed;
            //movieDB.idActor = getActoridByMovie(movieDTO.Id);
            movieDB.idActor = null;

            return movieDB;

        }

        public Movie MapMovieDBfromDTOedit(MovieDTO movieDTO)
        {

            Movie movieDB = db.Movie.FirstOrDefault(x => x.id == movieDTO.Id);

            movieDB.title = movieDTO.Title;
            movieDB.genre = movieDTO.Genre;
            //movieDB.Job.assigned = movieDTO.Assigned;
            movieDB.Job.salary = movieDTO.Salary;
            //movieDB.Job.completed = movieDTO.Completed;
            //movieDB.Job.removed = movieDTO.Removed;
            //movieDB.idActor = getActoridByMovie(movieDTO.Id);
            //movieDB.idActor = null;

            return movieDB;

        }
        public List<MovieDTO> LoadMovieDTOList()
        {
            
            List<MovieDTO> mList = new List<MovieDTO>();

            foreach(Movie movie in db.Movie)
            {
                MovieDTO mDTO = MapMovieDTOfromDB(movie);
                if(mDTO.Removed == false)
                {
                    mList.Add(mDTO);
                }
                
            }

            return mList;
        }
       
        public List<MovieDTO> LoadMovieNameFilterDTOList(string text)
        {

            List<MovieDTO> mList = new List<MovieDTO>();

            foreach (Movie movie in db.Movie)
            {
                MovieDTO mDTO = MapMovieDTOfromDB(movie);
                if (mDTO.Removed == false && mDTO.Title == text)
                {
                    mList.Add(mDTO);
                }
            }

            return mList;
        }


        public MovieDTO getMovieDTObyId(int? id)
        {
            List<MovieDTO> mList = LoadMovieDTOList();
            return mList.Find(x => x.Id == id);
        }

        public Movie getMovieDBbyId(int? id)
        {
            return db.Movie.FirstOrDefault(x => x.id == id);
        }

        public void AddMovie(MovieDTO movieDTO)
        {
            Movie movieDB = MapMovieDBfromDTO(movieDTO);
            db.Movie.Add(movieDB);
            db.SaveChanges();
        }

        public void EditMovie(MovieDTO movieDTO)
        {
            Movie movieDB = MapMovieDBfromDTOedit(movieDTO);
            db.SaveChanges();
        }
        public void Remove(int id)
        {
            Movie movieDB = getMovieDBbyId(id);
            movieDB.Job.removed = true;
            db.SaveChanges();
        }
        public void AssignMovie(MovieDTO movieDTO, int actorId)
        {
            
            Movie movieDB = db.Movie.FirstOrDefault(x => x.id == movieDTO.Id);
            movieDB.idActor = actorId;
            movieDB.Job.assigned = true;
            db.SaveChanges();
        }



    }
}