using Data;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/fault")]
    public class FaultController : MyApiController, IDefaultMethods<Fault>
    {
        private IFaultRepository _repository;
        public FaultController(IAuth auth, IFaultRepository repository) : base(auth)
        {
            _repository = repository;
        }
        [HttpPost]
        public int Add([FromBody] Fault data)
        {
            return _repository.AddFault(data);
        }

        public void Delete(int id)
        {
            _repository.DeleteFault(id);
        }

        public void Edit(int id, Fault data)
        {
            data.FaultId = id;
            _repository.EditFault(data);
        }

        public Fault Get(int id)
        {
            return _repository.GetById(id);
        }

        public List<Fault> Get(int skip, int count)
        {
            return _repository.GetForPages(skip, count);
        }
        [HttpGet]
        [Route("car_faults/{id}")]
        public List<Fault> GetCarFaults(int id)
        {
            return _repository.GetCarFaults(id);
        }

        public List<Fault> Get(string query)
        {
            throw new NotImplementedException();
        }
    }
}
