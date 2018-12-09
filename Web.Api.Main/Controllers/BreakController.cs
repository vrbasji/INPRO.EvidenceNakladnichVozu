using Data;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/break")]
    public class BreakController : MyApiController, IDefaultMethods<HandBreak>, IDefaultMethods<AirBreak>
    {
        private IBreakRepository _breakRepository;

        public BreakController(IAuth auth, IBreakRepository breakRepository) : base(auth)
        {
            _breakRepository = breakRepository;
        }
        [Route("handbreak")]
        public int Add(HandBreak data)
        {
            return _breakRepository.AddHandBreak(data);
        }
        [Route("airbreak")]
        public int Add(AirBreak data)
        {
            return _breakRepository.AddAirBreak(data);
        }
        [Route("airbreak/{id}")]
        public void Delete(int id)
        {
            _breakRepository.DeletHandBreak(id);
        }
        [Route("airbreak/{id}")]
        [HttpDelete]
        public void DeleteAirBreak(int id)
        {
            _breakRepository.DeleteAirBreak(id);
        }
        [Route("handbreak")]
        [HttpPut]
        public void Edit(HandBreak data)
        {
            _breakRepository.EditHandBreak(data);
        }
        [Route("airbreak")]
        [HttpPut]
        public void Edit(AirBreak data)
        {
            _breakRepository.EditAirBreak(data);
        }
        [Route("handbreak/{id}")]
        public HandBreak Get(int id)
        {
            return _breakRepository.GetHandBreak(id);
        }
        [Route("handbreak/{skip}/{count}")]
        public List<HandBreak> Get(int skip, int count)
        {
            return _breakRepository.GetHandBrakeForPages(skip,count);
        }
        [Route("handbreak/{query}")]
        public List<HandBreak> Get(string query)
        {
            return _breakRepository.FindHandBreaks(query);
        }

        [Route("airbreak/{id}")]
        AirBreak IDefaultMethods<AirBreak>.Get(int id)
        {
            return _breakRepository.GetAirBreak(id);
        }
        [Route("airbreak/{skip}/{count}")]
        List<AirBreak> IDefaultMethods<AirBreak>.Get(int skip, int count)
        {
            return _breakRepository.GetAirBreakForPages(skip, count);
        }
        [Route("airbreak/{query}")]
        List<AirBreak> IDefaultMethods<AirBreak>.Get(string query)
        {
            return _breakRepository.FindAirBreak(query);
        }
    }
}