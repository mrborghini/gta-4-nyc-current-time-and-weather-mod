using System;
using GTA;
using GtaIVTimeAndWeater.Core;

class Program : Script
{
    private readonly Time _time;
    private readonly GtaIVTimeAndWeater.Core.Weather _weather;

    private GTA.Weather _currentWeather;

    private int _lastMinute;

    public Program()
    {
        Game.DisplayText("gta-4-nyc-current-time-and-weather-mod is now active");

        _time = new Time();
        // Coordinates of new york city
        _weather = new GtaIVTimeAndWeater.Core.Weather(40.730610f, -73.935242f);

        // Update the time every 0.5 seconds
        Timer currentTimeTimer = new Timer(500);
        currentTimeTimer.Tick += UpdateTimeTimer;
        currentTimeTimer.Start();

        // Update the weather every 30 minutes
        Timer newWeatherTimer = new Timer(1800000);
        newWeatherTimer.Tick += UpdateWeatherTimer;
        newWeatherTimer.Start();

        // Update everything on startup
        UpdateWeather();
        UpdateTime();
    }

    private void UpdateTimeTimer(object sender, EventArgs e)
    {
        UpdateTime();

        GTA.Weather detectedWeather = World.Weather;

        if (detectedWeather != _currentWeather)
        {
            Game.Console.Print($"Detected weather {detectedWeather} which is different than {_currentWeather}");
            World.Weather = _currentWeather;
        }

    }

    private void UpdateWeatherTimer(object sender, EventArgs e)
    {
        UpdateWeather();
    }

    private void UpdateTime()
    {
        // Set the time to the new york timezone
        _time.UpdateTime();

        // If the minute hasn't changed yet don't do anything
        if (_time.CurrentMinute != _lastMinute)
        {
            // Lock the time to the current hour and minute
            World.LockDayTime(_time.CurrentHour, _time.CurrentMinute);

            _lastMinute = _time.CurrentMinute;
        }
    }

    private void UpdateWeather()
    {
        _weather.UpdateWeather();
        World.Weather = _weather.CurrentWeather;
        _currentWeather = _weather.CurrentWeather;
        Game.Console.Print(_weather.DebugInfo);
        Game.Console.Print($"It's now {_weather.CurrentWeather} in New York City");
        _weather.DebugInfo = "";
    }
}
