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
    [RoutePrefix("api/goodgroup")]
    public class GoodGroupController : MyApiController, IDefaultMethods<GoodGroup>
    {
        private IGoodGroupRepository _repository;

        public GoodGroupController(IAuth auth, IGoodGroupRepository repository) : base(auth)
        {
            _repository = repository;
        }
        [HttpPost]
        public int Add([FromBody] GoodGroup data)
        {
            return _repository.AddGoodGroup(data);
        }

        public void Delete(int id)
        {
            _repository.DeleteGoodGroup(id);
        }

        public void Edit(int id, GoodGroup data)
        {
            data.GoodGroupId = id;
            _repository.EditGoodGroup(data);
        }

        public GoodGroup Get(int id)
        {
            return _repository.GetById(id);
        }

        public List<GoodGroup> Get(int skip, int count)
        {
            return _repository.GetForPages(skip, count);
        }

        public List<GoodGroup> Get(string query)
        {
            return _repository.FindGoodGroups(query);
        }
    }
}
