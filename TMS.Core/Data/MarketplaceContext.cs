namespace TMS.Core;

public static class MarketplaceContext
{
    public static IEnumerable<Contract> GetContracts()
    {
        using MySqlConnection connection = new(ConfigurationService.MarketplaceConnectionString);
        return connection.Query<Contract>("SELECT Client_Name ClientName,Job_Type JobType,Quantity,Origin,Destination,Van_Type VanType FROM Contract");
    }
}