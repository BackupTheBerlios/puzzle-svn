using System;
using Puzzle.NPersist.Framework;
using QuickStart.Domain;

namespace QuickStart.WinGui
{
	/// <summary>
	/// Summary description for AuthorServices.
	/// </summary>
	public class AuthorServices
	{
		public AuthorServices()
		{
		}

		public static Author CreateAuthor(IContext context, string firstName, string lastName)
		{
			//Ask the context to create a new author object
			Author author = (Author) context.CreateObject(typeof(Author));

			//Set the properties
			author.FirstName = firstName;
			author.LastName = lastName;

			//Commit the new object to the database
			context.Commit();

			return author;
		}
	}
}
