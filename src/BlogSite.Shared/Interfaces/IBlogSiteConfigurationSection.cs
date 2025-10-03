
namespace BlogSite.Shared.Interfaces;

public interface ISharpSiteConfigurationSection
{

	string SectionName { get; }

	Task OnConfigurationChanged(ISharpSiteConfigurationSection? oldConfiguration, IPluginManager pluginManager);

}
