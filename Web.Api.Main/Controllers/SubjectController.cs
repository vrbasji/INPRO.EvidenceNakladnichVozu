using Data;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/subject")]
    public class SubjectController : MyApiController, IDefaultMethods<Subject>
    {
        private ISubjectRepository _subjectRepository;

        public SubjectController(IAuth auth, ISubjectRepository subjectRepository) : base(auth)
        {
            _subjectRepository = subjectRepository;
        }
        [Route]
        public int Add(Subject data)
        {
            return _subjectRepository.AddSubject(data);
        }
        [Route]
        [HttpPut]
        public void Edit(Subject data)
        {
            _subjectRepository.EditSubject(data);
        }

        [Route("{id}")]
        public Subject Get(int id)
        {
            return _subjectRepository.GetSubject(id);
        }

        [Route("{id}")]
        public void Delete(int id)
        {
            _subjectRepository.DeleteSubject(id);
        }

        [Route("{skip}/{count}")]
        public List<Subject> Get(int skip, int count)
        {
            return _subjectRepository.GetForPages(skip, count);
        }

        [Route("{query}")]
        public List<Subject> Get(string query)
        {
            return _subjectRepository.FindSubjects(query);
        }
    }
}