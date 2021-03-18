using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockchainAssignment.Wallet;

namespace BlockchainAssignment
{
    public partial class BlockchainApp : Form
    {
        Blockchain blockchain;

        // initial contructor, initialises the gui and the blockchain
        public BlockchainApp()
        {
            InitializeComponent();
            blockchain = new Blockchain();
            richTextBox1.Text = "New blockchain initialised!";
        }

        // default form loader is required - left empty
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // this function is for printing the current block
        private void button1_Click(object sender, EventArgs e)
        {
            /// todo add error handling here to sanitise the input to ensure we only get int values, within the range of blocks that are created
            updateRichTextBox(blockchain.getBlockData(Int32.Parse(textBox1.Text)));
        }

        // updates the main text box, used throughout
        private void updateRichTextBox(String value)
        {
            richTextBox1.Text = value;
        }

        // validates the keys using the built in validation
        private void validateKeys_Click(object sender, EventArgs e)
        {
            //validate and update richtextbox with results
            bool validated = Wallet.Wallet.ValidatePrivateKey(privateKey.Text, publicKey.Text);
            if (validated == true)
            {
                richTextBox1.Text = "Keys validated successfully";
            }
            else
            {
                richTextBox1.Text = "Failed to validate keys.";
            }
        }

        // creates a new wallet
        private void createWallet_Click_1(object sender, EventArgs e)
        {
            String privKey;
            // create the new wallet, assigning the private key
            Wallet.Wallet myNewWallet = new Wallet.Wallet(out privKey);
            // get the public key from the wallet.publicID
            String pubKey = myNewWallet.publicID;
            // set the values in the gui
            privateKey.Text = privKey;
            publicKey.Text = pubKey;
        }

        // make a transaction
        private void makeTransaction_Click(object sender, EventArgs e)
        {           

            // We need to make sure that they are checked on being added to the transactionList? Or do we need to also check pending transations..
            if (Convert.ToDouble(amount.Text) <= getBalance())
            {
                Transaction t = new Transaction(publicKey.Text, address.Text, Double.Parse(amount.Text), Double.Parse(fee.Text), privateKey.Text);
                /// add the new transaction to the pending transactions
                blockchain.pendingTransactions.Add(t);
                richTextBox1.Text = t.getTransactionData();
            }
            else
            {
                richTextBox1.Text = "Not enough coins in wallet to make transaction.";
            }
            
        }

        // generate a block
        private void generateBlock_Click(object sender, EventArgs e)
        {
            // string to hold type of transaction selector
            String transactionSelection;
            // if miningSettings are unchosen, use altruistic
            if (miningSettings.SelectedValue == null)
            {
                transactionSelection = "Altruistic";
            }
            // otherwise get from the selected value
            else
            {
                transactionSelection = miningSettings.SelectedValue.ToString();
            }
            // get preferred address if provided
            String preferredAddress = "";
            try
            {
                // get preferred address if possible
                preferredAddress = addressPreference.Text;
            }
            catch
            {
                // do nothing if addressPreference is null
            }
            
            // add new block
            blockchain.addBlock(new Block(blockchain.getBlockchainSize(), blockchain.getLastBlock().getHash(), blockchain.pendingTransactions, publicKey.Text, transactionSelection, preferredAddress));
            richTextBox1.Text = blockchain.getLastBlock().getData();
        }

        // prints information about all of the blocks in the blockchain
        private void showAllBlocks_Click(object sender, EventArgs e)
        {
            String allBlocks = "";
            // add data to allBlocks variable
            foreach (Block b in blockchain.getAllBlocks())
            {
                allBlocks += b.getData() + "\n\n";
            }
            // sets the richtextbox equal to all the data provided
            richTextBox1.Text = allBlocks;
        }

        // set the richtextbox equal to the pending transaction list
        private void showPendingTransactions_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = blockchain.listPendingTransactions();
        }


        // validate the whole blockchain  
        private void validateBlockchain_Click(object sender, EventArgs e)
        {
            // in here we want to iterate through all of the block in the blockchain and ensure that they match i.e. previous hash is true
            List<Block> blocks = blockchain.getAllBlocks();
            String prev_hash = null;
            bool passed = true;
            foreach (Block b in blocks)
            {
                richTextBox1.Text = "Validating blockchain...\n";
                // if merkle root not the same then failed to validate
                if (b.getIndex() != 0 && !(b.getMerkleRoot() == b.calculateMerkleRoot(b.getAllTransactions())))
                {
                    richTextBox1.Text += "\nMerkle root incorrect for block: \n" + b.getData();
                    passed = false;
                }
                foreach (Transaction t in b.getAllTransactions())
                {
                    // if sender is miner rewards then there is no digital signature
                    if ((t.getHash() != t.generateHash()))
                    {
                        // they are not the same, so error
                        richTextBox1.Text += "\nHash is not correct for transaction: \n" + t.getTransactionData() + "\nIn block:\n" + b.getData();
                        passed = false;
                    }
                    if ((t.getSender() != "Mine Rewards") && !Wallet.Wallet.ValidateSignature(publicKey.Text, t.getHash(), t.getSignature()))
                    {
                        richTextBox1.Text += "\nSignature not validated for transaction: \n" + t.getTransactionData() + "\nIn block:\n" + b.getData();
                        passed = false;
                    }
                }
                // if genesis block
                if (prev_hash == null)
                {
                    prev_hash = b.getHash();
                }
                // otherwise get previous hash from block
                else
                {
                    if (prev_hash == b.getPrevHash())
                    {
                        prev_hash = b.getHash();
                    }
                    else
                    {
                        richTextBox1.Text += "\nHash and previous hash do not match at block \n" + b.getData();
                        passed = false;
                    }
                }
            }
            if (passed)
            {
                richTextBox1.Text += "Successfully Validated Blockchain, all blocks are correct.";
            }
        }

        // update the rich text box to show the balance of the user
        private void balanceCheck_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "The current balance is: " + getBalance();
        }

        // gets the balance by iterating through the blockchain finding all the transactions
        private double getBalance()
        {
            double balance = 0.0f;
            // loop all blocks, all transactions if negative remove, if positive add to balance
            foreach (Block b in blockchain.getAllBlocks())
            {
                foreach (Transaction t in b.getAllTransactions())
                {
                    // we can't use if else, because if we do then people could send money to themselves allowing them to gain infinite money
                    if (t.getRecipient() == publicKey.Text)
                    {
                        balance += t.getCoins();
                    }
                    if (t.getSender() == publicKey.Text)
                    {
                        balance -= t.getCoins();
                    }
                }
            }
            // checks pending transactions to avoid double spend
            foreach (Transaction t in blockchain.pendingTransactions)
            {
                // to avoid the issue of people sending more money, their current balance is calculated off their pendingTransactions that are outgoing also
                if (t.getSender() == publicKey.Text)
                {
                    balance -= t.getCoins();
                }
            }
            return balance;
        }
    }
}
