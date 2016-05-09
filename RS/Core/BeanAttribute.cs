using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.Core
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class BeanAttribute : Attribute
    {
        public string id { get; set; }
    }
}