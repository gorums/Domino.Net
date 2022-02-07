using LiteDB;

namespace Domino.Net.Coin;

/// <summary>
/// 
/// </summary>
public class Blockchain
{
    /// <summary>
    /// 
    /// </summary>
    public Blockchain()
    {
        Initialize();
    }

    private static void Initialize()
    {
        var blocks = GetBlocks();
        // blocks.EnsureIndex(x => x.Height);

        if (blocks.Count() < 1)
        {
            // create genesis transaction and block
            var transactions = CreateGenesisTransction();

            // Create genesis block
            var gnsBlock = Block.Genesis(transactions);
            blocks.Insert(gnsBlock);            
        }
    }

    public static ILiteCollection<Block> GetBlocks()
    {
        var coll = DbContext.DB.GetCollection<Block>(DbContext.TBL_BLOCKS);
        coll.EnsureIndex(x => x.Height);
        return coll;
    }

    public static Block GetGenesisBlock()
    {
        var blockchain = GetBlocks();
        var block = blockchain.FindOne(Query.All(Query.Ascending));
        return block;
    }

    public static Block GetLastBlock()
    {
        var blockchain = GetBlocks();
        var block = blockchain.FindOne(Query.All(Query.Descending));
        return block;
    }

    public static int GetHeight()
    {
        var lastBlock = GetLastBlock();
        return lastBlock.Height;
    }    

    /// <summary>
    /// 
    /// </summary>
    public void CreateBlock()
    {
        var lastBlock = GetLastBlock();

        var trxPool = Transaction.GetPool();
        var transactions = trxPool.FindAll();

        var block = new Block(lastBlock, transactions.ToArray());

        AddBlock(block);

        // clear mempool
        trxPool.DeleteAll();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task AddTransactionToPoolAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var trxPool = Transaction.GetPool();
        trxPool.Insert(transaction);

        return Task.CompletedTask;
    }

    private static void AddBlock(Block block)
    {
        var blocks = GetBlocks();
        blocks.Insert(block);
    }

    private static Transaction[] CreateGenesisTransction()
    {
        var newTrx = new Transaction("system", "ga1", 1000000);
        Transaction.AddToPool(newTrx);

        var trxPool = Transaction.GetPool();

        var transactions = trxPool.FindAll();

        var lastBlock = GetLastBlock();
        var block = new Block(lastBlock, transactions.ToArray());

        // add block to blockchain
        AddBlock(block);

        // move all record from trx pool to transactions table
        foreach (Transaction trx in transactions)
        {
            Transaction.Add(trx);
        }

        // clear mempool
        trxPool.DeleteAll();

        return transactions.ToArray();
    }
}