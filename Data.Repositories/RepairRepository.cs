using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RepairRepository : IRepairRepository
    {
        private ENVCtx _dbContext;

        public RepairRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddRepair(Repair repair)
        {
            try
            {
                if (repair.Fault != null)
                {
                    var fault = _dbContext.Faults.FirstOrDefault(x => x.FaultId == repair.Fault.FaultId);
                    if (fault != null)
                        repair.Fault = fault;
                }
                
                _dbContext.Repairs.Add(repair);
                _dbContext.SaveChanges();

                return repair.RepairId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Repair DeleteRepair(int repairId)
        {
            var repair = _dbContext.Repairs.FirstOrDefault(x => x.RepairId == repairId);
            if (repair == null) return null;
            _dbContext.Repairs.Remove(repair);
            _dbContext.SaveChanges();
            return repair;
        }

        public Repair EditRepair(Repair repair)
        {
            if (repair == null) return null;
            var ed = _dbContext.Repairs.FirstOrDefault(x => x.RepairId == repair.RepairId);
            if (ed != null)
            {
                ed.Description = repair.Description;
                ed.RepairDate = repair.RepairDate;

                if (repair.Fault != null)
                {
                    var fault = _dbContext.Faults.FirstOrDefault(x => x.FaultId == repair.Fault.FaultId);
                    if (fault == null) fault = repair.Fault;
                    ed.Fault = fault;
                }
                _dbContext.SaveChanges();
            }
            return ed;
        }

        public Repair GetById(int repairId)
        {
            return _dbContext.Repairs.FirstOrDefault(x => x.RepairId == repairId);
        }

        public List<Repair> GetCarRepair(int carId)
        {
            return _dbContext.Repairs.Where(x => x.Fault.Car.CarId == carId).OrderBy(x=>x.RepairDate).ToList();
        }

        public List<Repair> GetForPages(int start, int end)
        {
            return _dbContext.Repairs.OrderBy(x => x.RepairDate).Skip(start).Take(end).ToList();
        }
    }
}
