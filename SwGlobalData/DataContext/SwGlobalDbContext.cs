using SwGlobalData.Entities.Auth;
using SwGlobalData.Entities.Plan;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.DataContext
{
    public class SwGlobalDbContext: DbContext
    {
        public SwGlobalDbContext()
            : base("name=SwGlobalDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<ValueAddedService> ValueAddedServices { get; set; }
        public virtual DbSet<Recharge> Recharges { get; set; }
        public virtual DbSet<RechargeType> GetRechargeTypes { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }




        public async Task<bool> TrySaveChanges()
        {
            try
            {
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
