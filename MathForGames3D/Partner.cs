using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Partner : Actor
    {
        private float _collisionRadius;

        public Partner(float x, float y, float z, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, z, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        public Partner(float x, float y, float z, Color rayColor, Shape shape, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, z, rayColor, shape, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        public override void OnCollision(Actor[] other)
        {
            /*if (other is Player && _seconds > 5)
                ;*/

            base.OnCollision(other);
        }

        public override void Update(float deltaTime)
        {
            CheckCollision(_collisionTarget);

            base.Update(deltaTime);
        }
    }
}