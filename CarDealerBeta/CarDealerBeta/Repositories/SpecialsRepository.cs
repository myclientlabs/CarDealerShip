using CarDealerBeta.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Repositories
{
    public class SpecialsRepository : ISpecialsRepository
    {
        CarEntities db;
        public SpecialsRepository()
        {
            db = new CarEntities();
        }

        public List<Special> GetSpecials()
        {
           return db.Specials.ToList();
        }
    }
}