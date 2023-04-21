using CyberWork.Accounting.Domain.Entities;
using CyberWork.Accounting.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CyberWork.Accounting.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }


    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Organizations.Any())
        {
            _context.Organizations.Add(new Organization
            {
                Code = "CW",
                Name = "CyberWork",
                ShortName = "CW",
                OrganizationLevel = "CW",
            });
        }

        if (!_context.AppResourceActions.Any())
        {
            _context.AppResourceActions.AddRange(new[]
            {
                new AppResourceAction
                {
                    Code = "Create",
                    Name = "Thêm",
                    Description = "",
                    Status = Status.Active
                },
                new AppResourceAction
                {
                    Code = "Read",
                    Name = "Xem",
                    Description = "",
                    Status = Status.Active
                },
                new AppResourceAction
                {
                    Code = "Update",
                    Name = "Sửa",
                    Description = "",
                    Status = Status.Active
                },
                new AppResourceAction
                {
                    Code = "Delete",
                    Name = "Xoá",
                    Description = "",
                    Status = Status.Active
                }
            });
        }

        if (!_context.AppResources.Any())
        {
            List<Guid> resourceGuids = new()
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
            };

            _context.AppResources.AddRange(new[]
            {
                new AppResource
                {
                    Id = resourceGuids[0],
                    Code = "subjects",
                    Name = "Đối tượng",
                    Description = "",
                    Type = AppResourceType.Group,
                    Category = AppResourceCategory.Category,
                    Status = Status.Active
                },
                new AppResource
                {
                    Code = "customers",
                    Name = "Khách hàng",
                    Description = "",
                    Type = AppResourceType.Item,
                    Category = AppResourceCategory.Category,
                    GroupId = resourceGuids[0],
                    Status = Status.Active
                },
                new AppResource
                {
                    Code = "suppliers",
                    Name = "Nhà cung cấp",
                    Description = "",
                    Type = AppResourceType.Item,
                    Category = AppResourceCategory.Category,
                    GroupId = resourceGuids[0],
                    Status = Status.Active
                },
                new AppResource
                {
                    Code = "employees",
                    Name = "Nhân viên",
                    Description = "",
                    Type = AppResourceType.Item,
                    Category = AppResourceCategory.Category,
                    GroupId = resourceGuids[0],
                    Status = Status.Active
                },
                new AppResource
                {
                    Code = "customeroremployeegroup",
                    Name = "Nhóm khách hàng, nhà cung cấp",
                    Description = "",
                    Type = AppResourceType.Item,
                    Category = AppResourceCategory.Category,
                    GroupId = resourceGuids[0],
                    Status = Status.Active
                },




                 new AppResource
                {
                    Id = resourceGuids[1],
                    Code = "cash",
                    Name = "Tiền mặt",
                    Description = "",
                    Type = AppResourceType.Group,
                    Category = AppResourceCategory.Business,
                    Status = Status.Active
                },
                new AppResource
                {
                    Code = "receipt",
                    Name = "Thu tiền",
                    Description = "",
                    Type = AppResourceType.Item,
                    Category = AppResourceCategory.Business,
                    GroupId = resourceGuids[1],
                    Status = Status.Active
                },
                new AppResource
                {
                    Code = "payment",
                    Name = "Chi tiền",
                    Description = "",
                    Type = AppResourceType.Item,
                    Category = AppResourceCategory.Business,
                    GroupId = resourceGuids[1],
                    Status = Status.Active
                },
                new AppResource
                {
                    Code = "cashreconciliation",
                    Name = "Kiểm kê quỹ",
                    Description = "",
                    Type = AppResourceType.Item,
                    Category = AppResourceCategory.Business,
                    GroupId = resourceGuids[1],
                    Status = Status.Active
                },
                new AppResource
                {
                    Code = "projectedcashflow",
                    Name = "Dự báo dòng tiền",
                    Description = "",
                    Type = AppResourceType.Item,
                    Category = AppResourceCategory.Business,
                    GroupId = resourceGuids[1],
                    Status = Status.Active
                },
            });
        }

        await _context.SaveChangesAsync();
    }
}