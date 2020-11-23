using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Projectile : Actor
    {
        private float _collisionRadius;

        public Projectile(float x, float y, float z, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, z, collisionRadius, icon, color)
        {
            _collisionRadius = collisionRadius;
        }

        public Projectile(float x, float y, float z, Color rayColor, Shape shape, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
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
                if (other[i] is Enemy)
                {
                    Scene currentScene = Engine.GetScenes(Engine.CurrentSceneIndex);
                    currentScene.RemoveActor(this);

                }
            }

            base.OnCollision(other);
        }
    }
}
