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

        public Car DeleteCar(int carId)
        {
            var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == carId);
            if (car == null) return null;
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
            return car;
        }

        public Car EditCar(Car car)
        {
            var ed = _dbContext.Cars.FirstOrDefault(x => x.CarId == car.CarId);
            if (ed != null)
            {
                ed.Certification = car.Certification;
                ed.ChangeHistories = car.ChangeHistories;
                ed.Faults = car.Faults;
                ed.GoodGroup = car.GoodGroup;
                ed.LastRevision = car.LastRevision;
                ed.LastZTE = car.LastZTE;
                ed.LastZTL = car.LastZTL;
                ed.Name = car.Name;
                ed.Owner = car.Owner;
                ed.RevisionPeriod = car.RevisionPeriod;
                ed.Revisions = car.Revisions;
                ed.Serie = car.Serie;
                ed.ServiceResponsiblePerson = car.ServiceResponsiblePerson;
                ed.State = car.State;
                _dbContext.SaveChanges();
            }
            return ed;
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

        public List<Car> GetForPages(int start, int end)
        {
            return GetAll().Skip(start).Take(end).ToList();
        }
    }
}
