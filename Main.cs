using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public class Input
    {
        private static readonly Keys[] konamiCode =
        {
            Keys.Up,
            Keys.Up,
            Keys.Down,
            Keys.Down,
            Keys.Left,
            Keys.Right,
            Keys.Left,
            Keys.Right,
            Keys.B,
            Keys.A,
            Keys.Enter
        };
        private static Keys[] konamiCodeDetected = new Keys[11];
        private static int konamiCodeIndex = 0;
        private static bool konamiCodeActivated = false;
        public static bool KonamiCodeActivated()
        {
            return konamiCodeActivated;
        }
        public static void DeactivateKonamiCode()
        {
            konamiCodeActivated = false;
        }
        private static void ScanForKonamiCode()
        {
            Console.WriteLine(konamiCodeIndex);


            var pressedKeys = currentKeyboardState.GetPressedKeys();
            if (pressedKeys.Length == 0)
            {
                return;
            }

            var nextKey = pressedKeys[0];
            
            if (IsKeyPressed(konamiCode[konamiCodeIndex]))
            {
                konamiCodeIndex++;
                if (konamiCodeIndex >= konamiCode.Length)
                {
                    konamiCodeActivated = true;
                    konamiCodeIndex = 0;
                }
            }
            else if (!previousKeyboardState.IsKeyDown(nextKey))
            {
                konamiCodeIndex = 0;
            }


        }

        







        public static KeyboardState currentKeyboardState;
        public static KeyboardState previousKeyboardState;

        public static bool IsAnyKeyPressed()
        {
            return currentKeyboardState.GetPressedKeys().Length != 0;
        }
        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }


        public static bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }











        public static void UpdateInput()
        {
            
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            ScanForKonamiCode();
            //Diagnostics.Debug.WriteLine("Hello world");
        }
    }




    public class Cheats
    {
        private void KillEveryone()
        {
            List<Object> objects = new List<Object>();
            foreach (var character in objects.OfType<Character>())
            {
                character.Health = 0;
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















    public class Character : Object
    {
        public float Health { get { return health; } set { health = value; } }
        private float health = 100.0f;


        public void Die()
        {
           
        }
    }
    public class Player : Character
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


        public override void Update(GameTime gameTime)
        {
            if (Hidden)
            {
                return;
            }

        }





    }
    public class Object
    { 

        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public bool Hidden { get { return hidden; } set { hidden = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }



        public Color Color { get { return color; } set { color = value; } }
        public byte Opacity { get { return color.A; } set { color.A = value; } }


        private bool hidden = false;
        private Texture2D texture = null;
        private Vector2 position;
        private Color color = Color.White;


        public virtual void Update(GameTime gameTime) {}
        public void SetOpacity(float opacity)
        {
            SetOpacity((byte)(opacity * 255));
        }
        public void SetOpacity(int opacity)
        {
            SetOpacity((byte)255);
        }
        public void SetOpacity(byte opacity)
        {
            this.Opacity = opacity;
        }

        public void Destroy()
        {

        }

    }
    public class Main : Game
    {
        private static GraphicsDeviceManager _graphics;
        private static SpriteBatch _spriteBatch;


        private List<Object> objects = new List<Object>();
        private List<Object> objectsToRemove = new List<Object>();

        public void Destroy(Object obj)
        {
            objectsToRemove.Add(obj);
        }
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Input.KonamiCodeActivated())
                Exit();
            Input.UpdateInput();
            // TODO: Add your update logic here
            

            foreach (var obj in objectsToRemove)
            {
                objects.Remove(obj);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MonoGameOrange);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
