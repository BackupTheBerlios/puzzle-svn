using System;
using System.Reflection;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.TransactionalCache;

namespace NTorpedo.Auction.NPersist
{
	/// <summary>
	/// Summary description for ContextManager.
	/// </summary>
	public class ContextManager
	{
		public ContextManager()
		{
		}

		public static IContext CreateContext()
		{
			//if you are testing the AuctionServices directly (for example with NUNit tests)
			//you may want to set the useWebConfig flag to false and then set the desired settings
			//directly in the variables below:
			bool useWebConfig = true;

			//Set to true in order to use a transactional context that will only require
			//one sql statement for the ListAuctionTwiceWithTransaction test
			bool useTransactionalContext = false;

			//Set to true to mark all classes in the domain as read-only. This should only
			//be set to true for the ListAuctionTwiceWithoutTransaction test. When the classes
			//are marked as read-only, the ListAuctionTwiceWithoutTransaction test will only
			//require one sql statement.
			bool isReadOnly = false;

			//Specify the connection string to your sql server database containing the NTorpedo database
			string connectionString = ""; //"SERVER=(local);UID=sa;PWD=;DATABASE=NTorpedo;";

			if (useWebConfig)
			{
				string connStr = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];

				string ctxType = System.Configuration.ConfigurationSettings.AppSettings["NPersistContextType"];
				string readOnly = System.Configuration.ConfigurationSettings.AppSettings["NPersistReadOnly"];				

				if (ctxType != null && ctxType.ToLower() == "transactional")
					useTransactionalContext = true;
				else 
					useTransactionalContext = false;

				if (connStr != null && connStr.Length > 0)
					connectionString = connStr;

				if (readOnly != null && readOnly.ToLower() == "true")
					isReadOnly = true;
				else 
					isReadOnly = false;

			}

			//The npersist xml mapping has been compiled into the assembly containing the domain model.
			//In order to create our new context we will therefore pass the assembly that the mapping
			//file resides in and the name of the mapping file.
			Assembly asm = typeof(Auction).Assembly;
			string fileName = "NTorpedo.Auction.NPersist.map.npersist";

			IContext context = null; 

			//Create a transactional or a standard context
			if (useTransactionalContext)
				context = CreateTransactionalContext(asm, fileName);
			else
				context = CreateStandardContext(asm, fileName);

			//Set the connection string to the database
			if (connectionString.Length > 0)
				context.SetConnectionString(connectionString);

			//Make sure the database connections(s) (but there's only one in this case)
			// are kept open for the lifetime of the context. This gives better performance
			// for few concurrent clients but lower scalability for many concurrent clients.
			// Also it gives fewer "sql communication" hits in the Torpedo tests :-P
			context.GetDataSource().KeepConnectionOpen = true;

			//Send all insert/update/delete statements together in a batch
			context.SqlExecutor.ExecutionMode = ExecutionMode.BatchExecution; 

			//mark all objects in the domain map as read only if specified 
			//(should only be done for the ListAuctionTwiceWithoutTransaction test!)
			if (isReadOnly)
				context.DomainMap.IsReadOnly = true;

			return context;
		}

		//This method creates a non-transactional cache, meaning 
		//ListAuctionTwiceWithTransaction will require two sql statements.
		public static IContext CreateStandardContext(Assembly asm, string fileName)
		{

			IContext context = new Context(asm, fileName);
			
			//We must set AutoTransactions to false when using 
			//distributed transactions
			context.AutoTransactions = false;

			return context;
		}


		//This method creates a transactional cache, meaning 
		//ListAuctionTwiceWithTransaction will only require one sql statement.
		//For this to work you have to set up the permissions for allowing COM+ Events
		//to be called from ASP.NET. (Testing the ListAuctionTwiceWithTransaction method 
		//directly with for example an NUnit test will not require this excercise in 
		//permission management and so can be an easier way to try this out).
		public static IContext CreateTransactionalContext(Assembly asm, string fileName)
		{
			TransactionalContextFactory factory = new TransactionalContextFactory() ;
			IContext context =  factory.GetContext(asm, fileName);

			return context;
		}

	}
}
