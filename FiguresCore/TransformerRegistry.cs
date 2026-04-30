using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Figures
{
    public sealed class TransformerRegistry
    {
        private readonly List<IDataTransformer> _transformers = new();

        private TransformerRegistry()
        {
        }

        private static readonly Lazy<TransformerRegistry> _instance = new(() => new TransformerRegistry());

        public static TransformerRegistry Instance => _instance.Value;

        public void Register(IDataTransformer transformer)
        {
            if (transformer == null) return;
            if (_transformers.Any(t => t.GetType() == transformer.GetType() || t.Name == transformer.Name))
                return;

            _transformers.Add(transformer);
        }

        public IEnumerable<IDataTransformer> GetAll() => _transformers;

        public void AutoRegisterTransformers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (typeof(IDataTransformer).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            var transformer = Activator.CreateInstance(type) as IDataTransformer;
                            if (transformer != null)
                                Register(transformer);
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var t in ex.Types.Where(t => t != null))
                    {
                        if (typeof(IDataTransformer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                        {
                            var transformer = Activator.CreateInstance(t) as IDataTransformer;
                            if (transformer != null)
                                Register(transformer);
                        }
                    }
                }
                catch { }
            }
        }
    }
}
