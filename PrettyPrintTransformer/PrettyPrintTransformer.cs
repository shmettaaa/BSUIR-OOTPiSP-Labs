using System.Xml;
using System.Text;
using System.Security;
using Figures;

public class PrettyPrintTransformer : IDataTransformer
{
    public string Name => "Pretty Print XML";

    public string TransformBeforeSave(string data)
    {
        if (string.IsNullOrEmpty(data)) return data;

        var doc = new XmlDocument();
        doc.LoadXml(data);

        var sb = new StringBuilder();

        if (doc.FirstChild is XmlDeclaration decl)
        {
            sb.AppendFormat("<?xml version=\"{0}\" encoding=\"{1}\"?>", decl.Version, decl.Encoding);
            sb.AppendLine();
        }

        void FormatElement(XmlElement el, int indent)
        {
            var indentStr = new string('\t', indent);
            sb.Append(indentStr);
            sb.Append("<");
            sb.Append(el.Name);

            if (el.HasAttributes)
            {
                foreach (XmlAttribute attr in el.Attributes)
                {
                    sb.AppendLine();
                    sb.Append(indentStr);
                    sb.Append('\t');
                    sb.Append(attr.Name);
                    sb.Append("=\"");
                    sb.Append(SecurityElement.Escape(attr.Value));
                    sb.Append('\"');
                }
            }

            if (!el.HasChildNodes)
            {
                sb.Append(" />");
                sb.AppendLine();
                return;
            }

            sb.Append(">\n");

            if (el.ChildNodes.Count == 1 && el.FirstChild.NodeType == XmlNodeType.Text)
            {
                sb.Append(new string('\t', indent + 1));
                sb.Append(SecurityElement.Escape(el.InnerText));
                sb.AppendLine();
            }
            else
            {
                foreach (XmlNode child in el.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.Element)
                        FormatElement((XmlElement)child, indent + 1);
                    else if (child.NodeType == XmlNodeType.Text)
                    {
                        sb.Append(new string('\t', indent + 1));
                        sb.Append(SecurityElement.Escape(child.Value));
                        sb.AppendLine();
                    }
                }
            }

            sb.Append(indentStr);
            sb.Append("</");
            sb.Append(el.Name);
            sb.Append(">\n");
        }

        if (doc.DocumentElement != null)
            FormatElement(doc.DocumentElement, 0);

        return sb.ToString();
    }

    public string TransformAfterLoad(string data)
    {
        return data;
    }
}
