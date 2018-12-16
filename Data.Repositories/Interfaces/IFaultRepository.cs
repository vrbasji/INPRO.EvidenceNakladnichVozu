using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IFaultRepository
    {
        Fault GetById(int id);

        List<Fault> GetCarFaults(int carId);

        int AddFault(Fault fault);

        Fault EditFault(Fault fault);

        Fault DeleteFault(int id);

        List<Fault> GetForPages(int start, int end);
    }
}
