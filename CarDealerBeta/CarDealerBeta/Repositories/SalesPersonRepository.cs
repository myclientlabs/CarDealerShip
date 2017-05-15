using CarDealerBeta.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Repositories
{
    
    public class SalesPersonRepository : ISalesPersonRepository
    {
        CarEntities db;
        public SalesPersonRepository()
        {
            db = new CarEntities();
        }

        public Salesperson GetSalesPersonbyUserId(int userId)
        {
            return db.Salespersons.FirstOrDefault(s => s.UserId == userId);
        }
    }
}