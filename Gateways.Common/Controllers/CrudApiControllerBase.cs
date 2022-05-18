using AutoMapper;
using Gateways.Common.Errors;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Common.Controllers;

public class CrudApiControllerBase<TEntity, TGet, TGetDetails, TPost, TPut, TKey> : ApiControllerBase where TEntity : class, IEntity<TKey>
{
    private readonly Func<IQueryable<TEntity>, IQueryable<TEntity>>? includer;
    protected readonly IService<TEntity> service;

    public CrudApiControllerBase(
        IService<TEntity> service,
        IMapper mapper,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includer = null) : base(mapper)
    {
        this.includer = includer;
        this.service = service;
    }

    [HttpGet]
    public virtual Response<IEnumerable<TGet>> GetAll()
    {
        var models = service
            .AsNoTrackingWithIdentityResolution()
            .ToList();
        return OkResponse<IEnumerable<TGet>>(models);
    }

    [HttpGet("{id}")]
    public virtual Response<TGetDetails> Get(TKey id)
    {
        IQueryable<TEntity> query = service.AsNoTrackingWithIdentityResolution();
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
        service.Add(entity);
        service.Commit();
        return OkResponse<TGet>(entity);
    }

    [HttpPut("{id}")]
    public virtual Response<TGet> Put(TKey id, [FromBody] TPut model)
    {
        var entity = service.FirstOrDefault(g => Equals(g.Id, id));
        if (entity == null)
            throw new NotFoundError();
        mapper.Map(model, entity);
        service.Commit();
        return OkResponse<TGet>(entity);
    }

    [HttpDelete("{id}")]
    public virtual Response<TGet> Delete(TKey id)
    {
        var entity = service.FirstOrDefault(g => Equals(g.Id, id));
        if (entity == null)
            throw new NotFoundError();
        service.Remove(entity);
        service.Commit();
        return OkResponse<TGet>(entity);
    }
}