using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.Interfaces
{
    public interface IBaseBL<TDto>
    {
        IEnumerable<TDto> GetAll();

        TDto GetById(long id);

        void Create(TDto dto);

        void Update(TDto dto);

        void Delete(long id);
    }
}
