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

        Rent MakeRent(Rent rent);

        Rent GetRent(Rent rent);

        List<Rent> GetForPages(int start, int end);
    }
}
