

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;

namespace BlazorSignalRApp
{
  public class Message
  {
    public Message (string content, DateTime sentTime, string senderId)
    {
      Content = content;
      SentTime = sentTime;
      SenderId = senderId;
    }

    public int ID { get; set; }
    public string Content { get; set; }
    public DateTime SentTime { get; set; }
    public string SenderId { get; set; }

    public override string ToString()
    {
      return $"{SenderId}: {Content}";
    }

    //Create Sender here 

  }

  public class AppDBContext : IdentityDbContext
  {

    public AppDBContext (DbContextOptions<AppDBContext> options ) : base(options){}
    public DbSet<Message> Messages { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Message>().ToTable("Message");
    }
    
    //After creating user.
    //public DbSet<User> Users { get; set; }
  }

}