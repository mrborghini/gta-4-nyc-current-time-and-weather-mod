# Grand Theft Auto IV current New York city time and weather mod.

## What does this mod do?
This mod syncs your Grand Theft Auto IV time with the real current time in New York City and updates the weather every 30 minutes based on the real weather in New York City.

## How does it work?
For the time it's quite simple. This mod utilizes TimeZoneInfo api from Microsoft to retrieve the Eastern Standard Time (Without internet).

To retrieve the weather it gets a little more fun. So the weather cycle in GTA IV has the following weather options:

| Weather |
| -------- |
| Extra Sunny |
| Sunny |
| Sunny And Windy |
| Cloudy |
| Raining |
| Drizzle |
| Foggy |
| ThunderStorm |
| Extra Sunny 2 |
| Sunny And Windy 2 |

So how do we get the current weather from New York City? Well we retrieve the data from [here](https://api.open-meteo.com/v1/forecast?latitude=40.73061&longitude=-73.93524&current=weather_code&timezone=America%2FNew_York&forecast_days=1) and specifically the weather code which this code translates it to GTA weather. See [this for reference](https://www.nodc.noaa.gov/archive/arc0021/0002199/1.1/data/0-data/HTML/WMO-CODE/WMO4677.HTM).
Then it gets applied and then you have the real New York City weather inside of GTA IV.


## How to install? (Make sure you have Grand Theft Auto IV version v1.0.7.0)

1. Make sure you have curl installed by running `curl --version` in a cmd or powershell window (This is used to retrieve the weather in New York City) (Installed by default Windows 10 1803+)
2. [Download GTA 4 .Net ScriptHook](https://github.com/HazardX/gta4_scripthookdotnet/releases/tag/v1.7.1.7b)
3. Unzip it and add add the files and folders to your GTA IV folder
4. [Download the .dll file](https://github.com/mrborghini/gta-4-nyc-current-time-and-weather-mod/releases) and put in the scripts folder of your GTA IV
5. You should be able to launch GTA IV.

## How to build the mod?
1. Download and install [.NET Framework 4.8.1 Developer Pack](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net481)
2. Download and install any [.NET version](https://dotnet.microsoft.com/en-us/download) (It works with .NET 8.0)
3. Run the following command:
```
dotnet build
```
4. The `.dll` file should be inside of the `bin` folder