using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Figures
{
    public static class FigureRegistry
    {
        private static readonly Dictionary<string, FigureHandler> _handlers = new();

        public static void Register(string name, FigureHandler handler)
        {
            _handlers[name] = handler;
        }

        public static FigureHandler? Get(string name)
        {
            return _handlers.TryGetValue(name, out var h) ? h : null;
        }

        public static IEnumerable<string> GetAllNames()
        {
            return _handlers.Keys;
        }

        public static void AutoRegister()
        {
            var registrations = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IFigureRegistration).IsAssignableFrom(t)
                            && !t.IsInterface
                            && !t.IsAbstract);

            foreach (var type in registrations)
            {
                var instance = (IFigureRegistration)Activator.CreateInstance(type);
                instance.Register();
            }
        }
    }
}