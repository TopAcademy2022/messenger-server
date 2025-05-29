using System.Collections.Generic;

namespace MessengerServer.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        public bool Add(T entity);

        public T GetById(int id);

        public IEnumerable<T> GetAll();

        public bool UpdateById(T entity);

        public bool DeleteById(string id);
    }
}
