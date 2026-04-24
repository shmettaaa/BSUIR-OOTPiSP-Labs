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
            LoadPlugins();   // загружает плагины и вызывает их Register()
            FigureRegistry.AutoRegister();   // регистрирует встроенные фигуры
            TransformerRegistry.AutoRegisterTransformers(); // регистрирует встроенные трансформаторы (если есть)
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
                System.Diagnostics.Debug.WriteLine("No plugins loaded from Plugins folder.");
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