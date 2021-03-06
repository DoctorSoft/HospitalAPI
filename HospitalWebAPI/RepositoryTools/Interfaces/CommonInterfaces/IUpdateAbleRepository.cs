﻿using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces.CommonInterfaces
{
    public interface IUpdateAbleRepository<T> : IReadOnlyRepository<T> 
        where T : class, IIdModel
    {
        void Update(int id, T model);

        int SaveChanges();
    }
}
