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
    public class FacilityTypeThanhDtRepository : GenericRepository<FacilityTypeThanhDt>
    {
        public FacilityTypeThanhDtRepository() { }
        public FacilityTypeThanhDtRepository(FA25_PRN222_3W_G2_CampusFacilityBookingContext context) => _context = context;
    }
}
