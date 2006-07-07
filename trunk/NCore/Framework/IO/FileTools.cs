using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Puzzle.NCore.Framework.IO
{
	public class FileTools
	{
		public static StreamWriter GetFileWriter(string path)
		{
			StreamWriter sw = new StreamWriter(path,false,Encoding.Default) ;
			return sw;
		}
	}
}
