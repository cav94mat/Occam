using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Occam.Core
{
    internal class OccamHost: IHost
    {
        private readonly IHost _host;
        private readonly IHostingEnvironment _env;
        private readonly ILogger _log;
        public OccamHost(IHost host, IHostingEnvironment env, ILogger<OccamHost> log)
        {
            _host = host;
            _env = env;
            _log = log;
        }        
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {            

            await StopAsync(cancellationToken);
        }        
        public void Dispose()
            => _host.Dispose();

        public Task StopAsync(CancellationToken cancellationToken)
            => _host.StopAsync(cancellationToken);

        IServiceProvider IHost.Services
            => _host.Services;        
        
    }
}
