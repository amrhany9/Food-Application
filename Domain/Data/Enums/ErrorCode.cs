using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Domain.Data.Enums
{
    public enum ErrorCode
    {
        None = 0,

        // Success codes
        Ok = 200,
        Created = 201,
        NoContent = 204,

        // Client error codes
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        Conflict = 409,
        UnprocessableEntity = 422,

        // Server error codes
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,

        // Application-specific error codes
        ValidationError = 1000,
        BusinessRuleViolation = 1001,
        DataConstraintViolation = 1002,
        ExternalServiceError = 1003
    }
}
