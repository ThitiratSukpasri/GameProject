using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public class KeyboardInfo
    {
        KeyboardState _old;
        KeyboardState _new;
        Keys[] _oldArray = new Keys[0];
        Keys[] _newArray = new Keys[0];
        List<Keys> _pressedList;
        List<Keys> _releasedList;
        public KeyboardInfo()
        {
            Update();
            Update();
        }
        public void Update()
        {
            _old = _new;
            _oldArray = _newArray;
            _new = Keyboard.GetState();
            _newArray = _new.GetPressedKeys();
            CalcPressedKeys();
            CalcReleasedKeys();
        }
        private void CalcPressedKeys()
        {
            _pressedList = _newArray.ToList();
            for (int i = 0; i < _oldArray.Length; ++i)
                _pressedList.Remove(_oldArray[i]);
        }
        private void CalcReleasedKeys()
        {
            _releasedList = _oldArray.ToList();
            for (int i = 0; i < _newArray.Length; ++i)
                _releasedList.Remove(_newArray[i]);
        }

        public bool IsKeyPressed(Keys key)
        {
            return !_old.IsKeyDown(key) && _new.IsKeyDown(key);
        }
        public bool IsKeyReleased(Keys key)
        {
            return _old.IsKeyDown(key) && !_new.IsKeyDown(key);
        }
        public bool IsKeyDown(Keys key)
        {
            return _new.IsKeyDown(key);
        }

        public List<Keys> GetDownKeys()
        {
            return _newArray.ToList();
        }
        public List<Keys> GetPressedKeys()
        {
            return _pressedList.ToList();
        }
        public List<Keys> GetReleasedKeys()
        {
            return _releasedList.ToList();
        }

        public bool IsAnyKeyChanged()
        {
            return _pressedList.Count != 0 || _releasedList.Count != 0;
        }
        public bool IsAnyKeyPressed()
        {
            return _pressedList.Count != 0;
        }
        public bool IsAnyKeyReleased()
        {
            return _releasedList.Count != 0;
        }
        public bool IsAnyKeyDown()
        {
            return _newArray.Length != 0;
        }

/*        public void OnKeyPressed(Keys key)
        {
            //keys.Add(key);
        }
        public void OnKeyReleased(Keys key)
        {
            //keys.Remove(key);
        }
        public void OnTextInput(Keys key, char text)
        {

        }*/
    }
}
