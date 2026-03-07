using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Poker
{
    public class StrategyManager
    {
        
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

                // Konvertuj u JSON sa lepim formatiranjem
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IncludeFields = true,
                    NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals

                };

                string json = JsonSerializer.Serialize(strategy, options);

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
                
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                Strategy strategy = JsonSerializer.Deserialize<Strategy>(json, options);

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
}