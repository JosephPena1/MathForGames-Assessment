using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Enemy : Actor
    {
        private float _collisionRadius;

        public Enemy(float x, float y, float z, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, z, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        public Enemy(float x, float y, float z, Color rayColor, Shape shape, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, z, rayColor, shape, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        public override void OnCollision(Actor other)
        {
            Random randomPos = new Random();
            if (other is Partner && _seconds > 1)
            {
                Velocity = new Vector3(2, 2, 2);
            }
                

            base.OnCollision(other);
        }

        public override void Update(float deltaTime)
        {
            CheckCollision(_collisionTarget);

            base.Update(deltaTime);
        }

    }
}