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
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in types)
            {
                if (typeof(IFigureRegistration).IsAssignableFrom(type) &&
                    !type.IsInterface &&
                    !type.IsAbstract)
                {
                    IFigureRegistration? registration =
                        (IFigureRegistration?)Activator.CreateInstance(type);

                    registration?.Register();
                }
            }
        }



    }
}