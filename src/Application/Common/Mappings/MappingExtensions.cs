using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyberWork.Accounting.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Common.Mappings;

public static class MappingExtensions
{
    public static IMappingExpression<TSource, TDestination>
        IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource,
            TDestination> expression)
    {
        var flags = BindingFlags.Public | BindingFlags.Instance;
        var sourceType = typeof(TSource);
        var destinationProperties = typeof(TDestination).GetProperties(flags);

        foreach (var property in destinationProperties)
        {
            var value = sourceType.GetProperty(property.Name, flags);
            if (value == null)
                expression.ForMember(property.Name, opt => opt.Ignore());
        }

        return expression;
    }

    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize,
            CancellationToken cancellationToken)
        where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(),
        pageNumber, pageSize, cancellationToken);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
        this IQueryable queryable, IConfigurationProvider configuration,
            CancellationToken cancellationToken)
        where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking()
            .ToListAsync(cancellationToken);
}
