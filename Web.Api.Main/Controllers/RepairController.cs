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
    [RoutePrefix("api/repair")]
    public class RepairController : MyApiController, IDefaultMethods<Repair>
    {
        private IRepairRepository _repository;
        public RepairController(IAuth auth, IRepairRepository repository) : base(auth)
        {
            _repository = repository;
        }
        [HttpPost]
        public int Add([FromBody] Repair data)
        {
            return _repository.AddRepair(data);
        }

        public void Delete(int id)
        {
            _repository.DeleteRepair(id);
        }

        public void Edit(int id, Repair data)
        {
            data.RepairId = id;
            _repository.EditRepair(data);
        }

        public Repair Get(int id)
        {
            return _repository.GetById(id);
        }

        public List<Repair> Get(int skip, int count)
        {
            return _repository.GetForPages(skip, count);
        }
        [HttpGet]
        [Route("{id}/repair")]
        public List<Repair> GetCarRepairs(int id)
        {
            return _repository.GetCarRepair(id);
        }

        public List<Repair> Get(string query)
        {
            throw new NotImplementedException();
        }
    }
}
