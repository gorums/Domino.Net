using LiteDB;

namespace Domino.Net.Coin;

internal class DbContext
{
    private static LiteDatabase? db;
    public const string DB_NAME = @"node.db";
    public const string TBL_BLOCKS = "tbl_blocks";
    public const string TBL_TRANSACTION_POOL = "tbl_transaction_pool";
    public const string TBL_TRANSACTIONS = "tbl_transactions";

    public static LiteDatabase DB 
    { 
        get 
        {
            if (db == null)
            {
                db = new LiteDatabase(DB_NAME);
            }

            return db;
        }
    }    

    /// <summary>
    /// Delete all rows for all table
    /// </summary>
    public static void ClearDB()
    {
        var coll = DB.GetCollection<Block>(TBL_BLOCKS);
        coll.DeleteAll();

        var coll2 = DB.GetCollection<Transaction>(TBL_TRANSACTION_POOL);
        coll2.DeleteAll();

        var coll3 = DB.GetCollection<Transaction>(TBL_TRANSACTIONS);
        coll3.DeleteAll();
    }

    /// <summary>
    /// Close database when app closed
    /// </summary>
    public static void CloseDB()
    {
        DB.Dispose();
    }
}
