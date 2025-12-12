using CFB.Entities.ThanhDT.Models;
using CFB.Repositories.ThanhDT.Base;
using CFB.Repositories.ThanhDT.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB.Repositories.ThanhDT
{
    public class FacilityThanhDtRepository : GenericRepository<FacilityThanhDt>
    {
        public FacilityThanhDtRepository() { }
        public FacilityThanhDtRepository(FA25_PRN222_3W_G2_CampusFacilityBookingContext context) => _context = context;

        // GetAllAsync phải implement lại
        public async Task<List<FacilityThanhDt>> GetAllAsync()
        {
            try
            {
                //return await _context.FacilityThanhDts.Include(a => a.FacilityTypeThanhDt).ThenInclude(....).OrderByDescending(a => a.CreatedAt).ToListAsync();
                return await _context.FacilityThanhDts.Include(a => a.FacilityTypeThanhDt).Include(a => a.CampusThanhDt).OrderByDescending(a => a.CreatedAt).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FacilityThanhDt> GetByIdAsync(int id)
        {
            try
            {
                return await _context.FacilityThanhDts.Include(a => a.FacilityTypeThanhDt).Include(a => a.CampusThanhDt).FirstOrDefaultAsync(a => a.FacilityThanhDtid == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<FacilityThanhDt>> SearchAsync(string facilityName, int capacity, string typeName)
        {
            try
            {
                return await _context.FacilityThanhDts.Include(a => a.FacilityTypeThanhDt)
                     .Include(a => a.CampusThanhDt)
                     .Where(a =>
                               (a.FacilityName.Contains(facilityName) || string.IsNullOrEmpty(facilityName))
                               && (a.Capacity == capacity || capacity == 0 || capacity == null)
                               && (a.FacilityTypeThanhDt.TypeName.Contains(typeName) || string.IsNullOrEmpty(typeName))
                           )
                     .OrderByDescending(a => a.CreatedAt)
                     .ToListAsync();
                     
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
