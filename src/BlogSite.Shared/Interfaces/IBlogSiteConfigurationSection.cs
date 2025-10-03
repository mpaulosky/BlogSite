
namespace BlogSite.Shared.Interfaces;

public interface IBlogSiteConfigurationSection
{

	string SectionName { get; }

	Task OnConfigurationChanged(IBlogSiteConfigurationSection? oldConfiguration, IPluginManager pluginManager);

}