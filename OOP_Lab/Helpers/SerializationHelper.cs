using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using OOP_Lab.Entities;

public static class SerializationHelper
{
    public static void SerializeToCsv(List<ApplicationEntitie> applications, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var app in applications)
            {
                writer.WriteLine(app.ToString());
            }
        }
    }

    public static List<ApplicationEntitie> DeserializeFromCsv(string filePath)
    {
        var applications = new List<ApplicationEntitie>();

        foreach (var line in File.ReadLines(filePath))
        {
            if (ApplicationEntitie.TryParse(line, out ApplicationEntitie app))
            {
                applications.Add(app);
            }
        }

        return applications;
    }

    public static void SerializeToJson(List<ApplicationEntitie> applications, string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        string json = JsonSerializer.Serialize(applications, options);
        File.WriteAllText(filePath, json);
    }

    public static List<ApplicationEntitie> DeserializeFromJson(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<ApplicationEntitie>>(json)
                ?? new List<ApplicationEntitie>();
        }
        catch
        {
            return new List<ApplicationEntitie>();
        }
    }
}
