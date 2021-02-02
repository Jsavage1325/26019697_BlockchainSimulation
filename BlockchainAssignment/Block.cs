using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Block
    {
        DateTime time;
        int index;
        String hash;
        String prevHash;
        List<Transaction> transactionList = new List<Transaction>();
        int nonce = 0;
        double difficulty = 5;
        double reward;
        String minerAddress;
        double fees = 0;


        public Block(int prevIndex, String previousHash, List<Transaction> pendingTransactions, String MinerAddress)
        {
            time = DateTime.Now;
            index = prevIndex++;
            prevHash = previousHash;
            /// before the hash is generated we are going to want to add up to 5 of the pending transactions to our block
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    /// ensure that the bottom most element is always added
                    transactionList.Add(pendingTransactions[0]);
                    pendingTransactions.RemoveAt(0);
                }
                catch
                {
                    break;
                }
            }
            // We can change the reward later on

            // Simple reward of one coin for now
            reward = 1;
            minerAddress = MinerAddress;
            sendRewardTransaction(reward, transactionList, minerAddress);
            hash = Mine();
        }

        public Block()
        {
            index = 0;
            time = DateTime.Now;
            prevHash = "";
            hash = Mine();
        }

        private String Mine()
        {
            bool done = false;
            String hash = "";
            while (!done) {
                hash = CreateHash();
                string difficultyCheck = hash.Substring(0, (int)difficulty);
                string difficultyString = "";
                for (int i = 0; i < difficulty; i++)
                {
                    difficultyString += "0";
                }
                if (difficultyCheck == difficultyString)
                {
                    done = true;
                } else
                {
                    nonce++;
                }
            }
            Console.Out.WriteLine("Successfully mined: " + hash);
            return hash;
        }

        private void sendRewardTransaction(double reward, List<Transaction> tList, String minerAddress)
        {
            double fees = 0;
            foreach (Transaction transaction in tList)
            {
                fees += transaction.getFee();
                this.fees = fees;
            }
            Transaction t = new Transaction("Miner Rewards", minerAddress, (reward + fees), 0, "");
            tList.Add(t);
        }

        private String CreateHash()
        {
            SHA256 hasher;
            hasher = SHA256Managed.Create();
            String input = index.ToString() + time.ToString() + prevHash + nonce + difficulty + reward;
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes((input)));

            String hash = string.Empty;

            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }
            return hash;
        }

        public String getData()
        {
            String data = "Block Index: " + index.ToString() + "\nTimestamp: " + time.ToString() + "\nHash: " + hash + "\nPrevious Hash: " + prevHash + "\nNonce: " + nonce + " \nDifficulty Level: " + difficulty;
            data += "\nReward: " + reward.ToString() + "\nFees: " + fees + "\nTotal Reward: " + (reward + fees) + "\nMiner Address: " + minerAddress;
            foreach (Transaction t in transactionList)
            {
                data += "\n\n" + t.getTransactionData();
            }
            return data;
        }

        public String getPrevHash()
        {
            return prevHash;
        }

        public String getHash()
        {
            return hash;
        }

    }
}
