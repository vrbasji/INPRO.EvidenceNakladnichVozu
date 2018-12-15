using Data;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using Web.Api.Main.Services;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/car")]
    public class CarController : MyApiController, IDefaultMethods<Car>
    {
        private ICarRepository _carRepository;
        private ICarHistoryRepository _carHistoryRepository;
        public CarController(IAuth auth, ICarRepository carRepository, ICarHistoryRepository carHistoryRepository) : base(auth)
        {
            _carRepository = carRepository;
            _carHistoryRepository = carHistoryRepository;
        }
        [HttpPost]
        public int Add(Car data)
        {
            return _carRepository.AddCar(data);
        }

        public void Edit(int id, Car data)
        {
            data.CarId = id;
            _carRepository.EditCar(data);
        }

        public Car Get(int id)
        {
            return _carRepository.GetById(id);
        }

        public void Delete(int id)
        {
            _carRepository.DeleteCar(id);
        }

        public List<Car> Get(int skip, int count)
        {
            return _carRepository.GetForPages(skip, count);
        }

        public List<Car> Get(string query)
        {
            return _carRepository.FindCars(query);
        }

        public List<ChangeHistory> GetHistory(int id, int skip, int count)
        {
            return _carHistoryRepository.GetChangeHistories(id, skip, count);
        }
    }
}