using AutoMapper;
using Gateways.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Gateways.Common.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    protected readonly IMapper mapper;

    public ApiControllerBase(
        IMapper mapper)
    {
        this.mapper = mapper;
    }

    protected Response<T> OkResponse<T>(object data)
    {
        var newData = mapper.Map<T>(data);
        return new Response<T> { Data = newData };
    }

    protected Response<T> OkResponse<T>(T data)
    {
        return new Response<T> { Data = data };
    }

    protected Response<T> OkResponse<TSource, T>(TSource data)
    {
        var newData = mapper.Map<T>(data);
        return new Response<T> { Data = newData };
    }

    protected PaginatedResponse<T> OkResponse<T>(IEnumerable<object> data, bool hasPrevious, bool hasNext)
    {
        var newData = mapper.Map<IEnumerable<T>>(data);
        var paginatedModel = new Paginated<T>
        {
            HasPrevious = hasPrevious,
            HasNext = hasNext,
            Items = newData,
        };
        return new PaginatedResponse<T> { Data = paginatedModel };
    }

    protected PaginatedResponse<T> OkResponse<T>(IEnumerable<T> data, bool hasPrevious, bool hasNext)
    {
        var paginatedModel = new Paginated<T>
        {
            HasPrevious = hasPrevious,
            HasNext = hasNext,
            Items = data,
        };
        return new PaginatedResponse<T> { Data = paginatedModel };
    }

    protected PaginatedResponse<T> OkResponse<TSource, T>(IEnumerable<TSource> data, bool hasPrevious, bool hasNext)
    {
        var newData = mapper.Map<IEnumerable<T>>(data);
        var paginatedModel = new Paginated<T>
        {
            HasPrevious = hasPrevious,
            HasNext = hasNext,
            Items = newData,
        };
        return new PaginatedResponse<T> { Data = paginatedModel };
    }

    protected PaginatedResponse<T> OkResponse<TSource, T>(Paginated<TSource> paginatedModel)
    {
        var newData = mapper.Map<IEnumerable<T>>(paginatedModel.Items);
        var newPaginatedModel = new Paginated<T>
        {
            HasPrevious = paginatedModel.HasPrevious,
            HasNext = paginatedModel.HasNext,
            Items = newData,
        };
        return new PaginatedResponse<T> { Data = newPaginatedModel };
    }
}
