using Northwind.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Repository
{
    public class RepositoryBase<TT> : IRepository<TT> where TT : class
    {
        // Repository Base classında context'e ihtiyacımız var. bu yüzden Northwind.Repository projemize Northwind.Entity projesini referans olarak eklememiz gerekir.

        //Singleton Pattern mimarisi, uygulamanın tek context yada tek connection üzerinden işlem yapmasının sağlandığı design pattern (tasarım deseni)dir.
        //sık bağlantı açılıp kapatılan uygulamalarda bu işlemler SQL server'a gereksiz yük bindirir. Bunun yerine hazırda context nesnesi var mı bakılır, eğer yoksa yeniden oluşturulur, varsa var olan kullanılır.

        private NorthwindEntities context;

        public NorthwindEntities Context
        {
            get
            {
                //if(context == null)
                //{
                //    context = new NorthwindEntities();
                //}
                //return context;
                return context ?? (context = new NorthwindEntities()); // ?? Null 
            }
            set
            {
                context = value;
            }
        }

        public List<TT> Listing()
        {
            //product entity si gelirse productlar listelenecek, Category Entity'si gelirse Categoryler listelenecek
            return Context.Set<TT>().ToList();
        }


        public bool Adding(TT entity)
        {
            // set<TT> : Context'in tt tipini algılamasını sağlar.
            Context.Set<TT>().Add(entity);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Updating(TT entity)
        {
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Deleting(TT entity)
        {
            Context.Set<TT>().Remove(entity);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
