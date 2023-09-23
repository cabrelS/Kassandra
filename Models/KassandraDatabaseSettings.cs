
namespace Kassandra.Models;

public class KassandraDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ShortsCollectionName { get; set; } = null!;
    public string KeysCollectionName { get; set; } = null!;
}
