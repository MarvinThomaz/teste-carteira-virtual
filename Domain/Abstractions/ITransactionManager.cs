using System;

namespace teste_carteira_virtual.Domain.Abstractions
{
    public interface ITransactionManager
    {
         IDisposable Begin();
         void Commit();
         void Rollback();
    }
}