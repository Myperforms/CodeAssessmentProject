using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Application.Features.Products.Models;
using MediatR;
using CodeAssessment.Application.Response;

namespace CodeAssessment.Application.Features.Products.Queries
{
    public class GetProductQuery : ProductModel, IRequest<BaseResponse<object>>
    {
    }
}
