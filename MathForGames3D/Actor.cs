using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    enum Shape
    {
        SPHERE,
        CUBE
    }

    class Actor
    {
        protected char _icon = ' ';
        protected Matrix4 _globalTransform = new Matrix4();
        protected Matrix4 _localTransform = new Matrix4();
        protected Matrix4 _rotation = new Matrix4();
        protected Matrix4 _translation = new Matrix4();
        protected Matrix4 _scale = new Matrix4();
        protected Actor[] _children = new Actor[0];
        private Vector3 _velocity = new Vector3();
        private Vector3 _acceleration = new Vector3();
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected float _rotationCounter = 0f;
        private float _collisionRadius;
        protected Actor _collisionTarget;
        protected int _seconds = 0;
        private float _totalFrames = 0f;
        private float _radians;
        private float rotation;
        private float _maxSpeed = 5;
        protected Shape _shape;

        public bool Started { get; private set; }
        public Actor Parent { get; private set; }

        public Vector3 Forward
        {
            get
            {
                return new Vector3(_globalTransform.m13, _globalTransform.m23, _globalTransform.m33).Normalized;
            }
            set
            {
                Vector3 lookPosition = GlobalPosition + value.Normalized;
                LookAt(lookPosition);
            }
        }

        public Vector3 GlobalPosition
        {
            get
            {
                return new Vector3(_globalTransform.m14, _globalTransform.m24, _globalTransform.m34);
            }

        }

        public Vector3 LocalPosition
        {
            get
            {
                return new Vector3(_localTransform.m14, _localTransform.m24, _localTransform.m34);
            }
            set
            {
                _translation.m14 = value.X;
                _translation.m24 = value.Y;
                _translation.m34 = value.Z;
            }
        }

        public Vector3 Velocity
        {
            get{return _velocity;}
            set{_velocity = value;}
        }

        public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

        protected Vector3 Acceleration { get => _acceleration; set => _acceleration = value; }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, float z, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            LocalPosition = new Vector3(x, y, z);
            _velocity = new Vector3();
            _color = color;
            _collisionRadius = collisionRadius;
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, float z, Color rayColor, Shape shape, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, z, collisionRadius, icon, color)
        {
            _rayColor = rayColor;
            _shape = shape;
        }

        public void AddChild(Actor child)
        {
            //Create a new array with a size one greater than our old array
            Actor[] appendedArray = new Actor[_children.Length + 1];
            //Copy the values from the old array to the new array
            for (int i = 0; i < _children.Length; i++)
            {
                appendedArray[i] = _children[i];
            }

            child.Parent = this;

            //Set the last value in the new array to be the actor we want to add
            appendedArray[_children.Length] = child;
            //Set old array to hold the values of the new array
            _children = appendedArray;
        }

        public bool RemoveChild(int index)
        {
            //Check to see if the index is outside the bounds of our array
            if (index < 0 || index >= _children.Length)
            {
                return false;
            }

            bool actorRemoved = false;

            //Create a new array with a size one less than our old array 
            Actor[] newArray = new Actor[_children.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _children.Length; i++)
            {
                //If the current index is not the index that needs to be removed,
                //add the value into the old array and increment j
                if (i != index)
                {
                    newArray[j] = _children[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                }
            }
            _children[index].Parent = null;
            //Set the old array to be the tempArray
            _children = newArray;
            return actorRemoved;
        }

        public bool RemoveChild(Actor child)
        {
            //Check to see if the actor was null
            if (child == null)
            {
                return false;
            }

            bool actorRemoved = false;
            //Create a new array with a size one less than our old array
            Actor[] newArray = new Actor[_children.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _children.Length; i++)
            {
                if (child != _children[i])
                {
                    newArray[j] = _children[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                }
            }
            child.Parent = null;
            //Set the old array to the new array
            _children = newArray;
            //Return whether or not the removal was successful
            return actorRemoved;
        }

        public void SetScale(Vector3 scale)
        {
            _scale.m11 = scale.X;
            _scale.m22 = scale.Y;
            _scale.m33 = scale.Z;
        }

        public void Scale(Vector3 scale)
        {
            if (scale.X != 0)
                _scale.m11 *= scale.X;
            if (scale.Y != 0)
                _scale.m22 *= scale.Y;
            if (scale.Z != 0)
                _scale.m33 *= scale.Z;
        }

        /// <summary>
        /// Set the rotation angle to be the given value in radians on the X axis
        /// </summary>
        /// <param name="radians">The angle to se the transform's rotation to</param>
        public void SetRotationX(float radians)
        {
            _radians = radians;
            _rotation.m22 = (float)Math.Cos(_radians);
            _rotation.m32 = -(float)Math.Sin(_radians);
            _rotation.m23 = (float)Math.Sin(_radians);
            _rotation.m33 = (float)Math.Cos(_radians);
        }

        /// <summary>
        /// Set the rotation angle to be the given value in radians on the Y axis
        /// </summary>
        /// <param name="radians">The angle to be the transform's rotation to</param>
        public void SetRotationY(float radians)
        {
            _radians = radians;
            _rotation.m11 = (float)Math.Cos(_radians);
            _rotation.m31 = (float)Math.Sin(_radians);
            _rotation.m13 = -(float)Math.Sin(_radians);
            _rotation.m33 = (float)Math.Cos(_radians);
        }

        /// <summary>
        /// Set the rotation angle to be the given value in radians on the Z axis
        /// </summary>
        /// <param name="radians">The angle to se the transform's rotation to</param>
        public void SetRotationZ(float radians)
        {
            _radians = radians;
            _rotation.m11 = (float)Math.Cos(_radians);
            _rotation.m12 = (float)Math.Sin(_radians);
            _rotation.m21 = -(float)Math.Sin(_radians);
            _rotation.m22 = (float)Math.Cos(_radians);
        }

        /// <summary>
        /// Increases the angle of rotation by the given amount.
        /// </summary>
        /// <param name="radians">The amount of radians to increase the rotation by</param>
        public void Rotate(float radians)
        {
            _radians += radians;
            SetRotationY(_radians);
        }

        /// <summary>
        /// Updates the actors forward vector to be
        /// the last direction it moved in
        /// </summary>
        protected void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;

            Forward = Velocity.Normalized;
        }

        /// <summary>
        /// Updates the global transform to be the combination of the paernt and local
        /// transforms. Updates the transforms for all children of this actor
        /// </summary>
        private void UpdateGlobalTransform()
        {
            if (Parent != null)
                _globalTransform = Parent._globalTransform * _localTransform;
            else
                _globalTransform = _localTransform;

            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].UpdateGlobalTransform();
            }
        }

        public virtual bool CheckCollision(Actor actor)
        {
            //if actor collides with actor call OnCollision and return true.
            if (actor == null)
                return false;

            if (actor._collisionRadius + _collisionRadius > (actor.GlobalPosition - GlobalPosition).Magnitude && actor != this)
            {
                OnCollision(actor);
                return true;
            }

            return false;
        }

        public virtual void OnCollision(Actor other)
        {

        }

        public void SetCollisionTarget(Actor actor)
        {
            _collisionTarget = actor;
        }

        public void LookAt(Vector3 position)
        {
            //Find the direction that the actor should look in 
            Vector3 direction = (position - LocalPosition).Normalized;

            //Use the dotproduct to find the angle the actor needs to rotate 
            float dotProd = Vector3.DotProduct(Forward, direction);
            if (Math.Abs(dotProd) > 1)
                return;
            float angle = (float)Math.Acos(dotProd);

            //Find a perpindicular vector to the direction 
            Vector3 perp = new Vector3(direction.Y, -direction.X, direction.Z);

            //Find the dot product of the perpindicular vector and the current forward 
            float perpinDot = Vector3.DotProduct(perp, Forward);

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

            _localTransform = _translation * _rotation * _scale;

            UpdateGlobalTransform();

            LocalPosition += _velocity * deltaTime;

            UpdateFacing();

            Velocity += Acceleration;

            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.Normalized * MaxSpeed;

            //SetRotationX(_rotationCounter);
            SetRotationY(_rotationCounter);
            //SetRotationZ(_rotationCounter);
            //_rotationCounter += 0.05f;

            if (_totalFrames == 75)
            {
                _seconds += 1;
                _totalFrames = 0;
            }
            _totalFrames++;


        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window
            Raylib.DrawLine(
                (int)(GlobalPosition.X * 32),
                (int)(GlobalPosition.Y * 32),
                (int)((GlobalPosition.X + Forward.X) * 32),
                (int)((GlobalPosition.Y + Forward.Y) * 32),
                Color.WHITE
            );
            Console.ForegroundColor = _color;
            DrawShape();
        }

        private void DrawShape()
        {
            switch (_shape)
            {
                case Shape.SPHERE:
                    Raylib.DrawSphere(new System.Numerics.Vector3(GlobalPosition.X, GlobalPosition.Y, GlobalPosition.Z), 2, _rayColor);
                    break;
                case Shape.CUBE:
                    Raylib.DrawCube(new System.Numerics.Vector3(GlobalPosition.X, GlobalPosition.Y, GlobalPosition.Z), 2, 2, 2, _rayColor);
                    break;
            }
        }

        public virtual void Debug()
        {
            if (Parent != null)
                Console.WriteLine("Velocity: " + Velocity.X + ", " + Velocity.Y);
        }

        public void Destroy()
        {

            if (Parent != null)
                Parent.RemoveChild(this);

            foreach (Actor child in _children)
                child.Destroy();

            End();
        }

        public virtual void End()
        {
            Started = false;
        }
    }
}