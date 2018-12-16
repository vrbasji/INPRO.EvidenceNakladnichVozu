using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IRepairRepository
    {
        Repair GetById(int repairId);

        List<Repair> GetCarRepair(int carId);

        int AddRepair(Repair repair);

        Repair EditRepair(Repair repair);

        Repair DeleteRepair(int repairId);

        List<Repair> GetForPages(int start, int end);
    }
}
