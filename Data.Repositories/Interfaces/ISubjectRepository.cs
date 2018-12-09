using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Subject GetSubject(int subjectId);

        int AddSubject(Subject subject);

        void EditSubject(Subject subject);

        void DeleteSubject(int subjectId);

        List<Subject> GetForPages(int skip, int count);

        List<Subject> FindSubjects(string query);
    }
}
