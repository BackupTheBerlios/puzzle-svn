namespace Puzzle.NAspect.Framework.Aop
{
	public interface IInterceptor
	{
		object HandleCall(MethodInvokation call);

	}
}