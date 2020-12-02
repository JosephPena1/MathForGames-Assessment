using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Bullet : Actor
    {
        private float _collisionRadius;
        private float _speed = 1;
        private Sprite _sprite;

        public float Speed { get => _speed; set => _speed = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Bullet(float x, float y)
            : base(x, y)
        {
            _sprite = new Sprite("Images/bullet.png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Bullet(float x, float y, float collisionRadius)
            : base(x, y, collisionRadius)
        {
            _sprite = new Sprite("Images/bullet.png");
        }

        /// <summary>
        /// Creates Bullets based on number given
        /// </summary>
        /// <param name="numBullets">Number of bullets made</param>
        /// <param name="actor">set collision for given actor</param>
        public static void CreateBullets(int numBullets, Actor actor)
        {
            Bullet[] bullets = new Bullet[numBullets];
            Scene currentScene = Engine.GetScenes(Engine.CurrentSceneIndex);
            
            Random randomPos = new Random();
            for (int i = 0; i < numBullets; i++)
            {
                bullets[i] = new Bullet(randomPos.Next(25, 35), randomPos.Next(5, 35));
                bullets[i].SetScale(2, 2);
                currentScene.AddActor(bullets[i]);
            }

            for (int i = 0; i < bullets.Length; i++)
            {
                actor.AddCollisionTarget(bullets[i]);
            }

        }

        public override void Update(float deltaTime)
        {
            if (Velocity.Magnitude != 0)
                SetRotation(-(float)Math.Atan2(Velocity.Y, Velocity.X));

            Random randomPos = new Random();
            if (GlobalPosition.X <= -5 || GlobalPosition.Y <= -5 && _seconds > 1)
            {
                SetTranslate(new Vector2(randomPos.Next(50, 55), randomPos.Next(0,30)));
            }

            Acceleration += new Vector2(-1, 0);
            //If the player is in range of the goal, end the game
            CheckCollision(_collisionTarget);

            base.Update(deltaTime);
        }

        public override void OnCollision(Actor[] actor)
        {
            /*for (int i = 0; i < actor.Length; i++)
            {
                if (actor[i] is Player && _seconds > 1)
                    GameManager.Gameover = true;
            }*/
            
            base.OnCollision(actor);
        }

        public override void Draw()
        {
            _sprite.Draw(_globalTransform);

            base.Draw();
        }
    }
}
