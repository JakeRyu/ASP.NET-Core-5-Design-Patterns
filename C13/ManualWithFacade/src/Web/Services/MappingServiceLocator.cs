﻿using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Web.Services
{
    public class MappingServiceLocator : IMappingService
    {
        private readonly IServiceProvider _serviceProvider;
        public MappingServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public TDestination Map<TSource, TDestination>(TSource entity)
        {
            var mapper = _serviceProvider.GetService<IMapper<TSource, TDestination>>();
            if (mapper == null)
            {
                throw new MapperNotFoundException(typeof(TSource), typeof(TDestination));
            }
            return mapper.Map(entity);
        }
    }
}
