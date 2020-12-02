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
        private Actor _target;

        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

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

        /// <summary>
        /// Checks to see if the target is within the given angle
        /// and within the given distance. Returns false if no
        /// target has been set. Both the angle and the distance are inclusive.
        /// </summary>
        /// <param name="maxAngle">The maximum angle (in radians) 
        /// that the target can be detected.</param>
        /// <param name="maxDistance">The maximum distance that the target can be detected.</param>
        /// <returns></returns>
        public bool CheckTargetInSight(float maxAngle, float maxDistance)
        {
            //Checks if the target has a value before continuing
            if (Target == null)
                return false;

            //Find the vector representing the distance between the actor and its target
            Vector3 direction = Target.LocalPosition - LocalPosition;
            //Get the magnitude of the distance vector
            float distance = direction.Magnitude;

            //Use the inverse cosine to find the angle of the dot product in radians
            float angle = (float)Math.Acos(Vector3.DotProduct(Forward, direction.Normalized));

            //Return true if the angle and distance are in range
            if (angle <= maxAngle && distance <= maxDistance)
                return true;

            return false;
        }

        public override void OnCollision(Actor[] other)
        {
            Random randomPos = new Random();
            for (int i = 0; i < other.Length; i++)
            {
                if (other[i] is Partner)
                {
                    Scene currentScene = Engine.GetScenes(Engine.CurrentSceneIndex);
                    currentScene.RemoveActor(this);
                    LocalPosition = new Vector3(100, 100, 100);

                    //LocalPosition = new Vector3(randomPos.Next(-20, 20), 0, randomPos.Next(-20, 20));
                }
            }

            base.OnCollision(other);
        }

        public override void Start()
        {
            GameManager.enemyCount++;
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            Vector3 direction = _target.LocalPosition - LocalPosition;
            Acceleration = direction;

            CheckCollision(_collisionTarget);

            base.Update(deltaTime);
        }

    }
}