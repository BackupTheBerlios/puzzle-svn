namespace Puzzle.NCore.Framework.Compression
{
	/// <summary>
	/// Summary description for Packer.
	/// </summary>
	public class GawdCompressor : ICompressor
	{
		public GawdCompressor()
		{
		}

		private HuffmanCompressor huffmanPacker = new HuffmanCompressor();
		private PatternCompressor patternPacker = new PatternCompressor();

		public byte[] Compress(byte[] data)
		{
			byte[] patternPacked = patternPacker.Compress(data);
			byte[] huffmanPacked = huffmanPacker.Compress(patternPacked);
			return huffmanPacked;
		}

		public byte[] Decompress(byte[] data)
		{
			byte[] Unhuffed = huffmanPacker.Decompress(data);
			byte[] Unpattern = patternPacker.Decompress(Unhuffed);

			return Unpattern;
		}
	}
}