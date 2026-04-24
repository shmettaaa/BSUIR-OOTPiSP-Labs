using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Figures;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Net;

public class HtmlReportTransformer : IDataTransformer
{
    public string Name => "HTML Report";

    private const string XsltTemplate = @"<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:output method='html' indent='yes'/>
  <xsl:param name='generatedOn'/>
  <xsl:template match='/'>
    <html>
      <head>
        <title>Shapes Report</title>
        <style>
          body { font-family: Arial; margin: 20px; }
          table { border-collapse: collapse; width: 100%; }
          th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
          th { background-color: #4CAF50; color: white; }
          tr:nth-child(even) { background-color: #f2f2f2; }
        </style>
      </head>
      <body>
        <h1>Shapes Report</h1>
        <p>Generated on: <xsl:value-of select='$generatedOn'/></p>
        <p>Total shapes: <xsl:value-of select='count(//Shape)'/></p>
        <table>
          <tr>
            <th>Type</th>
            <th>Parameters</th>
          </tr>
          <xsl:for-each select='//Shape'>
            <tr>
              <td><xsl:value-of select='@Type'/></td>
              <td>
                <xsl:for-each select=""@*[name()!='Type']"">
                  <b><xsl:value-of select='name()'/>:</b> <xsl:value-of select='.'/><br/>
                </xsl:for-each>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>";

    public string TransformBeforeSave(string data)
    {
        if (string.IsNullOrEmpty(data)) return data;

        var xslt = new XslCompiledTransform();
        using (var xsltReader = XmlReader.Create(new StringReader(XsltTemplate)))
        {
            xslt.Load(xsltReader);

            var args = new XsltArgumentList();
            args.AddParam("generatedOn", string.Empty, DateTime.Now.ToString("u"));

            using (var inputReader = XmlReader.Create(new StringReader(data)))
            using (var outputWriter = new StringWriter())
            {
                xslt.Transform(inputReader, args, outputWriter);
                return outputWriter.ToString();
            }
        }
    }

    public string TransformAfterLoad(string data)
    {
        if (string.IsNullOrWhiteSpace(data)) return data;

        if (!data.Contains("<html", System.StringComparison.OrdinalIgnoreCase))
            return data;

        try
        {
            var shapes = new XElement("Shapes");

            var rowRegex = new Regex("<tr>\\s*<td>(.*?)</td>\\s*<td>(.*?)</td>\\s*</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var paramRegex = new Regex("<b>([^<:]+):</b>\\s*([^<]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match row in rowRegex.Matches(data))
            {
                var typeHtml = row.Groups[1].Value;
                var paramsHtml = row.Groups[2].Value;

                string type = WebUtility.HtmlDecode(StripTags(typeHtml)).Trim();
                var shapeElem = new XElement("Shape");
                shapeElem.SetAttributeValue("Type", type);

                foreach (Match p in paramRegex.Matches(paramsHtml))
                {
                    var name = WebUtility.HtmlDecode(p.Groups[1].Value).Trim();
                    var value = WebUtility.HtmlDecode(p.Groups[2].Value).Trim();
                   
                    if (string.IsNullOrEmpty(name)) continue;
                    var attrName = name.Replace(" ", "");
                    shapeElem.SetAttributeValue(attrName, value);
                }

                shapes.Add(shapeElem);
            }

            var doc = new XDocument(new XDeclaration("1.0", "utf-8", null), shapes);
            return doc.ToString();
        }
        catch
        {
            return data;
        }
    }

    private static string StripTags(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return Regex.Replace(input, "<.*?>", string.Empty);
    }
}