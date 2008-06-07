namespace Puzzle.SourceCode.SyntaxDocumentParsers
{
    public class ScanResultSegment
    {
        public BlockType BlockType;
        public bool HasContent;
        public bool IsEndSegment;
        public Pattern Pattern;
        public int Position;
        public Scope Scope;
        public Segment Segment;
        public string Token = "";
    }
}
