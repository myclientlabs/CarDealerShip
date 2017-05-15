using System.Collections.Generic;
using Data;

namespace Models.Interface
{
    public interface IVehicleRepo
    {
        IEnumerable<Vehicle> GetAll();
        Vehicle GetById(int id);

        void Create(Vehicle model);
        void Update(Vehicle model);
        void Delete(int id);
    }
}