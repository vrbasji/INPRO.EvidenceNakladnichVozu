using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IGoodGroupRepository
    {
        GoodGroup GetById(int id);

        int AddGoodGroup(GoodGroup good);

        GoodGroup EditGoodGroup(GoodGroup good);

        GoodGroup DeleteGoodGroup(int id);

        List<GoodGroup> GetForPages(int start, int end);

        List<GoodGroup> FindGoodGroups(string query);
    }
}
