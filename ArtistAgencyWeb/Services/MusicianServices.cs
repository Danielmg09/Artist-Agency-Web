using ArtistAgencyWeb.DTOs;
using ArtistAgencyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistAgencyWeb.Services
{
    public class MusicianServices
    {
        private agencydb2Entities db = new agencydb2Entities();
        ConcertServices concertServices = new ConcertServices();
        public MusicianDTO mapMusicianDTOfromDB(Musician musicianDB)
        {
            MusicianDTO musicianDTO = new MusicianDTO();

            musicianDTO.Id = musicianDB.Client.User.id;
            musicianDTO.FirstName = musicianDB.Client.User.firstName;
            musicianDTO.LastName = musicianDB.Client.User.lastName;
            musicianDTO.Income = musicianDB.Client.income ?? 0;
            musicianDTO.ConcertList = getConcertListbyMusician(musicianDB);
            musicianDTO.Removed = musicianDB.Client.User.removed ?? false;

            return musicianDTO;
        }

        public Musician mapMusicianDBfromDTO(MusicianDTO musicianDTO)
        {
            Musician musicianDB = new Musician();
            musicianDB.Client = new Client();
            musicianDB.Client.User = new User();

            musicianDB.Client.User.firstName = musicianDTO.FirstName;
            musicianDB.Client.User.lastName = musicianDTO.LastName;
            musicianDB.Client.income = musicianDTO.Income ?? 0;
            musicianDB.Client.User.removed = musicianDTO.Removed;

            return musicianDB;
        }

        public Musician mapMusicianDBfromDTOedit(MusicianDTO musicianDTO)
        {
            Musician musicianDB = db.Musician.FirstOrDefault(x => x.id == musicianDTO.Id);

            musicianDB.Client.User.firstName = musicianDTO.FirstName;
            musicianDB.Client.User.lastName = musicianDTO.LastName;
            musicianDB.Client.income = musicianDTO.Income;
            

            return musicianDB;
        }
        public List<ConcertDTO> getConcertListbyMusician(Musician musicianDB)
        {
            List<ConcertDTO> concerts = new List<ConcertDTO>();

            foreach (Concert c in db.Concert)
            {
                if (c.idMusician == musicianDB.id)
                {
                    ConcertDTO concertDTO = concertServices.mapConcertDTOfromDB(c);
                    concerts.Add(concertDTO);
                }
            }

            return concerts;
        }
        public List<MusicianDTO> loadMusicianDTOList()
        {
            List<MusicianDTO> mList = new List<MusicianDTO>();

            foreach (Musician musician in db.Musician)
            {
                MusicianDTO mDTO = mapMusicianDTOfromDB(musician);
                mList.Add(mDTO);
            }

            return mList;
        }

        public List<MusicianDTO> LoadMusicianNameFilterDTOList(string text)
        {

            List<MusicianDTO> mList = new List<MusicianDTO>();

            foreach (Musician musician in db.Musician)
            {
                MusicianDTO mDTO = mapMusicianDTOfromDB(musician);
                if (mDTO.Removed == false && mDTO.FirstName == text || mDTO.LastName == text)
                {
                    mList.Add(mDTO);
                }
            }

            return mList;
        }

        public Musician getMusicianDBbyId(int? id)
        {
            return db.Musician.FirstOrDefault(x => x.id == id);
        }
        public MusicianDTO getMusicianDTObyId(int? id)
        {
            List<MusicianDTO> mList = loadMusicianDTOList();
            return mList.Find(x => x.Id == id);
        }
        public void AddMusician(MusicianDTO musicianDTO)
        {
            Musician musicianDB = mapMusicianDBfromDTO(musicianDTO);
            db.Musician.Add(musicianDB);
            db.SaveChanges();
        }

        public void EditMusician(MusicianDTO musicianDTO)
        {
            Musician musicianDB = mapMusicianDBfromDTOedit(musicianDTO);
            db.SaveChanges();
        }

        public void Perform(int? musicianId, int? concertId)
        {
            Concert concert = db.Concert.FirstOrDefault(x => x.id == concertId);
            concert.Job.completed = true;
            Musician musician = db.Musician.FirstOrDefault(x => x.id == musicianId);
            musician.Client.income += concert.Job.salary;
            db.SaveChanges();
        }

        public void Quit(int? musicianId, int? concertId)
        {
            Concert concert = db.Concert.FirstOrDefault(x => x.id == concertId);
            concert.Job.assigned = false;
            concert.idMusician = null;
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            Musician musicianDB = getMusicianDBbyId(id);
            musicianDB.Client.User.removed = true;
            db.SaveChanges();
        }

    }
}