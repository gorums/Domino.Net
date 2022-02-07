using System.Security.Cryptography;
using System.Text;

namespace Domino.Net.Coin;

public class Block
{
    public Block(Block lastBlock, Transaction[] transactions) : 
        this(lastBlock.Height + 1, DateTime.Now.Ticks, lastBlock.Hash, lastBlock.Creator, transactions)
    {
    }

    public Block(int height, Int64 timeStamp, byte[] prevHash, string creator, Transaction[] transactions)
    {
        Height = height;
        PrevHash = prevHash;        
        TimeStamp = timeStamp;
        Transactions = transactions;
        Hash = GenerateHash();
        Creator = creator;
    }    

    /// <summary>
    /// A sequence number of blocks.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// The time when the block was created.
    /// </summary>
    public Int64 TimeStamp { get; private set; }

    /// <summary>
    /// A hash of the previous block.
    /// </summary>
    public byte[] PrevHash { get; private set; }

    /// <summary>
    /// The hash of the block. The hash can be imagined as the unique identity of the block. There are no blocks that have the same hash.
    /// </summary>
    public byte[] Hash { get; private set; }
    

    /// <summary>
    /// Collections of transactions that occur
    /// </summary>
    public Transaction[] Transactions { get; set; }
    
    /// <summary>
    /// Who creates the block identified by the public key.
    /// </summary>
    public string Creator { get; set; }

    /// <summary>
    /// Generate hash of current block
    /// </summary>
    /// <returns></returns>
    public byte[] GenerateHash()
    {
        var sha = SHA256.Create();
        byte[] timeStamp = BitConverter.GetBytes(TimeStamp);

        var transactionHash = Transactions.ConvertToByte();

        byte[] headerBytes = new byte[timeStamp.Length + PrevHash.Length + transactionHash.Length];

        Buffer.BlockCopy(timeStamp, 0, headerBytes, 0, timeStamp.Length);
        Buffer.BlockCopy(PrevHash, 0, headerBytes, timeStamp.Length, PrevHash.Length);
        Buffer.BlockCopy(transactionHash, 0, headerBytes, timeStamp.Length + PrevHash.Length, transactionHash.Length);

        byte[] hash = sha.ComputeHash(headerBytes);

        return hash;
    }

    public static string GetHash(long timestamp, string lastHash, string transactions)
    {
        SHA256 sha256 = SHA256.Create();
        var strSum = timestamp + lastHash + transactions;
        byte[] sumBytes = Encoding.ASCII.GetBytes(strSum);
        byte[] hashBytes = sha256.ComputeHash(sumBytes);
        return Convert.ToBase64String(hashBytes);
    }

    internal static Block Genesis(Transaction[] transactions)
    {
        var ts = new DateTime(2022, 02, 07);
        var block = new Block(1, ts.Ticks, Encoding.ASCII.GetBytes("-"), "system", transactions);
        return block;
    }
}
