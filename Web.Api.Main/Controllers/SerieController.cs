using Data;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Api.Main.Servicies;

namespace Web.Api.Main.Controllers
{
    [RoutePrefix("api/serie")]
    public class SerieController : MyApiController, IDefaultMethods<Serie>
    {
        private ISerieRepository _serieRepository;

        public SerieController(IAuth auth, ISerieRepository serieRepository) : base(auth)
        {
            _serieRepository = serieRepository;
        }
        [HttpPost]
        public int Add(Serie data)
        {
            return _serieRepository.AddSerie(data);
        }

        public void Edit(int id, Serie data)
        {
            data.SerieId = id;
            _serieRepository.EditSerie(data);
        }

        public Serie Get(int id)
        {
            return _serieRepository.GetSerie(id);
        }

        public void Delete(int id)
        {
            _serieRepository.DeleteSerie(id);
        }

        public List<Serie> Get(int skip, int count)
        {
            return _serieRepository.GetForPages(skip, count);
        }

        public List<Serie> Get(string query)
        {
            return _serieRepository.FindSeries(query);
        }
    }
}