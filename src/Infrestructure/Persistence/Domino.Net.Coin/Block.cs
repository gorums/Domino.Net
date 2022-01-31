using System.Security.Cryptography;

namespace Domino.Net.Coin;

public class Block
{
    public Block(int height, byte[] prevHash, string creator, Transaction[] transactions)
    {
        Height = height;
        PrevHash = prevHash;        
        TimeStamp = DateTime.Now.Ticks;
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
}
