using Microsoft.Extensions.Hosting;

namespace BlogSite.Shared.Interfaces;

/// <summary>
/// Interface for services that need to register services with the web application.
/// </summary>
public interface IRegisterServices
{

	IHostApplicationBuilder RegisterServices(IHostApplicationBuilder services, bool disableRetry = false);

}