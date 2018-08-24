using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace NetCoreKit.Domain
{
  public interface IUnitOfWorkAsync : IRepositoryFactory, IDisposable
  {
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int ExecuteSqlCommand(string sql, params object[] parameters);
    Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
    Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
    int? CommandTimeout { get; set; }
    void Rollback();
  }
}
