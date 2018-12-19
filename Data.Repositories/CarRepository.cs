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
        private CarHistoryService _historyService;

        public CarRepository(ENVCtx dbContext, CarHistoryService historyService)
        {
            _dbContext = dbContext;
            _historyService = historyService;
        }

        public int AddCar(Car car)
        {
            try
            {
                if (car.Serie != null)
                {
                    var serie = _dbContext.Series.FirstOrDefault(x => x.SerieId == car.Serie.SerieId);
                    if (serie == null) serie = car.Serie;
                    car.Serie = serie;
                }
                if (car.Owner != null)
                {
                    var owner = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == car.Owner.SubjectId);
                    if (owner == null) owner = car.Owner;
                    car.Owner = owner;
                }
                if (car.ServiceResponsiblePerson != null)
                {
                    var resp = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == car.ServiceResponsiblePerson.SubjectId);
                    if (resp == null) resp = car.ServiceResponsiblePerson;
                    car.ServiceResponsiblePerson = resp;
                }
                car.State = State.New;
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
            if (car == null) return null;
            var ed = _dbContext.Cars.FirstOrDefault(x => x.CarId == car.CarId);
            if (ed != null)
            {
                var carBefore = new Car(ed.Name, ed.LastRevision, ed.RevisionPeriod, ed.LastZTE, ed.LastZTL, ed.Certification, ed.State,
                    ed.GoodGroup, ed.Serie, ed.ServiceResponsiblePerson, ed.Owner);
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
                ed.HandBreakWeight = car.HandBreakWeight;
                ed.AirBreakWeight = car.AirBreakWeight;

                if (car.Serie != null)
                {
                    var serie = _dbContext.Series.FirstOrDefault(x => x.SerieId == car.Serie.SerieId);
                    if (serie == null) serie = car.Serie;
                    ed.Serie = serie;
                }
                if (car.Owner != null)
                {
                    var owner = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == car.Owner.SubjectId);
                    if (owner == null) owner = car.Owner;
                    ed.Owner = owner;
                }
                if (car.ServiceResponsiblePerson != null)
                {
                    var resp = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == car.ServiceResponsiblePerson.SubjectId);
                    if (resp == null) resp = car.ServiceResponsiblePerson;
                    ed.ServiceResponsiblePerson = resp;
                }

                _historyService.SaveHistory(carBefore, ed, _dbContext);
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

        public List<Car> FindCars(string query)
        {
            if (int.TryParse(query, out int result))
            {
                return _dbContext.Cars.Where(x => x.RevisionPeriod == result).ToList();
            }
            if (DateTime.TryParse(query, out DateTime resultDate))
            {
                return _dbContext.Cars.Where(x => x.LastRevision == resultDate || x.LastZTE == resultDate || x.LastZTL == resultDate).ToList();
            }

            return _dbContext.Cars.Where(x => x.Name.Contains(query)).ToList();
        }

        public List<Car> GetAll()
        {
            return _dbContext.Cars.Where(x => x.State == State.New).ToList();
        }

        public List<Serie> GetAllSeries(int start, int end)
        {
            return _dbContext.Series.OrderBy(x => x.SerieId).Skip(start).Take(end).ToList();
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

        public List<Car> GetDashboard()
        {
            return _dbContext.Cars.OrderBy(x => x.LastRevision.Value).ThenBy(x=>x.RevisionPeriod).Take(10).ToList();
        }

        public List<Car> GetForPages(int start, int end)
        {
            return GetAll().OrderBy(x => x.CarId).Skip(start).Take(end).ToList();
        }
    }
}
