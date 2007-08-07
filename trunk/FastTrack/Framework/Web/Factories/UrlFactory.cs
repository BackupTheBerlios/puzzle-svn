using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Puzzle.FastTrack.Framework.Web.Structures;

namespace Puzzle.FastTrack.Framework.Web.Factories
{
    public class UrlFactory
    {

        #region Urls

        #region GetTypeEditUrl()

        public static string GetTypeEditUrl(string typeName, string specified)
        {
            string url = "";

            if (specified != null && specified != "")
                url = specified;

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["EditClassUrl-" + typeName];

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["EditClassUrl"];

            if (url == null || url == "")
                url = "EditClass.aspx";

            url += "?" + GetClassParameterName() + "=" + typeName;

            return url;
        }

        #endregion

        #region GetObjectViewUrl

        public static string GetObjectViewUrl(string typeName, string id, string specified)
        {
            string url = "";

            if (specified != null && specified != "")
                url = specified;

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["ViewUrl-" + typeName];

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["ViewUrl"];

            if (url == null || url == "")
                url = "View.aspx";

            url += "?" + GetTypeParameterName() + "=" + typeName;

            url += "&" + GetIdentityParameterName() + "=" + id;

            return url;
        }

        #endregion

        #region GetObjectEditUrl

        public static string GetObjectEditUrl(string typeName, string id, string specified)
        {
            string url = "";

            if (specified != null && specified != "")
                url = specified;

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["EditUrl-" + typeName];

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["EditUrl"];

            if (url == null || url == "")
                url = "Edit.aspx";

            url += "?" + GetTypeParameterName() + "=" + typeName;

            url += "&" + GetIdentityParameterName() + "=" + id;

            return url;
        }

        public static string GetObjectEditUrl(string typeName, string specified)
        {
            string url = "";

            if (specified != null && specified != "")
                url = specified;

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["EditUrl-" + typeName];

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["EditUrl"];

            if (url == null || url == "")
                url = "Edit.aspx";

            url += "?" + GetClassParameterName() + "=" + typeName;

            return url;
        }

        #endregion

        #region GetObjectListUrl

        public static string GetObjectListUrl(string className, string specified)
        {
            return GetObjectListUrl(className, "", "", "", specified); 
        }

        public static string GetObjectListUrl(string className, string typeName, string id, string propertyName, string specified)
        {
            string url = "";

            if (specified != null && specified != "")
                url = specified;

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["ListUrl-" + className];

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["ListUrl"];

            if (url == null || url == "")
                url = "List.aspx";

            url += "?" + GetClassParameterName() + "=" + className;

            if (typeName != "")
                url += "&" + GetTypeParameterName() + "=" + typeName;

            if (id != "")
                url += "&" + GetIdentityParameterName() + "=" + id;

            if (propertyName != "")
                url += "&" + GetPropertyParameterName() + "=" + propertyName;

            return url;
        }

        #endregion

        #region GetClassListUrl

        public static string GetClassListUrl(string specified)
        {
            string url = "";

            if (specified != null && specified != "")
                url = specified;

            if (url == null || url == "")
                url = System.Configuration.ConfigurationManager.AppSettings["DefaultUrl"];

            if (url == null || url == "")
                url = "Default.aspx";

            return url;
        }

        #endregion

        #region GetListPageUrl

        public static string GetListPageUrl(HttpRequest request, int currentPage, bool next, string propertyName)
        {
            if (propertyName == null)
                propertyName = "";

            string parameterName = propertyName.ToLower() + "page";

            int nextPage = currentPage;
            if (next)
                nextPage++;
            else
                nextPage--;

            IList<NameValuePair> addParameters = new List<NameValuePair>();
            addParameters.Add(new NameValuePair(parameterName, nextPage));

            return UpdateParametersInUrl(request, addParameters, null);
        }

        #endregion

        #region GetListSortUrl

        public static string GetListSortUrl(HttpRequest request, string sortPropertyName, bool descending, string propertyName)
        {
            if (propertyName == null)
                propertyName = "";

            string parameterName = propertyName.ToLower() + "sort";

            IList<NameValuePair> addParameters = new List<NameValuePair>();
            addParameters.Add(new NameValuePair(parameterName, sortPropertyName));

            IList<string> removeParameters = new List<string>();

            parameterName = propertyName.ToLower() + "desc";

            if (descending)
                addParameters.Add(new NameValuePair(parameterName, "1"));
            else
                removeParameters.Add(parameterName);

            return UpdateParametersInUrl(request, addParameters, removeParameters);
        }

        #endregion

        #region UpdateParametersInUrl

        private static string UpdateParametersInUrl(HttpRequest request, IList<NameValuePair> addParameters, IList<string> removeParameters)
        {
            string url = request.Path + "?";

            IList<string> parameterNames = new List<string>();
            if (addParameters != null)
                foreach (NameValuePair pair in addParameters)
                    parameterNames.Add(pair.Name.ToLower());

            if (removeParameters != null)
                foreach (string name in removeParameters)
                    parameterNames.Add(name.ToLower());

            foreach (string name in request.QueryString.AllKeys)
            {
                if (!parameterNames.Contains(name.ToLower()))
                    url += name + "=" + request.QueryString[name] + "&";
            }

            if (addParameters != null)
                foreach (NameValuePair pair in addParameters)
                    url += pair.Name + "=" + pair.Value.ToString() + "&";

            url = url.Substring(0, url.Length - 1);

            return url;
        }

        #endregion

        #endregion

        #region Parameter Names

        public static string GetTypeParameterName()
        {
            string name = "";

            name = System.Configuration.ConfigurationManager.AppSettings["TypeParameter"];

            if (name == null || name == "")
                name = "type";

            return name;
        }

        public static string GetIdentityParameterName()
        {
            string name = "";

            name = System.Configuration.ConfigurationManager.AppSettings["IdentityParameter"];

            if (name == null || name == "")
                name = "id";

            return name;
        }

        public static string GetPropertyParameterName()
        {
            string name = "";

            name = System.Configuration.ConfigurationManager.AppSettings["PropertyParameter"];

            if (name == null || name == "")
                name = "property";

            return name;
        }

        public static string GetClassParameterName()
        {
            string name = "";

            name = System.Configuration.ConfigurationManager.AppSettings["ClassParameter"];

            if (name == null || name == "")
                name = "class";

            return name;
        }

        #endregion

    }
}
