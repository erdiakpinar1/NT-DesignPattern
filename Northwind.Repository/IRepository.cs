using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Repository
{
    public interface IRepository<T> where T:class //Dışardan gelen liste product'ta olabilir customer'da
    {
        List<T> Listing();
        bool Adding(T entity);
        bool Updating(T entity);
        bool Deleting(T entity);

    }
}
