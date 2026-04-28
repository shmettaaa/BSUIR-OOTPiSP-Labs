using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Figures;

// Ensure FiguresCore assembly is loaded from the built project output so plugin types resolve at runtime
void TryLoadFiguresCore()
{
    string baseDir = AppContext.BaseDirectory;
    var dir = new DirectoryInfo(baseDir);
    while (dir != null)
    {
        string candidate = Path.Combine(dir.FullName, "FiguresCore", "bin", "Debug", "net10.0-windows", "FiguresCore.dll");
        if (File.Exists(candidate))
        {
            try
            {
                AssemblyLoadContext.Default.LoadFromAssemblyPath(candidate);
                Console.WriteLine("Loaded FiguresCore from: " + candidate);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load FiguresCore from " + candidate + ": " + ex.Message);
            }
        }
        dir = dir.Parent;
    }
    Console.WriteLine("FiguresCore.dll not found in expected build output locations. If missing, build FiguresCore project first.");
}

TryLoadFiguresCore();

string pluginsDir;
if (args.Length > 0 && Directory.Exists(args[0]))
{
    pluginsDir = args[0];
    Console.WriteLine("Using plugins folder from args: " + pluginsDir);
}
else
{
    pluginsDir = Path.Combine(AppContext.BaseDirectory, "Plugins");
    Console.WriteLine("Using default plugins folder: " + pluginsDir);
}
if (!Directory.Exists(pluginsDir)) { Console.WriteLine("Plugins folder not found: " + pluginsDir); return; }
foreach (var dll in Directory.GetFiles(pluginsDir, "*.dll"))
{
    Console.WriteLine($"--- {Path.GetFileName(dll)} ---");
    try
    {
        var asm = Assembly.LoadFrom(dll);
        var regs = asm.GetTypes().Where(t => typeof(IFigureRegistration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToArray();
        Console.WriteLine($"Found {regs.Length} IFigureRegistration types");
        foreach (var r in regs)
        {
            Console.WriteLine("Registration type: " + r.FullName);
            try
            {
                var inst = Activator.CreateInstance(r) as IFigureRegistration;
                if (inst != null)
                {
                    Console.WriteLine("Calling Register()...");
                    inst.Register();
                    Console.WriteLine("Register() called successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Register() failed: " + ex);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Load failed: " + ex);
    }
}

Console.WriteLine("Finished");
// Dump registered figure names
try
{
    Console.WriteLine("Registered figure types:");
    foreach (var n in FigureRegistry.GetAllNames()) Console.WriteLine(" - " + n);
}
catch (Exception ex)
{
    Console.WriteLine("Failed to enumerate FigureRegistry: " + ex.Message);
}
