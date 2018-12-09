using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private ENVCtx _dbContext;

        public SubjectRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddSubject(Subject subject)
        {
            try
            {
                _dbContext.Subjects.Add(subject);
                _dbContext.SaveChanges();
                return subject.SubjectId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void DeleteSubject(int subjectId)
        {
            var subject = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == subjectId);
            if (subject == null)
                throw new Exception("Subject with id " + subjectId + " was not found.");
            _dbContext.Subjects.Remove(subject);
            _dbContext.SaveChanges();
        }

        public void EditSubject(Subject subject)
        {
            var ed = GetSubject(subject.SubjectId);
            if (ed == null)
                throw new Exception("Subject with id " + subject.SubjectId + " was not found.");
            if (ed != null)
            {
                ed.Email = subject.Email;
                ed.ICO = subject.ICO;
                ed.Name = subject.Name;
                ed.OwnedCars = _dbContext.Cars.Where(x => x.Owner.SubjectId == subject.SubjectId).ToList();
                ed.RentedCars = _dbContext.Rents.Where(x => x.Subject.SubjectId == subject.SubjectId).ToList();
                ed.Telephone = subject.Telephone;
                _dbContext.SaveChanges();
            }
        }

        public List<Subject> FindSubjects(string query)
        {
            return _dbContext.Subjects
                    .Where(x => x.Email.Contains(query) || x.ICO.Contains(query) || x.Name.Contains(query) || x.Telephone.Contains(query))
                    .ToList();
        }

        public List<Subject> GetForPages(int skip, int count)
        {
            return _dbContext.Subjects
                    .OrderBy(x => x.SubjectId)
                    .Skip(skip)
                    .Take(count)
                    .ToList();
        }

        public Subject GetSubject(int subjectId)
        {
            return _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == subjectId);
        }
    }
}
