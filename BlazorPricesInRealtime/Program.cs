using BlazorPricesInRealtime.Components;
using BlazorPricesInRealtime.Hubs;
using MessagePack;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = new[] { "text/octet-stream" };
});

builder.Services.AddSignalR(options =>
{
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(40);
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
    options.EnableDetailedErrors = true;
})
.AddMessagePackProtocol(options =>
{
    options.SerializerOptions = MessagePack.MessagePackSerializerOptions.Standard.WithSecurity(MessagePackSecurity.UntrustedData);
});

builder.Services.AddSingleton<MainHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseResponseCompression();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapHub<MainHub>("/main-hub");

app.MapRazorComponents<App>()    
    .AddInteractiveServerRenderMode();

app.Run();
