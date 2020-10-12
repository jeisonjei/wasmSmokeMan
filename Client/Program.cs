using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using wasmSmokeMan.Shared.NaturalPhenomenaIndependent;
using wasmSmokeMan.Shared.SemanticObjects;
using wasmSmokeMan.Shared.SimpleObjects;
using wasmSmokeMan.Shared.NaturalPhenomenaDependent;
using wasmSmokeMan.Shared.CompoundObjects;
using wasmSmokeMan.Shared.Functions;
using MatBlazor;

namespace wasmSmokeMan.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            Climate climate = new Climate(-25, 20, 2);
            Floors floors = new Floors(1, 10);
            floors.AddRange((1, 10), 3.1);
            DoorOutside doorOutside = new DoorOutside(1.4, 2.4);
            DoorInside doorInside = new DoorInside(1.1, 2.1,DoorInside.Type.Usual,climate);
            Pressures pressures = new Pressures(floors, climate);
            Window window = new Window(1.8, 1, climate, pressures);
            StairCase stair = new StairCase(16, StairCase.Portal.Straight, floors, doorOutside, 1, doorInside, window);
            MethodsSupplyStair methodsSupplyStair = new MethodsSupplyStair(stair, climate);
            builder.Services.AddSingleton(climate);
            builder.Services.AddSingleton(floors);
            builder.Services.AddSingleton(doorOutside);
            builder.Services.AddSingleton(doorInside);
            builder.Services.AddSingleton(pressures);
            builder.Services.AddSingleton(window);
            builder.Services.AddSingleton(stair);
            builder.Services.AddSingleton(methodsSupplyStair);
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 4000;
            });
            await builder.Build().RunAsync();
        }
    }
}
