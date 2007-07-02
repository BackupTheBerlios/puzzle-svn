using System;
using System.Text;
using System.Reflection.Emit;

namespace Puzzle.NAspect.Framework
{
	public class ExtendedProperty : ExtendedMember
	{
        public override void Extend(Type baseType, TypeBuilder typeBuilder)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
