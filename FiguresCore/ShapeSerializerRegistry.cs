using System;
using System.Collections.Generic;

namespace Figures
{
    public static class ShapeSerializerRegistry
    {
        private static readonly Dictionary<Type, IShapeSerializer> _serializers = new();

        public static void Register(Type shapeType, IShapeSerializer serializer)
        {
            if (!_serializers.ContainsKey(shapeType))
                _serializers[shapeType] = serializer;
        }

        public static IShapeSerializer GetSerializer(Type shapeType)
        {
            _serializers.TryGetValue(shapeType, out var serializer);
            return serializer;
        }
    }
}