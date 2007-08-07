using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Puzzle.FastTrack.Framework.Controllers;
using Puzzle.FastTrack.Framework.Factories;
using System.Reflection;
using Puzzle.SideFX.Framework;

public partial class EditClass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        typeName = Request.QueryString["class"];

        if (!Page.IsPostBack)
            LoadPageData();
    }

    private string typeName = "";
    private IDomainController domainController;

    private IDomainController GetDomainController()
    {
        if (domainController == null)
            domainController = ControllerFactory.CreateDomainController();
        return domainController;
    }

    private void LoadPageData()
    {
        nameTextBox.Text = typeName;

        LoadDataTypes();
        LoadRelationshipMultiplicities();

        ListProperties();
    }

    private void LoadDataTypes()
    {
        propertyTypeDropDownList.Items.Add("System.Boolean");
        propertyTypeDropDownList.Items.Add("System.Byte");
        propertyTypeDropDownList.Items.Add("System.DateTime");
        propertyTypeDropDownList.Items.Add("System.Decimal");
        propertyTypeDropDownList.Items.Add("System.Int16");
        propertyTypeDropDownList.Items.Add("System.Int32");
        propertyTypeDropDownList.Items.Add("System.Int64");
        propertyTypeDropDownList.Items.Add("System.String");

        IDomainController domainController = GetDomainController();
        foreach (string typeName in domainController.GetTypeNames())
            relationshipTypeDropDownList.Items.Add(typeName);
    }

    private void LoadRelationshipMultiplicities()
    {
        relationshipMultiplicityDropDownList.Items.Add("Many-Many");
        relationshipMultiplicityDropDownList.Items.Add("One-Many");
        relationshipMultiplicityDropDownList.Items.Add("Many-One");
        relationshipMultiplicityDropDownList.Items.Add("Many-Many");
    }

    private void ListProperties()
    {
        Table table = new Table();

        Type type = GetDomainController().GetTypeFromTypeName(typeName); 
        if (type != null)
        {
            foreach (PropertyInfo property in type.GetProperties())
            {
                TableRow row = new TableRow();
                table.Rows.Add(row);

                TableCell cell = new TableCell();
                row.Cells.Add(cell);

                Label label = new Label();
                label.Text = property.Name;

                cell.Controls.Add(label);
            }
        }

        Panel panel = (Panel) this.FindControl("propertiesPanel");
        panel.Controls.Clear();
        panel.Controls.Add(table);        
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
    }

    protected void addPropertyButton_Click(object sender, EventArgs e)
    {
        string propertyName = propertyNameTextBox.Text;
        if (string.IsNullOrEmpty(propertyName))
            return;

        string propertyTypeName = propertyTypeDropDownList.SelectedValue;
        if (string.IsNullOrEmpty(propertyTypeName))
            return;

        if (string.IsNullOrEmpty(typeName))
            return;

        IEngine engine = EngineFactory.CreateEngine();

        string command = "create property " + typeName + "." + propertyName + " (Type = " + propertyTypeName + ", Nullable = " + nullableCheckBox.Checked.ToString();

        if (!string.IsNullOrEmpty(stringLengthTextBox.Text))
            command += ", Length = " + stringLengthTextBox.Text;

        if (!string.IsNullOrEmpty(columnNameTextBox.Text))
            command += ", Column = " + columnNameTextBox.Text;

        command += ")";

        engine.Execute(command);

        ListProperties();
    }
    protected void addRelationshipButton_Click(object sender, EventArgs e)
    {
        string propertyName = propertyNameTextBox.Text;
        if (string.IsNullOrEmpty(propertyName))
            return;

        string propertyTypeName = relationshipTypeDropDownList.SelectedValue;
        if (string.IsNullOrEmpty(propertyTypeName))
            return;

        string multiplicity = relationshipMultiplicityDropDownList.SelectedValue;
        if (string.IsNullOrEmpty(multiplicity))
            return;

        if (string.IsNullOrEmpty(typeName))
            return;

        IDomainController domainController = GetDomainController();
        Type type = domainController.GetTypeFromTypeName(typeName);
        Type propertyType = domainController.GetTypeFromTypeName(propertyTypeName);

        IEngine engine = EngineFactory.CreateEngine();

        string command = "create ";

        command += multiplicity.ToLower() + " ";

        command += typeName + "." + propertyName + " (Type = " + propertyTypeName;

        if (!string.IsNullOrEmpty(inverseNameTextBox.Text))
            command += ", Inverse = " + inverseNameTextBox.Text;


        command += ")";

        engine.Execute(command);

        ListProperties();
    }
    protected void addTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (addTypeDropDownList.SelectedValue == "Property")
        {
            propertyPanel.Visible = true;
            relationshipPanel.Visible = false;
        }
        else
        {
            propertyPanel.Visible = false;
            relationshipPanel.Visible = true;
        }
        ListProperties();
    }
}
