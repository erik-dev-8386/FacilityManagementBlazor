using CFB.Entities.ThanhDT.Models;
using CFB.Repositories.ThanhDT.Base;
using CFB.Repositories.ThanhDT.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB.Repositories.ThanhDT
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository() { }

        public SystemUserAccountRepository(FA25_PRN222_3W_G2_CampusFacilityBookingContext context) => _context = context;

        public async Task<SystemUserAccount> GetUserAccount(string userName, string password)
        {
            return _context.SystemUserAccounts.FirstOrDefault(u => u.Email == userName && u.Password == password && u.IsActive == true);

            // return _context.SystemUserAccounts.FirstOrDefault(u => u.UserName == userName && u.Password == password && u.IsActive == true);

            // return _context.SystemUserAccounts.FirstOrDefault(u => u.Phone == userName && u.Password == password && u.IsActive == true);

            //return _context.SystemUserAccounts.FirstOrDefault(u => u.EmployeeCode == userName && u.Password == password && u.IsActive == true);

        }
    }
}
