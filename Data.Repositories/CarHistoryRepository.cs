using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class CarHistoryRepository : ICarHistoryRepository
    {
        private ENVCtx _dbContext;

        public CarHistoryRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddChangeHistories(List<ChangeHistory> histories, ENVCtx ctx)
        {
            ctx.ChangeHistories.AddRange(histories);
            ctx.SaveChanges();
        }

        public void AddChangeHistory(ChangeHistory history)
        {
            if (history.Car == null) return;
            var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == history.Car.CarId);
            if(car != null)
            {
                history.Car = car;
                _dbContext.ChangeHistories.Add(history);
                _dbContext.SaveChanges();
            }
        }

        public List<ChangeHistory> GetChangeHistories(int carId, int skip, int count)
        {
            return _dbContext.ChangeHistories.Where(x => x.Car.CarId == carId).OrderBy(x => x.ChangeDate).Skip(skip).Take(count).ToList();
        }
    }
}
