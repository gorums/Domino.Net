using System.Text.Json;

namespace Domino.Net.Coin;

internal static class Utils
{
    internal static byte[] ConvertToByte(this Transaction[] lsTrx)
    {
        var transactionsString = JsonSerializer.Serialize(lsTrx);
        return transactionsString.ConvertToBytes();
    }

    internal static byte[] ConvertToBytes(this string arg)
    {
        return System.Text.Encoding.UTF8.GetBytes(arg);
    }
}

