namespace TaskTracker.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class dbTask : DbContext
    {
        // Your context has been configured to use a 'dbTask' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TaskTracker.Models.dbTask' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'dbTask' 
        // connection string in the application configuration file.
        public dbTask()
            : base("name=dbTask")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Timer> Timers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    }

    public class Timer
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime start_timestamp { get; set; }
        public DateTime end_timestamp { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}