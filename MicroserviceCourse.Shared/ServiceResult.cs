using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroserviceCourse.Shared;

public interface IRequestByServiceResult<T> : IRequest<ServiceResult<T>>;
public interface IRequestByServiceResult : IRequest<ServiceResult>;

public class ServiceResult
{
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }
    public ProblemDetails? Fail { get; set; }

    [JsonIgnore] public bool IsSuccess => Fail is null;
    [JsonIgnore] public bool IsFail => !IsSuccess;

    public static ServiceResult SuccessAsNoContent()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.NoContent
        };
    }

    public static ServiceResult ErrorAsNotFound()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.NotFound,
            Fail = new ProblemDetails
            {
                Title = "Not Found",
                Detail = "The requested resource was not found."
            }
        };
    }

    public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
    {
        return new ServiceResult
        {
            StatusCode = status,
            Fail = problemDetails
        };
    }

    public static ServiceResult Error(string title, string description, HttpStatusCode status)
    {
        return new ServiceResult
        {
            StatusCode = status,
            Fail = new ProblemDetails()
            {
                Title = title,
                Detail = description,
                Status = status.GetHashCode()
            }
        };
    }

    public static ServiceResult Error(string title, HttpStatusCode status)
    {
        return new ServiceResult
        {
            StatusCode = status,
            Fail = new ProblemDetails()
            {
                Title = title,
                Status = status.GetHashCode()
            }
        };
    }

    public static ServiceResult ErrorFromProblemDetails(Refit.ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails()
                {
                    Title = exception.Message
                },
                StatusCode = exception.StatusCode
            };
        }

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        return new ServiceResult()
        {
            Fail = problemDetails,
            StatusCode = exception.StatusCode
        };
    }

    public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            Fail = new ProblemDetails()
            {
                Title = "Validation errors occurred.",
                Detail = "Please check the errors for more details.",
                Extensions = errors,
                Status = HttpStatusCode.BadRequest.GetHashCode()
            }
        };
    }
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
    [JsonIgnore] public string? UrlAsCreated { get; set; }

    public static ServiceResult<T> SuccessAsOk(T data)
    {
        return new ServiceResult<T>
        {
            StatusCode = HttpStatusCode.OK,
            Data = data
        };
    }

    public static ServiceResult<T> SuccessAsCreated(T data, string url)
    {
        return new ServiceResult<T>
        {
            StatusCode = HttpStatusCode.Created,
            Data = data,
            UrlAsCreated = url
        };
    }

    public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode status)
    {
        return new ServiceResult<T>
        {
            StatusCode = status,
            Fail = problemDetails
        };
    }

    public new static ServiceResult<T> Error(string title, string description, HttpStatusCode status)
    {
        return new ServiceResult<T>
        {
            StatusCode = status,
            Fail = new ProblemDetails()
            {
                Title = title,
                Detail = description,
                Status = status.GetHashCode()
            }
        };
    }

    public new static ServiceResult<T> Error(string title, HttpStatusCode status)
    {
        return new ServiceResult<T>
        {
            StatusCode = status,
            Fail = new ProblemDetails()
            {
                Title = title,
                Status = status.GetHashCode()
            }
        };
    }

    public new static ServiceResult<T> ErrorFromProblemDetails(Refit.ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails()
                {
                    Title = exception.Message
                },
                StatusCode = exception.StatusCode
            };
        }

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        return new ServiceResult<T>()
        {
            Fail = problemDetails,
            StatusCode = exception.StatusCode
        };
    }

    public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
    {
        return new ServiceResult<T>
        {
            StatusCode = HttpStatusCode.BadRequest,
            Fail = new ProblemDetails()
            {
                Title = "Validation errors occurred.",
                Detail = "Please check the errors for more details.",
                Extensions = errors,
                Status = HttpStatusCode.BadRequest.GetHashCode()
            }
        };
    }
}