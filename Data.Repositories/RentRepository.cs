using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RentRepository : IRentRepository
    {
        private ENVCtx _dbContext;

        public RentRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }
        public Rent GetById(int id)
        {
            return _dbContext.Rents.FirstOrDefault(x => x.RentId == id);
        }

        public List<Rent> GetForPages(int start, int end)
        {
            return _dbContext.Rents.OrderBy(x => x.RentId).Skip(start).Take(end).ToList();
        }

        public Rent GetRent(Rent rent)
        {
            //TODO
            throw new NotImplementedException();
        }

        public List<Rent> GetRentBySubject(int id)
        {
            return _dbContext.Rents.Where(x => x.Subject.SubjectId == id).OrderBy(x => x.StartDate).ToList();
        }

        public Rent MakeRent(Rent rent)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
