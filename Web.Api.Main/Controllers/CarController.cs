using Data;
using Data.Repositories.Interfaces;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    public class CarController : MyApiController
    {
        private ICarRepository _carRepository;
        public CarController(IAuth auth, ICarRepository carRepository) : base(auth)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        [Route("api/car/{startId}/{endId}")]
        public IEnumerable<Car> GetAllPages(int startId, int endId)
        {
            return _carRepository.GetForPages(startId, endId);
        }
        [HttpGet]
        [Route("api/car/{id}")]
        public Car GetCar(int id)
        {
            return _carRepository.GetById(id);
        }
        [HttpGet]
        [Route("api/serie/{startId}/{endId}")]
        public List<Serie> GetSeries(int startId, int endId)
        {
            return _carRepository.GetAllSeries(startId, endId);
        }

        [HttpGet]
        [Route("api/servicehistory/{carId}")]
        public List<Revision> GetCarServiceHistory(int carId)
        {
            return _carRepository.GetCarRevision(carId);
        }

        [HttpPost]
        [Route("api/car")]
        public int AddCar([FromBody]Car car)
        {
            return _carRepository.AddCar(car);
        }

        [HttpPut]
        [Route("api/car")]
        public Car EditCar([FromBody]Car car)
        {
            return _carRepository.EditCar(car);
        }
        [HttpDelete]
        [Route("api/car/{id}")]
        public Car DeleteCar(int id)
        {
            return _carRepository.DeleteCar(id);
        }
    }
}