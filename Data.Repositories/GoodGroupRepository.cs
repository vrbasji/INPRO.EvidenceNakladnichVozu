using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GoodGroupRepository : IGoodGroupRepository
    {
        private ENVCtx _dbContext;

        public GoodGroupRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddGoodGroup(GoodGroup good)
        {
            try
            {
                _dbContext.GoodGroups.Add(good);
                _dbContext.SaveChanges();

                return good.GoodGroupId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public GoodGroup DeleteGoodGroup(int id)
        {
            var good = GetById(id);
            _dbContext.GoodGroups.Remove(good);
            _dbContext.SaveChanges();
            return good;
        }

        public GoodGroup EditGoodGroup(GoodGroup good)
        {
            if (good == null) return null;
            var ed = _dbContext.GoodGroups.FirstOrDefault(x => x.GoodGroupId == good.GoodGroupId);
            if (ed != null)
            {
                ed.Name = good.Name;

                _dbContext.SaveChanges();
            }
            return ed;
        }

        public List<GoodGroup> FindGoodGroups(string query)
        {
            return _dbContext.GoodGroups.Where(x => x.Name.Contains(query)).OrderBy(x => x.Name).ToList();
        }

        public GoodGroup GetById(int id)
        {
            return _dbContext.GoodGroups.FirstOrDefault(x=>x.GoodGroupId == id);
        }

        public List<GoodGroup> GetForPages(int start, int end)
        {
            return _dbContext.GoodGroups.OrderBy(x => x.GoodGroupId).Skip(start).Take(end).ToList();
        }
    }
}
