using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Repositories;

namespace Inventory.Service.Sevices.AlertService
{
    public class AlertService : IAlertService
    {
        private readonly IGenericRepository<Alert> _alertRepository;

        public AlertService(IGenericRepository<Alert> AlertRepository)
        {
            _alertRepository = AlertRepository;
        }

        public IEnumerable<Alert> GetAll()
        {
            return _alertRepository.GetAll();
        }

        public Alert GetById(int id)
        {
            return _alertRepository.GetById(id);
        }

        public void Insert(Alert alert)
        {
            _alertRepository.Add(alert);
        }

        public void Update(Alert alert)
        {
            _alertRepository.Update(alert);
        }

        public void Delete(int id)
        {
            _alertRepository.Delete(id);
        }
    }
}
