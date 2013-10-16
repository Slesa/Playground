using System;
using System.Windows;
using System.Windows.Controls;

namespace ItemsPanels
{
    public class RadialPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            var size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach (UIElement element in Children)
            {
                element.Measure(size);
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count == 0) return finalSize;

            var angle = 0.0;
            var incrementalAngularSpace = (360.0/Children.Count)*(Math.PI/180.0);

            var radiousX = finalSize.Width/2.4;
            var radiousY = finalSize.Height/2.4;

            foreach (UIElement element in Children)
            {
                var childPoint = new Point(Math.Cos(angle)*radiousX, -Math.Sin(angle)*radiousY);
                var actualChildPoint = new Point(finalSize.Width/2 + childPoint.X - element.DesiredSize.Width/2,
                                                 finalSize.Height/2 + childPoint.Y - element.DesiredSize.Height/2);
                element.Arrange(new Rect(actualChildPoint.X, actualChildPoint.Y, element.DesiredSize.Width, element.DesiredSize.Height));
                angle += incrementalAngularSpace;
            }
            return base.ArrangeOverride(finalSize);
        }
    }
}