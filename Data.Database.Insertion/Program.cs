using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database.Insertion
{
    class Program
    {
        private static Random rnd = new Random();
        private static int nameCounter = 1;
        private static int paramCounter = 1;
        private static int faultDescCounter = 1;
        private static int repairDesCounter = 1;
        private static int revisionDesCounter = 1;
        private static int airBreakCounter = 1;
        private static int handBreakDesCounter = 1;
        private static int subCounter = 1;
        static void Main(string[] args)
        {
            using (var ctx = new ENVCtx())
            {
                //Generovat subjekty
                for (int i = 0; i < 10; i++)
                {
                    ctx.Subjects.Add(GenOwner());
                }

                //Generovat serie
                for (int i = 0; i < 10; i++)
                {
                    ctx.Series.Add(GenSerie());
                }
                ctx.SaveChanges();
                //Generovat auta
                for (int i = 0; i < 15; i++)
                {
                    var car = GenCar();
                    car.Owner = GetRandomSubject(ctx);
                    car.Owner.OwnedCars.Add(car);
                    car.Serie = GetRandomSerie(ctx);
                    car.ServiceResponsiblePerson = GetRandomSubject(ctx);
                    ctx.Cars.Add(car);
                }
                ctx.SaveChanges();

                foreach (var car in ctx.Cars)
                {
                    var serie = ctx.Series.FirstOrDefault(x => x.SerieId == car.Serie.SerieId);
                    serie.Cars.Add(car);
                    ctx.SaveChanges();
                }
            }
            Console.WriteLine("DONE");
            Console.ReadKey();
        }
        private static Car GenCar()
        {
            Console.WriteLine("Generován vůz");
            var car = new Car()
            {
                Certification = Convert.ToBoolean(rnd.Next(0, 1)),
                GoodGroup = GenGoodGroup(),
                LastRevision = RandomDay(),
                LastZTE = RandomDay(),
                LastZTL = RandomDay(),
                Name = "Nazev vozu " + nameCounter++,
                RevisionPeriod = 30,
                State = GenState(),
            };
            car.ChangeHistories = GenHistories(rnd.Next(5, 10), car);
            car.Faults = GenFaults(rnd.Next(5, 10), car);
            car.Revisions = GenRevisions(rnd.Next(5, 10), car);
            return car;
        }

        private static Serie GenSerie()
        {
            Console.WriteLine("Generována serie");
            return new Serie()
            {
                Area = rnd.Next(10,20),
                AreaHeight = RandFloat(),
                BumpersLenght = rnd.Next(1,4),
                ConstructionWeight = RandFloat(),
                Lenght = rnd.Next(2,6),
                MaxSpeed = RandFloat(),
                NumberOfAxle = rnd.Next(1,4),
                NumberOfGear = rnd.Next(2,8),
                NumberOfPars = rnd.Next(2,8),
                Space = RandFloat(),
                Weight = RandFloat(),
                Wheelbase = RandFloat(),
                AirBreak = GenAirBrake(),
                HandBreak = GenHandBrake()
            };
        }
        private static HandBreak GenHandBrake()
        {
            return new HandBreak()
            {
                HandBreakWeight = rnd.Next(5, 30),
                Name = "Hand break" + handBreakDesCounter++
            };
        }

        private static AirBreak GenAirBrake()
        {
            return new AirBreak()
            {
                AirBreakWeight = rnd.Next(5, 30),
                Name = "Air break" + airBreakCounter++
            };
        }

        private static float RandFloat()
        {
            double mantissa = (rnd.NextDouble() * 2.0) - 1.0;
            // choose -149 instead of -126 to also generate subnormal floats (*)
            double exponent = Math.Pow(2.0, rnd.Next(1, 10));
            return (float)(mantissa * exponent);
        }

        private static Serie GetRandomSerie(ENVCtx ctx)
        {
            return ctx.Series.ToList().ElementAt(rnd.Next(ctx.Series.Count()));
        }

        private static List<Revision> GenRevisions(int count, Car car)
        {
            var revisions = new List<Revision>();
            for (int i = 0; i < count; i++)
            {
                revisions.Add(new Revision()
                {
                    Car = car,
                    Description = "Revision" + revisionDesCounter++ + " description",
                    LastRevisionDate = RandomDay()
                });
            }
            return revisions;
        }

        private static Subject GetRandomSubject(ENVCtx ctx)
        {
            return ctx.Subjects.ToList().ElementAt(rnd.Next(ctx.Subjects.Count()));
        }

        private static Subject GenOwner()
        {
            Console.WriteLine("Generován subjekt");
            return new Subject()
            {
                Email = "sub" + subCounter + "@email.cz",
                ICO = "1111111" + subCounter,
                Name = "sub" + subCounter + " jmeno",
                OwnedCars = new List<Car>(),
                RentedCars = new List<Rent>(),
                Telephone = "77744455" + subCounter++
            };
        }

        private static List<Fault> GenFaults(int count, Car car)
        {
            Console.WriteLine("Generován fault");
            var faults = new List<Fault>();
            for (int i = 0; i < count; i++)
            {
                var fault = new Fault()
                {
                    Car = car,
                    Description = "Fault" + faultDescCounter + " description",
                    FoundedDate = RandomDay(),
                    Name = "Fault" + faultDescCounter++
                };
                fault.Repairs = GenRepairs(rnd.Next(2, 4), fault);
                faults.Add(fault);

            }
            return faults;
        }

        private static List<Repair> GenRepairs(int count, Fault fault)
        {
            Console.WriteLine("Generována oprava");
            var repairs = new List<Repair>();
            for (int i = 0; i < count; i++)
            {
                repairs.Add(new Repair()
                {
                    Fault = fault,
                    Description = "Repair" + repairDesCounter++ + " description",
                    RepairDate = RandomDay()
                });
            }
            return repairs;
        }

        private static List<ChangeHistory> GenHistories(int count, Car car)
        {
            Console.WriteLine("Generována historie vozu " + car.CarId);
            var changes = new List<ChangeHistory>();
            for (int i = 0; i < count; i++)
            {
                changes.Add(new ChangeHistory()
                {
                    Car = car,
                    ChangeDate = RandomDay(),
                    NameOfParameter = "Param" + paramCounter,
                    NewValue = "New value" + paramCounter,
                    OldValue = "Old value" + paramCounter++
                });
            }
            return changes;
        }

        private static GoodGroup GenGoodGroup()
        {
            return rnd.Next(0, 1) == 0 ? GoodGroup.DruhZbozi1 : GoodGroup.DruhZbozi2;
        }
        private static State GenState()
        {
            return rnd.Next(0, 1) == 0 ? State.Excluded : State.New;
        }
        private static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        public static TimeSpan GenRevPeriod()
        {
            return TimeSpan.FromSeconds((double)new Random().Next(10,50));
        }
    }
}
