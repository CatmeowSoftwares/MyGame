using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyGame
{
    namespace System
    {
        class Out
        {
            public static void println(String str)
            {
                Console.WriteLine(str);
            }
        }
    }

    public class HelloWorld
    {
        public static void main(String[] args)
        {
            System.Out.println("Hello world!");
        }
    }


    public class Player : Object
    {
        public static void Move()
        {

            switch (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                case true:
                    break;
            }
        }





    }
    public class Button : Object
    {
        public bool Hovering { get { return hovering; } set { hovering = value; } }
        private bool hovering = false;








    }
    public class Object
    { 

        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public bool Hidden { get { return hidden; } set { hidden = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }

        private bool hidden = false;
        private Texture2D texture = null;
        private Vector2 position;

    }
    public class Main : Game
    {
        private static GraphicsDeviceManager _graphics;
        private static SpriteBatch _spriteBatch;


       



        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
