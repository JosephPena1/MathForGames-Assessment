using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    /// <summary>
    /// This is the base class for all objects that will 
    /// be moved or interacted with in the game
    /// </summary>
    class Actor
    {
        protected char _icon = ' ';
        protected Vector2 _velocity;
        protected Matrix3 _globalTransform;
        protected Matrix3 _localTransform;
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        private Matrix3 _scale = new Matrix3();
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        private Sprite _sprite;

        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get { return new Vector2(_translation.m11, _translation.m21).Normalized; }
            set
            {
                _translation.m11 = value.X;
                _translation.m21 = value.Y;
            }
        }

        public Vector2 WorldPosition
        {
            get { return new Vector2(_globalTransform.m13, _globalTransform.m23); }
        }

        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTransform.m13, _localTransform.m23); }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
            }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="y">Position on the x axis</param>
        /// <param name="x">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float y, float x, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _sprite = new Sprite("Images/player.png");
            _localTransform = new Matrix3();
            _globalTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            _velocity = new Vector2();
            Forward = new Vector2(1, 0);
            _color = color;
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="y">Position on the x axis</param>
        /// <param name="x">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
            _localTransform = new Matrix3();
            _globalTransform = new Matrix3();
            _rayColor = rayColor;
        }

        public void AddChild(Actor child)
        {
            Actor[] tempArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
        }

        public bool RemoveChild(Actor child)
        {
            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length - 1];

            bool childRemoved = false;
            int j = 0;
            for (int i = 0; i < _children.Length; i++)
            {
                if (child != _children[i])
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                    childRemoved = true;
            }

            _children = tempArray;
            child._parent = null;
            return childRemoved;
        }

        public void SetTranslate(Vector2 position)
        {
            _translation.m13 = position.X;
            _translation.m23 = position.Y;
        }

        public void SetRotation(float radians)
        {
            _rotation.m11 = (float)Math.Cos(radians);
            _rotation.m12 = (float)Math.Sin(radians);
            _rotation.m21 = -(float)Math.Sin(radians);
            _rotation.m22 = (float)Math.Cos(radians);
        }

        public void SetScale(float x, float y)
        {
            _scale.m11 = x;
            _scale.m22 = y;
        }

        private void UpdateGlobalTransform()
        {
            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = _localTransform;
        }

        private void UpdateLocalTransform()
        {
            _localTransform = _translation * _rotation * _scale;
        }

        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;

        }

        private void UpdateChild()
        {
            for (var i = 0; i < _children.Length; i++)
            {
                _children[i]._velocity = _children[i]._parent._velocity;
                _children[i]._rotation = _children[i]._parent._rotation;
                _children[i]._scale = _children[i]._parent._scale;
            }
        }

        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            UpdateGlobalTransform();
            UpdateLocalTransform();
            UpdateChild();
            UpdateFacing();

            //Increase position by the current velocity
            LocalPosition += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            _sprite.Draw(_localTransform);

            Raylib.DrawText(_icon.ToString(), (int)(LocalPosition.X * 32), (int)(LocalPosition.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(LocalPosition.X * 32),
                (int)(LocalPosition.Y * 32),
                (int)((LocalPosition.X + Forward.X) * 32),
                (int)((LocalPosition.Y + Forward.Y) * 32),
                _rayColor
            );
            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;

            //Only draws the actor on the console if it is within the bounds of the window
            if (LocalPosition.X >= Console.WindowWidth && LocalPosition.X < Console.WindowWidth
                && LocalPosition.Y <= 0 && LocalPosition.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)LocalPosition.X, (int)LocalPosition.Y);
                Console.Write(_icon);
            }

            //Reset console text color to be default color
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}