using AutoMapper;
using probo_checker.Common.AutoMapper.Interfaces;
using System;
using System.Linq;

namespace probo_checker.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.ConfigureMapping();
        }

        private void ConfigureMapping()
        {
            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().FullName.Contains("probo-checker"))
                .SelectMany(a => a.GetTypes())
                .ToArray();

            var withBidirectionalMapping = allTypes
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && t.GetInterfaces()
                                .Where(i => i.IsGenericType)
                                .Select(i => i.GetGenericTypeDefinition())
                                .Contains(typeof(IMapWith<>)))
                .Select(t => new
                {
                    Type1 = t,
                    Type2 = t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapWith<>))
                        .SelectMany(i => i.Arguments)
                        .First()
                })
                .ToArray();

            //Create bidirectional mapping for all types implementing the IMapWith<TModel> interface
            foreach (var mapping in withBidirectionalMapping)
            {
                this.CreateMap(mapping.Type1, mapping.Type2);
                this.CreateMap(mapping.Type2, mapping.Type1);
            }

            // Create custom mapping for all types implementing the ICustomMapper interface
            var withCustomMapping = allTypes.Where(t => t.IsClass
                                                        && !t.IsAbstract
                                                        && typeof(ICustomMapper).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<ICustomMapper>()
                .ToArray();

            foreach (var mapping in withCustomMapping)
            {
                mapping.ConfigureMapping(this);
            }
        }
    }
}