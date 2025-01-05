using Ecommerce_Core.DTOs;
using Ecommerce_Core.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Mapping_Profiles
{
    public class MappingProfile
    {
        private static readonly TypeAdapterConfig _config = new TypeAdapterConfig();

        static MappingProfile()
        {
            _config.NewConfig<Product, ProductDto>();
        }

        public static TypeAdapterConfig Config => _config;
    }
}
