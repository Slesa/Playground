using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ItemsPanels
{
    public abstract class TextOnPathBase : FrameworkElement
    {
        public static readonly DependencyProperty FontFamilyProperty =
            TextElement.FontFamilyProperty.AddOwner(typeof(TextOnPathBase),
                new FrameworkPropertyMetadata(OnFontPropertyChanged));

        public FontFamily FontFamily
        {
            set { SetValue(FontFamilyProperty, value); }
            get { return (FontFamily)GetValue(FontFamilyProperty); }
        }

        public static readonly DependencyProperty FontStyleProperty =
            TextElement.FontStyleProperty.AddOwner(typeof(TextOnPathBase),
                new FrameworkPropertyMetadata(OnFontPropertyChanged));

        public FontStyle FontStyle
        {
            set { SetValue(FontStyleProperty, value); }
            get { return (FontStyle)GetValue(FontStyleProperty); }
        }

        public static readonly DependencyProperty FontWeightProperty =
            TextElement.FontWeightProperty.AddOwner(typeof(TextOnPathBase),
                new FrameworkPropertyMetadata(OnFontPropertyChanged));

        public FontWeight FontWeight
        {
            set { SetValue(FontWeightProperty, value); }
            get { return (FontWeight)GetValue(FontWeightProperty); }
        }

        public static readonly DependencyProperty FontStretchProperty =
            TextElement.FontStretchProperty.AddOwner(typeof(TextOnPathBase),
                new FrameworkPropertyMetadata(OnFontPropertyChanged));

        static void OnFontPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as TextOnPathBase).OnFontPropertyChanged(args);
        }

        protected abstract void OnFontPropertyChanged(DependencyPropertyChangedEventArgs args);

        public FontStretch FontStretch
        {
            set { SetValue(FontStretchProperty, value); }
            get { return (FontStretch)GetValue(FontStretchProperty); }
        }

        public static readonly DependencyProperty ForegroundProperty =
            TextElement.ForegroundProperty.AddOwner(typeof(TextOnPathBase),
                new FrameworkPropertyMetadata(OnForegroundPropertyChanged));

        public Brush Foreground
        {
            set { SetValue(ForegroundProperty, value); }
            get { return (Brush)GetValue(ForegroundProperty); }
        }

        static void OnForegroundPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as TextOnPathBase).OnForegroundPropertyChanged(args);
        }

        protected abstract void OnForegroundPropertyChanged(DependencyPropertyChangedEventArgs args);

        public static readonly DependencyProperty TextProperty =
            TextBlock.TextProperty.AddOwner(typeof(TextOnPathBase),
                new FrameworkPropertyMetadata(OnTextPropertyChanged));

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        static void OnTextPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as TextOnPathBase).OnTextPropertyChanged(args);
        }

        protected abstract void OnTextPropertyChanged(DependencyPropertyChangedEventArgs args);

        public static readonly DependencyProperty PathFigureProperty =
            DependencyProperty.Register("PathFigure",
                typeof(PathFigure),
                typeof(TextOnPathBase),
                new FrameworkPropertyMetadata(OnPathPropertyChanged));

        public PathFigure PathFigure
        {
            set { SetValue(PathFigureProperty, value); }
            get { return (PathFigure)GetValue(PathFigureProperty); }
        }

        static void OnPathPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as TextOnPathBase).OnPathPropertyChanged(args);
        }

        protected abstract void OnPathPropertyChanged(DependencyPropertyChangedEventArgs args);

        public static double GetPathFigureLength(PathFigure pathFigure)
        {
            if (pathFigure == null)
                return 0;

            var isAlreadyFlattened = pathFigure.Segments.All(pathSegment => pathSegment is PolyLineSegment || pathSegment is LineSegment);

            var pathFigureFlattened = isAlreadyFlattened ? pathFigure : pathFigure.GetFlattenedPathFigure();
            var length = 0.0;
            var pt1 = pathFigureFlattened.StartPoint;

            foreach (var pathSegment in pathFigureFlattened.Segments)
            {
                if (pathSegment is LineSegment)
                {
                    var pt2 = (pathSegment as LineSegment).Point;
                    length += (pt2 - pt1).Length;
                    pt1 = pt2;
                }
                else if (pathSegment is PolyLineSegment)
                {
                    var pointCollection = (pathSegment as PolyLineSegment).Points;
                    foreach (var pt2 in pointCollection)
                    {
                        length += (pt2 - pt1).Length;
                        pt1 = pt2;
                    }
                }
            }
            return length;
        }
    }
}