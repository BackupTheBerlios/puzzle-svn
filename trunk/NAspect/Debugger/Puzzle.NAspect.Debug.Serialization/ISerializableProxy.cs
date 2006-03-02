using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization
{
    public interface ISerializableProxy
    {
        SerializedProxy GetSerializedProxy();
    }
}
