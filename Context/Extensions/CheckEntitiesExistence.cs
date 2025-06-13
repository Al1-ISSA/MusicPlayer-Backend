using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Exceptions;

namespace MusicBackend.Context.Extensions;

public static class CheckEntitiesExistence
{
    public static async Task<TEnt> CheckEntityExistenceAsync<TEnt>(this IQueryable<TEnt> source, 
        Expression<Func<TEnt,bool>> predicate,object key, CancellationToken cancellationToken = default) where TEnt : class
    {
        var entity = await source.FirstOrDefaultAsync(predicate, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(typeof(TEnt).Name, key);
        }
        return entity;
    }
    
    public static async Task<TEnt> CheckEntityAlreadyExistsAsync<TEnt>(this IQueryable<TEnt> source, 
        Expression<Func<TEnt,bool>> predicate,object key,string attribute, CancellationToken cancellationToken = default) where TEnt : class
    {
        var entity = await source.FirstOrDefaultAsync(predicate, cancellationToken);
        if (entity != null)
        {

            throw new AlreadyExistsException(typeof(TEnt).Name, attribute, key);
        }

        return entity;
    }
}