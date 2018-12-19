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

        public Rent GetRent(int carId, int subjectId, DateTime dueDay)
        {
            var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == carId);
            var subject = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == subjectId);
            if (car == null || subject == null) return null;

            car.State = State.Rented;
            var rent = new Rent()
            {
                Car = car,
                RentType = RentType.Rented,
                StartDate = DateTime.Now,
                Subject = subject,
                EndDate = dueDay
            };
            var retRent = _dbContext.Rents.Add(rent);
            _dbContext.SaveChanges();
            return retRent;
        }
        public Rent MakeRent(int carId, int subjectId, DateTime dueDay)
        {
            var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == carId);
            var subject = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == subjectId);
            if (car == null || subject == null) return null;

            car.State = State.InRent;
            var rent = new Rent()
            {
                Car = car,
                RentType = RentType.Owned,
                StartDate = DateTime.Now,
                Subject = subject,
                EndDate = dueDay
            };
            var retRent = _dbContext.Rents.Add(rent);
            _dbContext.SaveChanges();
            return retRent;
        }

        public List<Rent> GetRentBySubject(int id)
        {
            return _dbContext.Rents.Where(x => x.Subject.SubjectId == id).OrderBy(x => x.StartDate).ToList();
        }
    }
}
