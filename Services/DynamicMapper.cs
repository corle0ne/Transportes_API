using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace Transportes_API.Services

{
    public class DynamicMapper
    {
        //metodo que mapea de forma dinamica difereentes tipos de objetos(por ejemplo modelos originales a DOT y viceversa)

        public static TDestination Map<TSource, TDestination>(TSource source) 
            where TSource: class //se declara una clase abstracta como tipo de objeto de entrada
            where TDestination: class, new() //se declara una clase abstracta como tipo de objeto de salida


        {
            if (source == null)throw new ArgumentNullException("source");
            
            var destination =new TDestination();   //creo una instancia del tipo de objeto de salida

            //recuperar las propiedades (los atributos de mis elementos) usando la biblioteca system.reflexion
            //Mediante reflexión, puedes acceder a las propiedades de un tipo (clase, estructura, etc.) en tiempo de ejecución, incluso si no conoces el tipo exacto en tiempo de compilación.
            //GetProperties: Devuelve un array con todas las propiedades públicas del tipo especificado.
            //BindingFlags: Opciones que especifican qué miembros buscar(públicos, privados, estáticos, etc.).
            //using System.Reflection;

            var sourceProperties=typeof (TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProperties=typeof (TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //recorro todos los atributos y propiedadees del objeto de origen para equipararlos con el objeto de salida
            foreach(var sourceProperty in sourceProperties)
            {
                //recupero cada propiedad de la clase donde empate tanto el nombre de la proppiedad como el tipo de datos  (aqui es donde se mapean los objetos)
                var destinationProperty=destinationProperties.FirstOrDefault(dp=>dp.Name.ToLower() == sourceProperty.Name.ToLower() && dp.PropertyType==sourceProperty.PropertyType);

                if(destinationProperty != null && destinationProperty.CanWrite)
                {
                    var value=sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                }
            }
            return destination;

        }
    }
}
