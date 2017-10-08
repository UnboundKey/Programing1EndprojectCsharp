using System;
using System.Linq;

namespace Work_now_please
{
	class MainClass
	{
		private static string filePath = "bank.log";
		private static int currAcc;
		private static int accVars = 4;

		public static string[,] accounts = { { "Phil", "Collins", "10000", "6922" }, { "Stevey", "Wonder", "1000", "0000" }, {"Gordon", "Freeman", "1000", "5555" } };

		public static void writeToLog(string error)
		{
			System.IO.File.AppendAllText(filePath, "\n" + DateTime.Now + ": " + error);

		}

		public static void login()
		{
			Console.WriteLine("What is your account id?: ");
			string input = Console.ReadLine();
			if (input.All(char.IsDigit))
			{
				int convInput = Convert.ToInt16(input);

				if (convInput < accounts.Length / accVars)
				{
					writeToLog("Account " + convInput + " started log in sequence");
					Console.WriteLine("You are trying to log in as: " + accounts[convInput, 0]);
					Console.WriteLine("What is " + accounts[convInput, 0] + "\'s pincode");
					string passInput = Console.ReadLine();

					if (passInput == accounts[convInput, 3])
					{
						Console.WriteLine("Welcome " + accounts[convInput, 0] + " " + accounts[convInput, 1]);
						currAcc = convInput;
						writeToLog("Account Login Success");
						actionMenu();
					}
					else {
						Console.WriteLine("Invalid Password");
						writeToLog("Password Authentication Failed: Invalid password");
						login();
					}

				}
				else {
					string _error = DateTime.Now + ": " + "Someone tried to access account " + convInput + ". It did'nt exist or was not found";
					Console.WriteLine("Account " + convInput + " is not in our database");
					writeToLog(_error);
					login();
				}

			}
			else {
				string _error = DateTime.Now + ": " + "Invalid Input. Expected [int]";
				Console.WriteLine("error: " + _error);
				writeToLog(_error);
				login();
			}

		}

		public static void withdraw(int amount)
		{
			int currentBalance = Convert.ToInt16(accounts[currAcc, 2]);
			if (currentBalance > amount)
			{
				int newbalance = currentBalance - amount;
				accounts[currAcc, 2] = Convert.ToString(newbalance);
				writeToLog("Account " + currAcc + " Succesfully withdrew " + amount + " from his/her account");
				balance();
				actionMenu();
			}
			else
			{
				Console.WriteLine("Withdrawal Failed: Insuffisient Funds");
				writeToLog("Withdrawal Failed: Insuffisient Funds");
				actionMenu();
			}
		}

		public static void deposit(int amount)
		{
			int currentBalance = Convert.ToInt16(accounts[currAcc, 2]);

			int newbalance = currentBalance + amount;
			accounts[currAcc, 2] = Convert.ToString(newbalance);
			writeToLog("Account " + currAcc + " Succesfully deposited " + amount + " from his/her account");
			balance();
			actionMenu();

		}

		public static void balance()
		{
			Console.Clear();
			Console.WriteLine("Your Balance is: " + Convert.ToInt16(accounts[currAcc, 2]));
			writeToLog("Account " + currAcc + " Checked his/her Balance");
			actionMenu();
		}

		public static void actionMenu()
		{
			Console.WriteLine("Available Actions");
			Console.WriteLine("Balance");
			Console.WriteLine("Withdraw");
			Console.WriteLine("Deposit");
			Console.WriteLine("exit");

			string input = Console.ReadLine();

			switch (input.ToLower())
			{
				case "balance":
					balance();
					break;
				case "withdraw":
					Console.WriteLine("How much?");
					string _amount = Console.ReadLine();
					if (_amount.All(char.IsDigit))
					{
						withdraw(Convert.ToInt32(_amount));
					}
					else {
						Console.WriteLine("Invalid Input. Expected [int]");
						actionMenu();
					}
					break;

					case "deposit":
					Console.WriteLine("How much?");
					 _amount = Console.ReadLine();
					if (_amount.All(char.IsDigit))
					{
						deposit(Convert.ToInt32(_amount));
					}
					else {
						Console.WriteLine("Invalid Input. Expected [int]");
						actionMenu();
					}
					break;

				case "exit":
					Environment.Exit(-1);
					break;
					
				default:
					Console.WriteLine("Command not available");
					actionMenu();
					break;
			}
		}

		public static void Main(string[] args)
		{
			login();
		}
	}
}
