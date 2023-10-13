using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tasks.Service.Utilities;

public static class JsonUtilities
{
    public static async Task<T?> ReadAsync<T>(string filePath)
    {
        using FileStream stream = File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }


    public static async Task WriteAsync<T>(string filePath, T payload, bool replace = true)
    {
        using FileStream stream = replace ? File.Create(filePath) : File.OpenWrite(filePath);
        await JsonSerializer.SerializeAsync(stream, payload);
    }




    public static string ToJsonString<T>(T payload)
    {
        var result = JsonSerializer.Serialize(
            payload, 
            options: new()
            {
                AllowTrailingCommas = true,
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            });

        return result;
    }


}
