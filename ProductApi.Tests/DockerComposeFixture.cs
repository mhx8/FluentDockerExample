using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;

namespace ProductApi.Tests;

public class DockerComposeFixture : IDisposable
{
    private ICompositeService? _compositeService;
    private IHostService? _dockerHost;
    private bool _isDisposed;

    public void InitDockerHost(
        string dockerComposePath)
    {
        EnsureDockerHost();
        Build(dockerComposePath)
            .Start();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(
        bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing)
        {
            _compositeService?.Dispose();
            _dockerHost?.Dispose();
            _compositeService = null;
            _dockerHost = null;
        }

        _isDisposed = true;
    }

    private DockerComposeCompositeService Build(
        string dockerComposeFilePath)
        => new(
            _dockerHost,
            new DockerComposeConfig
            {
                ComposeFilePath = [dockerComposeFilePath],
                ForceRecreate = true,
                RemoveOrphans = true,
                StopOnDispose = true
            });

    private void EnsureDockerHost()
    {
        while (true)
        {
            if (_dockerHost?.State == ServiceRunningState.Running)
            {
                return;
            }

            IList<IHostService> hosts = new Hosts().Discover();
            _dockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
            if (_dockerHost != null)
            {
                if (_dockerHost.State != ServiceRunningState.Running)
                {
                    _dockerHost.Start();
                }

                return;
            }

            if (hosts.Count > 0)
            {
                _dockerHost = hosts[0];
            }

            if (_dockerHost != null)
            {
                return;
            }
        }
    }
}