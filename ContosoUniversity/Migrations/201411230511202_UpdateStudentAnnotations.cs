namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentAnnotations : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Student", name: "FirstMidName", newName: "FirstName");
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "FirstName", c => c.String());
            AlterColumn("dbo.Student", "LastName", c => c.String());
            RenameColumn(table: "dbo.Student", name: "FirstName", newName: "FirstMidName");
        }
    }
}
