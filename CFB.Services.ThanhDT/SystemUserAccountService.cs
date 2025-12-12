using CFB.Entities.ThanhDT.Models;
using CFB.Repositories.ThanhDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB.Services.ThanhDT
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;

        public SystemUserAccountService() => _repository ??= new SystemUserAccountRepository();

        public async Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            try
            {
                return await _repository.GetUserAccount(username, password);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
