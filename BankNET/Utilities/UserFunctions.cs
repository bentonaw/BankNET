﻿using BankNET.Data;
using BankNET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankNET.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankNET.Utilities
{
    // Static class containing methods for all functions available to regular users.
    internal static class UserFunctions
    {

        internal static void ViewAccountBalance(BankContext context, string userName)
        {
            {
                // Retrive user information from database
                User user = context.Users
                    .Where(u => u.Name == userName)
                    .Include(u => u.Accounts)
                    .Single();
                // Dusplaying the account and balance for the user
                foreach (var account in user.Accounts)
                {
                    Console.WriteLine($"{account.Name}: {account.Balance}");
                }
            }
            // Empty line for better output fromatting
            Console.WriteLine();

        // Add method for viewing all user accounts with balance.


        // Add method for moving money between users internal accounts. - h

        //public static void ViewAccounts() // Which user that successfully logged in in between the ()
        //{
        //    Console.Clear();
        //    Console.WriteLine($"Saldo för användare {currentUser}: {accounts[currentUser]}"); // The account
        //    Console.WriteLine($"Saldo för användare {currentUser}: {accounts[currentUser]}"); // Second account if the user have one
        //}


        // Add method for moving money between users internal accounts.



        // Add methods for deposit/withdraw.
        //public static void Deposit() // which user that is logged in in between the ()
        //{
        //    using(BankContext context = new BankContext())
        //    {
        //        Console.Clear();
        //        Console.WriteLine();  // Choose which account to deposit to.
        //        Console.WriteLine($"How much would you like to deposit: ");
        //        try
        //        {
        //            decimal deposit = decimal.Parse(Console.ReadLine());
        //            // accessing the account choosen and the balance, ex user.SelectedAccount.Balance += deposit
        //            context.SaveChanges();

        //            Console.WriteLine("You have deposited : $" + deposit ); // the deposit to the account
        //            Console.WriteLine("Your new balance is: $" ); // , ex user.SelectedAccount.Balance
        //        }
        //        catch
        //        {
        //            Console.WriteLine("Invalid input. PLease enter a valid amount.");
        //        }
        //    }
        //}

        //public static void Withdraw() // which user that is logged in between the ()
        //{
        //    using(BankContext context = new BankContext())
        //    {
        //        Console.Clear();
        //        Console.WriteLine(); // Which account the user would like to withdraw from
        //        Console.WriteLine("How much would you like to withdraw: ");
        //        try
        //        {
        //            decimal withdrawal = decimal.Parse(Console.ReadLine());
        //            if (withdrawal > 0 && withdrawal <= ) // The current user and the choosen account, ex user.SelectedAccount.Balance
        //            {
        //                //The user and account - the withdrawal ex, User.SelectedAccount.Balance -= withdrawal
        //                context.SaveChanges();

        //                Console.WriteLine("You have withdrawn: $" + withdrawal);
        //                Console.WriteLine("Your current balance is: $" ); // ex, user.SelectedAccount.Balance
        //            }
        //            else
        //            {
        //                Console.WriteLine("Insufficient balance. ");
        //            }
        //        }
        //        catch
        //        {
        //            Console.WriteLine("Invalid input. Please enter a valid amount.");
        //        }
        //    }
        //}


        }
            // Add method for moving money between users internal accounts.
 
        internal static void Withdraw(BankContext context, string userName)
        {
            // Retrive user info from database
            User user = context.Users
                .Include(u => u.Accounts)
                .Single(u => u.Name == userName);

            // Displaying the user´s accounts
            Console.WriteLine("Select the account you would like to withdraw from: ");
            foreach (var account in user.Accounts)
            {
                Console.WriteLine($"{account.Id}. {account.Name}: {account.Balance}");
            }

            Console.WriteLine("How much would you like to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                Console.WriteLine("Select the account by entering the account number: ");
                if (int.TryParse(Console.ReadLine(), out int selectedAccountId))
                {
                    // Find the selected account
                    var selectedAccount = user.Accounts.SingleOrDefault(account => account.Id == selectedAccountId);

                    if (selectedAccount != null)
                    {
                        // Check if there is sufficient balance
                        if (amount <= selectedAccount.Balance)
                        {
                            // Update the account balance with the withdrawal
                            selectedAccount.Balance -= amount;

                            try
                            {
                                context.SaveChanges();

                                // Displaying the withdraw details and the updated balance
                                Console.WriteLine($"You have withdrawn {amount:C} from {selectedAccount.Name}.");
                                Console.WriteLine($"Your new balance for {selectedAccount.Name} is: {selectedAccount.Balance:C}");
                            }
                            catch (Exception e)
                            {
                                // Handling any error that occur during saving
                                Console.WriteLine($"Error saving changes to the database");
                            }
                        }
                        else
                        {
                            // Display message if their isn´t enough money
                            Console.WriteLine("Insufficient balance. Withdrawal canceled.");
                        }
                    }
                    else
                    {
                        // Invalid account number
                        Console.WriteLine("Invalid account number. Withdrawal canceled.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for account number. Withdrawal canceled.");
                }
            }
            else
            {
                // Invalid amount
                Console.WriteLine("Invalid input for withdrawal amount. Withdrawal canceled.");
            }
        }

        internal static void Deposit(BankContext context, string userName)
        {
            // Retrive user info from database
            User user = context.Users
            .Include(u => u.Accounts)
            .Single(u => u.Name == userName);

            //Displaying the user´s account
            Console.WriteLine("Select the account you would like to deposit into: ");
            foreach (var account in user.Accounts)
            {
                Console.WriteLine($"{account.Id}. {account.Name}: {account.Balance}");
            }

            Console.WriteLine("How much would you like to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                Console.WriteLine("Select the account by entering the account number: ");
                if (int.TryParse(Console.ReadLine(), out int selectedAccountId))
                {
                    // Find the selected account
                    var selectedAccount = user.Accounts.SingleOrDefault(account => account.Id == selectedAccountId);

                    if (selectedAccount != null)
                    {
                        // Updating the balance with the deposit
                        selectedAccount.Balance += amount;

                        try
                        {
                            context.SaveChanges();
                            //Desplaying the deposit details and updated balance
                            Console.WriteLine($"You have deposited {amount:C} into {selectedAccount.Name}.");
                            Console.WriteLine($"Your new balance for {selectedAccount.Name} is: {selectedAccount.Balance:C}");
                        }
                        catch (Exception e)
                        {
                            // Handling any error that occur during saving
                            Console.WriteLine($"Error saving changes to the database");
                        }
                    }
                    else
                    {
                        // Invalid account number
                        Console.WriteLine("Invalid account number. Deposit canceled.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for account number. Deposit canceled.");
                }
            }
            else
            {
                // Invalid amount
                Console.WriteLine("Invalid input for deposit amount. Deposit canceled.");
            }

        }

        // Add method for transactions between separate users accounts. (Extra)




        // Method for creating new accounts.
        public static void CreateNewAccount(BankContext context, User user)
        {
            
            Console.Clear();
            Console.Write("Enter new account name: ");

            string newAccountName = Console.ReadLine();
                
            // Generates a new account number and checks if it is unique. If it isn't, a new one is generated until it becomes unique.
            string newAccountNumber;
            do
            {
                newAccountNumber = BankHelpers.GenerateAccountNumber();

            } while (!BankHelpers.IsAccountNumberUnique(newAccountNumber));

            // Checks if the proposed name that the user entered isn't null. If it isn't, a new account is created.
            if (!string.IsNullOrWhiteSpace(newAccountName))
            {
                DbHelpers.CreateNewAccount(context, newAccountName, newAccountNumber, user);

                Console.Clear() ;
                Console.WriteLine($"Successfully created new account with account name: {newAccountName}\n" +
                    $"Your account number is {newAccountNumber}");

                Thread.Sleep(1000);
            }

            else
            {
                Console.Clear();
                Console.WriteLine("Account name cannot be empty");

                Thread.Sleep(1000);
            }
            
        }

        // Method for transferring money internally between 2 accounts belonging to the same user.
        public static void TransferInternal(User user)
        {
            Console.Clear();
            Console.Write("Transfer to which account? \n");
            string accountRecieving = Console.ReadLine();

            Console.Write("Transfer from which account? \n");
            string accountSending = Console.ReadLine();

            // Checks if the accounts the user wants to transfer between exist.
            if (BankHelpers.IsAccountNameMatching(accountRecieving, user) && BankHelpers.IsAccountNameMatching(accountSending, user))
            {                            
                decimal transferAmount = 0;
                bool validInput = false;

                // Keeps the user stuck in a loop until they enter a valid numeric amount.
                do
                {
                    Console.Clear();
                    Console.Write("Enter amount you wish to transfer: ");
                    try
                    {
                        transferAmount = decimal.Parse(Console.ReadLine());
                        validInput = true;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine("Please enter a valid amount, numbers only.");
                        Thread.Sleep(2000);
                    }

                }while(!validInput);
                
                // Creating reference accounts for the sending and recieving accounts.
                Account sendingAccount = user.Accounts.FirstOrDefault(account => account.AccountName.ToLower()  == accountSending.ToLower());
                Account recievingAccount = user.Accounts.FirstOrDefault(account => account.AccountName.ToLower()  == accountRecieving.ToLower());

                // Check if there is enough balance to complete the transaction and proceeds to do so if there is.
                if(BankHelpers.IsThereBalance(sendingAccount, transferAmount))
                {
                    using(var context = new BankContext())
                    {
                        DbHelpers.TransferInternal(context, sendingAccount, recievingAccount, transferAmount);
                    }
                }
                
                // Error message if there isn't enough balance.
                else
                {
                    Console.Clear();
                    Console.WriteLine("Transaction failed. Not enough funds in the account.");
                    Thread.Sleep(1000);
                }
            }

            // Error message if the user enters an account name that doesn't match any existing account.
            else
            {
                Console.Clear();
                Console.WriteLine("Account names do not match. Transaction failed.");
                Thread.Sleep(1000);
            }
        }

    }
}


