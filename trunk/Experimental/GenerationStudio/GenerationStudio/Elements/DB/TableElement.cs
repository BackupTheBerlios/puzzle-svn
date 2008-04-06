using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;
using System.Data;
using System.Windows.Forms;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof(DataBaseElement))]
    [ElementName("Table")]
    [ElementIcon("GenerationStudio.Images.table.gif")]
    public class TableElement : NamedElement
    {
        [ElementVerb("View table data")]
        public void ViewTableContent(IHost host)
        {
            try
            {
                DataBaseElement db = Parent as DataBaseElement;
                IDbConnection connection = db.GetConnection();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = string.Format("select * from [{0}]", this.Name);
                connection.Open();
                IDataReader reader = command.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);

                reader.Close();
                connection.Close();
                TableDataView editor = host.GetEditor<TableDataView>(this, "View Table");
                editor.Data = dt;
                host.ShowEditor(editor);
            }
            catch(Exception x)
            {
                MessageBox.Show("An error occured");
            }
        }
        public override bool GetDefaultExpanded()
        {
            return false;
        }

        [ElementVerb("Exclude / Include")]
        public void ExcludeInclude(IHost host)
        {
            Excluded = !Excluded;
        }
    }
}
