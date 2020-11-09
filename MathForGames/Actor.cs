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
        protected Matrix3 _localTransform = new Matrix3();
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        private Matrix3 _scale = new Matrix3();
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected float _rotationAngle;
        private float _rotateCounter = 0f;
        private float _collisionRadius;
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
            _localTransform = new Matrix3();
            _globalTransform = new Matrix3();
            Forward = new Vector2(1, 0);
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
            //_children[i].Follow(_children[i]._parent.LocalPosition);
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

        public void Rotate(float radians)
        {
            _rotationAngle += radians;
            SetRotation(_rotationAngle);
        }

        public void SetScale(float x, float y)
        {
            _scale.m11 = x;
            _scale.m22 = y;
        }

        /// <summary>
        /// Checks to see if this actor overlaps with another.
        /// </summary>
        /// <param name="other">The actor that this actor is checking collision against</param>
        /// <returns></returns>
        public virtual bool CheckCollision(Actor other)
        {
            //if actor collides with actor call oncollision and return true.

            /*if ()
            {
                OnCollision(other);
                return true;
            }*/

            return false;
        }

        /// <summary>
        /// Use this to define game logic for this actors collision.
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(Actor other)
        {
            //remove actor on hit

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
            Forward = Velocity.Normalized;
        }

        private void UpdateChild()
        {
            for (var i = 0; i < _children.Length; i++)
            {
                _children[i]._velocity = _children[i]._parent._velocity;
                _children[i]._rotation = _children[i]._parent._rotation;
                _children[i]._scale = _children[i]._parent._scale;
                _children[i]._sprite = _children[i]._parent._sprite;
            }
        }

        public void Follow(Vector2 position)
        {
            //Find the direction that the actor should look in 
            Vector2 direction = (position - LocalPosition).Normalized;

            //Use the dotproduct to find the angle the actor needs to rotate 
            float dotProduct = Vector2.DotProduct(Forward, direction);
            if (Math.Abs(dotProduct) > 1)
                return;
            float angle = (float)Math.Acos(dotProduct);

            //Find a perpindicular vector to the direction 
            Vector2 perpin = new Vector2(direction.Y, -direction.X);

            //Find the dot product of the perpindicular vector and the current forward 
            float perpinDot = Vector2.DotProduct(perpin, Forward);

            //If the result isn't 0, use it to change the sign of the angle to be either positive or negative 
            if (perpinDot != 0)
                angle *= -perpinDot / Math.Abs(perpinDot);

            Rotate(angle);
        }

        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            SetRotation(_rotateCounter);
            _rotateCounter += 0.05f;
            
            UpdateLocalTransform();
            UpdateGlobalTransform();

            UpdateChild();
            //SetRotation(-(float)Math.Atan2(Velocity.Y, Velocity.X));

            //Increase position by the current velocity
            LocalPosition += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement

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