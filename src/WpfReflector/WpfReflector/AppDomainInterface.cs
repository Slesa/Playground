using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfReflector
{
    /**
     * Provides functionality to access the current application domain´s
     * type system.
     * */
    public class AppDomainInterface
    {
        public static IEnumerable<string> GetAllNamesOfFieldsOfType(Type superType)
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                   from type in assembly.GetTypes()
                   from field in type.GetFields()
                   where superType.IsAssignableFrom(field.FieldType)
                   orderby type.Name, field.Name
                   select type.FullName + "." + field.Name;
        }

        public static IEnumerable<string> GetAllNamesOfPropertiesOfType(Type superType)
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                   from type in assembly.GetTypes()
                   from prop in type.GetProperties()
                   where superType.IsAssignableFrom(prop.PropertyType)
                   orderby type.Name, prop.Name
                   select type.FullName + "." + prop.Name;
        }

        public static IEnumerable<string> GetAllNamesOfSubtypesOfType(Type superType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => superType.IsAssignableFrom(type))
                .Select(type => type.FullName);
        }

        public static Type ResolveTypeName(String typeName)
        {
            Type resultType = null;
            
            var assemblies = AppDomain
                 .CurrentDomain
                 .GetAssemblies();

            foreach (var assembly in assemblies)
            {
                String potentialAssemblyQualifiedName = typeName + "," + assembly.FullName;
                resultType = Type.GetType(potentialAssemblyQualifiedName);

                if (resultType != null)
                {
                    break;
                }
            }

            return resultType;
        }
    }
}
