using Data;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Api.Main.Models;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/rent")]
    public class RentController : MyApiController, IDefaultMethods<Rent>
    {
        private IRentRepository _repository;
        public RentController(IAuth auth, IRentRepository repository) : base(auth)
        {
            _repository = repository;
        }

        [HttpPost]
        public int Add([FromBody] Rent data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id, Rent data)
        {
            throw new NotImplementedException();
        }

        public Rent Get(int id)
        {
            return _repository.GetById(id);
        }
        [HttpPost]
        [Route("rentto")]
        public Rent MakeRent([FromBody] RentModel rent)
        {
            return _repository.MakeRent(rent.CarId, rent.SubjectId, rent.DueDate);
        }
        [HttpPost]
        [Route("rentfrom")]
        public Rent GetRent([FromBody] RentModel rent)
        {
            return _repository.GetRent(rent.CarId, rent.SubjectId, rent.DueDate);
        }
        [HttpGet]
        [Route("forsubject/{id}")]
        public List<Rent> GetBySubjectId(int id)
        {
            return _repository.GetRentBySubject(id);
        }

        public List<Rent> Get(int skip, int count)
        {
            return _repository.GetForPages(skip, count);
        }

        public List<Rent> Get(string query)
        {
            throw new NotImplementedException();
        }
    }
}
