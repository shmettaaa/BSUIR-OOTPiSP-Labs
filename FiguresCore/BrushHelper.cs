using System;
using System.Windows.Media;

namespace Figures
{
    public static class BrushHelper
    {
        public static string ToArgbString(SolidColorBrush brush)
        {
            if (brush == null) return "#FF000000";
            var color = brush.Color;
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static SolidColorBrush FromArgbString(string argb)
        {
            if (string.IsNullOrEmpty(argb) || argb.Length != 9 || !argb.StartsWith("#"))
                return Brushes.Black;
            byte a = Convert.ToByte(argb.Substring(1, 2), 16);
            byte r = Convert.ToByte(argb.Substring(3, 2), 16);
            byte g = Convert.ToByte(argb.Substring(5, 2), 16);
            byte b = Convert.ToByte(argb.Substring(7, 2), 16);
            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }
    }
}