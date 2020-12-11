using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using wasmSmokeMan.Shared.RemoveHall;
using wasmSmokeMan.Shared.SupplyElevator;
using wasmSmokeMan.Shared.SupplyStaircase.CompoundObjects;
using wasmSmokeMan.Shared.SupplyStaircase.Functions;
using wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaDependent;
using wasmSmokeMan.Shared.SupplyStaircase.SimpleObjects;
using Climate1 = wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaIndependent.Climate;
using Climate2 = wasmSmokeMan.Shared.RemoveHall.Climate;
using Climate3 = wasmSmokeMan.Shared.SupplyElevator.Climate;
using Floors1 = wasmSmokeMan.Shared.SupplyStaircase.SemanticObjects.Floors;
using Floors3 = wasmSmokeMan.Shared.SupplyElevator.Floors;
using Window = wasmSmokeMan.Shared.SupplyStaircase.SimpleObjects.Window;
using CurrieTechnologies.Razor.Clipboard;

namespace wasmSmokeMan.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(
                sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //регистрация классов, которые относятся к расчёту ПОДПОР В ЛЕСТНИЧНУЮ КЛЕТКУ. Функциональность некоторых классов повторяется, но решено всё равно для каждого расчёта создавать свои классы чтобы избежать путаницы
            Climate1 climate1 = new Climate1(-25, 20, 2);
            Floors1 floors1 = new Floors1(1, 10);
            floors1.AddRange((1, 10), 3.1);
            DoorOutside doorOutside = new DoorOutside(1.4, 2.4);
            DoorInside doorInside = new DoorInside(1.1, 2.1, DoorInside.Type.Usual, climate1);
            Pressures pressures = new Pressures(floors1, climate1);
            Window window = new Window(1.8, 1, climate1, pressures);
            StairCase stair = new StairCase(16, StairCase.Portal.Straight, floors1, doorOutside, 1, doorInside, window);
            MethodsSupplyStair methodsSupplyStair = new MethodsSupplyStair(stair, climate1);
            builder.Services.AddSingleton(climate1);
            builder.Services.AddSingleton(floors1);
            builder.Services.AddSingleton(doorOutside);
            builder.Services.AddSingleton(doorInside);
            builder.Services.AddSingleton(pressures);
            builder.Services.AddSingleton(window);
            builder.Services.AddSingleton(stair);
            builder.Services.AddSingleton(methodsSupplyStair);
            //регистрация классов, которые относятся к расчёту ДЫМОУДАЛЕНИЕ ИЗ КОРИДОРОВ
            Climate2 climate2 = new Climate2(26, 26, 2);
            List<Opening> openings = new List<Opening>();
            Room room = new Room(25, 2.8, openings, 14, 400, climate2);
            DoorHall doorHall = new DoorHall(1.1, 2.1, DoorHall.Type.Usual, climate2);
            Hall hall = new Hall(30, 15, 2.8, doorHall, room, climate2, BuildingType.Residential);
            Network network = new Network(1, 10, hall, climate2);
            builder.Services.AddSingleton(climate2);
            builder.Services.AddSingleton(room);
            builder.Services.AddSingleton(doorHall);
            builder.Services.AddSingleton(hall);
            builder.Services.AddSingleton(network);
            //регистрация классов, которые относятся к расчёту ПОДПОР В ЛИФТ
            Climate3 climate3 = new Climate3(-25, 18, 2);
            Floors3 floors3 = new Floors3(1, 10);
            floors3.AddRange((1, 10), 3.2);
            ElevatorDoor elevatorDoor = new ElevatorDoor(1, 2);
            HallDoor hallDoor = new HallDoor(1.1, 2.1);
            Elevator elevator = new Elevator(floors3, 4, 5, elevatorDoor, hallDoor, climate3, false);
            builder.Services.AddSingleton(climate3);
            builder.Services.AddSingleton(floors3);
            builder.Services.AddSingleton(elevatorDoor);
            builder.Services.AddSingleton(hallDoor);
            builder.Services.AddSingleton(elevator);
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 4000;
            });
            builder.Services.AddClipboard();
            await builder.Build().RunAsync();
        }
    }
}