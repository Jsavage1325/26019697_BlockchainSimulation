using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

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
        String merkleRoot;


        public Block(int prevIndex, String previousHash, List<Transaction> pendingTransactions, String MinerAddress)
        {
            // create stopwatch instance and start
            Stopwatch s = new Stopwatch();
            s.Start();

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
            // Simple reward of one coin for now (will also be plus fees)
            reward = 1;
            minerAddress = MinerAddress;
            sendRewardTransaction(reward, transactionList, minerAddress);
            merkleRoot = calcMerkleRoot(transactionList);
            // this should automatically set the value of the hash
            MineThreaded();

            // Stop timer and print time taken
            s.Stop();
            TimeSpan ts = s.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        public Block()
        {
            index = 0;
            time = DateTime.Now;
            prevHash = "";
            hash = Mine();
        }

        private void startHashThreads()
        {
            int numThreads = 6;
            bool done = false;
            object locker = new object();
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < numThreads; i++)
            {
                Thread t = new Thread(() => calculateHashThreaded(i, locker, done));
                threads.Add(t);
                t.Start();
            }
            threads.WaitAll();
            
        }

        private void calculateHashThreaded(int threadNo, object locker, bool done)
        {

            while (!done)
            {
                
                String hash = "";
                int myNonce;
                // saves to myNonce then increments for next process to take next nonce
                lock (locker)
                {
                    myNonce = this.nonce++;
                }
                // Thread.Sleep(1000);
                // this is the main computation in the thread, done on my nonce
                hash = CreateHashWithThread(myNonce);
                string difficultyCheck = hash.Substring(0, (int)difficulty);
                string difficultyString = "";
                for (int i = 0; i < difficulty; i++)
                {
                    difficultyString += "0";
                }
                // double check for not done - if done then we need to NOT accidentally increment the nonce etc
                lock (locker)
                {
                    if (difficultyCheck == difficultyString && !done)
                    {
                        done = true;
                        // set the nonce equal to the nonce used in this thread
                        this.nonce = myNonce;
                        this.hash = hash;
                    }
                }
            }
        }
    


        private void MineThreaded()
        {
            startHashThreads();
        }

        private String Mine()
        {

            bool done = false;
            String hash = "";
            while (!done)
            {
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
                }
                else
                {
                    nonce++;
                }
            }
            return hash;
        }

        private void sendRewardTransaction(double reward, List<Transaction> tList, String minerAddress)
        {
            double fees = 0;
            foreach (Transaction transaction in tList)
            {
                fees += transaction.getFee();
            }
            this.fees = fees;
            Transaction t = new Transaction("Mine Rewards", minerAddress, (reward + fees), 0, "");
            tList.Add(t);
        }


        private String CreateHashWithThread(int nonce)
        {
            SHA256 hasher;
            hasher = SHA256Managed.Create();
            String input = index.ToString() + time.ToString() + prevHash + nonce + difficulty + reward + merkleRoot;
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes((input)));

            String hash = string.Empty;

            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }
            return hash;
        }

        private String CreateHash()
        {
            SHA256 hasher;
            hasher = SHA256Managed.Create();
            String input = index.ToString() + time.ToString() + prevHash + nonce + difficulty + reward + merkleRoot;
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
            String data = "Block Index: " + this.index.ToString() + "\nTimestamp: " + this.time.ToString() + "\nHash: " + this.hash + "\nPrevious Hash: " + this.prevHash + "\nNonce: " + nonce + " \nDifficulty Level: " + difficulty;
            data += "\nReward: " + this.reward.ToString() + "\nFees: " + this.fees + "\nTotal Reward: " + (this.reward + this.fees) + "\nMiner Address: " + this.minerAddress;
            data += "\nMerkle Root: " + this.merkleRoot;
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

        public List<Transaction> getAllTransactions()
        {
            return transactionList;
        }


        public String calculateMerkleRoot(List<Transaction> transactionList)
        {
            return calcMerkleRoot(transactionList);
        }

        private static string calcMerkleRoot(List<Transaction> transactionList)
        {
            List<String> hashList = new List<String>();
            for (int i = 0; i < transactionList.Count(); i++)
            {
                hashList.Add(transactionList[i].getHash());
            }
            // now we can pass the list of hashes as leaves of the merkel tree
            return MerkleRoot(hashList);
        }

        private static string MerkleRoot(List<String> merkelLeaves)
        {
            
            // if empty then return null. This should never happen, but better safe then sorry...
            if (merkelLeaves == null || !merkelLeaves.Any())
            {
                return string.Empty;
            }
            // if count is one, then return
            if (merkelLeaves.Count() == 1)
            {
                return merkelLeaves.First();
            }
            // if odd number, we can add an empty string to the final one, so they will not error on concat
            if (merkelLeaves.Count() % 2 > 0)
            {
                merkelLeaves.Add("");
            }

            // create a new tree with branches, which we can recurse for
            List<String> merkleBranches = new List<string>();

            for (int i = 0; i < merkelLeaves.Count(); i += 2)
            {
                // Calculate new hash, and add as merkle branch
                merkleBranches.Add(HashCode.HashTools.combineHash(merkelLeaves[i], merkelLeaves[i+1]));
            }
            // recurse until single hash and count == 1, will activate first return statement
            return MerkleRoot(merkleBranches);
        }

        public String getMerkleRoot()
        {
            return merkleRoot;
        }

        public int getIndex()
        {
            return index;
        }
    }
}
