﻿using TimeTable203.Common;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context;
using TimeTable203.Repositories;
using TimeTable203.Services;
using TimeTable203.Shared;

namespace TimeTable203.Api.Infrastructures
{
    static internal class ServiceCollectionExtensions
    {
        public static void AddDependencies(this IServiceCollection service)
        {
            service.AddTransient<IDateTimeProvider, DateTimeProvider>();
            service.AddTransient<IDbWriterContext, DbWriterContext>();
            service.RegisterAutoMapperProfile<ApiAutoMapperProfile>();
            service.AddTransient<IApiValidatorService, ApiValidatorService>();

            service.RegisterModule<ServiceModule>();
            service.RegisterModule<RepositoryModule>();
            service.RegisterModule<ContextModule>();

            service.RegisterAutoMapper();
        }
    }
}
