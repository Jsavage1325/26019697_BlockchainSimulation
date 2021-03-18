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
        int difficulty;
        int newDifficulty;
        double reward;
        String minerAddress;
        double fees = 0;
        String merkleRoot;
        TimeSpan expectedBlockTime = TimeSpan.FromMinutes(2);
        TimeSpan blockTime;


        public Block(int prevIndex, String previousHash, List<Transaction> pendingTransactions, String MinerAddress, String transactionSelection, String preferredAddress, int prevDifficulty)
        {
            difficulty = prevDifficulty;
            // create stopwatch instance and start
            Stopwatch s = new Stopwatch();
            s.Start();

            time = DateTime.Now;
            index = prevIndex++;
            prevHash = previousHash;
            /// before the hash is generated we are going to want to add up to 5 of the pending transactions to our block
            getTransactions(pendingTransactions, transactionSelection, preferredAddress);

            // Simple reward of one coin for now (will also be plus fees)
            reward = 1;
            minerAddress = MinerAddress;
            sendRewardTransaction(minerAddress);
            merkleRoot = calcMerkleRoot(transactionList);
            // this should automatically set the value of the hash
            MineThreaded();

            // Stop timer and print time taken
            s.Stop();
            TimeSpan ts = s.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            // set blocktime equal to the time taken to mine
            blockTime = ts;
            Console.WriteLine("RunTime " + elapsedTime);
            // change the difficulty at the end of the block
            // increaseDecreaseDifficulty();
        }

        public Block()
        {
            index = 0;
            time = DateTime.Now;
            prevHash = "";
            hash = Mine();
            // difficulty = 5 to start
            difficulty = 5;
        }

        public TimeSpan getBlockTime()
        {
            return blockTime;
        }

        // this function checks to see if the block time was less/more than expected
        public void increaseDecreaseDifficulty()
        {
            if (blockTime < expectedBlockTime)
            {
                // increase difficulty for next block
                newDifficulty = difficulty + 1;
            }
            else if (blockTime > expectedBlockTime)
            {
                // decrease difficulty for next block
                newDifficulty = difficulty - 1;
            }
        }


        // this function gets the transactions for the block using the preferred method from the gui
        public void getTransactions(List<Transaction> pendingTransactions, String transactionSelection, String preferredAddress)
        {
            // FIFO (first in first out method)
            if (transactionSelection == "Altruistic")
            {
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
            }
            // Greedy (highest fee first) method
            else if (transactionSelection == "Greedy")
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        // loop trhough getting the highest fee and the index of the highest fee
                        double highestFee = -1;
                        int indexMaxFee = -1;
                        foreach (Transaction t in pendingTransactions)
                        {
                            // replace values if fee is higher
                            if (t.getFee() > highestFee)
                            {
                                highestFee = t.getFee();
                                indexMaxFee = pendingTransactions.FindIndex(a => a.getFee() == highestFee);
                            }
                        }
                        // add and remove at index of highest fee
                        transactionList.Add(pendingTransactions[indexMaxFee]);
                        pendingTransactions.RemoveAt(indexMaxFee);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            // Address Preference - gets preferred sender address. if none available does altruistic
            else if (transactionSelection == "Address Preference")
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        // find transaction with preferred address
                        index = pendingTransactions.FindIndex(a => a.getSender() == preferredAddress);
                        if (index == -1)
                        {
                            // If there is none with matching, do Altruistic search
                            transactionList.Add(pendingTransactions[0]);
                            pendingTransactions.RemoveAt(0);
                        }
                        else
                        {
                            // do index search based off of index via preferred address
                            transactionList.Add(pendingTransactions[index]);
                            pendingTransactions.RemoveAt(index);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            // Random - randomly select transactions to add
            else if (transactionSelection == "Random")
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        // generate random number between 0 and list length
                        Random rnd = new Random();
                        int value = rnd.Next(0, pendingTransactions.Count() - 1);
                        transactionList.Add(pendingTransactions[value]);
                        pendingTransactions.RemoveAt(value);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }

        
        // This function starts threads to hash with increased speed
        private void startHashThreads()
        {
            // create number of threads, a done flag and a locker object
            int numThreads = 4;
            bool done = false;
            object locker = new object();
            // create an empty list of threads to add all the threads to
            List<Thread> threads = new List<Thread>();
            // loop through number of threads and create a new thread with calculateHashThreaded
            for (int i = 0; i < numThreads; i++)
            {
                // add thread with method
                Thread t = new Thread(() => calculateHashThreaded(i, locker, done));
                // add thread to list
                threads.Add(t);
                // start thread
                t.Start();
            }
            threads.WaitAll();
            
        }

        // this is the function that will be run by each of the threads
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

        public int getNewDifficulty()
        {
            return newDifficulty;
        }
    
        private void MineThreaded()
        {
            startHashThreads();
        }

        // original mine function create to mine on a single thread
        private String Mine()
        {
            // done flag is set to false and hash is set to empty
            bool done = false;
            String hash = "";
            // while loop, exit condition = done
            while (!done)
            {
                // creates a hash
                hash = CreateHash();
                // now we check to see if the hash meets the difficulty level
                string difficultyCheck = hash.Substring(0, (int)difficulty);
                string difficultyString = "";
                for (int i = 0; i < difficulty; i++)
                {
                    difficultyString += "0";
                }
                if (difficultyCheck == difficultyString)
                {
                    // if found set done to true
                    done = true;
                }
                else
                {
                    // if not found increment hash
                    nonce++;
                }
            }
            return hash;
        }

        // this sends the transaction reward to the minerAddress
        private void sendRewardTransaction(String minerAddress)
        {
            // fees are set to 0
            double fees = 0;
            // fees are added for each transaction that will be added
            foreach (Transaction transaction in this.transactionList)
            {
                fees += transaction.getFee();
            }
            // set fees
            this.fees = fees;
            // add miner transaction based on fees
            Transaction t = new Transaction("Mine Rewards", minerAddress, (this.reward + fees), 0, "");
            // add mining transaction to list
            this.transactionList.Add(t);
        }


        // this function creates a hash with a thread (similar to below but uses nonce)
        // this function is not commented as CreateHash is and does the same
        private String CreateHashWithThread(int nonce)
        {
            SHA256 hasher;
            hasher = SHA256Managed.Create();
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes((index.ToString() + time.ToString() + prevHash + nonce + difficulty + reward + merkleRoot)));

            String hash = string.Empty;

            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }
            return hash;
        }

        // creates a hash using values from the block
        private String CreateHash()
        {
            // create a hasher
            SHA256 hasher = SHA256Managed.Create();
            // create a byte type hash
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes((index.ToString() + time.ToString() + prevHash + nonce + difficulty + reward + merkleRoot)));

            String hash = string.Empty;
            // convert from byte to hash
            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }
            return hash;
        }

        // gets all the data about the block. Used for the GUI
        public String getData()
        {
            // get info about block and add to string
            String data = "Block Index: " + this.index.ToString() + "\nTimestamp: " + this.time.ToString() + "\nHash: " + this.hash + "\nPrevious Hash: " + this.prevHash + "\nNonce: " + nonce + " \nDifficulty Level: " + difficulty;
            data += "\nReward: " + this.reward.ToString() + "\nFees: " + this.fees + "\nTotal Reward: " + (this.reward + this.fees) + "\nMiner Address: " + this.minerAddress;
            data += "\nMerkle Root: " + this.merkleRoot;
            // get data about every transaction in the block
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
            // create a new empty list of hashes
            List<String> hashList = new List<String>();
            // add all the hashes to the list
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
