using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface ISerieRepository
    {
        Serie GetSerie(int serieId);

        int AddSerie(Serie serie);

        void EditSerie(Serie serie);

        void DeleteSerie(int serieId);

        List<Serie> GetForPages(int skip, int count);

        List<Serie> FindSeries(string query);
    }
}
