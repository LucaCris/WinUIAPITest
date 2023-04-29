using APIModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("Login", (LoginRequest req) => new LoginResponse() { Token = "123", Message = $"Login OK for '{req.UserName}'" });

app.MapPost("GetContent", (ContentRequest req) =>
{
    bool isLogged = req.Token == "123";
    return new ContentResponse() { Content = isLogged ? "ABCD" : null, Error = !isLogged, Message = !isLogged ? "Not logged" : null };
});

app.MapPost("LockContent", (LockContentRequest req) =>
{
    bool isLogged = req.Token == "123";
    bool hasLock = req.RequireLock;
    return new LockContentResponse() { Content = isLogged ? "EFGH" : null, CanModifyContent = hasLock, Error = !isLogged, Message = !isLogged ? "Not logged" : null };
});

app.Run();
