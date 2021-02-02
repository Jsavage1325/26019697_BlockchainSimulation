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
        public BlockchainApp()
        {
            InitializeComponent();
            blockchain = new Blockchain();
            richTextBox1.Text = "New blockchain initialised!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /// todo add error handling here to sanitise the input to ensure we only get int values, within the range of blocks that are created
            updateRichTextBox(blockchain.getBlockData(Int32.Parse(textBox1.Text)));
        }

        private void updateRichTextBox(String value)
        {
            richTextBox1.Text = value;
        }


        private void createWallet_Click(object sender, EventArgs e)
        {
            
        }

        private void validateKeys_Click(object sender, EventArgs e)
        {
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

        private void createWallet_Click_1(object sender, EventArgs e)
        {
            String privKey;
            Wallet.Wallet myNewWallet = new Wallet.Wallet(out privKey);
            String pubKey = myNewWallet.publicID;
            privateKey.Text = privKey;
            publicKey.Text = pubKey;
        }

        private void makeTransaction_Click(object sender, EventArgs e)
        {
            Transaction t = new Transaction(publicKey.Text, address.Text, Double.Parse(amount.Text), Double.Parse(fee.Text), privateKey.Text);
            /// add the new transaction to the pending transactions
            blockchain.pendingTransactions.Add(t);
            richTextBox1.Text = t.getTransactionData();
        }

        private void generateBlock_Click(object sender, EventArgs e)
        {
            blockchain.addBlock(new Block(blockchain.getBlockchainSize(), blockchain.getLastBlock().getHash(), blockchain.pendingTransactions, publicKey.Text));
            richTextBox1.Text = blockchain.getLastBlock().getData();
        }

        private void showAllBlocks_Click(object sender, EventArgs e)
        {
            String allBlocks = "";
            foreach (Block b in blockchain.getAllBlocks())
            {
                allBlocks += b.getData() + "\n\n";
            }
            richTextBox1.Text = allBlocks;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void showPendingTransactions_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = blockchain.listPendingTransactions();
        }

        private void validateBlockchain_Click(object sender, EventArgs e)
        {
            // in here we want to iterate through all of the block in the blockchain and ensure that they match i.e. previous hash is true
            List<Block> blocks = blockchain.getAllBlocks();
            String prev_hash = null;
            bool passed = true;
            foreach (Block b in blocks)
            {
                if (prev_hash == null)
                {
                    prev_hash = b.getHash();
                }
                else
                {
                    if (prev_hash == b.getPrevHash())
                    {
                        prev_hash = b.getHash();
                    }
                    else
                    {
                        richTextBox1.Text = "Failed to validate blockchain at block " + b.getData();
                        passed = false;
                    }

                }
            }
            if (passed)
            {
                richTextBox1.Text = "Successfully Validated Blockchain, all blocks are correct.";
            }
        }
    }
}
