using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdvancedWPFPanel
{
    public class DraggablePanel : Canvas
    {
        private Point _lastPoint;
        private bool _isInDrag;

        static DraggablePanel()
        {
            //TODO: ne bu?
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DraggablePanel), new FrameworkPropertyMetadata(typeof(DraggablePanel)));
        }

        public DraggablePanel()
        {
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            if (visualAdded is UIElement added)
            {
                AddChild(added);
            }

            if (visualRemoved is UIElement removed)
            {
                RemoveChild(removed);
            }
        }

        private void AddChild(UIElement element)
        {
            Canvas.SetLeft(element, 0);
            Canvas.SetTop(element, 0);

            element.MouseLeftButtonDown += UIElement_MouseLeftButtonDown;
            element.MouseLeftButtonUp += UIElement_MouseLeftButtonUp;
        }

        private void RemoveChild(UIElement element)
        {
            element.MouseLeftButtonDown -= UIElement_MouseLeftButtonDown;
            element.MouseLeftButtonUp -= UIElement_MouseLeftButtonUp;
        }

        private void UIElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Down");

            if (sender is UIElement uiElement)
            {
                uiElement.MouseMove += UiElement_MouseMove;

                _lastPoint = e.GetPosition(this);

                _isInDrag = true;
                uiElement.CaptureMouse();
            }
        }

        private void UIElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Up");

            if (sender is UIElement uiElement)
            {
                uiElement.MouseMove -= UiElement_MouseMove;

                uiElement.ReleaseMouseCapture();
                _isInDrag = false;
            }
        }

        private void UiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isInDrag)
            {
                var element = sender as UIElement;

                var currentPoint = e.GetPosition(this);

                Console.WriteLine("currentPoint: " + currentPoint + ", _lastPoint: " + _lastPoint);

                var left = Canvas.GetLeft(element);
                var top = Canvas.GetTop(element);

                var diff = currentPoint - _lastPoint;

                Canvas.SetLeft(element, left + diff.X);
                Canvas.SetTop(element, top + diff.Y);

                _lastPoint = currentPoint;
            }
        }
    }
}
