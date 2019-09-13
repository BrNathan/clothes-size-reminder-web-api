using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces.Repositories;
using DAL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public static class DependenciesManager
    {
        /// <summary>
        /// Register all the available types (Services & repositories)
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterDependencies(IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterServices(services);
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IClothesCategoryRepository, ClothesCategoryRepository>();
            services.AddScoped<IClothesRepository, ClothesRepository>();
            services.AddScoped<IClothesSizeRepository, ClothesSizeRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IClothesCategoryService, ClothesCategoryService>();
            services.AddScoped<IClothesService, ClothesService>();
            services.AddScoped<IClothesSizeService, ClothesSizeService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
