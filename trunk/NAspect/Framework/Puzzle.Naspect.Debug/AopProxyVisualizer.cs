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
            SerializedProxy data = (SerializedProxy)objectProvider.GetObject ();
            MessageBox.Show ("apa" + data.TypeName);
        }
    }

    public class AopProxyObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            ISerializableProxy realProxy = target as ISerializableProxy;
           
            SerializedProxy serializedProxy = realProxy.GetSerializedProxy ();
            SerializedProxy s2 = new  SerializedProxy();
            s2.TypeName = serializedProxy.TypeName;
            base.GetData(s2, outgoingData);
        }

       
    }


}
