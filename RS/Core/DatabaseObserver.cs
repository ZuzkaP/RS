using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.Core
{
    public abstract class DatabaseObserver
    {
        protected MainDB database;
        public abstract void Update(MainDB database);
    }
}