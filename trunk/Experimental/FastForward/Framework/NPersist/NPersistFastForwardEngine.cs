using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastForward.Framework.Service;

namespace Puzzle.FastForward.Framework.NPersist
{
    public class NPersistFastForwardEngine : FastForwardEngine
    {
        public NPersistFastForwardEngine()
        {
            this.RegisterService<ISchemaService>(new NPersistSchemaService(this));
            this.RegisterService<IObjectService>(new NPersistObjectService(this));
        }
    }
}
