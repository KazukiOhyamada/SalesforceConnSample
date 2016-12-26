﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Salesforce.Common;
using Salesforce.Force;
using System.Threading.Tasks;
using System.Dynamic;

namespace SalesforceConnSample
{

	public partial class Default : System.Web.UI.Page
	{
		private static readonly string SecurityToken = ConfigurationManager.AppSettings["SecurityToken"];
		private static readonly string ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
		private static readonly string ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
		private static readonly string Username = ConfigurationManager.AppSettings["Username"];
		private static readonly string Password = ConfigurationManager.AppSettings["Password"] + SecurityToken;
		private static readonly string IsSandboxUser = ConfigurationManager.AppSettings["IsSandboxUser"];

		public void button1Clicked(object sender, EventArgs args)
		{
			System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls; //must Tls12
			button1.Text = "You clicked me";
		}

		private static async Task RunSample()
		{
			var auth = new AuthenticationClient();

			// Authenticate with Salesforce
			Console.WriteLine("Authenticating with Salesforce");
			var url = IsSandboxUser.Equals("true", StringComparison.CurrentCultureIgnoreCase)
				? "https://test.salesforce.com/services/oauth2/token"
				: "https://login.salesforce.com/services/oauth2/token";

			await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);
			Console.WriteLine("Connected to Salesforce");

			var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);

			// Create a sample record
			Console.WriteLine("Creating test record.");
			var account = new Account { Name = "Test Account" };
			var createAccountResponse = await client.CreateAsync(Account.SObjectTypeName, account);
			account.Id = createAccountResponse.Id;

			if (account.Id == null)
			{
				Console.WriteLine("Failed to create test record.");
				return;
			}

			Console.WriteLine("Successfully created test record.");
			
		}

		private class Account
		{
			public const String SObjectTypeName = "Account";

			public String Id { get; set; }
			public String Name { get; set; }
		}
	}
}
