using System;
using System.Collections;

namespace Puzzle.NCore.Framework.Logging
{
	public class LogManager : ILogManager
	{
		public LogManager()
		{
		}

		#region Public Property Loggers

		private IList loggers = new ArrayList();

		public IList Loggers
		{
			get { return this.loggers; }
			set { this.loggers = value; }
		}

		#endregion

		public virtual void Debug(object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Debug(message, verbose);
			}
		}

		public virtual void Info(object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Info(message, verbose);
			}
		}

		public virtual void Warn(object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Warn(message, verbose);
			}
		}

		public virtual void Error(object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Error(message, verbose);
			}
		}

		public virtual void Fatal(object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Fatal(message, verbose);
			}
		}

		public virtual void Debug(object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Debug(message, verbose, t);
			}
		}

		public virtual void Info(object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Info(message, verbose, t);
			}
		}

		public virtual void Warn(object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Warn(message, verbose, t);
			}
		}

		public virtual void Error(object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Error(message, verbose, t);
			}
		}

		public virtual void Fatal(object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Fatal(message, verbose, t);
			}
		}

		public virtual void Debug(object sender, object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Debug(sender, message, verbose);
			}
		}

		public virtual void Info(object sender, object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Info(sender, message, verbose);
			}
		}

		public virtual void Warn(object sender, object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Warn(sender, message, verbose);
			}
		}

		public virtual void Error(object sender, object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Error(sender, message, verbose);
			}
		}

		public virtual void Fatal(object sender, object message, object verbose)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Fatal(sender, message, verbose);
			}
		}

		public virtual void Debug(object sender, object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Debug(sender, message, verbose, t);
			}
		}

		public virtual void Info(object sender, object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Info(sender, message, verbose, t);
			}
		}

		public virtual void Warn(object sender, object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Warn(sender, message, verbose, t);
			}
		}

		public virtual void Error(object sender, object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Error(sender, message, verbose, t);
			}
		}

		public virtual void Fatal(object sender, object message, object verbose, Exception t)
		{
			foreach (ILogger logger in loggers)
			{
				logger.Fatal(sender, message, verbose, t);
			}
		}
	}
}