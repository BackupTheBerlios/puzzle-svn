namespace Puzzle.NCore.Framework.Compression
{
	/// <summary>
	/// Summary description for IWebServiceCompressor.
	/// </summary>
	public interface IWebServiceCompressor
	{
		string Compress(string dataToCompress);

		string Decompress(string dataToDecompress);
	}
}