using Northwind.DTO;
using Northwind.Entity;
using Northwind.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Northwind.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductService.svc or ProductService.svc.cs at the Solution Explorer and start debugging.

    public class ProductService : ServiceBase<ProductRepository, Product, ProductDTO>
    {
        //product service isimli bir service oluşturunca arka planda Iproductservice isimli bir interface oluştur. productservice servisimiz IproductService isimli interface'den türeyecek. Hiyerarşiyi bu şekilde bırakısak, IService ve serviceBase içindeki methodlar kullanılamayacaklar oysaki biz Iservice ve serviceBase içindeki methodların (Listing, Deleting, Updateing, Adding) tüm servicelerde (product, Category, supplier için yazılacak) tekrar tekrar yazılmaması için oluşturmuştuk.  
    }
}
