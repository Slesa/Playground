using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ItemsPanels
{
    public class TextOnPathControl: UserControl
    {
        readonly Panel _mainPanel;
        const double Fontsize = 100;

        public TextOnPathControl()
        {
            _mainPanel = new Canvas();
            Content = _mainPanel;
        }

        static void OnFontPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as TextOnPathControl).OrientTextOnPath();
        }

        public static readonly DependencyProperty PathFigureProperty =
            DependencyProperty.Register("PathFigure",
                typeof(PathFigure),
                typeof(TextOnPathControl),
                new FrameworkPropertyMetadata(OnPathPropertyChanged));

        public PathFigure PathFigure
        {
            set { SetValue(PathFigureProperty, value); }
            get { return (PathFigure)GetValue(PathFigureProperty); }
        }

        static void OnPathPropertyChanged(DependencyObject obj,
                                DependencyPropertyChangedEventArgs args)
        {
            (obj as TextOnPathControl).OrientTextOnPath();
        }

        public static readonly DependencyProperty TextProperty =
            TextBlock.TextProperty.AddOwner(typeof(TextOnPathControl),
                new FrameworkPropertyMetadata(OnTextPropertyChanged));

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        static void OnTextPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var ctrl = obj as TextOnPathControl;
            ctrl._mainPanel.Children.Clear();

            if (String.IsNullOrEmpty(ctrl.Text))
                return;

            foreach (var ch in ctrl.Text)
            {
                var textBlock = new TextBlock();
                textBlock.Text = ch.ToString();
                textBlock.FontSize = Fontsize;
                ctrl._mainPanel.Children.Add(textBlock);
            }
            ctrl.OrientTextOnPath();
        }


        void OrientTextOnPath()
        {
            var pathLength = TextOnPathBase.GetPathFigureLength(PathFigure);
            var textLength = 0.0;

            foreach (UIElement child in _mainPanel.Children)
            {
                child.Measure(new Size(Double.PositiveInfinity,
                                       Double.PositiveInfinity));
                textLength += child.DesiredSize.Width;
            }

            if (pathLength == 0 || textLength == 0)
                return;

            var scalingFactor = pathLength / textLength;
            var pathGeometry = new PathGeometry(new PathFigure[] { PathFigure });
            var baseline = scalingFactor * Fontsize * FontFamily.Baseline;
            var progress = 0.0;

            foreach (UIElement child in _mainPanel.Children)
            {
                var width = scalingFactor * child.DesiredSize.Width;
                progress += width / 2 / pathLength;
                Point point, tangent;

                pathGeometry.GetPointAtFractionLength(progress,
                                                out point, out tangent);

                var transformGroup = new TransformGroup();

                transformGroup.Children.Add(
                    new ScaleTransform(scalingFactor, scalingFactor));
                transformGroup.Children.Add(
                    new RotateTransform(Math.Atan2(tangent.Y, tangent.X)
                        * 180 / Math.PI, width / 2, baseline));
                transformGroup.Children.Add(
                    new TranslateTransform(point.X - width / 2,
                                           point.Y - baseline));

                child.RenderTransform = transformGroup;
                progress += width / 2 / pathLength;
            }
        }

        static TextOnPathControl()
        {
            FontFamilyProperty.OverrideMetadata(typeof(TextOnPathControl),
                new FrameworkPropertyMetadata(OnFontPropertyChanged));
            FontStyleProperty.OverrideMetadata(typeof(TextOnPathControl),
                new FrameworkPropertyMetadata(OnFontPropertyChanged));
            FontWeightProperty.OverrideMetadata(typeof(TextOnPathControl),
                new FrameworkPropertyMetadata(OnFontPropertyChanged));
            FontStretchProperty.OverrideMetadata(typeof(TextOnPathControl),
                new FrameworkPropertyMetadata(OnFontPropertyChanged));
        }
    }
}