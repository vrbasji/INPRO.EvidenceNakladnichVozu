using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IRevisionRepository
    {
        Revision GetById(int revisionId);

        List<Revision> GetCarRevision(int carId);

        int AddRevision(Revision revision);

        Revision EditRevision(Revision revision);

        Revision DeleteRevision(int revisionId);

        List<Revision> GetForPages(int start, int end);
    }
}
