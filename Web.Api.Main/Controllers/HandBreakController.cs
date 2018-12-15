using Data;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/handbreak")]
    public class HandBreakController : MyApiController, IDefaultMethods<HandBreak>
    {
        private IBreakRepository _breakRepository;

        public HandBreakController(IAuth auth, IBreakRepository breakRepository) : base(auth)
        {
            _breakRepository = breakRepository;
        }
        [HttpPost]
        public int Add(HandBreak data)
        {
            return _breakRepository.AddHandBreak(data);
        }

        public void Delete(int id)
        {
            _breakRepository.DeletHandBreak(id);
        }

        public void Edit(int id, HandBreak data)
        {
            data.HandBreakId = id;
            _breakRepository.EditHandBreak(data);
        }

        public HandBreak Get(int id)
        {
            return _breakRepository.GetHandBreak(id);
        }

        public List<HandBreak> Get(int skip, int count)
        {
            return _breakRepository.GetHandBrakeForPages(skip,count);
        }

        public List<HandBreak> Get(string query)
        {
            return _breakRepository.FindHandBreaks(query);
        }
    }
}