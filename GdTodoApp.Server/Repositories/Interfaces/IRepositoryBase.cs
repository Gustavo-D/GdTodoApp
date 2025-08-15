namespace GdToDoApp.Server.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task Create(T model);
        public Task Update(T model);
        public Task<bool> DeleteById(long id);
        public Task<T[]> GetById(long? id, bool tracked = true);
    }
}
