namespace Puzzle.NAspect.Framework.Aop
{
	public interface IProxyAware
	{
		void SetProxy(IAopProxy target);
	}
}