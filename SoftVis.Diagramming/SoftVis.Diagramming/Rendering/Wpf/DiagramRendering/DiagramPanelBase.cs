﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Codartis.SoftVis.Diagramming;
using Codartis.SoftVis.Diagramming.Shapes;
using Codartis.SoftVis.Rendering.Wpf.Common;
using Codartis.SoftVis.Rendering.Wpf.DiagramRendering.ShapeControls;

namespace Codartis.SoftVis.Rendering.Wpf.DiagramRendering
{
    /// <summary>
    /// Base class for panels that render diagrams by creating and arranging diagram shapes.
    /// </summary>
    internal abstract class DiagramPanelBase : Panel
    {
        protected readonly Dictionary<DiagramShape, DiagramShapeControlBase> DiagramShapeToControlMap =
            new Dictionary<DiagramShape, DiagramShapeControlBase>();

        protected readonly Dictionary<DiagramShapeControlBase, DiagramShape> ControlToDiagramShapeMap =
            new Dictionary<DiagramShapeControlBase, DiagramShape>();

        private Rect _contentRect;

        public static readonly DependencyProperty DiagramProperty =
            DependencyProperty.Register("Diagram", typeof(Diagram), typeof(DiagramPanelBase),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, Diagram_PropertyChanged));

        public Diagram Diagram
        {
            get { return (Diagram)GetValue(DiagramProperty); }
            set { SetValue(DiagramProperty, value); }
        }

        private static void Diagram_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var diagramPanel = (DiagramPanelBase)obj;
            var diagram = (Diagram)e.NewValue;
            diagramPanel.AddDiagram(diagram);

            diagram.ShapeAdded += diagramPanel.OnShapeAdded;
            diagram.ShapeRemoved += diagramPanel.OnShapeRemoved;
            diagram.Cleared += diagramPanel.OnDiagramCleared;
        }

        protected Rect ContentRect
        {
            get { return _contentRect == Rect.Empty ? new Rect(0,0,0,0) : _contentRect ; }
            private set { _contentRect = value; }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (var child in Children.OfType<DiagramShapeControlBase>())
                child.Measure(availableSize);

            ContentRect = Children.OfType<DiagramShapeControlBase>().Select(i => i.Rect).Union();
            return ContentRect.Size;
        }

        private void AddDiagram(Diagram diagram)
        {
            AddAllGraphElements(diagram);
        }

        private void OnShapeAdded(object sender, DiagramShape shape)
        {
            if (DiagramShapeToControlMap.ContainsKey(shape))
                return;

            if (shape is DiagramNode)
                CreateDiagramNodeControl((DiagramNode)shape);
            else if (shape is DiagramConnector)
                CreateDiagramConnectorControl((DiagramConnector)shape);
        }

        private void OnShapeRemoved(object sender, DiagramShape shape)
        {
            if (!DiagramShapeToControlMap.ContainsKey(shape))
                return;

            RemoveDiagramShapeControl(shape);
        }

        private void OnDiagramCleared(object sender, EventArgs e)
        {
            Children.Clear();
            DiagramShapeToControlMap.Clear();
            ControlToDiagramShapeMap.Clear();
        }

        private void AddAllGraphElements(Diagram diagram)
        {
            foreach (var node in diagram.Nodes)
                CreateDiagramNodeControl(node);

            foreach (var connector in diagram.Connectors)
                CreateDiagramConnectorControl(connector);
        }

        private void CreateDiagramNodeControl(DiagramNode diagramNode)
        {
            var control = new DiagramNodeControl { DataContext = diagramNode };
            control.PreviewMouseDoubleClick += OnDiagramNodeDoubleClicked;
            control.PreviewMouseLeftButtonDown += OnDiagramNodeLeftButtonDown;

            DiagramShapeToControlMap.Add(diagramNode, control);
            ControlToDiagramShapeMap.Add(control, diagramNode);
            Children.Add(control);
        }

        private void CreateDiagramConnectorControl(DiagramConnector diagramConnector)
        {
            var control = new DiagramConnectorControl { DataContext = diagramConnector };
            DiagramShapeToControlMap.Add(diagramConnector, control);
            ControlToDiagramShapeMap.Add(control, diagramConnector);
            Children.Add(control);
        }

        private void RemoveDiagramShapeControl(DiagramShape diagramShape)
        {
            var control = DiagramShapeToControlMap[diagramShape];
            Children.Remove(control);
            DiagramShapeToControlMap.Remove(diagramShape);
            ControlToDiagramShapeMap.Remove(control);
        }

        private void OnDiagramNodeLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var senderDiagramNode = sender as DiagramNodeControl;
            if (senderDiagramNode == null || !ControlToDiagramShapeMap.ContainsKey(senderDiagramNode))
                return;

            Diagram.OnShapeSelected(ControlToDiagramShapeMap[senderDiagramNode]);
        }

        private void OnDiagramNodeDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var senderDiagramNode = sender as DiagramNodeControl;
            if (senderDiagramNode == null || !ControlToDiagramShapeMap.ContainsKey(senderDiagramNode))
                return;

            Diagram.OnShapeActivated(ControlToDiagramShapeMap[senderDiagramNode]);
            e.Handled = true;
        }
    }
}