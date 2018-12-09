using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface IBreakRepository
    {
        HandBreak GetHandBreak(int handBreakId);

        int AddHandBreak(HandBreak handBreak);

        void EditHandBreak(HandBreak handBreak);

        void DeletHandBreak(int breakreakId);

        List<HandBreak> GetHandBrakeForPages(int skip, int count);

        List<HandBreak> FindHandBreaks(string query);


        AirBreak GetAirBreak(int airBrakeId);

        int AddAirBreak(AirBreak airBrake);

        void EditAirBreak(AirBreak airBrake);

        void DeleteAirBreak(int airBrakeId);

        List<AirBreak> GetAirBreakForPages(int skip, int count);

        List<AirBreak> FindAirBreak(string query);
    }
}
