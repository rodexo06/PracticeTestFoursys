using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PracticeTestFoursys.Application.Mapping {
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            IEnumerable<Type> mapFromTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)));

            foreach (Type mapperType in mapFromTypes)
            {
                object mapperInstance = Activator.CreateInstance(mapperType);

                MethodInfo methodInfo = mapperType.GetMethod("Mapping") ?? mapperType.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(mapperInstance, new object[] { this });
            }
        }
    }
}
