using System;
using System.Collections.Generic;
using System.Linq;
using EGO.Data;

namespace EGO.Service
{
    public class ClientRepository : IClientRepository
    {
        private ApplicationDbContext _datacontext;
        private readonly IRepository<Client> _saleBatchRepository;

        public ClientRepository()
        {
            _datacontext = new ApplicationDbContext();
            _saleBatchRepository = new RepositoryService<Client>(_datacontext);  
        }

        public List<Client> GetAll()
        {
            return _saleBatchRepository.GetAll().ToList();
        }

        public Client GetById(int id)
        {
            return _saleBatchRepository.GetById(id);
        }

        public void Insert(Client model)
        {
           _saleBatchRepository.Insert(model);
        }

        public void Update(Client model)
        {
            _saleBatchRepository.Update(model);
        }

        public void Delete(Client model)
        {
            _saleBatchRepository.Delete(model);
        }

        public IEnumerable<Client> Find(Func<Client, bool> predicate)
        {
            return _saleBatchRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
    public interface IClientRepository : IDisposable
    {
        List<Client> GetAll();
        Client GetById(int id);
        void Insert(Client model);
        void Update(Client model);
        void Delete(Client model);
        IEnumerable<Client> Find(Func<Client, bool> predicate);
    }
}
