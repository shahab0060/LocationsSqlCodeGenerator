using System.Text.Json;
using System.Net.Http;
using System.Diagnostics.Metrics;
using System.Text;
public class City
{
    public string name { get; set; }
}

public class State
{
    public string name { get; set; }
    public List<City> cities { get; set; }
}

public class Country
{
    public string name { get; set; }
    public List<State> states { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/refs/heads/master/json/countries%2Bstates%2Bcities.json"; // Replace with real JSON URL
        var httpClient = new HttpClient();
        var json = await httpClient.GetStringAsync(url);

        var countries = JsonSerializer.Deserialize<List<Country>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        int idCounter = 1;
        var sqlBuilder = new StringBuilder();
        var countryIdMap = new Dictionary<string, int>();

        foreach (var country in countries)
        {
            int countryId = idCounter++;
            countryIdMap[country.name] = countryId;
            sqlBuilder.AppendLine($@"INSERT INTO ""Locations"" (""Id"", ""Title"", ""ParentId"",""CreateDate"",""IsDelete"") 
VALUES ({countryId}, '{Escape(country.name)}', NULL,Now(),FALSE);");

            if (country.states == null) continue;

            foreach (var state in country.states)
            {
                int stateId = idCounter++;
                sqlBuilder.AppendLine($@"INSERT INTO ""Locations"" (""Id"", ""Title"", ""ParentId"",""CreateDate"",""IsDelete"") 
VALUES ({stateId}, '{Escape(state.name)}', {countryId},Now(),FALSE);");

                if (state.cities == null) continue;

                foreach (var city in state.cities)
                {
                    int cityId = idCounter++;
                    sqlBuilder.AppendLine($@"INSERT INTO ""Locations"" (""Id"", ""Title"", ""ParentId"",""CreateDate"",""IsDelete"")  " +
                        $"VALUES ({cityId}, '{Escape(city.name)}', {stateId},Now(),FALSE);");
                }
            }
        }

        // Output T-SQL
        File.WriteAllText("locations.sql", sqlBuilder.ToString());
        Console.WriteLine("SQL file generated: locations.sql");
        Console.ReadKey();
    }

    static string Escape(string input)
    {
        return input.Replace("'", "''");
    }
}
