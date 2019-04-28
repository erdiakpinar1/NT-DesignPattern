using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Extensions
{
    // Extension methodlar ve bu methodların bulunduğu class'lar "static" olmalıdır.
    public static class Extension
    {
        // C#'ta bulunan Object class'ının içine yeni bir method ekleyeceğiz.
        //(this Object source); object tipinin içerisine bu method (Changer) eklenecektir. Bu yazım tarzı ile, hali hazırda bulunan bir sınıfın içerisine dışarıdan bir method eklemiş olacağız. Bundan sonra projenin içerisine herhengi bir sınıf eklendiğinde bu method o sınıfın içinde otomatik olarak varmış gibi olacak (inheritance'dan dolayı). Kısacası bu gösterim extension method gösterimidir.

        // T Changer<T>(this Object source): Source elemanı hangi instance üzerinden "." yazarak method'a ulaşıyorsa o instance'ı temsil eder. Biz changer methoduyla source elemanını "T" tipine dönüştüreceğiz. VE geriye "T" eleman döndüreceğiz. 

        /// <summary>
        /// Product Nesnesinin içerisindeki propertyleri ProductDTO içerisine koyacağız, ya da ProductDTO içerisindeki property'leri product nesnesinin içerisine koyacağız.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Changer<T>(this Object source)
        {
            // T tipinde instance oluştur ve oluşan instance'ı yine T tipinde bir değişkene ata
            T target = Activator.CreateInstance<T>();

            Type targetType = target.GetType();
            Type sourceType = source.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] targetProperties = targetType.GetProperties();

            foreach (PropertyInfo pInf in sourceProperties)
            {
                object value = pInf.GetValue(source);

                PropertyInfo targetpInf = targetProperties.FirstOrDefault(x => x.Name == pInf.Name);

                if (targetpInf != null)
                    targetpInf.SetValue(target, value);
            }
            return target;
        }
    }
}
