using System;
using Puzzle.NAspect.Extensions.Utils.Log4NET;

namespace Puzzle.NAspect.Extensions.Attributes.Logging.Log4NET
{
	/// <summary>
	/// Log4NET Attribute
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class Log4NetAttribute : System.Attribute
	{
		public enum Levels
		{
			DEBUG,
			INFO,
			WARN,
			ERROR,
			FATAL
		}

		private string m_strFormat = null;
		private string m_strLogPath = null;
		private Levels m_level = Levels.INFO;

		public string LogPath
		{
			get { return m_strLogPath; }
			set 
			{ 
				m_strLogPath = value; 
				Log4NetConfig.Configure(m_strLogPath);
			}
		}

		public string Format
		{
			get { return m_strFormat; }

			set 
			{ 
				m_strFormat = value; 
				
				if(m_strFormat == "") 
					m_strFormat = null;
			}
		}

		public Levels Level
		{
			get { return m_level; }
			set { m_level = value; }
		}
	}
}
