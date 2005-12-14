using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Puzzle.NPresent.Framework.ViewModel
{
	/// <summary>
	/// Summary description for ViewSerializer.
	/// </summary>
	public class ViewSerializer
	{
		public ViewSerializer()
		{
		}

		
		public virtual IList Load(string fileName)
		{
			StreamReader reader = null;
			string xml = "";
			try
			{
				reader = File.OpenText(fileName);
				xml = reader.ReadToEnd() ;
				reader.Close() ;
				reader = null;
			}
			catch (Exception ex)
			{
				if (reader != null)
				{
					try
					{
						reader.Close();
					}
					catch
					{
					}
					throw new IOException("Could not load npersist xml mapping file!", ex); // do not localize
				}
			}
			return Deserialize(xml);
		}



		
		public IList Deserialize(string xml)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlNode xmlView;
			xmlDoc.LoadXml(xml);
			xmlView = xmlDoc.SelectSingleNode("view");
			return DeserializeView(xmlView);
		}

		protected virtual IList DeserializeView(XmlNode xmlDom)
		{
			IList list = new ArrayList();

			XmlNodeList xmlClassViews = xmlDom.SelectNodes("class");
			foreach (XmlNode xmlClassView in xmlClassViews)
			{
				ClassView classView = DeserializeClassView(xmlClassView);
				if (classView != null)
					list.Add(classView);
			}

			return list;
		}

		protected ClassView DeserializeClassView(XmlNode xmlClassView)
		{
			ClassView classView = new ClassView();

			if (!(xmlClassView.Attributes["name"] == null))
				classView.Name = xmlClassView.Attributes["name"].Value;
			else
				throw new FormatException("The class element must contain a name attribute!");

			XmlNodeList xmlPropertyViews = xmlClassView.SelectNodes("property");
			foreach (XmlNode xmlPropertyView in xmlPropertyViews)
				DeserializePropertyView(xmlPropertyView, classView);

			return classView;
		}

		protected void DeserializePropertyView(XmlNode xmlPropertyView, ClassView classView)
		{
			PropertyView propertyView = new PropertyView(classView);

			if (!(xmlPropertyView.Attributes["name"] == null))
				propertyView.Name = xmlPropertyView.Attributes["name"].Value;
			else
				throw new FormatException("The property element must contain a name attribute!");

			if (!(xmlPropertyView.Attributes["path"] == null))
				propertyView.Name = xmlPropertyView.Attributes["path"].Value;

			if (!(xmlPropertyView.Attributes["category"] == null))
				propertyView.Category = xmlPropertyView.Attributes["category"].Value;

			if (!(xmlPropertyView.Attributes["description"] == null))
				propertyView.Description = xmlPropertyView.Attributes["description"].Value;

			if (!(xmlPropertyView.Attributes["display-name"] == null))
				propertyView.DisplayName = xmlPropertyView.Attributes["display-name"].Value;

			if (!(xmlPropertyView.Attributes["read-only"] == null))
				propertyView.IsReadOnly  = ParseBool(xmlPropertyView.Attributes["display-name"].Value);

			if (!(xmlPropertyView.Attributes["default"] == null))
				propertyView.DefaultValue  = xmlPropertyView.Attributes["default"].Value;

		}

		
		protected virtual bool ParseBool(string str)
		{
			if (str.ToLower(CultureInfo.InvariantCulture) == "false" || str.ToLower(CultureInfo.InvariantCulture) == "0" || str.ToLower(CultureInfo.InvariantCulture) == "off" || str.ToLower(CultureInfo.InvariantCulture) == "no")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
