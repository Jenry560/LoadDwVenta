using LoadDwVenta.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDwVenta.Data.Interfaces
{
    public interface IDimProductService
    {
        Task<OperationResult> LoadProducts();
    }
}
