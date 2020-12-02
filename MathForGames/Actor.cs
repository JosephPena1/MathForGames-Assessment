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
        protected int _seconds = 0;
        protected int _iSeconds = 0;
        private float _totalFrames = 0f;
        private float _totalIFrames = 0f;
        protected char _icon = ' ';
        private Vector2 _velocity = new Vector2();
        protected Vector2 _acceleration = new Vector2();
        protected Matrix3 _globalTransform = new Matrix3();
        protected Matrix3 _localTransform = new Matrix3();
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        private Matrix3 _scale = new Matrix3();
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected float _rotationAngle;
        protected float _rotateCounter = 0f;
        private float _collisionRadius = 0.5f;
        protected Actor[] _collisionTarget = new Actor[0];
        private float _maxSpeed = 5;

        public bool Started { get; private set; }
        public Actor Parent { get; private set; }

        public Vector2 Forward
        {
            get { return new Vector2(_globalTransform.m11, _globalTransform.m21); }
            set { LookAt(value.Normalized + GlobalPosition); }
        }

        public Vector2 GlobalPosition
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

        public Vector2 Velocity { get => _velocity; set => _velocity = value; }

        protected Vector2 Acceleration { get => _acceleration; set => _acceleration = value; }

        public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="y">Position on the x axis</param>
        /// <param name="x">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float y, float x)
        {
            _localTransform = new Matrix3();
            _globalTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            //Forward = new Vector2(1, 0);
            _collisionRadius = 0.5f;
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="y">Position on the x axis</param>
        /// <param name="x">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, float collisionRadius)
        {
            _localTransform = new Matrix3();
            _globalTransform = new Matrix3();
            //Forward = new Vector2(1, 0);
            _collisionRadius = collisionRadius;
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
            _translation = Matrix3.CreateTranslation(position);
        }

        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }

        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(new Vector2(x, y));
        }

        /// <summary>
        /// Checks to see if this actor overlaps with another.
        /// </summary>
        /// <param name="other">The actor that this actor is checking collision against</param>
        /// <returns></returns>
        public virtual bool CheckCollision(Actor[] actor)
        {
            //if actor collides with actor call OnCollision and return true.
            if (actor == null)
                return false;

            for (int i = 0; i < actor.Length; i++)
            {
                if (actor[i]._collisionRadius + _collisionRadius > (actor[i].GlobalPosition - GlobalPosition).Magnitude && actor[i] != this)
                {
                    OnCollision(actor);
                }

                else if (i == actor.Length)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Use this to define game logic for this actors collision.
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(Actor[] actor)
        {

        }

        public void AddCollisionTarget(Actor actor)
        {
            Actor[] appendedArray = new Actor[_collisionTarget.Length + 1];

            for (int i = 0; i < _collisionTarget.Length; i++)
            {
                appendedArray[i] = _collisionTarget[i];
            }

            appendedArray[_collisionTarget.Length] = actor;
            _collisionTarget = appendedArray;
        }

        private void UpdateTransforms()
        {
            _localTransform = _translation * _rotation * _scale;

            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Engine.GetCurrentScene().World * _localTransform;
        }

        public virtual void UpdateFacing()
        {
            /*if (_velocity.Magnitude <= 0)
                return;

            Forward = Velocity;*/
        }

        public void LookAt(Vector2 position)
        {
            Vector2 direction = (position - GlobalPosition).Normalized;

            float angle = Vector2.FindAngle(Forward, direction);

            Rotate(-angle);
        }

        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {

            /*if (Velocity.Magnitude != 0)
                SetRotation(-(float)Math.Atan2(Velocity.Y, Velocity.X));*/

            /*SetRotation(_rotateCounter);
            _rotateCounter += 0.05f;*/

            UpdateTransforms();

            //UpdateFacing();

            if (Velocity.Magnitude != 0)
            {
                Velocity.X -= Velocity.X / 20;
                Velocity.Y -= Velocity.Y / 20;
            }

            Velocity += Acceleration;

            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.Normalized * MaxSpeed;

            //Increase position by the current velocity
            LocalPosition += _velocity * deltaTime;

            if (_totalFrames == 70)
            {
                _seconds += 1;
                _totalFrames = 0;
            }
            if (_totalIFrames == 10)
            {
                _iSeconds += 1;
                _totalIFrames = 0;
            }
            _totalIFrames++;
            _totalFrames++;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement

            //Raylib.DrawText(_icon.ToString(), (int)(GlobalPosition.X * 32), (int)(GlobalPosition.Y * 32), 32, _rayColor);
            /*Raylib.DrawLine(
                (int)(GlobalPosition.X * 32),
                (int)(GlobalPosition.Y * 32),
                (int)((GlobalPosition.X + Forward.X) * 32),
                (int)((GlobalPosition.Y + Forward.Y) * 32),
                _rayColor
            );*/
            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;

            //Only draws the actor on the console if it is within the bounds of the window
            if (GlobalPosition.X >= Console.WindowWidth && GlobalPosition.X < Console.WindowWidth
                && GlobalPosition.Y <= 0 && GlobalPosition.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)GlobalPosition.X, (int)GlobalPosition.Y);
                Console.Write(_icon);
            }

            //Reset console text color to be default color
            Console.ForegroundColor = Engine.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }

        public virtual void Destroy()
        {

            if (Parent != null)
                Parent.RemoveChild(this);

            foreach (Actor child in _children)
                child.Destroy();

            End();
        }

    }
}