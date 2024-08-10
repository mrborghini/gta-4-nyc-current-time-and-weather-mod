using System;
using System.Diagnostics;

namespace GtaIVTimeAndWeater.Core
{
    class Weather
    {
        // Api we will use to fetch the weather
        private const string BASEURL = "https://api.open-meteo.com";
        public float Latitude { get; }
        public float Longitude { get; }

        public string DebugInfo { get; set; }

        public GTA.Weather CurrentWeather { get; private set; }

        public Weather(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public void UpdateWeather()
        {
            // Get the weather code
            int weatherCode = FetchWeatherCode();

            AddDebugInfo($"Current weather code: {weatherCode}");

            CurrentWeather = ConvertToGTAWeather(weatherCode);
        }

        private int FetchWeatherCode()
        {
            // Fetch the weather code based on the longitude and latitude
            string fetchUrl = $"{BASEURL}/v1/forecast?latitude={Latitude}&longitude={Longitude}&current=weather_code&timezone=America%2FNew_York&forecast_days=1";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "curl.exe",
                Arguments = fetchUrl,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                string jsonString = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                return GetWeatherCodeFromString(jsonString);
            }
        }

        private int GetWeatherCodeFromString(string data)
        {
            try
            {
                // Get rid of json
                // Use a single character for the split method
                string[] splitData = data.Split(new[] { "\"weather_code\":" }, StringSplitOptions.None);

                // Split the part that contains the weather code
                string[] codeStringArray = splitData[2].Split(new[] { '}' }, StringSplitOptions.RemoveEmptyEntries);

                // Extract the weather code string
                string codeString = codeStringArray[0].Trim();

                // Parse the code to integer
                int code = int.Parse(codeString);

                return code;
            }
            catch (Exception e)
            {
                AddDebugInfo($"Could not get code: {e.Message}");
                return -1;
            }
        }

        private GTA.Weather ConvertToGTAWeather(int weatherCode)
        {
            // Based on the real weather codes return the GTA weather equivelent.
            // See https://www.nodc.noaa.gov/archive/arc0021/0002199/1.1/data/0-data/HTML/WMO-CODE/WMO4677.HTM 
            // as reference

            switch (weatherCode)
            {
                case 0:
                    return GTA.Weather.ExtraSunny;
                case 1:
                    return GTA.Weather.Sunny;
                case 3:
                case 19:
                    return GTA.Weather.Cloudy;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 28:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    return GTA.Weather.Foggy;
                case 13:
                case 17:
                case 29:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                    return GTA.Weather.ThunderStorm;
                case 14:
                case 15:
                case 16:
                case 20:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    return GTA.Weather.Drizzle;
                case 18:
                    return GTA.Weather.SunnyAndWindy;
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                case 91:
                case 92:
                case 93:
                case 94:
                    return GTA.Weather.Raining;
                default:
                    return GTA.Weather.Sunny;
            }
        }
        private void AddDebugInfo(string info)
        {
            DebugInfo += $"{info} ";
        }
    }
}