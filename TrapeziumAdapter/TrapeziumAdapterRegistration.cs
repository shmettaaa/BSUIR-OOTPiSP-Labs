using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Figures;

namespace TrapeziumAdapter
{
    public class TrapeziumAdapterRegistration : IFigureRegistration
    {
        public void Register()
        {
            string pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins", "TrapeziumLibrary.dll");

            var assembly = Assembly.LoadFrom(pluginPath);

            // Get Trapezium type
            var trapeziumType = assembly.GetType("TrapeziumLibrary.Trapezium");

            // Get TrapeziumFactory (which implements IDrawableShape with Draw method)
            var factoryType = assembly.GetTypes().FirstOrDefault(t => t.Name == "TrapeziumFactory");
            var factory = Activator.CreateInstance(factoryType);

            // Create adapters
            var factoryAdapter = new TrapeziumFactoryAdapter(trapeziumType);
            var rendererAdapter = new TrapeziumRendererAdapter(factory);

            // Register
            FigureRegistry.Register("Трапеция", new FigureHandler(factoryAdapter, rendererAdapter));
        }
    }
}