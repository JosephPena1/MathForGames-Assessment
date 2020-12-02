using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    /// <summary>
    /// An actor that moves based on input given by the user
    /// </summary>
    class Player : Actor
    {
        private float _collisionRadius;
        private float _speed = 1;
        private float rotation;
        private float _scaleX = 1;
        private float _scaleY = 1;
        private Sprite _sprite;

        public float Speed { get => _speed; set => _speed = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Player(float x, float y)
            : base(x, y)
        {
            _sprite = new Sprite("Images/player.png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Player(float x, float y, float collisionRadius)
            : base(x, y, collisionRadius)
        {
            _sprite = new Sprite("Images/player.png");
        }

        public override void Start()
        {
            GameManager.onWin += DrawWinText;

            base.Start();
        }

        public override void UpdateFacing()
        {
            LookAt(new Vector2(Raylib.GetMousePosition().X / 32, Raylib.GetMousePosition().Y / 32));
        }

        public override void Update(float deltaTime)
        {
            UpdateFacing();

            float xDirection = 0;
            float yDirection = 0;

            xDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_D));
            yDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_S));

            Acceleration = new Vector2(xDirection, yDirection);

            if (Engine.GetKeyPressed((int)KeyboardKey.KEY_UP))
            {
                _scaleX += 1;
                _scaleY += 1;
            }
            else if (Engine.GetKeyPressed((int)KeyboardKey.KEY_DOWN))
            {
                _scaleX -= 1;
                _scaleY -= 1;
            }
            SetScale(_scaleX, _scaleY);

            if (Engine.GetKeyDown((int)KeyboardKey.KEY_LEFT_CONTROL))
                Acceleration /= 10;

            //Set the actors current velocity to be the vector with the direction found scaled by the speed


            /*SetRotation(_rotateCounter);
            _rotateCounter += 0.05f;*/

            CheckCollision(_collisionTarget);

            Console.WriteLine("x:" + Math.Round(GlobalPosition.X) + " " + "y:" + Math.Round(GlobalPosition.Y));

            base.Update(deltaTime);
        }

        //Set every wall to collide with player
        public override void OnCollision(Actor[] actor)
        {
            for (int i = 0; i < actor.Length; i++)
            {
                if (actor[i] is Bullet && _seconds > 1)
                {
                    GameManager.Gameover = true;
                    /*Scene currentScene = Engine.GetScenes(Engine.CurrentSceneIndex);
                    currentScene.RemoveActor(this);*/
                }
            }

            base.OnCollision(actor);
        }

        public override void Draw()
        {
            _sprite.Draw(_globalTransform);
            base.Draw();
        }

        public void DrawWinText()
        {
            Raylib.DrawText("You win. \nPress esc to quit", 0, 0, 20, Color.GREEN);
        }
    }
}