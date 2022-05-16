using AutoMapper;
using Gateways.Common.Errors;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.UseCases;
using Gateways.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Common.Controllers;

public class CrudApiControllerBase<TEntity, TGet, TPost, TPut> : ApiControllerBase where TEntity : Entity
{
    private readonly IUseCase<TEntity> useCase;

    public CrudApiControllerBase(IUseCase<TEntity> useCase, IMapper mapper) : base(mapper)
    {
        this.useCase = useCase;
    }

    [HttpGet]
    public Response<IEnumerable<TGet>> GetAll()
    {
        var models = useCase
            .AsNoTrackingWithIdentityResolution()
            .ToList();
        return OkResponse<IEnumerable<TGet>>(models);
    }

    [HttpGet("{id}")]
    public Response<TGet> Get(string id)
    {
        var model = useCase
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefault(g => g.Id == id);
        if (model == null)
            throw new NotFoundError();
        return OkResponse<TGet>(model);
    }

    [HttpPost]
    public Response<TGet> Post([FromBody] TPost model)
    {
        var entity = mapper.Map<TEntity>(model);
        useCase.Add(entity);
        useCase.Commit();
        return OkResponse<TGet>(entity);
    }

    [HttpPut("{id}")]
    public Response<TGet> Put(string id, [FromBody] TPut model)
    {
        var entity = useCase.FirstOrDefault(g => g.Id == id);
        if (entity == null)
            throw new NotFoundError();
        mapper.Map(model, entity);
        useCase.Commit();
        return OkResponse<TGet>(entity);
    }

    [HttpDelete("{id}")]
    public Response<TGet> Delete(string id)
    {
        var entity = useCase.FirstOrDefault(g => g.Id == id);
        if (entity == null)
            throw new NotFoundError();
        useCase.Remove(entity);
        useCase.Commit();
        return OkResponse<TGet>(entity);
    }
}