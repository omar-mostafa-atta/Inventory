using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Repositories;

namespace Inventory.Service.Sevices.AlertService
{
    public interface IAlertService
    {
        IEnumerable<Alert> GetAll();
        Alert GetById(int id);
        void Insert(Alert alert);
        void Update(Alert alert);
        void Delete(int id);

    }
}
