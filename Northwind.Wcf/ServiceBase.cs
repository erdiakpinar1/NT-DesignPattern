using Northwind.Repository;
using Northwind.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.DTO;
using Northwind.Entity;

namespace Northwind.Wcf
{
    public class ServiceBase<Rep, Entity, DTO> : IService<DTO>
        where DTO : class
        where Entity : class
        where Rep : RepositoryBase<Entity>

    {
        // Rep :RepositoryBase<Entity> : serviceBase'in rep hareket tipi RepositoryBase tipinde olduğu belirtildiği için servicebase sınıfını kullandığımız yerlerde Rep hareket tipi için productRepository, CategoryRepository, supplierRepository yazılabilir. Çünkü bu sınıflar repositoryBase sınıfından türemiştir.

        // ServiceBase sınıfı RepositoryBase sınıfına talepleri gönderen, ve repositoryBase sınıfından responseları alan bir sınıftır. serviceBase sınıfının hangi repositoryBase sınfı (ProductRepository mi?, CategoryRepository mi?...) ile iletişide olduğunu bilmek gerekir. ayrıca servicebase sınıfı client'a DTO nesnesi yollamalı ve client'tan gelen DTO nesnesini RepositoryBase sınıfına gönderirken Entity'e dönüştürmesi gerekir.

        // ServiceBase'in, hem repositoryTipi, hem Entity tipi, hemde DTO tipi argümanlarına ihtiyacı vardır. 

        private Rep repository;
        public Rep Repository
        {
            get
            {
                // generic tip için instance oluşturmak istediğinizde
                // repository = new Rep()
                // gibi bir işlem yapamıyoruz. Generic tip için instance oluşturmada kullanılacak class'ın Activator ve methodun adı CreateInstance isimli Generic method'dur
                //Rep dışarıdan alınan tiptir ve instance bu tip için üretilecektir
                //repository = repository == null ? Activator.CreateInstance<Rep>():repository;
                repository = repository ?? Activator.CreateInstance<Rep>();
                return repository;
            }
            set
            {
                repository = value;
            }
        }
        public bool Adding(DTO dto)
        {
            //throw new NotImplementedException();
            return Repository.Adding(dto.Changer<Entity>());
        }

        public bool Deleting(DTO dto)
        {
            //throw new NotImplementedException();
            //Repository.Deleting(dto);
            return Repository.Deleting(dto.Changer<Entity>());
        }

        public List<DTO> Listing()
        {
            // ServiceBase'inden RepositoryBase'e talep gönderilecektir.  
            //throw new NotImplementedException();
            // return Repository.Listining();
            //service katmanımız repositoryden entity alır. öncelikle alınan entitylerin dto nesnesine dönüştürülmesi gerekir.
            //Bizim DTO-to-Entity ve Entity-to-Dto çevirmine ihtiyacımız vardır. 

            // d.Changer<Product>(): "d." nın anlamı changer method'una hangi kaynak(source) üstünden ulaştığınızı gösterir. Yani changer'a ProductDTO üzerinden ulaşıyorsunuz. Başk bir değişle ProductDTO nesnesini Product Nesnesine dönüştürüyorsunuz.
            // <Product>(): Changer methodunun dışardan istediği argüman tipini gösterir. Changer hangi tipe dönüştürülecekse o tip argüman tip olarak belirtilir.
            // Product prod = : Changer method'unun return tipini gösterir.

            //Deneme kodları
            //ProductDTO d = new ProductDTO();
            //Product prod = d.Changer<Product>();

            //Product p = new Product();
            //ProductDTO pdt = p.Changer<ProductDTO>();

            //Product p = new Product();
            //p.ProductName = "MyAvokado";
            //p.UnitPrice = 19;
            //p.UnitsInStock = 199;
            //ProductDTO dt = p.Changer<ProductDTO>();
            //throw new NotImplementedException();

            return Repository.Listing().Select(x => x.Changer<DTO>()).ToList();
        }

        public bool Updating(DTO dto)
        {
            //throw new NotImplementedException();
            return Repository.Updating(dto.Changer<Entity>());
        }
    }
}