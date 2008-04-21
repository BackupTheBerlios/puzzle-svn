using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlCommentData
    {        
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        string Text { get; set; }
    }
}
