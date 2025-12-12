using CFB.Entities.ThanhDT.Models;
using CFB.Repositories.ThanhDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB.Services.ThanhDT
{
    public class CampusThanhDtService
    {
        private readonly CampusThanhDtRepository _repository;

        public CampusThanhDtService() => _repository ??= new CampusThanhDtRepository();

        public async Task<List<CampusThanhDt>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
