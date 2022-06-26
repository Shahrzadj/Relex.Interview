using Microsoft.EntityFrameworkCore;
using Relex.Interview.Data;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Data.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("InterviewDb")
    ,x=>x.MigrationsAssembly("Relex.Interview.Data")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>) );
builder.Services.AddTransient(typeof(IProductBatchRepository), typeof(ProductBatchRepository));

var app = builder.Build();

app.UseSwagger(); 
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(builder=>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.Run();
