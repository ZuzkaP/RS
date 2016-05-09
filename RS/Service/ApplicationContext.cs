using RS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RS.Service
{
    public class ApplicationContext
    {
        private Dictionary<string, object> beans = new Dictionary<string, object>();

        public ApplicationContext(string context)
        {
            XmlDocument document = new XmlDocument();
            document.Load(context);

            XmlNodeList list = document.GetElementsByTagName("bean");
            foreach(XmlNode node in list)
            {
                string name = node.Attributes["id"].Value;
                string @class = node.Attributes["class"].Value;

                // add all beans to the context
                beans.Add(name, ResolveType(@class));
            }
        }

        private object ResolveType(string @class)
        {
            object obj = Activator.CreateInstance(Type.GetType(@class));
            return obj;
        }

        internal List<object> GetAllBeans()
        {
            return beans.Values.ToList();
        }

        public object GetBean(string id)
        {
            object @out;
            beans.TryGetValue(id, out @out);
            return @out;
        }
    }
}