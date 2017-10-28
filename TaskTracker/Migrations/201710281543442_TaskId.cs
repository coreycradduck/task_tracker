namespace TaskTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timers", "TaskId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timers", "TaskId");
        }
    }
}
