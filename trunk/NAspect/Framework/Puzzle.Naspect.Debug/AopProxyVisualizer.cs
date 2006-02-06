using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Debug.Serialization;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Windows.Forms;

namespace Puzzle.Naspect.Debug
{
    public class AopProxyVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            SerializedProxy s = (SerializedProxy)objectProvider.GetObject();
            MessageBox.Show (s.TypeName);
        }
    }

    public class AopProxyObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            ISerializableProxy realProxy = (ISerializableProxy)target;

            SerializedProxy serializedProxy = realProxy.GetSerializedProxy ();

            base.GetData(serializedProxy, outgoingData);
        }
    }
}
