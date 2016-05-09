using RS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RS.Service
{
    public class BeanResolver
    {
        public static void ResolveBeansForClass(object obj)
        {
            var props = obj.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(BeanAttribute)));
            foreach (PropertyInfo property in props)
            {
                BeanAttribute attribute = (BeanAttribute)property.GetCustomAttribute(typeof(BeanAttribute));
                string beanId = attribute.id;

                property.SetValue(obj, Services.Instance.GetBean<object>(beanId));
            }
        }
    }
}