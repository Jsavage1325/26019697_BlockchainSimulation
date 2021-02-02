namespace BlockchainAssignment
{
    partial class BlockchainApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.publicKey = new System.Windows.Forms.TextBox();
            this.privateKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.validateKeys = new System.Windows.Forms.Button();
            this.createWallet = new System.Windows.Forms.Button();
            this.makeTransaction = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.amount = new System.Windows.Forms.TextBox();
            this.fee = new System.Windows.Forms.TextBox();
            this.address = new System.Windows.Forms.TextBox();
            this.generateBlock = new System.Windows.Forms.Button();
            this.showAllBlocks = new System.Windows.Forms.Button();
            this.showPendingTransactions = new System.Windows.Forms.Button();
            this.validateBlockchain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox1.Location = new System.Drawing.Point(16, 15);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(875, 386);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 423);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Print Block";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(147, 424);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 2;
            // 
            // publicKey
            // 
            this.publicKey.Location = new System.Drawing.Point(150, 513);
            this.publicKey.Name = "publicKey";
            this.publicKey.Size = new System.Drawing.Size(741, 22);
            this.publicKey.TabIndex = 4;
            // 
            // privateKey
            // 
            this.privateKey.Location = new System.Drawing.Point(150, 563);
            this.privateKey.Name = "privateKey";
            this.privateKey.Size = new System.Drawing.Size(741, 22);
            this.privateKey.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 493);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Public Key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Private Key";
            // 
            // validateKeys
            // 
            this.validateKeys.Location = new System.Drawing.Point(31, 543);
            this.validateKeys.Name = "validateKeys";
            this.validateKeys.Size = new System.Drawing.Size(110, 30);
            this.validateKeys.TabIndex = 12;
            this.validateKeys.Text = "Validate Keys";
            this.validateKeys.UseVisualStyleBackColor = true;
            this.validateKeys.Click += new System.EventHandler(this.validateKeys_Click);
            // 
            // createWallet
            // 
            this.createWallet.Location = new System.Drawing.Point(31, 505);
            this.createWallet.Name = "createWallet";
            this.createWallet.Size = new System.Drawing.Size(110, 30);
            this.createWallet.TabIndex = 13;
            this.createWallet.Text = "Create Wallet";
            this.createWallet.UseVisualStyleBackColor = true;
            this.createWallet.Click += new System.EventHandler(this.createWallet_Click_1);
            // 
            // makeTransaction
            // 
            this.makeTransaction.Location = new System.Drawing.Point(266, 422);
            this.makeTransaction.Name = "makeTransaction";
            this.makeTransaction.Size = new System.Drawing.Size(92, 44);
            this.makeTransaction.TabIndex = 14;
            this.makeTransaction.Text = "Make Transaction";
            this.makeTransaction.UseVisualStyleBackColor = true;
            this.makeTransaction.Click += new System.EventHandler(this.makeTransaction_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(364, 429);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Amount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(374, 455);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Fee";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(263, 484);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Address";
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(428, 426);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(98, 22);
            this.amount.TabIndex = 18;
            // 
            // fee
            // 
            this.fee.Location = new System.Drawing.Point(428, 454);
            this.fee.Name = "fee";
            this.fee.Size = new System.Drawing.Size(98, 22);
            this.fee.TabIndex = 19;
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(329, 483);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(562, 22);
            this.address.TabIndex = 20;
            // 
            // generateBlock
            // 
            this.generateBlock.Location = new System.Drawing.Point(31, 454);
            this.generateBlock.Name = "generateBlock";
            this.generateBlock.Size = new System.Drawing.Size(110, 44);
            this.generateBlock.TabIndex = 21;
            this.generateBlock.Text = "Generate New Block";
            this.generateBlock.UseVisualStyleBackColor = true;
            this.generateBlock.Click += new System.EventHandler(this.generateBlock_Click);
            // 
            // showAllBlocks
            // 
            this.showAllBlocks.Location = new System.Drawing.Point(31, 594);
            this.showAllBlocks.Name = "showAllBlocks";
            this.showAllBlocks.Size = new System.Drawing.Size(110, 48);
            this.showAllBlocks.TabIndex = 22;
            this.showAllBlocks.Text = "Show All Blocks";
            this.showAllBlocks.UseVisualStyleBackColor = true;
            this.showAllBlocks.Click += new System.EventHandler(this.showAllBlocks_Click);
            // 
            // showPendingTransactions
            // 
            this.showPendingTransactions.Location = new System.Drawing.Point(152, 594);
            this.showPendingTransactions.Name = "showPendingTransactions";
            this.showPendingTransactions.Size = new System.Drawing.Size(109, 48);
            this.showPendingTransactions.TabIndex = 24;
            this.showPendingTransactions.Text = "Show Pending Transactions";
            this.showPendingTransactions.UseVisualStyleBackColor = true;
            this.showPendingTransactions.Click += new System.EventHandler(this.showPendingTransactions_Click);
            // 
            // validateBlockchain
            // 
            this.validateBlockchain.Location = new System.Drawing.Point(267, 594);
            this.validateBlockchain.Name = "validateBlockchain";
            this.validateBlockchain.Size = new System.Drawing.Size(109, 48);
            this.validateBlockchain.TabIndex = 25;
            this.validateBlockchain.Text = "Validate Blockchain";
            this.validateBlockchain.UseVisualStyleBackColor = true;
            this.validateBlockchain.Click += new System.EventHandler(this.validateBlockchain_Click);
            // 
            // BlockchainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(908, 645);
            this.Controls.Add(this.validateBlockchain);
            this.Controls.Add(this.showPendingTransactions);
            this.Controls.Add(this.showAllBlocks);
            this.Controls.Add(this.generateBlock);
            this.Controls.Add(this.address);
            this.Controls.Add(this.fee);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.makeTransaction);
            this.Controls.Add(this.createWallet);
            this.Controls.Add(this.validateKeys);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.privateKey);
            this.Controls.Add(this.publicKey);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BlockchainApp";
            this.Text = "Blockchain App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox publicKey;
        private System.Windows.Forms.TextBox privateKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button validateKeys;
        private System.Windows.Forms.Button createWallet;
        private System.Windows.Forms.Button makeTransaction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.TextBox fee;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Button generateBlock;
        private System.Windows.Forms.Button showAllBlocks;
        private System.Windows.Forms.Button showPendingTransactions;
        private System.Windows.Forms.Button validateBlockchain;
    }
}

