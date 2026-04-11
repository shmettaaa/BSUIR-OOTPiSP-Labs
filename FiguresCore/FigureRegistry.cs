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
            if (!_handlers.ContainsKey(name))
                _handlers[name] = handler;
        }

        public static FigureHandler Get(string name)
        {
            _handlers.TryGetValue(name, out var handler);
            return handler;
        }

        public static IEnumerable<string> GetAllNames() => _handlers.Keys;

        public static void AutoRegister()
        {
            var assemblies = new List<Assembly>();

            assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());

            var entry = Assembly.GetEntryAssembly();
            if (entry != null && !assemblies.Contains(entry))
                assemblies.Add(entry);

            if (entry != null)
            {
                foreach (var an in entry.GetReferencedAssemblies())
                {
                    try
                    {
                        var a = Assembly.Load(an);
                        if (!assemblies.Contains(a)) assemblies.Add(a);
                    }
                    catch { }
                }
            }

            foreach (var assembly in assemblies.Distinct())
            {
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    types = ex.Types.Where(t => t != null).ToArray()!;
                }
                catch
                {
                    continue;
                }

                foreach (Type type in types)
                {
                    if (type == null) continue;

                    if (typeof(IFigureRegistration).IsAssignableFrom(type) &&
                        !type.IsInterface &&
                        !type.IsAbstract)
                    {
                        try
                        {
                            IFigureRegistration? registration = (IFigureRegistration?)Activator.CreateInstance(type);
                            registration?.Register();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Failed to create/register {type.FullName}: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}