﻿using System;
using System.Windows;
using Codartis.SoftVis.UI.Geometry;
using Codartis.SoftVis.UI.Wpf.Commands;
using Codartis.SoftVis.UI.Wpf.Common.Geometry;

namespace Codartis.SoftVis.UI.Wpf.ViewModel
{
    /// <summary>
    /// A button on a diagram shape.
    /// </summary>
    public abstract class DiagramButtonViewModelBase : ViewModelBase
    {
        private readonly double _buttonRadius;
        private readonly RectRelativeLocation _rectRelativeLocation;
        private readonly Action<DiagramShapeViewModelBase> _clickCommandDelegate;

        private Size _size;
        private Point _relativeTopLeft;
        private bool _isVisible;
        private bool _isEnabled;

        public DelegateCommand ClickCommand { get; private set; }
        public DiagramShapeViewModelBase AssociatedDiagramShapeViewModel { get; private set; }

        protected DiagramButtonViewModelBase(double buttonRadius, RectRelativeLocation rectRelativeLocation,
            Action<DiagramShapeViewModelBase> clickCommandDelegate)
        {
            _buttonRadius = buttonRadius;
            _rectRelativeLocation = rectRelativeLocation;
            _clickCommandDelegate = clickCommandDelegate;

            _size = new Size(buttonRadius * 2, buttonRadius * 2);
            _isVisible = false;
            _isEnabled = true;

            ClickCommand = new DelegateCommand(OnClick);
        }

        public Size Size
        {
            get { return _size; }
            set
            {
                if (_size != value)
                {
                    _size = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Width");
                    OnPropertyChanged("Height");
                }
            }
        }

        public double Width => Size.Width;
        public double Height => Size.Height;

        public Point RelativeTopLeft
        {
            get { return _relativeTopLeft; }
            set
            {
                if (_relativeTopLeft != value)
                {
                    _relativeTopLeft = value;
                    OnPropertyChanged();
                    OnPropertyChanged("RelativeLeft");
                    OnPropertyChanged("RelativeTop");
                }
            }
        }

        public double RelativeTop => RelativeTopLeft.Y;
        public double RelativeLeft => RelativeTopLeft.X;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public virtual void AssociateWith(DiagramShapeViewModelBase diagramShapeViewModel)
        {
            AssociatedDiagramShapeViewModel = diagramShapeViewModel;
            RelativeTopLeft = CalculateTopLeft(diagramShapeViewModel);
            IsVisible = true;
        }

        public void Hide()
        {
            AssociatedDiagramShapeViewModel = null;
            IsVisible = false;
        }

        private Point CalculateTopLeft(DiagramShapeViewModelBase diagramShapeViewModel)
        {
            var parentTopLeftToButtonCenter = GetButtonCenterRelativeToDiagramShape(diagramShapeViewModel.Size);
            var buttonCenterToButtonTopLeft = new Vector(-_buttonRadius, -_buttonRadius);
            var location = parentTopLeftToButtonCenter + buttonCenterToButtonTopLeft;
            return location;
        }

        private Point GetButtonCenterRelativeToDiagramShape(Size diagramShapeSize)
        {
            return new Rect(diagramShapeSize).GetRelativePoint(_rectRelativeLocation);
        }

        private void OnClick()
        {
            _clickCommandDelegate(AssociatedDiagramShapeViewModel);
        }
    }
}
