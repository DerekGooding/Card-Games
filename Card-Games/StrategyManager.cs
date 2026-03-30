using System.Text.Json;
using System.Text.Json.Serialization;

namespace Card_Games;

public class StrategyManager
{
    private static readonly JsonSerializerOptions _saveOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
    };

    private static readonly JsonSerializerOptions _loadOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };


    public static void SaveStrategyToJson(Strategy strategy, string filePath)
    {
        try
        {
            // Kreiraj direktorijum ako ne postoji
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonSerializer.Serialize(strategy, _saveOptions);

            // Napiši u fajl
            File.WriteAllText(filePath, json);

            Console.WriteLine($"✓ Strategija sačuvana: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Greška pri čuvanju: {ex.Message}");
        }
    }

    public static Strategy LoadStrategyFromJson(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Fajl ne postoji: {filePath}");
            }

            string json = File.ReadAllText(filePath);

            Strategy strategy = JsonSerializer.Deserialize<Strategy>(json, _loadOptions);

            Console.WriteLine($"✓ Strategija učitana: {filePath}");
            return strategy;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Greška pri učitavanju: {ex.Message}");
            return null;
        }
    }
}