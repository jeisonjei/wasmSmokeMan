using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MatBlazor;
using wasmSmokeMan.Shared.SupplyStaircase.CompoundObjects;
using wasmSmokeMan.Shared.SupplyStaircase.Functions;
using wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaDependent;
using wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaIndependent;
using wasmSmokeMan.Shared.SupplyStaircase.SemanticObjects;
using wasmSmokeMan.Shared.SupplyStaircase.SimpleObjects;
using wasmSmokeMan.Shared.RemoveHall;
using Climate1 = wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaIndependent.Climate;
using Climate2=wasmSmokeMan.Shared.RemoveHall.Climate;
using Window = wasmSmokeMan.Shared.SupplyStaircase.SimpleObjects.Window;

namespace wasmSmokeMan.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            //регистрация классов, которые относятся к расчёту ПОДПОР В ЛЕСТНИЧНУЮ КЛЕТКУ. Функциональность некоторых классов повторяется, но решено всё равно для каждого расчёта создавать свои классы чтобы избежать путаницы
            Climate1 climate1 = new Climate1(-25, 20, 2);
            Floors floors = new Floors(1, 10);
            floors.AddRange((1, 10), 3.1);
            DoorOutside doorOutside = new DoorOutside(1.4, 2.4);
            DoorInside doorInside = new DoorInside(1.1, 2.1, DoorInside.Type.Usual, climate1);
            Pressures pressures = new Pressures(floors, climate1);
            Window window = new Window(1.8, 1, climate1, pressures);
            StairCase stair = new StairCase(16, StairCase.Portal.Straight, floors, doorOutside, 1, doorInside, window);
            MethodsSupplyStair methodsSupplyStair = new MethodsSupplyStair(stair, climate1);
            builder.Services.AddSingleton(climate1);
            builder.Services.AddSingleton(floors);
            builder.Services.AddSingleton(doorOutside);
            builder.Services.AddSingleton(doorInside);
            builder.Services.AddSingleton(pressures);
            builder.Services.AddSingleton(window);
            builder.Services.AddSingleton(stair);
            builder.Services.AddSingleton(methodsSupplyStair);
            //регистрация классов, которые относятся к расчёту ДЫМОУДАЛЕНИЕ ИЗ КОРИДОРОВ
            Climate2 climate2 = new Climate2(26, 26, 2);
            Opening opening=new Opening(1,2);
            List<Opening> openings=new List<Opening>();
            openings.Add(opening);
            Room room=new Room(25,2.8,openings,14,400,climate2);
            DoorHall doorHall=new DoorHall(1.1,2.1,DoorHall.Type.Usual,climate2);
            Hall hall=new Hall(30,15,2.8,doorHall,room,climate2,BuildingType.Residential);
            Network network=new Network(1,10,hall,climate2);
            builder.Services.AddSingleton(climate2);
            builder.Services.AddSingleton(opening);
            builder.Services.AddSingleton(room);
            builder.Services.AddSingleton(doorHall);
            builder.Services.AddSingleton(hall);
            builder.Services.AddSingleton(network);

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