using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;

namespace Puzzle.NAspect.Standard
{
    [Mixin(typeof(DirtyTrackedMixin))]
    public class DirtyTrackedAspect : ITypedAspect
    {

        [Interceptor(Index = 1, TargetAttribute = typeof(MakeDirtyAttribute))]
        public void MakeDirty(BeforeMethodInvocation call)
        {
            IDirtyTracked target = call.Target as IDirtyTracked;
            target.IsDirty = true;            
        }

        [Interceptor(Index = 1, TargetAttribute = typeof(ClearDirtyAttribute))]
        public void ClearDirty(BeforeMethodInvocation call)
        {
            IDirtyTracked target = call.Target as IDirtyTracked;
            target.IsDirty = false;    
        }
    }
}
