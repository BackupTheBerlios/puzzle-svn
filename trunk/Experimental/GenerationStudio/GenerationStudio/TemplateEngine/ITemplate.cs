using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GenerationStudio.Elements;

namespace GenerationStudio.TemplateEngine
{
    public interface ITemplate
    {
        void Render(TextWriter output, RootElement root);
    }
}
