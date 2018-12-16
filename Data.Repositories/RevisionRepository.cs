using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RevisionRepository : IRevisionRepository
    {
        private ENVCtx _dbContext;

        public RevisionRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddRevision(Revision revision)
        {
            try
            {
                if (revision.Car != null)
                {
                    var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == revision.Car.CarId);
                    if(car == null)
                        return -1;
                    revision.Car = car;
                }
                _dbContext.Revisions.Add(revision);
                _dbContext.SaveChanges();

                return revision.RevisionId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Revision DeleteRevision(int revisionId)
        {
            var revision = _dbContext.Revisions.FirstOrDefault(x => x.RevisionId == revisionId);
            if (revision == null) return null;
            _dbContext.Revisions.Remove(revision);
            _dbContext.SaveChanges();
            return revision;
        }

        public Revision EditRevision(Revision revision)
        {
            if (revision == null) return null;
            var ed = _dbContext.Revisions.FirstOrDefault(x => x.RevisionId == revision.RevisionId);
            if (ed != null)
            {
                ed.LastRevisionDate = revision.LastRevisionDate;
                ed.Description = revision.Description;

                if (revision.Car != null)
                {
                    var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == revision.Car.CarId);
                    if (car != null)
                        ed.Car = car;
                }
                _dbContext.SaveChanges();
            }
            return ed;
        }

        public Revision GetById(int revisionId)
        {
            return _dbContext.Revisions.FirstOrDefault(x => x.RevisionId == revisionId);
        }

        public List<Revision> GetCarRevision(int carId)
        {
            return _dbContext.Revisions.Where(x => x.Car.CarId == carId).ToList().OrderBy(x=>x.LastRevisionDate).ToList();
        }

        public List<Revision> GetForPages(int start, int end)
        {
            return _dbContext.Revisions.OrderBy(x => x.RevisionId).Skip(start).Take(end).ToList();
        }
    }
}
