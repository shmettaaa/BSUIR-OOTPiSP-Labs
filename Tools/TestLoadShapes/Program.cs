using System;
using System.IO;
using System.Reflection;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: TestLoadShapes <assembly-path> <xml-file>");
            return 1;
        }
        string asmPath = args[0];
        string xmlPath = args[1];
        try
        {
            var asm = Assembly.LoadFrom(asmPath);
            // Ensure FiguresCore loaded
            var asmDir = Path.GetDirectoryName(Path.GetFullPath(asmPath));
            var fig = Path.Combine(asmDir, "FiguresCore.dll");
            if (File.Exists(fig)) Assembly.LoadFrom(fig);

            var t = asm.GetType("ShapeCollectionSerializer");
            if (t == null) t = asm.GetType("OOTPiSP Lab4.ShapeCollectionSerializer");
            if (t == null)
            {
                Console.WriteLine("ShapeCollectionSerializer type not found.");
                return 1;
            }
            var m = t.GetMethod("LoadFromString", BindingFlags.Public | BindingFlags.Static);
            if (m == null)
            {
                Console.WriteLine("LoadFromString method not found.");
                return 1;
            }
            var xml = File.ReadAllText(xmlPath);
            var result = m.Invoke(null, new object[] { xml });
            Console.WriteLine("Loaded shapes count: " + ((System.Collections.ICollection)result).Count);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("EX: " + ex);
            return 2;
        }
    }
}
