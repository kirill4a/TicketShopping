using DbUp.Engine;
using DbUp;
using System.Reflection;
using TicketShopping.Persistence.Migrator.MySql.Infrastructure;

namespace TicketShopping.Persistence.Migrator.MySql.App;

internal static class Runner
{
    internal static int RunMigrations(string connectionString)
    {
        Console.WriteLine("Starting migration....");

        var upgrader = GetUpgradeEngine(connectionString);
        return UpdateDatabase(upgrader);
    }

    private static UpgradeEngine GetUpgradeEngine(string connectionString)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyName = assembly.GetName().Name;
        var schemaScriptPrefix = $"{assemblyName}.{Constants.PrefixSchemaScript}";
        var dataScriptPrefix = $"{assemblyName}.{Constants.PrefixDataScript}";

        var upgrader =
           DeployChanges.To
               .MySqlDatabase(connectionString)
               .WithScriptsEmbeddedInAssembly(assembly,
                                              script => script.StartsWith(schemaScriptPrefix),
                                              encoding: System.Text.Encoding.UTF8,
                                              new SqlScriptOptions()
                                              {
                                                  ScriptType = DbUp.Support.ScriptType.RunOnce,
                                                  RunGroupOrder = 0
                                              })
               .WithScriptsEmbeddedInAssembly(assembly,
                                              script => script.StartsWith(dataScriptPrefix),
                                              encoding: System.Text.Encoding.UTF8,
                                              new SqlScriptOptions()
                                              {
                                                  ScriptType = DbUp.Support.ScriptType.RunOnce,
                                                  RunGroupOrder = 1
                                              })
               .LogToConsole()
               .Build();

        return upgrader;
    }

    private static int UpdateDatabase(UpgradeEngine engine)
    {
        var result = engine.PerformUpgrade();

        if (!result.Successful)
        {
            ConsoleUtils.WriteLineRed(result.Error.ToString());
            return 1;
        }
        ConsoleUtils.WriteLineGreen("Database is of the latest version now");
        return 0;
    }
}
