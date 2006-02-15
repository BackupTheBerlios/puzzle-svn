using System;

using Puzzle.NAspect.Extensions;

//TODO: Fix References

namespace Puzzle.NAspect.Extensions.Examples.Logging
{
	/// <summary>
	/// Summary description for LogAoPTarget.
	/// </summary>
	public class Calculator
	{

		[Log4NetAttribute(Level=Log4NetAttribute.Levels.FATAL, LogPath = "sub.log", Format = "Subtraction {2} = {0} - {1}")
		,Log4NetAttribute(LogPath = "sub.sum.log")]
		public virtual int Sub(int nA, int nB)
		{
			return nA - nB;
		}

		[Log4NetAttribute(LogPath = "sum.log", Format = "The sum of {0} and {1} is {2}")
		,Log4NetAttribute(LogPath = "sub.sum.log")]
		public virtual int Sum(int nA, int nB)
		{
			return nA + nB;
		}

		[Log4NetAttribute(LogPath = "mul.log")]
		public virtual int Mul(int nA, int nB)
		{
			return nA * nB;
		}

		[Log4NetAttribute(Level = Log4NetAttribute.Levels.ERROR)]
		public virtual int Div(int nA, int nB)
		{
			return Divv(nA, nB);
		}

		[Log4NetAttribute(Level = Log4NetAttribute.Levels.ERROR)]
		protected virtual int Divv(int nA, int nB)
		{
			return nA / nB;
		}

		[Log4NetAttribute(Format = "{2} = {0} + {1}, {3} = {0} - {1}, {4} = {0}*{1}")]
		public virtual int CalculateA(int nA, int nB, ref int nSum, out int nSub)
		{
			nSum = nA + nB;
			nSub = nA - nB;
			return nA * nB;
		}
	}
}