using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Actor
    {
        protected char _icon = ' ';
        protected Vector3 _velocity;
        protected Matrix4 _globalTransform = new Matrix4();
        protected Matrix4 _localTransform = new Matrix4();
        private Matrix4 _translation = new Matrix4();
        private Matrix4 _rotation = new Matrix4();
        private Matrix4 _scale = new Matrix4();
        protected Color _rayColor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected float _rotationAngle;
        private float _rotateCounter = 0f;

        public bool Started { get; private set; }

        public Vector3 GlobalPosition
        {
            get { return new Vector3(_globalTransform.m13, _globalTransform.m23, _globalTransform.m33); }
        }

        public Vector3 LocalPosition
        {
            get { return new Vector3(_localTransform.m13, _localTransform.m23, _globalTransform.m33); }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
            }
        }

        public Vector3 Velocity
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
        public Actor(float y, float x, float z, char icon = ' ')
        {
            _localTransform = new Matrix4();
            _globalTransform = new Matrix4();
            LocalPosition = new Vector3(x, y, z);
            _velocity = new Vector3();
            _rayColor = Color.WHITE;
            _icon = icon;
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="y">Position on the x axis</param>
        /// <param name="x">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, float z, Color rayColor, char icon = ' ')
            : this(x, y, icon)
        {
            _localTransform = new Matrix4();
            _globalTransform = new Matrix4();
            LocalPosition = new Vector3(x, y, z);
            _velocity = new Vector3();
            _rayColor = rayColor;
            _icon = icon;
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

        public void SetTranslate(Vector4 position)
        {
            _translation = Matrix4.CreateTranslation(position);
        }

        public void SetRotationX(float radians)
        {
            _rotation = Matrix4.CreateRotationX(radians);
        }

        public void SetRotationY(float radians)
        {
            _rotation = Matrix4.CreateRotationY(radians);
        }

        public void SetRotationZ(float radians)
        {
            _rotation = Matrix4.CreateRotationZ(radians);
        }

        /*public void Rotate(float radians)
        {
            _rotation *= Matrix4.CreateRotation(radians);
        }*/

        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(new Vector3(x, y, z));
        }

        private void UpdateTransforms()
        {
            _localTransform = _translation * _rotation * _scale;

            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Game.GetCurrentScene().World * _localTransform;
        }

        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            //SetRotation(_rotateCounter);
            _rotateCounter += 0.05f;

            UpdateTransforms();

            //Increase position by the current velocity
            LocalPosition += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            Raylib.DrawText(_icon.ToString(), (int)(GlobalPosition.X * 32), (int)(GlobalPosition.Y * 32), 32, _rayColor);

            //Only draws the actor on the console if it is within the bounds of the window
            if (GlobalPosition.X >= Console.WindowWidth && GlobalPosition.X < Console.WindowWidth
                && GlobalPosition.Y <= 0 && GlobalPosition.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)GlobalPosition.X, (int)GlobalPosition.Y);
                Console.Write(_icon);
            }
        }

        public virtual void End()
        {
            Started = false;
        }
    }
}
