using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.FastTrack.Framework.Presenters
{
    public interface IDomainPresenter
    {
        object GetView(Type type);

        object GetView(object obj);

        object GetView(IList objects);

        object GetEditor(object obj);
    }
}
