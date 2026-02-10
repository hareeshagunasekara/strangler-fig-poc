var builder = WebApplication.CreateBuilder(args);

// Reverse proxy: load routes and clusters from config (appsettings.json "ReverseProxy" section).
// UseModernHomeConfigFilter applies the rollback toggle: when UseModernHome is false, / and /home go to legacy.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddConfigFilter<ProxyFacade.Configuration.UseModernHomeConfigFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// All traffic goes through YARP; no MVC endpoints so the facade is proxy-only.
app.MapReverseProxy();

app.Run();

