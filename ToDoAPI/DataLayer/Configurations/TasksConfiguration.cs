using DataLayer.Entities;
using DataLayer.Entities.EnumClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;

namespace DataLayer.Configurations
{
    public class TasksConfiguration : IEntityTypeConfiguration<TodoTask>
    {
        public void Configure(EntityTypeBuilder<TodoTask> builder)
        {
            // 1️⃣ Primary Key
            builder.HasKey(t => t.TaskID);

            builder.Property(t => t.TaskID)
                .ValueGeneratedOnAdd(); // Identity / Auto-Increment

            builder.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(t => t.description)
                .HasMaxLength(1000);

            builder.Property(t => t.userID)
                .IsRequired();

            builder.Property(t => t.status)
                .HasConversion(
                    v => (byte)v,
                    v => (enTaskStatus)v
                )
                .HasColumnType("TINYINT")
                .IsRequired();

            builder.Property(t => t.createdAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(t => t.updatedAt)
                .IsRequired(false);

            builder.Property(t => t.DueDate)
                .IsRequired();

            builder.Property(t => t.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(t => t.user)
                .WithMany(u => u.todoTasks)
                .HasForeignKey(t => t.userID)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed Data
            builder.HasData(
             new TodoTask
             {
                 TaskID = 1,
                 title = "مهمة الادمن",
                 description = "مهمة تجريبية للادمن",
                 userID = "user-admin",
                 status = enTaskStatus.Done,
                 createdAt = new DateTime(2025, 10, 27, 0, 0, 0, DateTimeKind.Utc),
                 DueDate = new DateTime(2025, 10, 30, 0, 0, 0, DateTimeKind.Utc),
                 IsDeleted = false
             },
    new TodoTask
    {
        TaskID = 2,
        title = "مهمة المستخدم",
        description = "مهمة تجريبية للمستخدم",
        userID = "user-normal",
        status = enTaskStatus.pending,
        createdAt = new DateTime(2025, 10, 27, 0, 0, 0, DateTimeKind.Utc),
        DueDate = new DateTime(2025, 10, 30, 0, 0, 0, DateTimeKind.Utc),
        IsDeleted = false
    }
            );

        }
    }
}
