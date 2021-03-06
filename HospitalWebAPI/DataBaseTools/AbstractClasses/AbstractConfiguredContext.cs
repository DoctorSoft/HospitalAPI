﻿using System.Collections.Generic;
using System.Data.Entity;
using DataBaseTools.Interfaces;
using EntityFramework.BulkInsert.Extensions;

namespace DataBaseTools.AbstractClasses
{
    public abstract class AbstractConfiguredContext : DbContext, IDataBaseContext
    {
        private readonly IDataBaseConfigurationFactory _configurationFactory;

        protected AbstractConfiguredContext(string databaseName, IDataBaseConfigurationFactory configurationFactory)
            : base(databaseName)
        {
            this._configurationFactory = configurationFactory;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(_configurationFactory.GetConfigurations(modelBuilder));
        }
    }
}
