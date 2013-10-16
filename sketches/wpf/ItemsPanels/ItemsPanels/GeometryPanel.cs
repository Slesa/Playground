﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ItemsPanels
{
    public class GeometryPanel : Panel
    {
        public static readonly DependencyProperty PathFigureProperty =
            DependencyProperty.Register("PathFigure",
                typeof(PathFigure),
                typeof(GeometryPanel),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange|FrameworkPropertyMetadataOptions.AffectsMeasure));

        public PathFigure PathFigure
        {
            set { SetValue(PathFigureProperty, value); }
            get { return (PathFigure)GetValue(PathFigureProperty); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach (UIElement element in Children)
            {
                element.Measure(size);
            }
            return base.MeasureOverride(availableSize);
        }

        //Size newArrangeOverride(Size finalSize)
        protected override Size ArrangeOverride(Size finalSize)
        {
            var pathLength = TextOnPathBase.GetPathFigureLength(PathFigure);
            var neededSpace = 0.0; // finalSize.Width;


            foreach (UIElement child in Children)
            {
                //child.Measure(new Size(Double.PositiveInfinity,
                //                       Double.PositiveInfinity));
                neededSpace += child.DesiredSize.Width;
            }

            if (!pathLength.Equals(0.0) && !neededSpace.Equals(0.0) && !finalSize.Width.Equals(0.0))
            {
                var spaceAvail = pathLength - neededSpace;
                var spaceStep = spaceAvail / Children.Count;
                var stepPerc = spaceStep / spaceAvail;

                var scalingFactor = pathLength / neededSpace;
                var pathGeometry = new PathGeometry(new[] {PathFigure});
                var baseline = scalingFactor;

                var progress = 0.0; // stepPerc / 2;

                foreach (UIElement element in Children)
                {
                    var width = /*scalingFactor**/element.DesiredSize.Width;
                    progress += stepPerc / 2;

                    Point point, tangent;
                    pathGeometry.GetPointAtFractionLength(progress,
                                                          out point, out tangent);

                    var transformGroup = new TransformGroup();

                    //transformGroup.Children.Add(
                    //    new ScaleTransform(scalingFactor, scalingFactor));
                    transformGroup.Children.Add(
                        new RotateTransform(Math.Atan2(tangent.Y, tangent.X)
                                                *180/Math.PI, width/2, baseline));
                    //transformGroup.Children.Add(
                    //    new TranslateTransform(point.X - width / 2,
                    //                           point.Y - baseline));

                    element.RenderTransform = transformGroup;

                    element.Arrange(new Rect(point.X, point.Y, element.DesiredSize.Width, element.DesiredSize.Height));

                    progress += stepPerc / 2;
                }
            }
            return base.ArrangeOverride(finalSize);
        }

        //protected override Size ArrangeOverride(Size finalSize)
        Size OriginalArrangeOverride(Size finalSize)
        {
            var pathLength = TextOnPathBase.GetPathFigureLength(PathFigure);
            var textLength = 0.0; // finalSize.Width;


            foreach (UIElement child in Children)
            {
                //child.Measure(new Size(Double.PositiveInfinity,
                //                       Double.PositiveInfinity));
                textLength += child.DesiredSize.Width;
            }

            if (!pathLength.Equals(0.0) && !textLength.Equals(0.0) && !finalSize.Width.Equals(0.0))
            {
                var scalingFactor = pathLength/textLength;
                var pathGeometry = new PathGeometry(new PathFigure[] {PathFigure});
                var baseline = scalingFactor;
                var progress = 0.0;

                foreach (UIElement element in Children)
                {
                    var width = scalingFactor*element.DesiredSize.Width;
                    progress += width/2/pathLength;
                    Point point, tangent;

                    pathGeometry.GetPointAtFractionLength(progress,
                                                          out point, out tangent);

                    var transformGroup = new TransformGroup();

                    //transformGroup.Children.Add(
                    //    new ScaleTransform(scalingFactor, scalingFactor));
                    transformGroup.Children.Add(
                        new RotateTransform(Math.Atan2(tangent.Y, tangent.X)
                                                *180/Math.PI, width/2, baseline));
                    //transformGroup.Children.Add(
                    //    new TranslateTransform(point.X - width / 2,
                    //                           point.Y - baseline));

                    element.RenderTransform = transformGroup;

                    element.Arrange(new Rect(point.X, point.Y, element.DesiredSize.Width, element.DesiredSize.Height));

                    progress += width/2/pathLength;
                }
            }
            return base.ArrangeOverride(finalSize);
        }
    }
}