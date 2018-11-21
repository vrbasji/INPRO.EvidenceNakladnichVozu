using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICarRepository
    {
        List<Car> GetAll();

        Car GetById(int carId);

        List<Revision> GetCarRevision(int carId);

        bool ExcludeCar(int carId);

        bool AddCar(Car car);
    }
}
