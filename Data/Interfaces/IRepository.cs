using System.Linq.Expressions;

namespace AracKiralamaPortal.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); // ID'ye göre bir kayıt getir
        Task<IEnumerable<T>> GetAllAsync(); // Tüm kayıtları getir
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate); // Şarta göre kayıtları getir
        Task AddAsync(T entity); // Yeni bir kayıt ekle
        void Update(T entity); // Var olan kaydı güncelle
        void Delete(T entity); // Kaydı sil
    }
}
