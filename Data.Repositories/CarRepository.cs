using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private ENVCtx _dbContext;

        public CarRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddCar(Car car)
        {
            try
            {
                _dbContext.Cars.Add(car);
                return _dbContext.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExcludeCar(int carId)
        {
            try
            {
                var car = _dbContext.Cars.First(x => x.CarId == carId);
                car.State = State.Excluded;
                return _dbContext.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Car> GetAll()
        {
            return _dbContext.Cars.ToList();
        }

        public Car GetById(int carId)
        {
            return _dbContext.Cars.FirstOrDefault(x => x.CarId == carId);
        }

        public List<Revision> GetCarRevision(int carId)
        {
                return _dbContext.Revisions
                    .Where(x => x.Car.CarId == carId)
                    .ToList();
        }
    }
}
