using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static Scene[] _scenes;
        private static int _currentSceneIndex;

        public static int CurrentSceneIndex { get => _currentSceneIndex; set => _currentSceneIndex = value; }

        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Returns the scene at the index given.
        /// Returns an empty scene if the index is out of bounds
        /// </summary>
        /// <param name="index">The index of the desired scene</param>
        /// <returns></returns>
        public static Scene GetScene(int index)
        {
            if (index < 0 || index > _scenes.Length)
                return new Scene();

            return _scenes[index];
        }

        /// <summary>
        /// Returns the scene that is at the index of the 
        /// current scene index
        /// </summary>
        /// <returns></returns>
        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }

        /// <summary>
        /// Adds the given scene to the array of scenes.
        /// </summary>
        /// <param name="scene">The scene that will be added to the array</param>
        /// <returns>The index the scene was placed at. Returns -1 if
        /// the scene is null</returns>
        public static int AddScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return -1;

            //Create a new temporary array that one size larger than the original
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy values from old array into new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Store the current index
            int index = _scenes.Length;

            //Sets the scene at the new index to be the scene passed in
            tempArray[index] = scene;

            //Set the old array to the tmeporary array
            _scenes = tempArray;

            return index;
        }

        /// <summary>
        /// Finds the instance of the scene given that inside of the array
        /// and removes it
        /// </summary>
        /// <param name="scene">The scene that will be removed</param>
        /// <returns>If the scene was successfully removed</returns>
        public static bool RemoveScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return false;

            bool sceneRemoved = false;

            //Create a new temporary array that is one less than our original array
            Scene[] tempArray = new Scene[_scenes.Length - 1];

            //Copy all scenes except the scene we don't want into the new array
            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            //If the scene was successfully removed set the old array to be the new array
            if (sceneRemoved)
                _scenes = tempArray;

            return sceneRemoved;
        }

        /// <summary>
        /// Sets the current scene in the game to be the scene at the given index
        /// </summary>
        /// <param name="index">The index of the scene to switch to</param>
        public static void SetCurrentScene(int index)
        {
            //If the index is not within the range of the the array return
            if (index < 0 || index >= _scenes.Length)
                return;

            //Call end for the previous scene before changing to the new one
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            //Update the current scene index
            _currentSceneIndex = index;
        }

        public static Scene GetScenes(int index)
        {
            return _scenes[index];
        }

        /// <summary>
        /// Returns true while a key is being pressed
        /// </summary>
        /// <param name="key">The ascii value of the key to check</param>
        /// <returns></returns>
        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }

        /// <summary>
        /// Returns true while if key was pressed once
        /// </summary>
        /// <param name="key">The ascii value of the key to check</param>
        /// <returns></returns>
        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        }

        public Engine()
        {
            _scenes = new Scene[0];
        }

        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            //Creates a new window for raylib
            Raylib.InitWindow(1200, 760, "Math For Games");
            Raylib.SetTargetFPS(60);

            //Set up console window
            Console.CursorVisible = false;
            Console.Title = "Math For Games";

            //Create a new scene for our actors to exist in
            Scene scene1 = new Scene();

            //Create the actors to add to our scene
            Player player = new Player(0, 0, 0.5f);
            Goal goal = new Goal(0, 0, 2, player);

            //Set player's starting stats
            player.Speed = 1;
            player.SetTranslate(new Vector2(5, 10));
            player.SetScale(3, 3);
            //player.AddChild(partner1);

            //partner1.SetTranslate(new Vector2(3, 0));

            goal.SetScale(3, 3);
            goal.SetTranslate(new Vector2(35, 10));

            /*wall.SetScale(3, 3);
            wall.SetTranslate(new Vector2(20, 10));*/

            //Add actors to the scenes
            scene1.AddActor(player);
            scene1.AddActor(goal);

            goal.AddCollisionTarget(player);

            //Sets the starting scene index and adds the scenes to the scenes array
            int startingSceneIndex = 0;
            startingSceneIndex = AddScene(scene1);

            //Sets the current scene to be the starting scene index
            SetCurrentScene(startingSceneIndex);

            Bullet.CreateBullets(100, player);
            /*Console.WriteLine("Choose a difficulty");
            Console.WriteLine("[1] Easy \n[2] Normal \n[3] Hard");
            char input = Console.ReadKey().KeyChar;

            switch (input)
            {
                case '1':
                    Bullet.CreateBullets(40, player);
                    break;

                case '2':
                    Bullet.CreateBullets(60, player);
                    break;

                case '3':
                    Bullet.CreateBullets(80, player);
                    break;
            }*/

        }

        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="deltaTime">The time between each frame</param>
        public void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            //Console.WriteLine(Raylib.GetMousePosition());
            GameManager.Counter();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }

        //Called when the game ends.
        public void End()
        {
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }

        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            //Call start for all objects in game
            Start();

            //Loops the game until either the game is set to be over or the window closes
            while (!GameManager.Gameover && !Raylib.WindowShouldClose())
            {
                //Stores the current time between frames
                float deltaTime = Raylib.GetFrameTime();
                //Call update for all objects in game
                Update(deltaTime);
                //Call draw for all objects in game
                Draw();
                //Clear the input stream for the console window
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }

            End();
        }

    }
}