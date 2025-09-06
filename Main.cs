using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
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



















    public class Camera
    {
        public Vector2 Center { get; set; }

        private Vector2 _quarterScreen;
        public Vector2 GetTopLeft() => Center - _quarterScreen;
        public Camera(GraphicsDeviceManager graphicsDeviceManager)
        {
            _quarterScreen = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth / 2, graphicsDeviceManager.PreferredBackBufferHeight / 2);
        }


        public Camera(Rectangle rectangle, Vector2 center)
        {
            this.Center = center;
            _quarterScreen = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
        }

        public void MoveToward(Vector2 target, float deltaTimeInMs, float movePercentage = 0.02f)
        {
            Vector2 differenceInPosition = target - Center;

            differenceInPosition *= movePercentage;

            var fractionOfPassedTime = deltaTimeInMs / 10;

            Center += differenceInPosition * fractionOfPassedTime;

            if ((target - Center).Length() < movePercentage)  
            {
                Center = target;
            }
        }



    }







    public class PhysicsObject : Object
    {


        private bool physicsDisabled = false;

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        private Vector2 velocity = Vector2.Zero;

        public bool AffectedByGravity { get { return affectedByGravity; } set { affectedByGravity = value; } }
        private bool affectedByGravity = true;



        public bool OnFloor { get { return onFloor; } set { onFloor = value; } }
        private bool onFloor = false;







    }



    public class Character : PhysicsObject
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
        public Vector2 Position { get { return position; } set { position = value; } }
        public Rectangle SourceRectangle { get { return sourceRectangle; } set { sourceRectangle = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public float Rotation { get { return rotation; } set { rotation = value; } }
        public Vector2 Origin { get { return origin; } set { origin = value; } }
        public Vector2 Scale { get { return scale; } set { scale = value; } }
        public SpriteEffects Effects { get { return effects; } set { effects = value; } }
        public float LayerDepth { get { return layerDepth; } set { layerDepth = value; } }
        public bool Hidden { get { return hidden; } set { hidden = value; } }





        public byte Opacity { get { return color.A; } set { color.A = value; } }


        private bool hidden = false;

        





        private Texture2D texture = null;
        private Vector2 position = Vector2.Zero;
        private Rectangle sourceRectangle = Rectangle.Empty;
        private Color color = Color.White;
        private float rotation = 0.0f;
        private Vector2 origin = Vector2.Zero;
        private Vector2 scale = Vector2.One;
        private SpriteEffects effects = SpriteEffects.None;
        private float layerDepth = 0.0f;













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


        public static Dictionary<String, Texture2D> GetTextures()
        {
            return textures;
        }


        public static Texture2D GetTexture(string name)
        {
            textures.TryGetValue(name, out var texture);
            return texture;
        }

        public static void LoadTextures(ref ContentManager contentManager, string path = "Content/Textures")
        {
            string newPath = path;
            string[] paths = System.IO.Directory.GetFiles(path);
            string[] directories = System.IO.Directory.GetDirectories(path);
            foreach (var file in paths)
            {

                try
                {
                    var relativePath = Path.GetRelativePath(contentManager.RootDirectory, file);
                    relativePath = relativePath.Replace("\\", "/");
                    var textureName = Path.GetFileNameWithoutExtension(file);
                    Texture2D texture = contentManager.Load<Texture2D>(relativePath.Replace(".xnb", ""));
                    textures.Add(textureName, texture);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            foreach (var file in directories)
            {
                if (file[0] == '_')
                {
                    continue;
                }
                LoadTextures(ref contentManager, newPath + "/" + file);
              

            }



        }




        public static void LoadAudio(ContentManager contentManager, string path = "Content/Assets/Audio")
        {

        }



    }
















    public class Main : Game
    {
        private static GraphicsDeviceManager _graphics;
        private static SpriteBatch _spriteBatch;

        private static ContentManager _contentManager;

        private List<Object> objects = new List<Object>();
        private List<Object> objectsToRemove = new List<Object>();

        private static Player player;
        private Camera mainCamera;

        private Object jellyshocker;
        private Vector2 cameraPos = Vector2.Zero;
        public void Destroy(Object obj)
        {
            objectsToRemove.Add(obj);
        }
        public Main()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            player = new Player();
            player.Texture = AssetManager.GetTexture("Player");
            jellyshocker = new Object();
            _contentManager = new ContentManager(Services, Content.RootDirectory);
            // TODO: Add your initialization logic here
            mainCamera = new Camera(_graphics);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetManager.LoadTextures(ref _contentManager);

            jellyshocker.Texture = AssetManager.GetTexture("jellyshocker");
            jellyshocker.Scale = new Vector2(0.25f, 0.25f);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            try
            {
                // process inputs -> process behaviors -> calculate velocities -> move entities -> resolve collisions -> render frame


                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // process inputs
                Input.UpdateInput();








                // process behaviors







                // calculate velocities
                if (Input.IsKeyDown(Keys.A))
                {
                    Vector2 velocity = player.Velocity;
                    velocity.X -= (float)(100 * gameTime.ElapsedGameTime.TotalSeconds);
                    player.Velocity = velocity;
                }
                else if (Input.IsKeyDown(Keys.D))
                {
                    Vector2 velocity = player.Velocity;
                    velocity.X += (float)(100 * gameTime.ElapsedGameTime.TotalSeconds);
                    player.Velocity = velocity;
                }
                if (Input.IsKeyPressed(Keys.Space))
                {
                    Vector2 velocity = player.Velocity;
                    velocity.Y = -400;
                    player.Velocity = velocity;
                }



                /*
                if (Input.IsKeyDown(Keys.W))
                {
                    cameraPos.Y -= (float)(100 * gameTime.ElapsedGameTime.TotalSeconds);
                }
                else if (Input.IsKeyDown(Keys.S))
                {
                    cameraPos.Y += (float)(100 * gameTime.ElapsedGameTime.TotalSeconds);
                }
                */



                // move entities



                foreach (var obj in objects.OfType<PhysicsObject>())
                {
                    Vector2 velocity = obj.Velocity;

                    if (obj.AffectedByGravity)
                    {
                        if (!obj.OnFloor)
                        {

                            velocity.Y -= (float)(9.81 * 100 * gameTime.ElapsedGameTime.TotalSeconds);
                        }
                        else
                        {
                            velocity.Y = 0;
                        }
                    }


                    obj.Velocity = velocity;
                    obj.Position += obj.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }




                // resolve collision








                // TODO: Add your update logic here
                cameraPos = player.Position;
                mainCamera.MoveToward(cameraPos, 1.0f);

                foreach (var obj in objectsToRemove)
                {
                    obj.Destroy();
                    objects.Remove(obj);
                }

                base.Update(gameTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MonoGameOrange);

            // TODO: Add your drawing code here
            Matrix transform = Matrix.CreateTranslation(-mainCamera.GetTopLeft().X, -mainCamera.GetTopLeft().Y, 0);

            _spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: transform);


            _spriteBatch.Draw(jellyshocker.Texture, Vector2.Zero, Color.White);

            foreach (Object obj in objects)
            {
                _spriteBatch.Draw(
                    obj.Texture,
                    obj.Position,
                    obj.SourceRectangle,
                    obj.Color,
                    obj.Rotation,
                    obj.Origin,
                    obj.Scale,
                    obj.Effects,
                    obj.LayerDepth
                    );
            }

            base.Draw(gameTime);
            _spriteBatch.End();



        }
    }
}
