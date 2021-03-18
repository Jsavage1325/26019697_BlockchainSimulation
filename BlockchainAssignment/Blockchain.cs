using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Blockchain
    {
        List<Block> Blocks = new List<Block>();
        public List<Transaction> pendingTransactions = new List<Transaction>();

        // this adds a new block to the blockchain (unused in final product)
        public Blockchain()
        {
            Blocks.Add(new Block());
        }

        // gets the data for a specific block using block.getData()
        public String getBlockData(int index)
        {
            return Blocks[index].getData();
        }


        public Block getLastBlock()
        {
            return Blocks[Blocks.Count - 1];
        }

        public int getBlockchainSize()
        {
            return Blocks.Count;
        }

        public void addBlock(Block b)
        {
            Blocks.Add(b);
        }

        public List<Block> getAllBlocks()
        {
            return Blocks;
        }

        // lists all the pending transactions in the blockchain
        public String listPendingTransactions()
        {
            String pendingTransactionData = "";
            foreach (Transaction t in pendingTransactions)
            {
                pendingTransactionData += t.getTransactionData() + "\n\n";
            }
            return pendingTransactionData;
        }
    }
}
