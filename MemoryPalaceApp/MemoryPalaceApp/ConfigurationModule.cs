using System.IO;
using System.Reflection;
using Autofac;
using Newtonsoft.Json;
using Module = Autofac.Module;

namespace MemoryPalaceApp
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Get and deserialize config.json file from Configuration folder.
            var embeddedResourceStream = Assembly.GetAssembly(typeof(IConfiguration)).GetManifestResourceStream("MemoryPalaceApp.Configuration.config.json");
            if (embeddedResourceStream == null)
                return;

            using (var streamReader = new StreamReader(embeddedResourceStream))
            {
                var jsonString = streamReader.ReadToEnd();
                var configuration = JsonConvert.DeserializeObject<Configuration>(jsonString);

                if (configuration == null)
                    return;

                builder.Register<IConfiguration>(c => configuration).SingleInstance();
            }
        }
    }
}