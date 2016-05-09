using RS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.DAO
{
    public class TrainingDao : DatabaseObserver
    {
        public override void Update(MainDB database)
        {
            this.database = database;
        }
    }
}