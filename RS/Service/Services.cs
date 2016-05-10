using RS.Core;
using RS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.Service
{
    public class Services
    {
        private static Services instance = new Services();
        private ApplicationContext applicationContext;

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static Services Instance { get { return instance; } }

        private List<DatabaseObserver> observers = new List<DatabaseObserver>();

        private Services()
        {
            applicationContext = new ApplicationContext(
                string.Format(@"C:\\Users\Zuzka\\Documents\\Visual Studio 2015\\Projects\\RS\RS\\Context\\context.xml", Environment.UserName)
            );

            foreach(object obj in applicationContext.GetAllBeans())
            {
                if(obj is DatabaseObserver)
                {
                    Attach(obj as DatabaseObserver);
                }
            }
        }

        private void Attach(DatabaseObserver observer)
        {
            observers.Add(observer);
        }

        public void Notify(MainDB database)
        {
            foreach(DatabaseObserver observer in observers)
            {
                observer.Update(database);
            }
        }

        public T GetBean<T>(string id)
        {
            return (T) applicationContext.GetBean(id);
        }
    }
}