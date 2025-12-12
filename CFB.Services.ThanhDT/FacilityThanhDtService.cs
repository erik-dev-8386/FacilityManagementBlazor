using CFB.Entities.ThanhDT.Models;
using CFB.Repositories.ThanhDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB.Services.ThanhDT
{
    public class FacilityThanhDtService
    {
        private readonly FacilityThanhDtRepository _repository;
        public FacilityThanhDtService() => _repository ??= new FacilityThanhDtRepository();

        // implement CRUD
        public async Task<List<FacilityThanhDt>> GetAllAsync()
        {
            try
            {
                /*
                 * Implement business rules
                 */
                return await _repository.GetAllAsync();
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
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // create trả về số lượng insert dưới db
        public async Task<int> CreateAsync(FacilityThanhDt facilityThanhDt)
        {
            try
            {
                /*
                 * Implement business rule
                 */

                return await _repository.CreateAsync(facilityThanhDt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // create giống update copy create
        public async Task<int> UpdateAsync(FacilityThanhDt facilityThanhDt)
        {
            try
            {
                /*
                 * Implement business rule
                 */

                return await _repository.UpdateAsync(facilityThanhDt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Delete thì trả về bool(True/False)
        public async Task<bool> DeleteAsyc(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);

                if (item != null)
                {
                    return await _repository.RemoveAsync(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        public async Task<List<FacilityThanhDt>> SearchAsync(string facilityName, int capacity, string typeName)
        {
            try
            {
                return await _repository.SearchAsync(facilityName, capacity, typeName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}