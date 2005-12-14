using System.Collections;
using System.IO;

namespace Puzzle.SourceCode
{
	/// <summary>
	/// Language list class
	/// </summary>
	public class LanguageList
	{
		private ArrayList mLanguages = null;


		/// <summary>
		/// 
		/// </summary>
		public LanguageList()
		{
			mLanguages = new ArrayList();

			string[] files = Directory.GetFiles(".", "*.syn");
			foreach (string file in files)
			{
				//try
				//{
				SyntaxLoader l = new SyntaxLoader();
				mLanguages.Add(l.Load(file));
				//}
				//catch(System.Exception x)
				//{
				//	Console.WriteLine (x.Message);
				//}
			}

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public Language GetLanguageFromFile(string path)
		{
			string extension = Path.GetExtension(path);
			foreach (Language lang in mLanguages)
			{
				foreach (FileType ft in lang.FileTypes)
				{
					if (extension.ToLower() == ft.Extension.ToLower())
					{
						return lang;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ArrayList ListLanguages()
		{
			return mLanguages;
		}
	}
}