using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository<TDto, TKey>
    {
        TDto Get(TKey key);

        IEnumerable<TDto> GetAll();

        TKey Add(TDto item);

        Boolean Update(TDto item);

        Boolean Remove(Guid id);
    }
}
