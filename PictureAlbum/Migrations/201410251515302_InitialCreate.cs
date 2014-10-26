namespace ImageOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PictureAlbums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        PictureAlbum_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PictureAlbums", t => t.PictureAlbum_Id)
                .Index(t => t.PictureAlbum_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "PictureAlbum_Id", "dbo.PictureAlbums");
            DropIndex("dbo.Pictures", new[] { "PictureAlbum_Id" });
            DropTable("dbo.Pictures");
            DropTable("dbo.PictureAlbums");
        }
    }
}
