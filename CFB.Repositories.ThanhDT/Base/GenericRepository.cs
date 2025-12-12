using CFB.Repositories.ThanhDT.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB.Repositories.ThanhDT.Base
{
    /// <summary>
    /// Nhiệm vụ là tương tác trực tiếp xuống db
    /// </summary>
    /// <typeparam name="T"></typeparam>
    // T là kiểu Tree, đúng kiểu truyền vào
    // Hàm đầu tiên là default constructor ko tham số
    // ?? ~~ if(_context = null)
    // Có tham số thì assign vào biến local
    // GetAll và GetAllAsync
    // GetAll: Lấy các phần tử kiểu tree  => theo dạng List
    // GetAllAsync : Lấy các phần tử kiểu tree theo kiểu syno, multi thread
    // Update: 2 đứa cùng update thì 1 người bị lock  còn 1 người được sử dụng
    // UpdateAsync: 2 đứa cùng update thì 1 người bị lock thì người kia vẫn thấy được
    // chuyển 50tr - xem tài khoản 5tr => xem tài khoản 5tr
    public class GenericRepository<T> where T : class
    {
        protected FA25_PRN222_3W_G2_CampusFacilityBookingContext _context;

        public GenericRepository()
        {
            _context ??= new FA25_PRN222_3W_G2_CampusFacilityBookingContext();
        }

        public GenericRepository(FA25_PRN222_3W_G2_CampusFacilityBookingContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            //// Turning off Tracking for UpdateAsync in Entity Framework
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            // Modified: tôi đang update nè!
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            // Dưới db vẫn còn nguyên nhưng trong bộ nhớ thì mất rồi
            await _context.SaveChangesAsync();
            // Khúc này db mới mất
            return true;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(string code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }
        public T GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        #region Separating asigned entity and save operators        

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}
