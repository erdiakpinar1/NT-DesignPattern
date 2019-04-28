using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Northwind.Wcf
{
    [ServiceContract]
    public interface IService<DTO> where DTO:class
    {
        //service katmanı client tan talebi alıp Repository katmanına iletir
        // Iservice interface'i ve içerisinde tanımlı methodlar client ile iletişime geçeceği için Contract(sözleşme0) içerisine dahil edilmesi gerekir.
        // Bu katmanın olmasının sebebi , clientların direk olarak entity ve facade'lar ulaşması içindir. service katmanın client tarafından gelen nesneler DTO nesneleridir. (Entity Nesneleri gelmez)
        
        // Dto katmanı entitylerin aynısı olacaktır sadece dto içerisinde serilize edilebilir nesneler barındırılır. 
        //client ile service arasında gidip/gelen  nesnelerin serilize edilebilir olması gerekir.

        [OperationContract]
        List<DTO> Listing();
        [OperationContract]
        bool Adding(DTO dto);
        [OperationContract]
        bool Updating(DTO dto);
        [OperationContract]
        bool Deleting(DTO dto);
        
    }
}