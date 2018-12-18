using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class ENVCtx : IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<ChangeHistory> ChangeHistories { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Revision> Revisions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<HandBreak> HandBreaks { get; set; }
        public DbSet<AirBreak> AirBreaks { get; set; }
        public DbSet<GoodGroup> GoodGroups { get; set; }

        public ENVCtx():base(nameOrConnectionString:"EvidenceNakladnichVozuConStr")
        {

        }

    }
}
