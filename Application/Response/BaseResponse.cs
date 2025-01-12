using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CodeAssessment.Application.Response
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BaseResponse<T> : ControllerBase
    {
        [JsonProperty]
        public int Code { get; set; }

        [JsonProperty]
        public bool HasError { get; set; } = false;

        [JsonProperty]
        public string Message { get; set; }

        [JsonProperty]
        public T Data { get; set; }

        public IActionResult Result
        {
            get
            {
                switch (this.Code)
                {
                    case StatusCodes.Status200OK:
                        return this.Ok(this);

                    case StatusCodes.Status500InternalServerError:
                        return this.StatusCode(StatusCodes.Status500InternalServerError, this);

                    case StatusCodes.Status400BadRequest:
                        return this.StatusCode(StatusCodes.Status400BadRequest, this);

                    default:
                        return this.NotFound(this);
                }
            }
        }
    }
}
