using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MusicInside.Batch.Importer.Implementations;
using MusicInside.Batch.Importer.Interfaces;
using MusicInside.DataAccessLayer.Context;
using NLog.Extensions.Logging;
using System.IO;

namespace MusicInside.Batch.Importer.Infrastructure
{
    public class BatchDependencyInjection
    {
        public static ServiceProvider BuildContainer()
        {
            // Retrieve base path
            string BasePath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            // Set up fon configuration sources
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            IConfiguration conf = confBuilder.Build();

            return new ServiceCollection()
                .AddLogging(builder => {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                })
                .AddDbContext<MusicInsideDbContext>(options => options.UseSqlServer(conf.GetConnectionString("MusicInsideDatabase")))
                .Configure<MusicFilesOptions>(conf.GetSection("MusicFiles"))
                .AddSingleton<IFlowHelper, FlowHelper>()
                .AddSingleton<IDbHelper, DbHelper>()
                .BuildServiceProvider();
        }
    }
}
