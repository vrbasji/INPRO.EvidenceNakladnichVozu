using Data.Database;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class SerieRepository : ISerieRepository
    {
        private ENVCtx _dbContext;

        public SerieRepository(ENVCtx dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddSerie(Serie serie)
        {
            try
            {
                serie.AirBreak = _dbContext.AirBreaks.FirstOrDefault(x => x.AirBreakId == serie.AirBreak.AirBreakId);
                serie.HandBreak = _dbContext.HandBreaks.FirstOrDefault(x => x.HandBreakId == serie.HandBreak.HandBreakId);
                _dbContext.Series.Add(serie);
                _dbContext.SaveChanges();
                return serie.SerieId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void DeleteSerie(int serieId)
        {
            var serie = GetSerie(serieId);
            if (serie == null)
                throw new Exception("Serie with id " + serie.SerieId + " was not found.");
            _dbContext.Series.Remove(serie);
            _dbContext.SaveChanges();
        }

        public void EditSerie(Serie serie)
        {
            var ed = GetSerie(serie.SerieId);
            if (ed == null)
                throw new Exception("Serie with id " + serie.SerieId + " was not found.");
            if (ed != null)
            {
                ed.AirBreak = _dbContext.AirBreaks.FirstOrDefault(x => x.AirBreakId == serie.AirBreak.AirBreakId);
                ed.HandBreak = _dbContext.HandBreaks.FirstOrDefault(x => x.HandBreakId == serie.HandBreak.HandBreakId);
                ed.Area = serie.Area;
                ed.AreaHeight = serie.AreaHeight;
                ed.BumpersLenght = serie.BumpersLenght;
                ed.Cars = _dbContext.Cars.Where(x=>x.Serie.SerieId == serie.SerieId).ToList();
                ed.ConstructionWeight = serie.ConstructionWeight;
                ed.Lenght = serie.Lenght;
                ed.MaxSpeed = serie.MaxSpeed;
                ed.NumberOfAxle = serie.NumberOfAxle;
                ed.NumberOfGear = serie.NumberOfGear;
                ed.NumberOfPars = serie.NumberOfPars;
                ed.Space = serie.Space;
                ed.Weight = serie.Weight;
                ed.Wheelbase = serie.Wheelbase;
                _dbContext.SaveChanges();
            }
        }

        public List<Serie> FindSeries(string query)
        {
            if(int.TryParse(query, out int intResult))
            {
                return _dbContext.Series
                    .Where(x => x.Area == intResult || x.BumpersLenght == intResult || 
                    x.Lenght == intResult || x.NumberOfAxle == intResult || 
                    x.NumberOfGear == intResult || x.NumberOfPars == intResult)
                    .ToList();
            }
            if (float.TryParse(query, out float floatResult))
            {
                return _dbContext.Series
                    .Where(x => x.AreaHeight == floatResult || x.ConstructionWeight == floatResult ||
                    x.MaxSpeed == floatResult || x.Space == floatResult ||
                    x.Weight == floatResult || x.Wheelbase == floatResult)
                    .ToList();
            }
            return null;
        }

        public List<Serie> GetForPages(int skip, int count)
        {
            return _dbContext.Series
                .OrderBy(x => x.SerieId)
                .Skip(skip)
                .Take(count)
                .ToList();
        }

        public Serie GetSerie(int serieId)
        {
            return _dbContext.Series
                .FirstOrDefault(x => x.SerieId == serieId);
        }
    }
}
