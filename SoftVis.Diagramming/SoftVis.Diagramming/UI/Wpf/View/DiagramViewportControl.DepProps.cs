﻿using System.Windows.Controls;
using System.Windows.Media;
using Codartis.SoftVis.UI.Wpf.Commands;

namespace Codartis.SoftVis.UI.Wpf.View
{
    partial class DiagramViewportControl
    {
        public double PanAndZoomControlHeight
        {
            get { return (double)GetValue(PanAndZoomControlHeightProperty); }
            set { SetValue(PanAndZoomControlHeightProperty, value); }
        }

        public double MinZoom
        {
            get { return (double)GetValue(MinZoomProperty); }
            set { SetValue(MinZoomProperty, value); }
        }

        public double MaxZoom
        {
            get { return (double)GetValue(MaxZoomProperty); }
            set { SetValue(MaxZoomProperty, value); }
        }

        public double LargeZoomIncrement
        {
            get { return (double)GetValue(LargeZoomIncrementProperty); }
            set { SetValue(LargeZoomIncrementProperty, value); }
        }

        public double ViewportZoom
        {
            get { return (double)GetValue(ViewportZoomProperty); }
            set { SetValue(ViewportZoomProperty, value); }
        }

        public Transform ViewportTransform
        {
            get { return (Transform)GetValue(ViewportTransformProperty); }
            set { SetValue(ViewportTransformProperty, value); }
        }

        public TransitionedTransform TransitionedViewportTransform
        {
            get { return (TransitionedTransform)GetValue(TransitionedViewportTransformProperty); }
            set { SetValue(TransitionedViewportTransformProperty, value); }
        }

        public ContentPresenter FocusedDiagramShapeContainer
        {
            get { return (ContentPresenter)GetValue(FocusedDiagramShapeContainerProperty); }
            set { SetValue(FocusedDiagramShapeContainerProperty, value); }
        }

        public VectorDelegateCommand WidgetPanCommand
        {
            get { return (VectorDelegateCommand)GetValue(WidgetPanCommandProperty); }
            set { SetValue(WidgetPanCommandProperty, value); }
        }

        public DoubleDelegateCommand WidgetZoomCommand
        {
            get { return (DoubleDelegateCommand)GetValue(WidgetZoomCommandProperty); }
            set { SetValue(WidgetZoomCommandProperty, value); }
        }

        public DelegateCommand WidgetZoomToContentCommand
        {
            get { return (DelegateCommand)GetValue(WidgetZoomToContentCommandProperty); }
            set { SetValue(WidgetZoomToContentCommandProperty, value); }
        }

        public VectorDelegateCommand MousePanCommand
        {
            get { return (VectorDelegateCommand)GetValue(MousePanCommandProperty); }
            set { SetValue(MousePanCommandProperty, value); }
        }

        public ZoomDelegateCommand MouseZoomCommand
        {
            get { return (ZoomDelegateCommand)GetValue(MouseZoomCommandProperty); }
            set { SetValue(MouseZoomCommandProperty, value); }
        }

        public VectorDelegateCommand KeyboardPanCommand
        {
            get { return (VectorDelegateCommand)GetValue(KeyboardPanCommandProperty); }
            set { SetValue(KeyboardPanCommandProperty, value); }
        }

        public ZoomDelegateCommand KeyboardZoomCommand
        {
            get { return (ZoomDelegateCommand)GetValue(KeyboardZoomCommandProperty); }
            set { SetValue(KeyboardZoomCommandProperty, value); }
        }

        public Viewport.ResizeCommand ViewportResizeCommand
        {
            get { return (Viewport.ResizeCommand)GetValue(ViewportResizeCommandProperty); }
            set { SetValue(ViewportResizeCommandProperty, value); }
        }

        public Viewport.PanCommand ViewportPanCommand
        {
            get { return (Viewport.PanCommand)GetValue(ViewportPanCommandProperty); }
            set { SetValue(ViewportPanCommandProperty, value); }
        }

        public Viewport.ZoomCommand ViewportZoomCommand
        {
            get { return (Viewport.ZoomCommand)GetValue(ViewportZoomCommandProperty); }
            set { SetValue(ViewportZoomCommandProperty, value); }
        }

        public Viewport.ZoomToContentCommand ViewportZoomToContentCommand
        {
            get { return (Viewport.ZoomToContentCommand)GetValue(ViewportZoomToContentCommandProperty); }
            set { SetValue(ViewportZoomToContentCommandProperty, value); }
        }
    }
}
