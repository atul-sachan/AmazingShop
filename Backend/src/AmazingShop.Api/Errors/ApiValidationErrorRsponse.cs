using System.Collections.Generic;

namespace AmazingShop.Api.Errors
{
    public class ApiValidationErrorRsponse : ApiResponse
    {
        public ApiValidationErrorRsponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}