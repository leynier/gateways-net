using AutoMapper;
using Gateways.Business.Contracts.Contracts;
using Gateways.Business.Contracts.Services;
using Gateways.Common.Errors;
using Gateways.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Common.Controllers;

public class CrudApiControllerBase<TEntity, TGet, TGetDetails, TPost, TPut, TKey> : ApiControllerBase where TEntity : class, IEntity<TKey>
{
    private readonly Func<IQueryable<TEntity>, IQueryable<TEntity>>? includer;
    private readonly Func<IQueryable<TEntity>, IQueryable<TEntity>>? order;
    protected readonly IService<TEntity> service;

    public CrudApiControllerBase(
        IService<TEntity> service,
        IMapper mapper,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includer = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? order = null) : base(mapper)
    {
        this.includer = includer;
        this.order = order;
        this.service = service;
    }

    [HttpGet]
    public virtual PaginatedResponse<TGet> GetAll([FromQuery] PaginationQueryModel pagination)
    {
        var skip = pagination.Page * pagination.PageSize;
        var take = pagination.PageSize;
        var query = service.Query().AsNoTrackingWithIdentityResolution();
        if (order != null)
            query = order(query);
        var models = query.Skip(skip).Take(pagination.PageSize).ToList();
        var hasPrevious = skip > 0;
        var hasNext = models.Count == pagination.PageSize && query.Skip(skip + take).Any();
        return OkResponse<TGet>(models, hasPrevious, hasNext);
    }

    [HttpGet("{id}")]
    public virtual Response<TGetDetails> Get(TKey id)
    {
        IQueryable<TEntity> query = service.Query().AsNoTrackingWithIdentityResolution();
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
        var entity = service.Query().FirstOrDefault(g => Equals(g.Id, id));
        if (entity == null)
            throw new NotFoundError();
        mapper.Map(model, entity);
        service.Commit();
        return OkResponse<TGet>(entity);
    }

    [HttpDelete("{id}")]
    public virtual Response<TGet> Delete(TKey id)
    {
        var entity = service.Query().FirstOrDefault(g => Equals(g.Id, id));
        if (entity == null)
            throw new NotFoundError();
        service.Remove(entity);
        service.Commit();
        return OkResponse<TGet>(entity);
    }
}