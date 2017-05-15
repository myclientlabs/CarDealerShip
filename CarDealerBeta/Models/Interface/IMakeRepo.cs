using System.Collections.Generic;
using Data;

namespace Models.Interface
{
    public interface IMakeRepo
    {
        IEnumerable<Make> GetAll();


        void Create(Make model);

    }
}