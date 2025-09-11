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

        







        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;

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
        private static Player _player;
        public static void EnableCheats(Player player)
        {
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







    public class PhysicsObject : GameObject
    {

        public bool PhysicsDisabled { get { return physicsDisabled; } set { physicsDisabled = value; } }
        private bool physicsDisabled = false;

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        private Vector2 velocity = Vector2.Zero;

        public bool AffectedByGravity { get { return affectedByGravity; } set { affectedByGravity = value; } }
        private bool affectedByGravity = true;



        public bool OnFloor { get { return onFloor; } set { onFloor = value; } }
        private bool onFloor = false;







    }




    public class Tile
    {
        public int Width {get {return width;}}
        public int Height {get {return height;}}
        private int width = 8;
        private int height = 8;
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



        private static int frame;
        private static float elapsed;
        private static int fps = 10;
        private static float durationPerFrame = 100.0f;

        public static void UpdateAnimations(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsed >= durationPerFrame || elapsed/60 <= fps)
            {
                ++frame;
            }

        }
    }

    public class Player : Character
    {
        public string Name { get { return name; } set { name = value; } }
        private string name = "";

        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } } 
        private float moveSpeed = 250.0f;

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
    public class UIObject : Object
    {


        
        public int Width { get { return rectangle.Width;} set { rectangle.Width = value; }}
        public int Height { get { return rectangle.Height; } set { rectangle.Height = value; } }


        public Rectangle Rectangle { get { return rectangle;} set { rectangle = value;}}
        private Rectangle rectangle = Rectangle.Empty;

    }
    public class Button : UIObject
    {


        public bool Hovering { get { return hovering; } set { hovering = value; } }
        public bool Pressed { get { return pressed; } set { pressed = value; } }
        public bool Down { get { return down; } set { down = value; } }
        private bool hovering = false;
        private bool pressed = false;
        private bool down = false;

        public Button()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Hidden)
            {
                return;
            }

            Rectangle rectangle = this.Rectangle;

            hovering = false;
            down = false;
            pressed = false;

            if (rectangle.Contains(Mouse.GetState().Position))
            {
                hovering = true;
                pressed = !down;
                down = Mouse.GetState().LeftButton == ButtonState.Pressed;
            }

        }





    }


    public class GameObject : Object
    {

    }

    public class Object
    {

        public Texture2D Texture
        {
            get
            {

                return texture;
            }
            set
            {

                if (value == null)
                {
                    throw new System.Exception("Failed to set Texture");
                }

                texture = value;
            }
        }





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



        public Rectangle Collider { get { return collider; } set { collider = value; } }
        private Rectangle collider;








        public virtual void Update(GameTime gameTime) { }
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
            if (texture == null)
            {
                throw new System.Exception("Failed to get texture!");
            }
            return texture;
        }

        public static void LoadTextures(ContentManager contentManager, string path = "Content/bin/Assets/Textures")
        {
            string newPath = path;
            string[] paths = System.IO.Directory.GetFiles(path);
            string[] directories = System.IO.Directory.GetDirectories(path);
            foreach (var file in paths)
            {

                try
                {
                    var relativePath = Path.GetRelativePath(contentManager.RootDirectory, file);
                    var textureName = Path.GetFileNameWithoutExtension(file);
                    var texturePath = relativePath.Replace(".xnb", null);
                    Texture2D texture = contentManager.Load<Texture2D>(texturePath);
                    textures.Add(textureName, texture);
                    Console.WriteLine("Successfully loaded texture: " + textureName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to load texture");
                    Console.WriteLine(e);
                    
                }

            }


        }




        public static void LoadAudio(ContentManager contentManager, string path = "Content/Assets/Audio")
        {
            return;
            string newPath = path;
            string[] paths = System.IO.Directory.GetFiles(path);
            string[] directories = System.IO.Directory.GetDirectories(path);
            foreach (var file in paths)
            {

                try
                {
                    var relativePath = Path.GetRelativePath(contentManager.RootDirectory, file);
                    var audioName = Path.GetFileNameWithoutExtension(file);
                    var audioPath = relativePath.Replace(".ogg", null);
                    Texture2D texture = contentManager.Load<Texture2D>(audioPath);
                    textures.Add(audioName, texture);
                    Console.WriteLine("Successfully loaded audio: " + audioName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to load audio");
                    Console.WriteLine(e);
                    
                }
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

        private static Player player;
        private Camera mainCamera;

        private Object jellyshocker;
        private Vector2 cameraPos = Vector2.Zero;


        private static Texture2D colliderPixel;

        private Button button;




        private Texture2D CreateRectangleTexture(int w, int h)
        {
            Color[] colors = new Color[w * h];
            for (int i = 0; i < w * h; ++i)
            {
                colors[i] = new Color(255, 0, 255);
            }
            Texture2D texture = new Texture2D(GraphicsDevice, w, h);
            texture.SetData(colors);
            return texture;
        }


        public void Destroy(Object obj)
        {
            objectsToRemove.Add(obj);
        }
        private void DrawUI(SpriteBatch spriteBatch)
        {
            foreach (var obj in objects.OfType<UIObject>())
            {
                if (obj.Hidden || obj.Texture == null) { continue; }

                spriteBatch.Draw(
                    obj.Texture,
                    obj.Position,
                    obj.SourceRectangle == Rectangle.Empty ? null : obj.SourceRectangle,
                    obj.Color,
                    obj.Rotation,
                    obj.Origin,
                    obj.Scale,
                    obj.Effects,
                    obj.LayerDepth
                    );
            }
        }
        private void DrawGameObjects(SpriteBatch spriteBatch)
        {
             foreach (Object obj in objects.OfType<GameObject>())
            {
                if (obj.Hidden || obj.Texture == null) { continue; }


                spriteBatch.Draw(
                    obj.Texture,
                    obj.Position,
                    obj.SourceRectangle == Rectangle.Empty ? null : obj.SourceRectangle,
                    obj.Color,
                    obj.Rotation,
                    obj.Origin,
                    obj.Scale,
                    obj.Effects,
                    obj.LayerDepth
                    );


            }
        }

        private void DrawColliders(SpriteBatch spriteBatch)
        {
            foreach (var obj in objects)
            {
                var collider = obj.Collider;
                if (collider == Rectangle.Empty) { continue; }

                spriteBatch.Draw(colliderPixel, collider, new Color(255, 0, 0, 64));
            }
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
            button = new Button();
            button.Position = new Vector2(100.0f, 100.0f);
            button.Rectangle = new Rectangle(100, 100, 100, 100);
            colliderPixel = new Texture2D(GraphicsDevice, 1, 1);
            colliderPixel.SetData(new Color[] { Color.White });
            player = new Player();
            jellyshocker = new Object();
            jellyshocker.Position = new Vector2(-100, -200);
            _contentManager = new ContentManager(Services, Content.RootDirectory);
            // TODO: Add your initialization logic here
            mainCamera = new Camera(_graphics);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetManager.LoadTextures(_contentManager);
            try
            {
                player.Texture = AssetManager.GetTexture("Player");
                jellyshocker.Texture = AssetManager.GetTexture("jellyshocker");
                Console.WriteLine("Setting textures successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            jellyshocker.Opacity = 1;
            jellyshocker.Scale = new Vector2(0.125f);
            button.Texture = CreateRectangleTexture(button.Width, button.Height);
            objects.Add(player);
            //objects.Add(jellyshocker);
            objects.Add(button);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // process inputs -> process behaviors -> calculate velocities -> move entities -> resolve collisions -> render frame


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || button.Pressed)
                Exit();

            // process inputs
            Input.UpdateInput();
            

            foreach (var button in objects.OfType<Button>())
            {
                button.Update(gameTime);
            }








            // process behaviors







            // calculate velocities
            if (Input.IsKeyDown(Keys.A))
            {
                Vector2 velocity = player.Velocity;
                velocity.X = (float)(player.MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                player.Velocity = velocity;
            }
            else if (Input.IsKeyDown(Keys.D))
            {
                Vector2 velocity = player.Velocity;
                velocity.X = (float)(player.MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                player.Velocity = velocity;
            }
            else
            {
                Vector2 velocity = player.Velocity;
                velocity.X = 0;
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
                if (!obj.PhysicsDisabled)
                {
                    if (obj.AffectedByGravity)
                    {
                        if (!obj.OnFloor)
                        {

                            //velocity.Y += (float)(9.81 * gameTime.ElapsedGameTime.TotalSeconds);
                        }
                        else
                        {
                            velocity.Y = 0;
                        }
                    }
                }

                obj.Velocity = velocity;
                obj.Position += obj.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }




            // resolve collision


            foreach (var objectA in objects.OfType<PhysicsObject>())
            {
                foreach (var objectB in objects.OfType<PhysicsObject>())
                {
                    if (objectA == objectB) { continue; }

                    Rectangle a = objectA.Collider;
                    Rectangle b = objectB.Collider;

                    if (a.Intersects(b))
                    {
                        Rectangle intersect = Rectangle.Intersect(a, b);
                        a.X -= intersect.Width / 2;
                        a.Y -= intersect.Height / 2;
                        b.X += intersect.Width / 2;
                        b.Y += intersect.Height / 2;

                    }

                    else if (b.Intersects(a))
                    {
                        Rectangle intersect = Rectangle.Intersect(b, a);
                        a.X += intersect.Width / 2;
                        a.Y += intersect.Height / 2;
                        b.X -= intersect.Width / 2;
                        b.Y -= intersect.Height / 2;

                    }



                }
            }


            foreach (var obj in objects)
            {
                Rectangle rect = obj.Collider;
                Vector2 pos = obj.Position;

                rect.X = (int)pos.X;
                rect.Y = (int)pos.Y;

                obj.Collider = rect;
                obj.Position = pos;
            }


            // TODO: Add your update logic here
            cameraPos = player.Position;
            mainCamera.MoveToward(cameraPos, (float)gameTime.ElapsedGameTime.TotalMilliseconds, 0.2f);

            foreach (var obj in objectsToRemove)
            {
                obj.Destroy();
                objects.Remove(obj);
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            Matrix transform = Matrix.CreateTranslation(-mainCamera.GetTopLeft().X, -mainCamera.GetTopLeft().Y, 0);
            _spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: transform);
            DrawGameObjects(_spriteBatch);
            //DrawColliders(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End();
            _spriteBatch.Begin();
            DrawUI(_spriteBatch);
            _spriteBatch.End();

        }










        
    }
}
