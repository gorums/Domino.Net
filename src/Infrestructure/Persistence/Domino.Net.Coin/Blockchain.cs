namespace Domino.Net.Coin;

/// <summary>
/// 
/// </summary>
public class Blockchain
{
    private readonly List<Transaction> transactionPool;

    private readonly IList<Block> blocks;

    /// <summary>
    /// 
    /// </summary>
    public Blockchain()
    {
        blocks = CreateGenesisBlock();
        transactionPool = new List<Transaction>();
    }

    /// <summary>
    /// 
    /// </summary>
    public void CreateBlock()
    {
        var lastBlock = GetLastBlock();
        var nextHeight = lastBlock.Height + 1;
        var prevHash = lastBlock.Hash;

        var transactions = this.GetTransationsFromThePool();
        var block = new Block(nextHeight, prevHash, "Admin", transactions);

        blocks.Add(block);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task AddTransactionToPoolAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        transactionPool.Add(transaction);

        return Task.CompletedTask;
    }    

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private List<Block> CreateGenesisBlock()
    {
        var lst = new List<Transaction>
        {
            new Transaction("System", "Genesis Account", 1000)
        };

        var trxByte = lst.ToArray().ConvertToByte();
        return new List<Block> { new Block(1, String.Empty.ConvertToBytes(), "Admin", lst.ToArray()) };
    }
    
    /// <summary>
    /// Get last block of blockchain    
    /// </summary>
    /// <returns></returns>
    private Block GetLastBlock()
    {
        return blocks[blocks.Count - 1];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private Transaction[] GetTransationsFromThePool(int amount = 200)
    {
        var transations = transactionPool.ToArray();
        transactionPool.Clear();

        return transations;
    }       
}