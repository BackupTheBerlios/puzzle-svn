using System;
using System.Xml;
using System.Configuration;


namespace Puzzle.NAspect.Extensions.Utils.Log4NET
{
	/// <summary>
	/// Summary description for Log4NetConfig.
	/// </summary>
	public class Log4NetConfig
	{
		public static void Configure(string strAppSettingsRepository) 
		{
			if(strAppSettingsRepository != null) 
			{
				try 
				{
					log4net.Repository.ILoggerRepository rep = 
						log4net.LogManager.CreateRepository(strAppSettingsRepository);

					string strUri = ConfigurationSettings.AppSettings[AppSettingsRepository];

					log4net.Config.XmlConfigurator.Configure(rep, new Uri(strUri));
				}
				catch (Exception _ex) 
				{
					//xxxxx
					_ex = _ex;
				}
			}
		}
	}
}