using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Player : Actor
    {
        private float _speed = 1;
        private float _collisionRadius;
        private float _scaleX = 1;
        private float _scaleY = 1;
        private float _scaleZ = 1;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Player(float x, float y, float z, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, z, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Player(float x, float y, float z, Color rayColor, Shape shape, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, z, rayColor, shape, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        /*public void CreateProjectile(Actor projectile)
        {
            AddChild(projectile);
        }*/

        public override void Update(float deltaTime)
        {
            //changes controls based on number. 1 = mouse, 2 = WASD.
            //int controls = 2;

            float xDirection = 0;
            float yDirection = 0;
            float zDirection = 0;

            xDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_D));
            yDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_Q))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_E));
            zDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_S));

            if (Engine.GetKeyPressed((int)KeyboardKey.KEY_UP))
            {
                _scaleX += 1;
                _scaleY += 1;
                _scaleZ += 1;
            }
            else if (Engine.GetKeyPressed((int)KeyboardKey.KEY_DOWN))
            {
                _scaleX -= 1;
                _scaleY -= 1;
                _scaleZ -= 1;
            }

            if (Engine.GetKeyDown((int)KeyboardKey.KEY_LEFT))
                _rotationCounter -= 0.05f;

            else if (Engine.GetKeyDown((int)KeyboardKey.KEY_RIGHT))
                _rotationCounter += 0.05f;

            SetScale(new Vector3(_scaleX, _scaleY, _scaleZ));

            //Set the actors current velocity to be the vector with the direction found scaled by the speed
            Velocity = new Vector3(xDirection, yDirection, zDirection);
            Velocity = Velocity.Normalized * Speed;

            /*SetRotation(_rotateCounter);
            _rotateCounter += 0.05f;*/

            /*CheckCollision(_collisionTarget);*/

            base.Update(deltaTime);
        }
    }
}