using Newtonsoft.Json;

namespace MemoryPalaceApp
{
    public interface IConfiguration
    {
        string ApiBaseAddress { get; set; }
    }

    public class Configuration : IConfiguration
    {
        [JsonConstructor]
        public Configuration()
        {
        
        }
    
        public string ApiBaseAddress { get; set; }
    }
}