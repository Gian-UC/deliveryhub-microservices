var builder = WebApplication.CreateBuilder(args);

// habilita o Reverse Proxy lendo exatamente do appsettings.json
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.Run();