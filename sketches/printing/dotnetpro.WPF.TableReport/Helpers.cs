using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace dotnetpro.WPF.TableReport
{

    public class Ranges : List<Range>
    {
        public Ranges() { }
    }

    public class Range
    {
        public Range() { }

        public int From;
        public int Until;

        public bool IsWithIn(int Col)
        {
            return (From <= Col & Until >= Col);
        }
    }

    public static class Helpers
    {
        [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern bool GlobalFree(IntPtr hMem);

        private static SolidColorBrush AlternatingBackground = (SolidColorBrush)new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2F2F2")).GetAsFrozen();

        public static Size Subtract(this Size s1, Size s2) { return new Size(s1.Width - s2.Width, s1.Height - s2.Height); }

        public static Border Add2Grid(this Border c1, int col, int row)
        {
            c1.SetValue(Grid.RowProperty, row);
            c1.SetValue(Grid.ColumnProperty, col);
            return c1;
        }

        public static Border SetBackground(this Border c1, bool Alternating, int row)
        {
            c1.Background = Brushes.White;
            if (Alternating & ((row % 2) != 0))
                c1.Background = AlternatingBackground;
            return c1;
        }

        public static String AddLetter(this String s1, int pos)
        {
            s1 = s1 + "." + pos.ToString();
            return s1;
        }
    }
}
