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

        [SwaggerOperation("GetCarServiceHistory")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public List<Revision> GetCarServiceHistory(int id)
        {
            return _carRepository.GetCarRevision(id);
        }

        [SwaggerOperation("GetAllCars")]
        public List<Car> GetAllCars()
        {
            return _carRepository.GetAll();
        }

        [SwaggerOperation("ArchiveCar")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public bool ArchiveCar([FromBody]Car car)
        {
            return _carRepository.ExcludeCar(car.CarId);
        }

        [SwaggerOperation("AddCar")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public bool AddCar([FromBody]Car car)
        {
            return _carRepository.AddCar(car);
        }
        [SwaggerOperation("GetCarState")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public Car GetCarState(int id)
        {
            return _carRepository.GetById(id);
        }
    }
}