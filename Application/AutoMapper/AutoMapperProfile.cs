using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAssessment.Application.AutoMapper
{
    using CodeAssessment.Application.Features.Products.Models;
    using global::AutoMapper;

    /// <summary>
    /// The profile of auto mapper.
    /// </summary>
    /// <seealso cref="Profile" />
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
        /// </summary>
        public AutoMapperProfile()
        {
            this.CreateMap<ProductModel, Domain.Entities.Product>().ReverseMap();
            this.CreateMap<ProductDetails, Domain.Entities.Product>().ReverseMap();
        }
    }
}
