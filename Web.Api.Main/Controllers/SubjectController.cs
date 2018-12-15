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
        [HttpPost]
        public int Add(Subject data)
        {
            return _subjectRepository.AddSubject(data);
        }
        public void Edit(int id, Subject data)
        {
            data.SubjectId = id;
            _subjectRepository.EditSubject(data);
        }

        public Subject Get(int id)
        {
            return _subjectRepository.GetSubject(id);
        }

        public void Delete(int id)
        {
            _subjectRepository.DeleteSubject(id);
        }

        public List<Subject> Get(int skip, int count)
        {
            return _subjectRepository.GetForPages(skip, count);
        }

        public List<Subject> Get(string query)
        {
            return _subjectRepository.FindSubjects(query);
        }
    }
}