using RS.Core;
using RS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ICollection<Trainings> GetTrainings()
        {
            return database.Trainings.ToList();
        }

        public DbSet<Trainings> GetRawTrainings()
        {
            return database.Trainings;
        }

        public void CreateTraining(Trainings training, Users users)
        {
            training.user_id = users.user_id;
            database.Trainings.Add(training);
            database.SaveChanges();
        }
    }
}