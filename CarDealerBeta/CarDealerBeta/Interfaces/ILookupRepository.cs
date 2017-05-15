using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerBeta.Interfaces
{
    public interface ILookupRepository
    {
        List<Make> GetMakes();
        List<Model> GetModels();
        List<Model> GetModelByMakeId(int makeId);
        
        List<Style> GetStyles();
        List<Transmission> GetTransmissions();
        List<Color> GetColors();

        List<State> GetStates();

        List<PurchaseType> GetPurchaseTypes();

    }
}
