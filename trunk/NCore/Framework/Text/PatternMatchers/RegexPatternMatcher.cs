using System;
using System.Text.RegularExpressions;

namespace Puzzle.NCore.Framework.Text.PatternMatchers
{
	/// <summary>
	/// Pattern matcher that allows regex patterns to be matched
	/// </summary>
	public class RegexPatternMatcher : PatternMatcherBase
	{
		private Regex regEx = null;

		public RegexPatternMatcher()
		{
			this.PatternChanged += new EventHandler(RegexPatternMatcher_PatternChanged);
		}

		public RegexPatternMatcher(string pattern) : this()
		{
			this.Pattern = pattern;
		}

		#region PUBLIC PROPERTY PATTERN (+PATTERNCHANGED EVENT)

		private string pattern;

		/// <summary>
		/// Gets or Sets the <c>Pattern</c> property
		/// </summary>
		public virtual string Pattern
		{
			get { return pattern; }
			set
			{
				//Ignore same value
				if (pattern == value)
					return;

				//Set the new value
				pattern = value;

				//Raise the changed event
				OnPatternChanged(EventArgs.Empty);
			}
		}

		#region PUBLIC EVENT PATTERNCHANGED

		/// <summary>
		/// Fires when the 'Pattern' Property changes
		/// </summary>
		public event EventHandler PatternChanged = null;

		/// <summary>
		/// Raises the <c>PatternChanged</c> Event
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected virtual void OnPatternChanged(EventArgs e)
		{
			if (PatternChanged != null)
				PatternChanged(this, e);
		}

		#endregion //END PUBLIC EVENT PATTERNCHANGED

		#endregion //END PUBLIC PROPERTY PATTERN (+PATTERNCHANGED EVENT)

		//perform the match
		public override int Match(string textToMatch, int matchAtIndex)
		{
			if (regEx == null)
				return 0;

			Match match = regEx.Match(textToMatch, matchAtIndex);

			if (match.Success)
			{
				return match.Length;
			}
			else
			{
				return 0;
			}
		}

		private void RegexPatternMatcher_PatternChanged(object sender, EventArgs e)
		{
			RegexOptions options = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline;

			regEx = new Regex(string.Format(@"\G({0})", this.Pattern), options);
		}
	}
}