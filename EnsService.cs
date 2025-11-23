using Nethereum.Web3;

internal class EnsService(string jsonRpc)
{
    public Web3 Web3 { get; } = new(jsonRpc);

    public async Task<string> GetEnsFromAddress(string ethAddress)
    {
        var ensService = Web3.Eth.GetEnsService();

        string ensName = "";

		try
		{
			ensName = await ensService.ReverseResolveAsync(ethAddress);

		}
        catch (Exception)
        {
            // ignored
        }

        return ensName;
    }
}