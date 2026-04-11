using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Figures;

namespace FiguresApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LoadPlugins();

            FigureRegistry.AutoRegister();

            base.OnStartup(e);
        }

        private void LoadPlugins()
        {
            string pluginsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if (!Directory.Exists(pluginsDir))
                Directory.CreateDirectory(pluginsDir);
            var loadedAny = false;
            foreach (string dllPath in Directory.GetFiles(pluginsDir, "*.dll"))
            {
                TryLoadPlugin(dllPath, ref loadedAny);
            }

            if (!loadedAny)
            {
                try
                {
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory;

                    string? dir = baseDir;
                    string? solutionRoot = null;
                    while (!string.IsNullOrEmpty(dir))
                    {
                        var slnFiles = Directory.GetFiles(dir, "*.sln", SearchOption.TopDirectoryOnly);
                        if (slnFiles.Length > 0)
                        {
                            solutionRoot = dir;
                            break;
                        }
                        var parent = Path.GetDirectoryName(dir);
                        if (string.IsNullOrEmpty(parent) || parent == dir) break;
                        dir = parent;
                    }

                    if (solutionRoot == null)
                    {
                        solutionRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", ".."));
                    }

                    System.Diagnostics.Debug.WriteLine($"Plugin search root: {solutionRoot}");

                    var candidateDlls = Directory.EnumerateFiles(solutionRoot, "*Plugin.dll", SearchOption.AllDirectories);
                    foreach (var dll in candidateDlls)
                    {
                        TryLoadPlugin(dll, ref loadedAny);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Plugin discovery scan failed: {ex.Message}");
                }
            }
        }

        private void TryLoadPlugin(string dllPath, ref bool loadedAny)
        {
            try
            {
                var fullPath = Path.GetFullPath(dllPath);
                var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(fullPath);
                var registrationTypes = assembly.GetTypes()
                    .Where(t => typeof(IFigureRegistration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                foreach (Type regType in registrationTypes)
                {
                    IFigureRegistration registration = (IFigureRegistration)Activator.CreateInstance(regType);
                    registration.Register();
                    loadedAny = true;
                    System.Diagnostics.Debug.WriteLine($"Plugin registration loaded: {regType.FullName} from {dllPath}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load plugin {dllPath}: {ex.Message}");
            }
        }
    }
}