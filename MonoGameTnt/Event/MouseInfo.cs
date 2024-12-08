using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Collections.Generic;

namespace ThanaNita.MonoGameTnt
{
    public enum MouseButtons { None = 0, Left = 1, Right = 2, Middle = 4 };
    public class MouseInfo
    {
        private GraphicsDevice device;
        private OrthographicCamera camera;

        private MouseStateTnt _old;
        private MouseStateTnt _new;

        public MouseInfo(GraphicsDevice device, OrthographicCamera camera)
        {
            this.device = device;
            this.camera = camera;

            _new = CreateState();
            Update();
        }

        private MouseStateTnt CreateState()
        {
            return new MouseStateTnt(Mouse.GetState(), device, camera);
        }

        public void Update() // Must be called every frame
        {
            var currentState = CreateState();

            _old = _new;
            _new = currentState;
        }

        // กรณีสร้างด้วยตัวนี้ จะเป็น read-only ห้ามเรียก Update()
        private MouseInfo(MouseStateTnt oldState, MouseStateTnt newState)
        {
            device = null;
            camera = null;
            _old = oldState;
            _new = newState;
        }
        public MouseInfo CreateCopy()
        {
            return new MouseInfo(_old, _new);
        }
        // ScreenPosition: Physical Screen Position (Client Area)
        // (0,0) is (Left,bottom) if using GeometricalYAxis
        public Vector2 ScreenPosition => _new.ScreenPosition;
        public Vector2 WorldPosition => _new.WorldPosition;
        public int Scroll => _new.State.ScrollWheelValue;

        public Vector2 DeltaScreenPosition => _new.ScreenPosition - _old.ScreenPosition;
        public Vector2 DeltaWorldPosition => _new.WorldPosition - _old.WorldPosition;
        public int DeltaScroll => _new.State.ScrollWheelValue - _old.State.ScrollWheelValue;

        // All Buttons Test
        public bool IsAnyButtonDown() => IsLeftButtonDown() || IsRightButtonDown() || IsMiddleButtonDown();
        public bool IsAnyButtonPressed() => IsLeftButtonPressed() || IsRightButtonPressed() || IsMiddleButtonPressed();
        public bool IsAnyButtonReleased() => IsLeftButtonReleased() || IsRightButtonReleased() || IsMiddleButtonReleased();

        // Button States (Down, or not)
        public bool IsLeftButtonDown() => IsButtonDown(MouseButtons.Left);
        public bool IsRightButtonDown() => IsButtonDown(MouseButtons.Right);
        public bool IsMiddleButtonDown() => IsButtonDown(MouseButtons.Middle);

        // Events (changes of states)
        public bool IsLeftButtonPressed() => IsButtonPressed(MouseButtons.Left);
        public bool IsLeftButtonReleased() => IsButtonReleased(MouseButtons.Left);

        public bool IsRightButtonPressed() => IsButtonPressed(MouseButtons.Right);
        public bool IsRightButtonReleased() => IsButtonReleased(MouseButtons.Right);

        public bool IsMiddleButtonPressed() => IsButtonPressed(MouseButtons.Middle);
        public bool IsMiddleButtonReleased() => IsButtonReleased(MouseButtons.Middle);

        public bool IsPositionChanged() => _old.State.Position != _new.State.Position;
        public bool IsScrolled() => _old.State.ScrollWheelValue != _new.State.ScrollWheelValue;


        // Generate events virtually from the polling
        public List<MouseEvent> GenerateEvents()
        {
            var events = new List<MouseEvent>();

            GenerateMouseEvents(events, MouseButtons.Left);
            GenerateMouseEvents(events, MouseButtons.Right);
            GenerateMouseEvents(events, MouseButtons.Middle);
            GenerateOtherEvents(events);

            return events;
        }

        private void GenerateMouseEvents(List<MouseEvent> events, MouseButtons button)
        {
            if (IsButtonPressed(button))
                events.Add(CreateMouseEvent(MouseEvent.EventType.MousePressed, button));
            if (IsButtonReleased(button))
                events.Add(CreateMouseEvent(MouseEvent.EventType.MouseReleased, button));
        }

        private void GenerateOtherEvents(List<MouseEvent> events)
        {
            if (IsPositionChanged())
                events.Add(CreateMouseEvent(MouseEvent.EventType.MouseMoved, MouseButtons.None));

            if (IsScrolled())
                events.Add(CreateMouseEvent(MouseEvent.EventType.MouseScrolled, MouseButtons.None));
        }

        private MouseEvent CreateMouseEvent(MouseEvent.EventType type, MouseButtons button)
        {
            return new MouseEvent(type, WorldPosition, DeltaScroll, button);
        }

        public bool IsButtonPressed(MouseButtons button)
        {
            return _old.State.IsButtonUp(button) && _new.State.IsButtonDown(button);
        }
        public bool IsButtonReleased(MouseButtons button)
        {
            return _old.State.IsButtonDown(button) && _new.State.IsButtonUp(button);
        }
        public bool IsButtonDown(MouseButtons button)
        {
            return _new.State.IsButtonDown(button);
        }
    }
}
