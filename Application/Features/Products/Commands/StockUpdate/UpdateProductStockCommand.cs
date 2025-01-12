﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Application.Features.Products.Models;
using CodeAssessment.Application.Response;
using MediatR;

namespace CodeAssessment.Application.Features.Products.Commands.StockUpdate
{
    public class UpdateProductStockCommand : ProductStockUpdateModel, IRequest<BaseResponse<object>>
    {
    }
}
