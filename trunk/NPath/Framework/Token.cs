using System;

namespace Puzzle.NPath.Framework
{
	public struct Token
	{
		public int Index;
		public string Text;
		public string[] Types;

		public static Token Empty
		{
			get
			{
				Token empty = new Token();
				return empty;
			}
		}

		public bool IsType(string type)
		{
			if (Types == null)
				return false;

			return Array.IndexOf(Types, type) >= 0;
		}

		public override string ToString()
		{
			return string.Format("{0}\t\t{1}", Text, Types);
		}
	}
}