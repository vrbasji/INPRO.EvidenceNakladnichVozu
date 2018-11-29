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

        public int AddCar(Car car)
        {
            try
            {
                var serie = _dbContext.Series.FirstOrDefault(x => x.SerieId == car.Serie.SerieId);
                if (serie == null) serie = car.Serie;
                car.Serie = serie;
                var owner = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == car.Owner.SubjectId);
                if (owner == null) owner = car.Owner;
                car.Owner = owner;
                var resp = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == car.ServiceResponsiblePerson.SubjectId);
                if (resp == null) resp = car.ServiceResponsiblePerson;
                car.ServiceResponsiblePerson = resp;
                _dbContext.Cars.Add(car);
                _dbContext.SaveChanges();

                return car.CarId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Fault AddFaultToCar(Fault fault, int carId)
        {
            var car = GetById(carId);
            if (car == null) return null;
            car.Faults.Add(fault);
            _dbContext.SaveChanges();
            return fault;
        }

        public Car DeleteCar(int carId)
        {
            var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == carId);
            if (car == null) return null;
            car.State = State.Excluded;
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
                ed.RevisionPeriod = car.RevisionPeriod;
                ed.Revisions = car.Revisions;
                ed.State = car.State;

                var serie = _dbContext.Series.FirstOrDefault(x => x.SerieId == car.Serie.SerieId);
                if (serie == null) serie = car.Serie;
                ed.Serie = serie;
                var owner = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == car.Owner.SubjectId);
                if (owner == null) owner = car.Owner;
                ed.Owner = owner;
                var resp = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == car.ServiceResponsiblePerson.SubjectId);
                if (resp == null) resp = car.ServiceResponsiblePerson;
                ed.ServiceResponsiblePerson = resp;

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

        public List<Serie> GetAllSeries()
        {
            return _dbContext.Series.ToList();
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
