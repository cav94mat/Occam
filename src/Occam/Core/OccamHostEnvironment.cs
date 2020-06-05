using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Occam.Core
{
    public class OccamHostEnvironment : IOccamHostEnvironment
    {
        private readonly IHostingEnvironment _env;
        public OccamHostEnvironment(IHostingEnvironment env)
        {
            this._env = env;
        }

        string IHostingEnvironment.EnvironmentName
        {
            get => _env.EnvironmentName;
            set => _env.EnvironmentName = value;
        }
        string IHostingEnvironment.ApplicationName
        {
            get => _env.ApplicationName;
            set => _env.ApplicationName = value;
        }
        string IHostingEnvironment.WebRootPath
        {
            get => _env.WebRootPath;
            set => _env.WebRootPath = value;
        }
        IFileProvider IHostingEnvironment.WebRootFileProvider
        {
            get => _env.WebRootFileProvider;
            set => _env.WebRootFileProvider = value;
        }
        string IHostingEnvironment.ContentRootPath
        {
            get => _env.ContentRootPath;
            set => _env.ContentRootPath = value;
        }
        IFileProvider IHostingEnvironment.ContentRootFileProvider
        {
            get => _env.ContentRootFileProvider;
            set => _env.ContentRootFileProvider = value;
        }
    }
}
