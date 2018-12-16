using Data;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/revision")]
    public class RevisionController : MyApiController, IDefaultMethods<Revision>
    {
        private IRevisionRepository _revisionRepository;

        public RevisionController(IAuth auth, IRevisionRepository revisionRepository) : base(auth)
        {
            _revisionRepository = revisionRepository;
        }

        [HttpPost]
        public int Add([FromBody] Revision data)
        {
            return _revisionRepository.AddRevision(data);
        }

        public void Delete(int id)
        {
            _revisionRepository.DeleteRevision(id);
        }

        public void Edit(int id, Revision data)
        {
            data.RevisionId = id;
            _revisionRepository.EditRevision(data);
        }

        public Revision Get(int id)
        {
            return _revisionRepository.GetById(id);
        }

        public List<Revision> Get(int skip, int count)
        {
           return _revisionRepository.GetForPages(skip, count);
        }

        [HttpGet]
        [Route("{id}/revisions")]
        public List<Revision> GetCarRevisions(int id)
        {
            return _revisionRepository.GetCarRevision(id);
        }

        public List<Revision> Get(string query)
        {
            throw new NotImplementedException();
        }
    }
}