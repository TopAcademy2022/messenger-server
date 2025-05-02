using System.Collections.Generic;

namespace MessengerServer.Domain.Repositories
{
    public interface IRepository<T>
    {
        public T GetById(int id);

        public IEnumerable<T> GetAll();

        public bool UpdateById(string id, T updated);

        public bool DeleteById(string id);
    }
}
