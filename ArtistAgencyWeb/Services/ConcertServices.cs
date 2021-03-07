using ArtistAgencyWeb.DTOs;
using ArtistAgencyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.Services
{
    public class ConcertServices
    {
        private agencydb2Entities db = new agencydb2Entities();

        public ConcertDTO mapConcertDTOfromDB(Concert concertDB)
        {
            ConcertDTO concertDTO = new ConcertDTO();

            concertDTO.Id = concertDB.Job.id;
            concertDTO.Salary = concertDB.Job.salary ?? 0;
            concertDTO.Venue = concertDB.venue;
            concertDTO.City = concertDB.city;
            concertDTO.Assigned = concertDB.Job.assigned ?? false;
            concertDTO.Completed = concertDB.Job.completed ?? false;
            concertDTO.Removed = concertDB.Job.removed ?? false;

            return concertDTO;
        }
        public Concert mapConcertDBfromDTO(ConcertDTO concertDTO)
        {
            
            Concert concertDB = new Concert();
            concertDB.Job = new Job();
            
            concertDB.Job.salary = concertDTO.Salary;
            concertDB.venue = concertDTO.Venue;
            concertDB.city = concertDTO.City;
            concertDB.Job.assigned = concertDTO.Assigned;
            concertDB.Job.completed = concertDTO.Completed;
            concertDB.Job.removed = concertDTO.Removed;
            //concertDB.idMusician = getMusicianIdByConcert(concertDTO.Id);
            concertDB.idMusician = null;

            return concertDB;
        }
        public Concert mapConcertDBfromDTOedit(ConcertDTO concertDTO)
        {

            Concert concertDB = db.Concert.FirstOrDefault(x => x.id == concertDTO.Id);

            concertDB.Job.salary = concertDTO.Salary;
            concertDB.venue = concertDTO.Venue;
            concertDB.city = concertDTO.City;
            

            return concertDB;
        }

        //public int? getMusicianIdByConcert(int concertId)
        //{
        //    int? musicianId = null;
        //    foreach (UserDTO u in UserServiceDTO.UserList)
        //    {
        //        if (u is MusicianDTO)
        //        {
        //            foreach (ConcertDTO c in ((MusicianDTO)u).ConcertList)
        //            {
        //                if (concertId == c.Id)
        //                {
        //                    musicianId = u.Id;
        //                    return musicianId;
        //                }
        //            }
        //        }
        //    }
        //    return musicianId;
        //}
        public List<ConcertDTO> LoadConcertDTOList()
        {
            List<ConcertDTO> cList = new List<ConcertDTO>();

            foreach (Concert concert in db.Concert)
            {
                ConcertDTO cDTO = mapConcertDTOfromDB(concert);
                cList.Add(cDTO);
            }

            return cList;
        }

        public List<ConcertDTO> LoadConcertNameFilterDTOList(string text)
        {

            List<ConcertDTO> cList = new List<ConcertDTO>();

            foreach (Concert concert in db.Concert)
            {
                ConcertDTO cDTO = mapConcertDTOfromDB(concert);
                if (cDTO.Removed == false && cDTO.Venue == text || cDTO.City == text)
                {
                    cList.Add(cDTO);
                }
            }

            return cList;
        }

        public ConcertDTO getConcertDTObyId(int? id)
        {
            List<ConcertDTO> cList = LoadConcertDTOList();
            return cList.Find(x => x.Id == id);
        }

        public Concert getConcertDBbyId(int? id)
        {
            return db.Concert.FirstOrDefault(x => x.id == id);
        }

        public void AddConcert(ConcertDTO concertDTO)
        {
            Concert concertDB = mapConcertDBfromDTO(concertDTO);
            db.Concert.Add(concertDB);
            db.SaveChanges();
        }

        public void EditConcert(ConcertDTO concertDTO)
        {
            Concert concertDB = mapConcertDBfromDTOedit(concertDTO);
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            Concert concertDB = getConcertDBbyId(id);
            concertDB.Job.removed = true;
            db.SaveChanges();
        }

        public void AssignConcert(ConcertDTO concertDTO, int musicianId)
        {

            Concert concertDB = db.Concert.FirstOrDefault(x => x.id == concertDTO.Id);
            concertDB.idMusician = musicianId;
            concertDB.Job.assigned = true;
            db.SaveChanges();
        }
    }
}