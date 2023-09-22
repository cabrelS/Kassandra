
namespace Kassandra.Models;

public class KassandraDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ShortsCollectionName { get; set; } = null!;
    public string KeyCollectionName { get; set; } = null!;
}
