using System;

public class Transaction
{
	private String hash;
	private String signature;
	private String senderAddress;
	private String recipientAddress;
	private DateTime timeStamp;
	private double coins;
	private double fee;
	public String signature;

	public Transaction(String senderAddress, String recipientAddress, double coins, double fees, String senderPrivKey)
	{
		this.timeStamp = DateTime.Now;
		this.senderAddress = senderAddress;
		this.recipientAddress = recipientAddress;
		this.coins = coins;
		this.fee = fee;
		this.hash = generateHash();

		/// private key and public key need to be verified before signing
		this.signature = Wallet.Wallet.CreateSignature(senderAddress, senderPrivKey, hash);
		

	}

	public String generateHash()
    {
		SHA256 hasher;
		hasher = SHA256Managed.Create();
		String input = senderAddress + recipientAddress + timeStamp.ToString() + coins.ToString() + fee.ToString()
		/// String input = index.ToString() + timestamp.ToString() + prevHash;
		Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes((input)));

		String hash = string.Empty;

		foreach (byte x in hashByte)
		{
			hash += String.Format("{0:x2}", x);
		}
		return hash;

	}
}
