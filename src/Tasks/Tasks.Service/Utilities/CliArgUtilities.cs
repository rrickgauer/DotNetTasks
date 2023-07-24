using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Utilities;

public static class CliArgUtilities
{
    /// <summary>
    /// Parse the given cli args into the specified class type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    /// <returns></returns>
    public static T ParseArgs<T>(string[] args)
    {
        return Parser.Default.ParseArguments<T>(args).Value;
    }

    /// <summary>
    /// Transform the given cli args class into a string for the command line
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cliArgs"></param>
    /// <returns></returns>
    public static string ToArgs<T>(T cliArgs)
    {
        if (cliArgs is null)
        {
            return string.Empty;
        }

        return Parser.Default.FormatCommandLine(cliArgs);
    }
}
