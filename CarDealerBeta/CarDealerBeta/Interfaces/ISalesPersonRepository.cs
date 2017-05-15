using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerBeta.Interfaces
{
    public interface ISalesPersonRepository
    {
        Salesperson GetSalesPersonbyUserId(int userId);
    }
}
