using Data.Database;
using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface ICarHistoryRepository
    {
        List<ChangeHistory> GetChangeHistories(int carId, int skip, int count);

        void AddChangeHistory(ChangeHistory history);
        void AddChangeHistories(List<ChangeHistory> histories, ENVCtx ctx);
    }
}
