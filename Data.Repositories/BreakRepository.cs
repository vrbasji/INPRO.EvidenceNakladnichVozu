using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class BreakRepository : IBreakRepository
    {
        private ENVCtx _dbContext;

        public BreakRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddAirBreak(AirBreak airBrake)
        {
            try
            {
                _dbContext.AirBreaks.Add(airBrake);
                _dbContext.SaveChanges();
                return airBrake.AirBreakId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int AddHandBreak(HandBreak handBreak)
        {
            try
            {
                _dbContext.HandBreaks.Add(handBreak);
                _dbContext.SaveChanges();
                return handBreak.HandBreakId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void DeleteAirBreak(int airBrakeId)
        {
            var pomBreak = GetAirBreak(airBrakeId);
            if (pomBreak == null)
                throw new Exception("Break with id " + pomBreak.AirBreakId + " was not found.");
            _dbContext.AirBreaks.Remove(pomBreak);
            _dbContext.SaveChanges();
        }

        public void DeletHandBreak(int handBreakId)
        {
            var pomBreak = GetHandBreak(handBreakId);
            if (pomBreak == null)
                throw new Exception("Break with id " + handBreakId + " was not found.");
            _dbContext.HandBreaks.Remove(pomBreak);
            _dbContext.SaveChanges();
        }

        public void EditAirBreak(AirBreak airBrake)
        {
            var ed = GetAirBreak(airBrake.AirBreakId);
            if (ed == null)
                throw new Exception("Brake with id " + airBrake.AirBreakId + " was not found.");
            if (ed != null)
            {
                ed.AirBreakWeight = airBrake.AirBreakWeight;
                ed.Name = airBrake.Name;
                _dbContext.SaveChanges();
            }
        }

        public void EditHandBreak(HandBreak handBreak)
        {
            var ed = GetHandBreak(handBreak.HandBreakId);
            if (ed == null)
                throw new Exception("Brake with id " + handBreak.HandBreakId + " was not found.");
            if (ed != null)
            {
                ed.HandBreakWeight = handBreak.HandBreakWeight;
                ed.Name = handBreak.Name;
                _dbContext.SaveChanges();
            }
        }

        public List<AirBreak> FindAirBreak(string query)
        {
            if (int.TryParse(query, out int result))
            {
                return _dbContext.AirBreaks
                    .Where(x => x.Name.Contains(query) || x.AirBreakWeight == result)
                    .ToList();
            }
            else
            {
                return _dbContext.AirBreaks
                    .Where(x => x.Name.Contains(query))
                    .ToList();
            }
        }

        public List<HandBreak> FindHandBreaks(string query)
        {
            if (int.TryParse(query, out int result))
            {
                return _dbContext.HandBreaks
                    .Where(x => x.Name.Contains(query) || x.HandBreakWeight == result)
                    .ToList();
            }
            else
            {
                return _dbContext.HandBreaks
                    .Where(x => x.Name.Contains(query))
                    .ToList();
            }
        }

        public AirBreak GetAirBreak(int airBrakeId)
        {
            return _dbContext.AirBreaks.FirstOrDefault(x => x.AirBreakId == airBrakeId);
        }

        public List<AirBreak> GetAirBreakForPages(int skip, int count)
        {
            return _dbContext.AirBreaks
                .OrderBy(x => x.AirBreakId)
                .Skip(skip)
                .Take(count)
                .ToList();
        }

        public List<HandBreak> GetHandBrakeForPages(int skip, int count)
        {
            return _dbContext.HandBreaks
                .OrderBy(x => x.HandBreakId)
                .Skip(skip)
                .Take(count)
                .ToList();
        }

        public HandBreak GetHandBreak(int handBreakId)
        {
            return _dbContext.HandBreaks.FirstOrDefault(x => x.HandBreakId == handBreakId);
        }
    }
}
