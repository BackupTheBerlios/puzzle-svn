using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.SideFX.Framework;

namespace Puzzle.FastForward.Framework.Service
{
    public class TextRenderService : IRenderService
    {
        public TextRenderService(IEngine engine)
        {
            this.engine = engine;
        }

        private IEngine engine;


        #region IRenderService Members

        public object Render(object obj, bool list)
        {
            StringBuilder sb = new StringBuilder();

            ISchemaService schemaService = engine.GetService<ISchemaService>();
            IObjectService objectService = engine.GetService<IObjectService>();

            string className = objectService.GetTypeName(obj);

            sb.Append(className + ": ");

            if (!list)
                sb.Append(Environment.NewLine);

            IList<string> propertyNames = schemaService.GetPropertyNames(obj.GetType());
            foreach (string propertyName in propertyNames)
            {
                sb.Append(propertyName + "=");
                if (objectService.IsNull(obj, propertyName))
                {
                    sb.Append("{null}");
                }
                else
                {
                    sb.Append(objectService.GetProperty(obj, propertyName).ToString());
                }
                if (list)
                    sb.Append(", ");
                else
                    sb.Append(Environment.NewLine);
            }
            
            if (list)
                if (propertyNames.Count > 0)
                    sb.Length -= 2;

            return sb.ToString();
        }

        #endregion
    }
}
