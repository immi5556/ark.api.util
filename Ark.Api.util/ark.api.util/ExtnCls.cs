using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ark.net.util
{
    public static class ServiceActivator
    {
        internal static IServiceProvider _serviceProvider = null;
        public static void UseArkUpload(this IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices;
        }
        public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
        {
            var provider = serviceProvider ?? _serviceProvider;
            return provider?
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
        }
    }
    public static class ExtnCls
    {
        public static void AddArkUpload(this IServiceCollection service)
        {
            service.AddArkUpload(new UploadConfig() { FullPath = System.IO.Path.Combine(Environment.CurrentDirectory, "wwwroot", "Upload") });
        }
        public static void AddArkUpload(this IServiceCollection service, UploadConfig config)
        {
            service.AddSingleton<UploadConfig>(config);
        }
        public static void AddArkUpload(this IServiceCollection service, string fullpath)
        {
             service.AddArkUpload(new UploadConfig() { FullPath = fullpath });
        }
        public static void AddArkUpload(this IServiceCollection service, IWebHostEnvironment env)
        {
            service.AddArkUpload(new UploadConfig() { FullPath = System.IO.Path.Combine(env.WebRootPath, "Upload") });
        }
    }
    public class UploadConfig
    {
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
    }
}
