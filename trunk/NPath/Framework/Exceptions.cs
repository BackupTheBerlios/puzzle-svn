using Puzzle.NPersist.Framework.Exceptions;

namespace Puzzle.NPath.Framework
{
	public class UnexpectedTokenException : NPathException
	{
		public UnexpectedTokenException(string message) : base(message)
		{
		}

		public UnexpectedTokenException(string found, int index, string near, params string[] expected) : base(BuildMessage(found, index, near, expected))
		{
		}

		public UnexpectedTokenException(string found, int index, string near, string expected) : base(BuildMessage(found, index, near, new string[] {expected}))
		{
		}

		public UnexpectedTokenException(string found, int index, string near) : base(BuildMessage(found, index, near))
		{
		}

		private static string BuildMessage(string found, int index, string near, string[] expectedTokens)
		{
			string expectedList = "";
			int i = 0;
			foreach (string expectedToken in expectedTokens)
			{
				expectedList += string.Format("'{0}'", expectedToken);
				i++;
				if (i != expectedTokens.Length)
					expectedList += ",";
			}

			string message = string.Format("Unexpected token '{0}' found near '{1}' expected {2}", found, near, expectedList); // do not localize
			return message;
		}

		private static string BuildMessage(string found, int index, string near)
		{
			string message = string.Format("Unexpected token '{0}' found near '{1}'", found, near); // do not localize
			return message;
		}
	}

	public class UnknownTokenException : NPathException
	{
		public UnknownTokenException(string found, int index, string near) : base(BuildMessage(found, index, near))
		{
		}

		private static string BuildMessage(string found, int index, string near)
		{
			string message = string.Format("Unknown token '{0}' found near '{1}'", found, near); // do not localize
			return message;
		}
	}

}