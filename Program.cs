using Spectre.Console;

AnsiConsole.Write(new FigletText("ENS Reverse Resolver").Color(Color.Gold1));

var rpcUrl = AnsiConsole.Ask<string>("Enter RPC url:");
var addressesPath = AnsiConsole.Ask<string>("Enter the path to the ETH addresses:");

if (!File.Exists(addressesPath))
{
    AnsiConsole.MarkupLine("[red]File not found![/]");
    return;
}

var ensService = new EnsService(rpcUrl);

foreach (var address in File.ReadLines(addressesPath))
{
    string resolvedEns = await ensService.GetEnsFromAddress(address);

    if (string.IsNullOrEmpty(resolvedEns))
        continue;

    var line = new[]
    {
        address,
        resolvedEns
    };

    var content = string.Join(":", line) + "\n";
    File.AppendAllText("result.txt", content);
}

AnsiConsole.MarkupLine("[cyan]Results saved to result.txt[/]");
Console.ReadKey();