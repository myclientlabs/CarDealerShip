using System.Collections.Generic;
using Data;

namespace Models.Interface
{
    public interface IModelRepo
    {
        IEnumerable<Model> GetAll();


        void Create(Model model);

    }
}