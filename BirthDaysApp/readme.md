# About

For an article focusing on an EF Core alternate base class for prototypes in console project that is easy to transfer to web projects.


## Console Project

```csharp
public partial class Context : ProtoTypeContext
{
    public Context()
    {
        
    }
    public virtual DbSet<BirthDays> BirthDays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BirthDays>(entity =>
        {
            entity.Property(e => e.YearsOld)
                .HasComputedColumnSql(
                    "((CONVERT([int],format(getdate(),'yyyyMMdd'))-CONVERT([int],format([BirthDate],'yyyyMMdd')))/(10000))", false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
```

---

```csharp
public abstract class ProtoTypeContext() : DbContext(BuildOptions())
{
    private static DbContextOptions BuildOptions()
    {
        var builder = new DbContextOptionsBuilder()
            .UseSqlServer(AppConnections.Instance.MainConnection);

        var env = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT") ?? "Production";

        if (env.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            builder.EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }

        return builder.Options;
    }
}
```

## Web Project

```csharp
public partial class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public virtual DbSet<BirthDay> BirthDays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BirthDay>(entity =>
        {
            entity.Property(e => e.YearsOld).HasComputedColumnSql("((CONVERT([int],format(getdate(),'yyyyMMdd'))-CONVERT([int],format([BirthDate],'yyyyMMdd')))/(10000))", false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
```

