using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IRentRepository
    {
        Rent GetById(int id);

        List<Rent> GetRentBySubject(int id);

        Rent MakeRent(int carId, int subjectId, DateTime dueDay);

        Rent GetRent(int carId, int subjectId, DateTime dueDay);

        List<Rent> GetForPages(int start, int end);
    }
}
