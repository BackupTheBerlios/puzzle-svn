using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Debug.Serialization;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Windows.Forms;
using Puzzle.Naspect.Debug.Forms;

namespace Puzzle.Naspect.Debug
{
    public class AopProxyVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {      
            SerializedProxy data = (SerializedProxy)objectProvider.GetObject ();
            //MessageBox.Show(data.ProxyType.FullName);
            AopProxyVisualizerForm form = new AopProxyVisualizerForm();
            windowService.ShowDialog(form);
        }
    }

    public class AopProxyObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            ISerializableProxy realProxy = target as ISerializableProxy;
           
            SerializedProxy serializedProxy = realProxy.GetSerializedProxy ();
            base.GetData(serializedProxy, outgoingData);
        }

       
    }


}
