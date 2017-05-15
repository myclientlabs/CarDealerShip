using CarDealerBeta.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Repositories
{
    public class LookupRepository : ILookupRepository
    {
        CarEntities db;
        public LookupRepository()
        {
            db = new CarEntities();
        }

        public List<Make> GetMakes()
        {
            return db.Makes.ToList();
        }
        public List<Model> GetModels()
        {
            return db.Models.ToList();
        }
        public List<Model> GetModelByMakeId(int makeId)
        {
            return db.Models.Where(s => s.MakeId == makeId).ToList();
        }
        public List<Style> GetStyles()
        {
            return db.Styles.ToList();
        }
        public List<Transmission> GetTransmissions()
        {
            return db.Transmissions.ToList();
        }
        public List<Color> GetColors()
        {
            return db.Colors.ToList();
        }
        public List<State> GetStates()
        {
            return db.States.ToList();
        }

        public List<PurchaseType> GetPurchaseTypes()
        {
            return db.PurchaseTypes.ToList();
        }
    }
}