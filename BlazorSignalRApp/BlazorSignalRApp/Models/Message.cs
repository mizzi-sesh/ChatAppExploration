

using Microsoft.EntityFrameworkCore;
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

  public class AppDBContext : DbContext
  {

    public AppDBContext (DbContextOptions<AppDBContext> options ) : base(options){}
    public DbSet<Message> Messages { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Message>().ToTable("Message");
    }
    
    //After creating user.
    //public DbSet<User> Users { get; set; }
  }

}