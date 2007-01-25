using System;
namespace Puzzle.NAspect.Framework.Aop
{
    interface IPointcutTarget
    {
        bool IsMatch(System.Reflection.MethodBase method);
    }
}
