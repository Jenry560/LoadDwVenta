using LoadDwVenta.Data.Core;
using LoadDwVenta.Data.Entities.DwVentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDwVenta.Data.Interfaces
{
    public interface IDimEmployeeService
    {
        Task<OperationResult> LoadEmployees();
    }
}
