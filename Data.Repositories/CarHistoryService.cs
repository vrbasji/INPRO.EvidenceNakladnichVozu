using Data;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Repositories
{
    public class CarHistoryService
    {
        private ICarHistoryRepository _carHistoryRepository;
        public CarHistoryService(ICarHistoryRepository carHistoryRepository)
        {
            _carHistoryRepository = carHistoryRepository;
        }
        public void SaveHistory(Car before, Car actual)
        {
            //foreach (var change in GetChanges(before,actual))
            //{
            //    var history = new ChangeHistory()
            //    {
            //        Car = actual,
            //        ChangeDate = DateTime.Now,
            //        NameOfParameter = change.PropertyName,
            //        NewValue = change.ActualValue,
            //        OldValue = change.BeforeValue
            //    };
            //    _carHistoryRepository.AddChangeHistory(history);
            //}
            _carHistoryRepository.AddChangeHistories(
                GetChanges(before, actual).Select(
                    x => new ChangeHistory()
                    {
                        Car = actual,
                        ChangeDate = DateTime.Now,
                        NameOfParameter = x.PropertyName,
                        NewValue = x.ActualValue,
                        OldValue = x.BeforeValue
                    }
                    ).ToList()
                );
        }

        private List<Change> GetChanges(Car before, Car actual)
        {
            var changes = new List<Change>();
            if (before.Certification != actual.Certification)
            {
                changes.Add(new Change("Certification", before.Certification, actual.Certification));
            }
            if (before.GoodGroup != actual.GoodGroup)
            {
                changes.Add(new Change("GoodGroup", before.GoodGroup, actual.GoodGroup));
            }
            if (before.LastRevision != actual.LastRevision)
            {
                changes.Add(new Change("LastRevision", before.LastRevision, actual.LastRevision));
            }
            if (before.LastZTE != actual.LastZTE)
            {
                changes.Add(new Change("LastZTE", before.LastZTE, actual.LastZTE));
            }
            if (before.LastZTL != actual.LastZTL)
            {
                changes.Add(new Change("LastZTL", before.LastZTL, actual.LastZTL));
            }
            if (before.Name != actual.Name)
            {
                changes.Add(new Change("Name", before.Name, actual.Name));
            }
            if (before.RevisionPeriod != actual.RevisionPeriod)
            {
                changes.Add(new Change("RevisionPeriod", before.RevisionPeriod, actual.RevisionPeriod));
            }
            if (before.State != actual.State)
            {
                changes.Add(new Change("State", before.State, actual.State));
            }
            if (before.Faults != actual.Faults && before.Faults != null && actual.Faults != null)
            {
                var changed = before.Faults.Except(actual.Faults);
                foreach (Fault fault in changed)
                {
                    changes.Add(new Change("Fault", null, SerializationHelper<Fault>.Serialize(fault)));
                }
            }
            if (before.Revisions != actual.Revisions && before.Revisions != null && actual.Revisions != null)
            {
                var revisions = before.Revisions.Except(actual.Revisions);
                foreach (Revision revision in revisions)
                {
                    changes.Add(new Change("Revision", null, SerializationHelper<Revision>.Serialize(revision)));
                }
            }
            if (before.Owner != actual.Owner && before.Owner != null && actual.Owner != null)
            {
                changes.Add(new Change("Owner", before.Owner.SubjectId, actual.Owner.SubjectId));
            }
            if (before.Serie != actual.Serie && before.Serie != null && actual.Serie != null)
            {
                changes.Add(new Change("Serie", before.Serie.SerieId, actual.Serie.SerieId));
            }
            if (before.ServiceResponsiblePerson != actual.ServiceResponsiblePerson && before.ServiceResponsiblePerson != null && actual.ServiceResponsiblePerson != null)
            {
                changes.Add(new Change("ServiceResponsiblePerson", before.ServiceResponsiblePerson.SubjectId, actual.ServiceResponsiblePerson.SubjectId));
            }
            return changes;
        }

        private static int GetEntityId<T>(T entity)
        {
            int id = -1;
            Type type = typeof(T);
            if (type == typeof(Car))
                id = (entity as Car).CarId;
            if (type == typeof(Fault))
                id = (entity as Fault).FaultId;
            if (type == typeof(AirBreak))
                id = (entity as AirBreak).AirBreakId;
            if (type == typeof(HandBreak))
                id = (entity as HandBreak).HandBreakId;
            if (type == typeof(Rent))
                id = (entity as Rent).RentId;
            if (type == typeof(Repair))
                id = (entity as Repair).RepairId;
            if (type == typeof(Revision))
                id = (entity as Revision).RevisionId;
            if (type == typeof(Serie))
                id = (entity as Serie).SerieId;
            if (type == typeof(Subject))
                id = (entity as Subject).SubjectId;
            if (type == typeof(User))
                id = (entity as User).UserId;
            return id;
        }

        private class Change
        {
            public string PropertyName { get; private set; }
            public string BeforeValue { get; private set; }
            public string ActualValue { get; private set; }

            public Change(string name, object before, object actual)
            {
                PropertyName = name;
                if (before != null)
                    BeforeValue = before.ToString();
                if (actual != null)
                    ActualValue = actual.ToString();
            }
        }

    }
}