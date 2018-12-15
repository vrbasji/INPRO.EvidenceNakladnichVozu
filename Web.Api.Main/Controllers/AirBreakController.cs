using Data;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/airbreak")]
    public class AirBreakController : MyApiController, IDefaultMethods<AirBreak>
    {
        private IBreakRepository _breakRepository;

        public AirBreakController(IAuth auth, IBreakRepository breakRepository) : base(auth)
        {
            _breakRepository = breakRepository;
        }
        [System.Web.Http.HttpPost]
        public int Add([FromBody] AirBreak data)
        {
            return _breakRepository.AddAirBreak(data);
        }

        public void Delete(int id)
        {
            _breakRepository.DeleteAirBreak(id);
        }

        public void Edit(int id, AirBreak data)
        {
            data.AirBreakId = id;
            _breakRepository.EditAirBreak(data);
        }

        public AirBreak Get(int id)
        {
            return _breakRepository.GetAirBreak(id);
        }

        public List<AirBreak> Get(int skip, int count)
        {
            return _breakRepository.GetAirBreakForPages(skip, count);
        }

        public List<AirBreak> Get(string query)
        {
            return _breakRepository.FindAirBreak(query);
        }
    }
}