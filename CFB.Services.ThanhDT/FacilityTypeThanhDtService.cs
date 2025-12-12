using CFB.Entities.ThanhDT.Models;
using CFB.Repositories.ThanhDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB.Services.ThanhDT
{
    public class FacilityTypeThanhDtService
    {
        private readonly FacilityTypeThanhDtRepository _repository;
        
        public FacilityTypeThanhDtService() => _repository ??= new FacilityTypeThanhDtRepository();

        // GetAll để lấy các Item lên người dùng chọn
        public async Task<List<FacilityTypeThanhDt>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
