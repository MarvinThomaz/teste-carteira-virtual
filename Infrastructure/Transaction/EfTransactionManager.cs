using System;
using Microsoft.EntityFrameworkCore.Storage;
using teste_carteira_virtual.Domain.Abstractions;
using teste_carteira_virtual.Infrastructure.Context;

namespace teste_carteira_virtual.Infrastructure.Transaction
{
    public class EfTransactionManager : ITransactionManager
    {
        private readonly DatabaseContext _context;
        private IDbContextTransaction _transaction;

        public EfTransactionManager(DatabaseContext context)
        {
            _context = context;
        }

        public IDisposable Begin()
        {
            return (_transaction = _context.Database.BeginTransaction());
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }
    }
}