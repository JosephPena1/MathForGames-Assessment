using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Goal : Actor
    {
        private float _collisionRadius;

        public Goal(float x, float y, float z, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
           : base(x, y, z, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        public Goal(float x, float y, float z, Color rayColor, Shape shape, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, z, rayColor, shape, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        public override void Update(float deltaTime)
        {
            CheckCollision(_collisionTarget);

            base.Update(deltaTime);
        }

        public override void OnCollision(Actor[] other)
        {
            for (int i = 0; i < other.Length; i++)
            {
                if (other[i] is Enemy && _seconds > 1)
                {
                    GameManager.Gameover = true;
                }
            }

            base.OnCollision(other);
        }
    }
}