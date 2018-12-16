using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FaultRepository : IFaultRepository
    {
        private ENVCtx _dbContext;

        public FaultRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddFault(Fault fault)
        {
            try
            {
                if (fault.Car != null)
                {
                    var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == fault.Car.CarId);
                    if (car != null)
                        fault.Car = car;
                }
                fault.Repairs = new List<Repair>();
                _dbContext.Faults.Add(fault);
                _dbContext.SaveChanges();

                return fault.FaultId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Fault DeleteFault(int id)
        {
            var fault = GetById(id);
            if (fault == null) return null;
            _dbContext.Faults.Remove(fault);
            _dbContext.SaveChanges();
            return fault;
        }

        public Fault EditFault(Fault fault)
        {
            if (fault == null) return null;
            var ed = GetById(fault.FaultId);
            if (ed != null)
            {
                ed.Description = fault.Description;
                ed.FoundedDate = fault.FoundedDate;
                ed.Name = fault.Name;

                if (fault.Car != null)
                {
                    var car = _dbContext.Cars.FirstOrDefault(x => x.CarId == fault.Car.CarId);
                    if (car == null) return null;
                    ed.Car = car;
                }
                if (fault.Repairs != null)
                {
                    var repairs = _dbContext.Repairs.Where(x => x.Fault.FaultId == fault.FaultId).ToList();
                    if (repairs != null)
                        ed.Repairs = repairs;
                }

                _dbContext.SaveChanges();
            }
            return ed;
        }

        public Fault GetById(int id)
        {
            return _dbContext.Faults.FirstOrDefault(x => x.FaultId == id);
        }

        public List<Fault> GetCarFaults(int carId)
        {
            return _dbContext.Faults.Where(x => x.Car.CarId == carId).OrderBy(x=>x.FoundedDate).ToList();
        }

        public List<Fault> GetForPages(int start, int end)
        {
            return _dbContext.Faults.OrderBy(x => x.FaultId).Skip(start).Take(end).ToList();
        }
    }
}
