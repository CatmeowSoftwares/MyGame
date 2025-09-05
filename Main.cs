using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame
{
    namespace Systenn
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









    public class HelloWorld
    {
        public static void main(String[] args)
        {
            Systenn.Out.println("Hello world!");
        }
    }






    public class Boolean
    {

        public static bool True =>
            true && !false && true == true && true != false && false == false && false != true &&
            (true || false) && !(false || false) && !(false && false) && (true && true) && !(false != false);





        public static bool False =>
            false && false != false;
    }





    public class Cheats
    {
        private static bool canCheat = false;
        private static bool cheating = false;
        private static bool immune = false;
        private static Player _player;
        public static void EnableCheats(Player player)
        {
            canCheat = true;
            cheating = true;
            _player = player;
        }
        public static void DisableCheats()
        {
            _player = null;
        }

        private static T GetItem<T>()
        {
            return default(T);
        }
        private static Item GetItem(string name)
        {
            Type type = Type.GetType(name);
            Item item = (Item)Activator.CreateInstance(type);
            return item;
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


    public class Animation
    {
        public int frame;
    }

    public class Player : Character
    {
        public string Name { get { return name; } set { name = value; } }
        private string name = "";




        private Item[] items = new Item[16];
        


        public Item[] Items { get { return items; }}
        private Item currentItemInCursor = null;


        public override void Update(GameTime gameTime)
        {
            if (currentItemInCursor != null)
            {
                if (currentItemInCursor is Item)
                {
                    var mouse = Mouse.GetState();
                    currentItemInCursor.Position = new Vector2(mouse.X, mouse.Y);
                }
            }
        }


        public void Move()
        {
            if (Input.IsKeyDown(Keys.A))
            {
                Vector2 vel = Velocity;
                vel.X = -100;
                Velocity = vel;
            }
            else if(Input.IsKeyDown(Keys.D))
            {
                Vector2 vel = Velocity;
                vel.X = 100;
                Velocity = vel;
            }

            if (Input.IsKeyPressed(Keys.Space))
            {
                Vector2 vel = Velocity;
                vel.Y = 400;
                Velocity = vel;
            }
        }
        public void SetName(string name)
        {
            if (name == "Catmeow" || name == "Catmeow123" || name == "catmeow" || name == "catmeow123")
            {
                Cheats.EnableCheats(this);
            }


            this.name = name;
        }









  

    }

    public class Item : Object 
    {

    }
    public class Button : Object
    {
        public bool Hovering { get { return hovering; } set { hovering = value; } }
        public bool Pressed { get { return pressed; } set { pressed = value; } }
        public bool Down { get { return down; } set { down = value; } }
        private bool hovering = false;
        private bool pressed = false;
        private bool down = false;

        private Rectangle rectangle = Rectangle.Empty;

        public Button(int x, int y, int w, int h, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = w;
            rectangle.Height = h;
            
        }
        public Button(int x, int y, int w, int h, Texture2D texture)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = w;
            rectangle.Height = h;
            this.Texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (Hidden)
            {
                return;
            }
            if (rectangle.Contains(Mouse.GetState().Position))
            {
                hovering = true;
                pressed = !down;
                down = Mouse.GetState().LeftButton == ButtonState.Pressed;
            }
            else
            {
                hovering = false;
                down = false;
                pressed = false;
            }
        }





    }
    public class Object
    { 

        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public bool Hidden { get { return hidden; } set { hidden = value; } }




        public Color Color { get { return color; } set { color = value; } }
        public byte Opacity { get { return color.A; } set { color.A = value; } }


        private bool hidden = false;
        private Texture2D texture = null;
        private Color color = Color.White;

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        private Vector2 velocity = Vector2.Zero;


        public Vector2 Position { get { return position; } set { position = value; } }
        private Vector2 position = Vector2.Zero;
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




    public static class AssetManager
    {
        private static Dictionary<String, Texture2D> textures = new Dictionary<string, Texture2D>();
        //private static Dictionary<String, Song> songs = new Dictionary<string, Song>();

        public static Texture2D GetTexture(string name)
        {
            textures.TryGetValue(name, out var texture);
            return texture;
        }

        public static void LoadTextures(ContentManager contentManager, string path = "Content")
        {
            /*
             
             
             foreach (file in files.readFiles())
            {
                if (file == folder)
                {
                    string newFolder = folder + file.name();
                    LoadTextures(contentManager, newFolder);
                }
                if (file == png || file == xnb)
                {
                    Texture2D texture = contentManager.Load(folder + file.GetName);
                    textures.Add(file.GetFileName().RemoveLast4Chars(), texture);
                }
            }
             
             
             
             
             */
            string newPath = path;
            foreach (var file in System.IO.Directory.GetFiles(path))
            {

                if (file.Substring(file.Length - 4) == ".xnb")
                {
                    Texture2D texture = contentManager.Load<Texture2D>(file);
                    textures.Add(file.Remove(file.Length - 4), texture);
                }

            }

            foreach (var file in System.IO.Directory.GetDirectories(newPath))
            {
                LoadTextures(contentManager, newPath);
              

            }



        }
    }
















    public class Main : Game
    {
        private static GraphicsDeviceManager _graphics;
        private static SpriteBatch _spriteBatch;

        private static ContentManager _contentManager;

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
            AssetManager.LoadTextures(_contentManager);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Console.WriteLine(AssetManager.GetTexture("Screenshot_1"));
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Input.UpdateInput();
            // TODO: Add your update logic here
            

            foreach (var obj in objectsToRemove)
            {
                obj.Destroy();
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
