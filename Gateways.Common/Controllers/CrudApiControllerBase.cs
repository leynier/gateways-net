using AutoMapper;
using Gateways.Common.Errors;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.UseCases;
using Gateways.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Common.Controllers;

public class CrudApiControllerBase<TEntity, TGet, TGetDetails, TPost, TPut, TKey> : ApiControllerBase where TEntity : Entity<TKey>
{
    private readonly Func<IQueryable<TEntity>, IQueryable<TEntity>>? includer;
    protected readonly IUseCase<TEntity> useCase;

    public CrudApiControllerBase(
        IUseCase<TEntity> useCase,
        IMapper mapper,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includer = null) : base(mapper)
    {
        this.includer = includer;
        this.useCase = useCase;
    }

    [HttpGet]
    public virtual Response<IEnumerable<TGet>> GetAll()
    {
        var models = useCase
            .AsNoTrackingWithIdentityResolution()
            .ToList();
        return OkResponse<IEnumerable<TGet>>(models);
    }

    [HttpGet("{id}")]
    public virtual Response<TGetDetails> Get(TKey id)
    {
        IQueryable<TEntity> query = useCase.AsNoTrackingWithIdentityResolution();
        if (includer != null)
            query = includer(query);
        var model = query.FirstOrDefault(g => Equals(g.Id, id));
        if (model == null)
            throw new NotFoundError();
        return OkResponse<TGetDetails>(model);
    }

    [HttpPost]
    public virtual Response<TGet> Post([FromBody] TPost model)
    {
        var entity = mapper.Map<TEntity>(model);
        useCase.Add(entity);
        useCase.Commit();
        return OkResponse<TGet>(entity);
    }

    [HttpPut("{id}")]
    public virtual Response<TGet> Put(TKey id, [FromBody] TPut model)
    {
        var entity = useCase.FirstOrDefault(g => Equals(g.Id, id));
        if (entity == null)
            throw new NotFoundError();
        mapper.Map(model, entity);
        useCase.Commit();
        return OkResponse<TGet>(entity);
    }

    [HttpDelete("{id}")]
    public virtual Response<TGet> Delete(TKey id)
    {
        var entity = useCase.FirstOrDefault(g => Equals(g.Id, id));
        if (entity == null)
            throw new NotFoundError();
        useCase.Remove(entity);
        useCase.Commit();
        return OkResponse<TGet>(entity);
    }
}