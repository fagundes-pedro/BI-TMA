using BI_TMA.Web.Services;
using BI_TMA.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cXmBCekx1RXxbf1x1ZF1MYl9bQH9PIiBoS35Rc0VnW3xccHZWQmlfWEdxVEBU");

builder.Services.AddTransient<AssistenteAPI>();
builder.Services.AddTransient<LinhaAPI>();
builder.Services.AddTransient<ChamadaAPI>();
builder.Services.AddTransient<ImportarChamadasAPI>();
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

await builder.Build().RunAsync();
