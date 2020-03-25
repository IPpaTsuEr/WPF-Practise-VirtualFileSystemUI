using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VMFWPF.Services
{
    struct HSVColor
    {
       public float H;
       public float S;
       public float V;
    }
    struct ColorSeries
    {
        public Color Deep;
        public Color Normal;
        public Color Light;

        public SolidColorBrush LightBrush;
    }

    class ColorHelper
    {
        public static Dictionary<string, ColorSeries> ColorMap = new Dictionary<string, ColorSeries>();

        public static ColorSeries GetColor(string str,float VOffset,float SOffset)
        {
            ColorSeries value;
            if(ColorMap.TryGetValue(str,out value)){
                return value;
            }
            else
            {
                uint code = (uint)str.GetHashCode();
                var hc = ConvertToHSV(Convert.ToString(code,16));
                Remap(ref hc.V, 0.3f, 0.9f);
                Remap(ref hc.S, 0.3f, 0.9f);
                var hcs = GetColorSeries(hc, VOffset,SOffset);
                ColorMap.Add(str,hcs);
                return hcs;
            }
        }

        public static Color ConvertStringToColor(string ColorStr)
        {
            if (ColorStr.StartsWith("#")) ColorStr = ColorStr.Substring(1);
            byte[] value = new byte[4];

            switch (ColorStr.Count())
            {
                case 6:
                    value[0] = Convert.ToByte(ColorStr.Substring(0,2), 16);
                    value[1] = Convert.ToByte(ColorStr.Substring(2,2), 16);
                    value[2] = Convert.ToByte(ColorStr.Substring(4,2), 16);
                    return Color.FromRgb(value[0],value[1],value[2]);

                case 8:
                    value[0] = Convert.ToByte(ColorStr.Substring(0, 2), 16);
                    value[1] = Convert.ToByte(ColorStr.Substring(2, 2), 16);
                    value[2] = Convert.ToByte(ColorStr.Substring(4, 2), 16);
                    value[3] = Convert.ToByte(ColorStr.Substring(6, 2), 16);
                    return Color.FromArgb(value[0], value[1], value[2],value[3]);

            }
            return new Color();
        }

        public static HSVColor ConvertToHSV(string RGBStr)
        {
            return ConvertToHSV(ConvertStringToColor(RGBStr));
        }

        public static HSVColor ConvertToHSV(Color RGB)
        {
            float Max, Min, Delta;
            var R = RGB.R / 255.0f;
            var G = RGB.G / 255.0f;
            var B = RGB.B / 255.0f;

            Max = Math.Max(R,Math.Max(G,B));
            Min = Math.Min(R,Math.Min(G,B));
            Delta = Max - Min;

            HSVColor result = new HSVColor();

            result.V = Max;

            if (Max != 0) result.S = Delta / Max;
            else { result.S = 0; result.H = -1 / 360; }

            if (Max == Min) result.H = 0;
            else if (Max == R && G>=B) result.H = (G - B) / Delta;
            else if (Max == R && G<B) result.H = 6+( (G - B) / Delta);
            else if (Max == G) result.H = 2 + ((B - R) / Delta);
            else if (Max == B) result.H = 4 + ((R - G) / Delta);
            result.H *= 60;
            if (result.H < 0) result.H += 360;
            if (result.H>= 360)result.H -= 360;
            
            return result;
        }

        public static Color ConvertToRGB(HSVColor HSV)
        {
            if (HSV.S == 0) return Color.FromRgb((byte)(HSV.V * 255), (byte)(HSV.V * 255), (byte)(HSV.V * 255));
            int section;
            float offset;
            section =(int)HSV.H / 60;
            offset = (HSV.H/60.0f) - section;

            float p, q, t;
            p = HSV.V * (1- HSV.S);
            q = HSV.V * (1- HSV.S* offset);
            t = HSV.V * (1- HSV.S*(1-offset));

            switch (section)
            {
                case 0:
                    return Color.FromRgb((byte)(HSV.V*255),(byte)(t * 255),(byte)(p * 255));
                case 1:
                    return Color.FromRgb((byte)(q * 255), (byte)(HSV.V * 255), (byte)(p * 255));
                case 2:
                    return Color.FromRgb((byte)(p * 255), (byte)(HSV.V * 255), (byte)(t * 255));
                case 3:
                    return Color.FromRgb((byte)(p * 255), (byte)(q * 255), (byte)(HSV.V * 255));
                case 4:
                    return Color.FromRgb((byte)(t * 255), (byte)(p * 255), (byte)(HSV.V * 255));
                default://5
                    return Color.FromRgb((byte)(HSV.V * 255), (byte)(p * 255), (byte)(q * 255));
            }
        }

        public static ColorSeries GetColorSeries(HSVColor color,float voffset, float soffset,Color Original)
        {
            ColorSeries result = new ColorSeries();
            result.Deep  = ConvertToRGB(new HSVColor() { H = color.H, S = LimitInRange(color.S - soffset), V = LimitInRange(color.V - voffset) });
            result.Light = ConvertToRGB(new HSVColor() { H = color.H, S = LimitInRange(color.S + soffset), V = LimitInRange(color.V + voffset) });
            result.Normal = Original;
            result.LightBrush = new SolidColorBrush(result.Light);
            return result;
        }

        public static ColorSeries GetColorSeries(HSVColor color,float voffset, float soffset)
        {
            ColorSeries result = new ColorSeries();
            result.Deep  = ConvertToRGB(new HSVColor() { H=color.H, S = LimitInRange(color.S - soffset), V = LimitInRange(color.V - voffset) });
            result.Light = ConvertToRGB(new HSVColor() { H=color.H, S = LimitInRange(color.S + soffset), V = LimitInRange(color.V + voffset) });
            result.Normal = ConvertToRGB(color);
            result.LightBrush = new SolidColorBrush(result.Light);
            return result;
        }

        public static float LimitInRange(float value,float Max=1f,float Min = 0f)
        {
            if (value > Max) return Max;
            if (value < Min) return Min;
            return value;
        }

        public static void Remap(ref float value,float MapLow,float MapHeight,float OriginalLow=0.0f,float OriginalHeight=1.0f)
        {
            float R = (MapHeight - MapLow) / (OriginalHeight - OriginalLow);
            value = value * R + MapLow;

        }
    }
}
