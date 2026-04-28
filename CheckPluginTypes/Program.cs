using System.Reflection;

string dllPath = @"D:\vsProjects\BSUIR OOTPiSP Labs\OOTPiSP Lab4\bin\Debug\net10.0-windows\Plugins\TrapeziumLibrary.dll";

Assembly assembly = Assembly.LoadFrom(dllPath);

Console.WriteLine($"Analyzing: {Path.GetFileName(dllPath)}\n");
Console.WriteLine(new string('=', 60));

Type[] types;
try
{
    types = assembly.GetTypes();
}
catch (ReflectionTypeLoadException ex)
{
    Console.WriteLine($"⚠️ Some types could not be loaded:\n");
    foreach (var loaderEx in ex.LoaderExceptions)
    {
        Console.WriteLine($"   Error: {loaderEx.Message}");
    }
    Console.WriteLine($"\n✅ Loading {ex.Types.Length} types (failed: {ex.Types.Count(t => t == null)})\n");
    types = ex.Types.Where(t => t != null).ToArray();
}

foreach (Type type in types)
{
    Console.WriteLine($"\n📦 {type.Name}");
    Console.WriteLine($"   Namespace: {type.Namespace}");

    // Interfaces
    var interfaces = type.GetInterfaces();
    if (interfaces.Any())
        Console.WriteLine($"   Implements: {string.Join(", ", interfaces.Select(i => i.Name))}");

    // Constructors
    var ctors = type.GetConstructors();
    if (ctors.Any())
        Console.WriteLine($"   Constructors: {ctors.Length}");

    // Properties
    var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    foreach (var prop in props)
        Console.WriteLine($"   Property: {prop.PropertyType.Name} {prop.Name}");

    // Methods
    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    foreach (var method in methods)
    {
        if (method.IsSpecialName) continue;
        var paramsStr = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
        Console.WriteLine($"   Method: {method.ReturnType.Name} {method.Name}({paramsStr})");
    }

    // Fields
    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
    foreach (var field in fields)
        Console.WriteLine($"   Field: {field.FieldType.Name} {field.Name}");
}

Console.WriteLine("\n" + new string('=', 60));
Console.WriteLine("Press any key to exit...");
Console.ReadKey();