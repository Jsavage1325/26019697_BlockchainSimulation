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

        public Blockchain()
        {
            Blocks.Add(new Block());
        }

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
