using SupplementsServer.API.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.
builder.Services.AddCors(o => o.AddPolicy("all", builder => {
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("all");

app.UseAuthorization();

app.MapControllers();

app.Run();
const string DATA_PATH = "./data.csv";
void ConfigureServices(IServiceCollection services) {
    services.AddTransient<ISupplementService>(serviceProvider => {
        return new SupplementService(DATA_PATH);
    });
}
