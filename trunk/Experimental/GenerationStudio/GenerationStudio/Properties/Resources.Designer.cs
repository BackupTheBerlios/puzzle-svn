﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenerationStudio.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GenerationStudio.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static System.Drawing.Bitmap _class {
            get {
                object obj = ResourceManager.GetObject("class", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;SyntaxDefinition Name=&quot;Visual C#&quot; Startblock=&quot;CS Code&quot;&gt;
        ///&lt;FileTypes&gt;
        ///	&lt;FileType Extension=&quot;.cs&quot; Name=&quot;C# code file&quot;/&gt;
        ///&lt;/FileTypes&gt;
        ///
        ///
        ///
        ///
        ///	&lt;Block Name=&quot;CS Code&quot; Style=&quot;CS Code&quot; EscapeChar=&quot;&quot; IsMultiline=&quot;true&quot;&gt;
        ///		&lt;Scope Start=&quot;{&quot; End=&quot;}&quot; Style=&quot;CS Scope&quot; Text=&quot;{...}&quot; CauseIndent=&quot;true&quot; /&gt;
        ///		&lt;Scope Start=&quot;#if&quot; End=&quot;#endif&quot; Style=&quot;CS Region&quot; Text=&quot;#if...endif&quot;  /&gt;
        ///		&lt;Scope Start=&quot;#region&quot; End=&quot;#endregion&quot; Style=&quot;CS Region&quot; Text=&quot;&quot; DefaultExpanded=&quot;false&quot; /&gt;
        ///		&lt;Bracket [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CSharp {
            get {
                return ResourceManager.GetString("CSharp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;SyntaxDefinition Name=&quot;C# template&quot; Startblock=&quot;Text&quot;&gt;
        ///&lt;FileTypes&gt;
        ///	&lt;FileType Extension=&quot;.cst&quot; Name=&quot;C# template file&quot;/&gt;
        ///&lt;/FileTypes&gt;
        ///
        ///
        ///  &lt;Block Name=&quot;Text&quot; Style=&quot;Text&quot; EscapeChar=&quot;&quot; IsMultiline=&quot;true&quot;&gt;
        ///    &lt;childSpanDefinitions&gt;      
        ///      &lt;Child Name=&quot;CS Directive&quot; /&gt;
        ///      &lt;Child Name=&quot;CS Code&quot; /&gt;
        ///    &lt;/childSpanDefinitions&gt;
        ///  &lt;/Block&gt;
        ///
        ///	&lt;Block Name=&quot;CS Directive&quot; Style=&quot;CS Directive Code&quot; EscapeChar=&quot;&quot; IsMultiline=&quot;true&quot;&gt;
        ///    &lt;Scope Start=&quot;&amp;lt;%@&quot; End=&quot;%&amp;gt;&quot; Style=&quot;CS Sc [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CSharpTemplate {
            get {
                return ResourceManager.GetString("CSharpTemplate", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap project {
            get {
                object obj = ResourceManager.GetObject("project", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap template {
            get {
                object obj = ResourceManager.GetObject("template", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
