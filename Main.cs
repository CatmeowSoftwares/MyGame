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


        private static List<Animation> animations = new List<Animation>();
        private int frame;
        private int maxFrames;
        private float elapsed;
        private int fps = 10;
        private float durationPerFrame = 100.0f;

        private int[] transition_linear = { 1, 2, 3, 4, 5, 6, 7, 8 };

        private static void Initialize()
        {

        }
        public static void UpdateAnimations(GameTime gameTime)
        {
            for (int i = 0; i < animations.Count; ++i)
            {
                animations[i].UpdateAnimation(gameTime);
            }
            

        }
        private void UpdateAnimation(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed < durationPerFrame || elapsed / 60 > fps)
                return;
            ++frame;
            elapsed = 0.0f;
            if (this.frame < maxFrames)
                return;
            frame = 0;
        }
    }

    public class Player : Character
    {
        public string Name 
        {
            get 
            { 
                return name; 
            } 
            set 
            { 
                if (value == "Catmeow" || value == "Catmeow123" || value == "catmeow" || value == "catmeow123")
                {
                    Cheats.EnableCheats(this);
                }
                name = value; 
            } 
        }
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

        public override void Destroy(ref List<Object> objects)
        {

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
            


            this.name = name;
        }









  

    }

    public class Item : Object 
    {

    }
    public class UIObject : Object
    {


        
        public int Width 
        {
            get 
            {
                Rectangle rect = Rectangle;
                return rect.Width;
            } 
            set 
            { 
                Rectangle rect = Rectangle;
                rect.Width = value; 
                Rectangle = rect;
            }
        }
        public int Height 
        { 
            get 
            { 
                Rectangle rect = Rectangle;
                return rect.Height;
            } 
            set 
            { 
                Rectangle rect = Rectangle;
                rect.Height = value; 
                Rectangle = rect;
            } 
        }


        




    }


    public class ProgressBar : UIObject
    {
        public float Min { get { return min; } set { min = value; } }
        public float Max { get { return max; } set { max = value; } }
        public float Value { get { return current_value; } set { current_value = value; } }

        public int Percent { get { return percent; } set { percent = value; } }

        private float min = 0.0f;
        private float max = 100.0f;
        private float current_value = 0.0f;
        private int percent;

        private float w;
        private float h;

        public ProgressBar(Texture2D texture,float width = 100.0f, float height = 16.0f, float min = 0.0f, float max = 100.0f)
        {
            Texture = texture;
            this.min = min;
            this.max = max;
            w = width;
            h = height;
        }
        public override void Update(GameTime gameTime)
        {
            float clmap = Math.Clamp(Value, Min, Max);
            float nromalize = (clmap - Min) / (Max - Min);
            percent = (int)nromalize * 100;
            Scale = new Vector2(nromalize * w, h);
            base.Update(gameTime);
        }

        public int ToPercent()
        {
            return percent;
        }
    }
    public class CombatText
    {
        private static List<CombatText> combatTexts = new List<CombatText>();
        private string text = "";
        private Vector2 scale = Vector2.One;
        private Color color;
        private Vector2 position;
        enum Type
        {
            NONE,
            
        }
        public static void CreateText(string text, Color? color = null, Vector2? scale = null)
        {
            CombatText cText = new CombatText(text, color, scale);
            combatTexts.Add(cText);
        }
        public static void CreateText(string text, Color? color = null, float? scale = null)
        {
            Vector2 scl = new Vector2(scale ?? 1.0f);
            CreateText(text, color, scl);
        }
        private CombatText(string text, Color? color = null, Vector2? scale = null)
        {
            this.text = text;
            this.color = color ?? Color.White;
            this.scale = scale ?? Vector2.One;
        }

         
        public static void DrawTexts(SpriteBatch spriteBatch,SpriteFont spriteFont ,GameTime gameTime)
        {
            for (int i = 0; i < combatTexts.Count; ++i)
            {
                CombatText combatText = combatTexts[i];
                spriteBatch.DrawString(spriteFont, combatText.text, combatText.position, combatText.color);
            }
        }
    }
    public class Button : UIObject
    {


        public bool Hovering { get { return hovering; } set { hovering = value; } }
        public bool Pressed { get { return pressed; } set { pressed = value; } }
        public bool Down { get { return down; } set { down = value; } }
        public bool Disabled { get { return disabled; } set { disabled = value; } }
        private bool hovering = false;
        private bool pressed = false;
        private bool down = false;
        private bool disabled = false;

        private bool wasDown = false;
        public Button()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Hidden || Disabled)
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
                down = Mouse.GetState().LeftButton == ButtonState.Pressed;
                pressed = down && !wasDown;
                wasDown = down;


                





            }

            if (hovering)
            {
                Mouse.SetCursor(MouseCursor.Hand);
            }
            else if (pressed)
            {
                //Mouse.SetCursor(MouseCursor.Handle);
            }
            else if (down)
            {
                //Mouse.SetCursor(MouseCursor.Handle);
            }
            else
            {
                Mouse.SetCursor(MouseCursor.Arrow);
            }

        }

        public override string ToString()
        {
            return "Hovering: " + hovering.ToString() + "Pressed: " + pressed.ToString() + "Down: " + down.ToString() + "Disabled: " + disabled.ToString(); 
        }



    }
    
    public class World
    {
        private List<Tile> tiles = new List<Tile>();
        




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
        public Rectangle? SourceRectangle { get { return sourceRectangle; } set { sourceRectangle = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public float Rotation { get { return rotation; } set { rotation = value; } }
        public Vector2 Origin { get { return origin; } set { origin = value; } }
        public Vector2 Scale { get { return scale; } set { scale = value; } }
        public SpriteEffects Effects { get { return effects; } set { effects = value; } }
        public float LayerDepth { get { return layerDepth; } set { layerDepth = value; } }
        public bool Hidden { get { return hidden; } set { hidden = value; } }
        public byte Opacity { get { return color.A; } set { color.A = value; } }
        public Rectangle Rectangle { get { return rectangle;} set { rectangle = value;}}
        private Rectangle rectangle = Rectangle.Empty;
        private bool hidden = false;
        private Texture2D texture = null;
        private Vector2 position = Vector2.Zero;
        private Rectangle? sourceRectangle = null;
        private Color color = Color.White;
        private float rotation = 0.0f;
        private Vector2 origin = Vector2.Zero;
        private Vector2 scale = Vector2.One;
        private SpriteEffects effects = SpriteEffects.None;
        private float layerDepth = 0.0f;
        public virtual void Update(GameTime gameTime) { }
        public virtual void Destroy(ref List<Object> objects)
        {

        }
        public void Resize(int px, int py)
        {
            Rectangle rect = this.Rectangle;
            rect.X += px;
            rect.Y += py;
            rect.Width -= px;
            rect.Height -= py;

            this.Rectangle = rect;
        }
    }
    public static class AssetManager
    {
        private static Dictionary<String, Texture2D> textures = new Dictionary<string, Texture2D>();
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
        public static void LoadTextures(ContentManager contentManager, string path = "Content/Assets/Textures")
        {
            string[] paths = Array.Empty<string>();
            string[] directories = Array.Empty<string>();
            try
            {
                paths = System.IO.Directory.GetFiles(path);
                directories = System.IO.Directory.GetDirectories(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            foreach (var file in paths)
            {
                try
                {
                    var relativePath = Path.GetRelativePath(contentManager.RootDirectory, file);
                    var textureName = Path.GetFileNameWithoutExtension(file);
                    var texturePath = relativePath.Replace(".xnb", "");
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

        private List<Object> objects = new List<Object>();


        private List<Object> objectsToUpdate = new List<Object>();
        private List<Object> objectsToDraw = new List<Object>();
        private List<Object> objectsToRemove = new List<Object>();

        private static Player player;
        private static ProgressBar progressBar;
        private Camera mainCamera;

        private GameObject background;
        private Vector2 cameraPos = Vector2.Zero;


        private static Texture2D pixel;

        private Button button;

        private bool uiHidden = false;

        private SpriteFont spriteFont;
        

        public Main()
        {
            Console.WriteLine("Start of Main Construction");
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            _graphics.ApplyChanges();
            Console.WriteLine("End of Main Construction");
        }

        protected override void Initialize()
        {
            Console.WriteLine("Start of Initialization");
            button = new Button();
            button.Position = new Vector2(100.0f, 100.0f);
            button.Rectangle = new Rectangle(100, 100, 100, 100);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            player = new Player();
            progressBar = new ProgressBar(CreateRectangleTexture(1, 1));
            progressBar.Position = new Vector2(25, 25);
            background = new GameObject();
            background.Position = new Vector2(-100, -200);
            // TODO: Add your initialization logic here
            mainCamera = new Camera(_graphics);
            Console.WriteLine("End of Initialization");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("Start of Loading Content");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            try { spriteFont = this.Content.Load<SpriteFont>("File"); }
            catch (Exception e) { Console.WriteLine(e); }
            AssetManager.LoadTextures(Content);
            try
            {
                player.Texture = AssetManager.GetTexture("Player");
                background.Texture = AssetManager.GetTexture("background");
                Console.WriteLine("Setting textures successful!");
            }
            catch (Exception e) { Console.WriteLine(e); }
            player.LayerDepth = 0.01f;
            background.Color = new Color(128, 0, 128, 128);
            background.Scale = new Vector2(0.0325f);
            button.Texture = CreateRectangleTexture(button.Width, button.Height);
            objects.Add(player);
            objects.Add(background);
            objects.Add(button);
            objects.Add(progressBar);
            // TODO: use this.Content to load your game content here
            Console.WriteLine("End of Loading Content");
        }

        protected override void Update(GameTime gameTime)
        {
            if (!this.IsActive)
                return;
            objectsToDraw.Clear();
            // process inputs -> process behaviors -> calculate velocities -> move entities -> resolve collisions -> render frame

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // process inputs
            Input.UpdateInput();
            if (Input.IsKeyPressed(Keys.F11))
            {
                uiHidden = !uiHidden;
            }
            foreach (var ui in objects.OfType<UIObject>())
            {
                ui.Update(gameTime);            
            }
            // process behaviors
            // calculate velocities
            if (Input.IsKeyDown(Keys.A))
            {
                Vector2 velocity = player.Velocity;
                velocity.X = (float)(100 * gameTime.ElapsedGameTime.TotalSeconds);
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

            if (button.Pressed)
            {
                player.Position = Vector2.Zero;
                player.Velocity = Vector2.Zero;
            }
            // TODO: Add your update logic here
            progressBar.Value = Mouse.GetState().X;
            cameraPos = player.Position;
            mainCamera.MoveToward(cameraPos, (float)gameTime.ElapsedGameTime.TotalMilliseconds, 0.2f);
            objectsToDraw = objects.ToList();
            foreach (var obj in objects)
            {
                
                if (obj is PhysicsObject)
                {
                    PhysicsObject physicsObject = (PhysicsObject)obj;
                    // move entities
                    Vector2 velocity = physicsObject.Velocity;
                    if (!physicsObject.PhysicsDisabled)
                    {
                        if (physicsObject.AffectedByGravity)
                        {
                            if (!physicsObject.OnFloor)
                            {

                                velocity.Y += (float)((98.1 * 2) * gameTime.ElapsedGameTime.TotalSeconds);
                            }
                            else
                            {
                                velocity.Y = 0;
                            }
                        }
                    }

                    physicsObject.Velocity = velocity;
                    physicsObject.Position += physicsObject.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;


                    // resolve collision
                    foreach (var obj2 in objects.OfType<PhysicsObject>())
                    {
                        PhysicsObject obj1 = (PhysicsObject)obj;
                        if (obj1 == obj2) { continue; }

                        Rectangle a = obj1.Rectangle;
                        Rectangle b = obj2.Rectangle;
                        Vector2 aPos = obj1.Position;
                        Vector2 bPos = obj2.Position;

                        if (a.Intersects(b))
                        {
                            Rectangle intersect = Rectangle.Intersect(a, b);
                            aPos.X -= intersect.Width / 2;
                            aPos.Y -= intersect.Height / 2;
                            bPos.X += intersect.Width / 2;
                            bPos.Y += intersect.Height / 2;

                        }

                        else if (b.Intersects(a))
                        {
                            Rectangle intersect = Rectangle.Intersect(b, a);
                            aPos.X += intersect.Width / 2;
                            aPos.Y += intersect.Height / 2;
                            bPos.X -= intersect.Width / 2;
                            bPos.Y -= intersect.Height / 2;

                        }

                        obj1.Position = aPos;
                        obj2.Position = bPos;

                    }
                }

                Rectangle rect = obj.Rectangle;
                Vector2 pos = obj.Position;

                rect.X = (int)pos.X;
                rect.Y = (int)pos.Y;

                obj.Rectangle = rect;
                obj.Position = pos;


                if (obj is UIObject)
                {
                    continue;
                }
                
                if (
                        (obj.Position.X + obj.Texture.Width < mainCamera.GetTopLeft().X) ||
                        (obj.Position.Y + obj.Texture.Height < mainCamera.GetTopLeft().Y) ||
                        (obj.Position.X > mainCamera.GetTopLeft().X + _graphics.PreferredBackBufferWidth) ||
                        (obj.Position.Y > mainCamera.GetTopLeft().Y + _graphics.PreferredBackBufferHeight)
                        )
                {
                    objectsToDraw.Remove(obj);
                }
            }
            
            foreach (var obj in objectsToRemove.ToList())
            {
                obj.Destroy(ref objects);
                objects.Remove(obj);
                objectsToRemove.Remove(obj);
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
            
            _spriteBatch.End();

            if (!uiHidden)
            {
                _spriteBatch.Begin();
                DrawUI(_spriteBatch);
                _spriteBatch.DrawString(spriteFont, Mouse.GetState().X.ToString(), new Vector2(25, 25), Color.White);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }




        public Texture2D CreateRectangleTexture(int w, int h)
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



        private void DrawUI(SpriteBatch spriteBatch)
        {
            foreach (var obj in objectsToDraw.OfType<UIObject>())
            {
                if (obj.Hidden || obj.Texture == null) { continue; }

                spriteBatch.Draw(
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
        }
        private void DrawGameObjects(SpriteBatch spriteBatch)
        {
            foreach (Object obj in objectsToDraw.OfType<GameObject>())
            {
                if (obj.Hidden || obj.Texture == null) { continue; }


                spriteBatch.Draw(
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
        }

        private void DrawColliders(SpriteBatch spriteBatch)
        {
            foreach (var obj in objectsToDraw)
            {
                var collider = obj.Rectangle;
                if (collider == Rectangle.Empty) { continue; }

                spriteBatch.Draw(pixel, collider, new Color(255, 0, 0, 64));
            }
        }







    }
}
