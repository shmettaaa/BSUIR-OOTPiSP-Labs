using System;
using System.Reflection;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: InspectXslt <path-to-dll>");
            return;
        }
        string path = args[0];
        try
        {
            // Ensure dependent assemblies (FiguresCore) are loadable: try to locate and load FiguresCore.dll
            var pluginDir = Path.GetDirectoryName(Path.GetFullPath(path));
            string figuresCorePath = null;
            // look in plugin dir, parent dirs and solution tree
            var dir = pluginDir;
            for (int i = 0; i < 6 && dir != null; i++)
            {
                var candidate = Path.Combine(dir, "FiguresCore.dll");
                if (File.Exists(candidate)) { figuresCorePath = candidate; break; }
                dir = Directory.GetParent(dir)?.FullName;
            }
            if (figuresCorePath == null)
            {
                // global search in repository (may be slower)
                var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "FiguresCore.dll", SearchOption.AllDirectories);
                if (files.Length > 0) figuresCorePath = files[0];
            }
            if (figuresCorePath != null)
            {
                try { Assembly.LoadFrom(figuresCorePath); } catch { }
            }

            var asm = Assembly.LoadFrom(path);
            Console.WriteLine("Types in assembly:");
            foreach (var tt in asm.GetTypes())
            {
                Console.WriteLine("  " + tt.FullName);
            }
            Type t = null;
            foreach (var tt in asm.GetTypes())
            {
                var f = tt.GetField("XsltTemplate", BindingFlags.NonPublic | BindingFlags.Static);
                if (f != null)
                {
                    t = tt;
                    var val = f.GetRawConstantValue() as string;
                    Console.WriteLine("---FOUND IN TYPE--- " + tt.FullName);
                    Console.WriteLine("---BEGIN---");
                    Console.WriteLine(val);
                    Console.WriteLine("---END---");
                // Try to load XSLT to reproduce the error
                try
                {
                    var settings = new System.Xml.XmlReaderSettings();
                    using (var sr = new System.IO.StringReader(val))
                    using (var xr = System.Xml.XmlReader.Create(sr, settings))
                    {
                        var xslt = new System.Xml.Xsl.XslCompiledTransform();
                        xslt.Load(xr);
                        Console.WriteLine("XSLT loaded successfully.");
                    }
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("XSLT load failed: " + ex2);
                }
                    break;
                }
            }
            if (t == null)
                Console.WriteLine("XsltTemplate field not found in any type.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex);
        }
    }
}
