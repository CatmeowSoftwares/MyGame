using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

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








    public class Container : Object
    {
        private Dictionary<Object, Vector2> objects = new Dictionary<Object, Vector2>();


        public void Add(Object obj, Vector2 offset)
        {
            Main.AddObject(obj);
            objects.Add(obj, offset);
        }

        public void Hide()
        {
            foreach (var obj in objects)
            {
                if (obj.Key is Button)
                {
                    Button button = (Button)obj.Key;
                    button.Disabled = true;
                }
                obj.Key.Hidden = true;
            }
        }
        public void Show()
        {
            foreach (var obj in objects)
            {
                if (obj.Key is Button)
                {
                    Button button = (Button)obj.Key;
                    button.Disabled = false;
                }
                obj.Key.Hidden = false;
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var _obj in objects)
            {
                var obj = _obj.Key;
                var offset = _obj.Value;
                obj.Position = Position + offset;
            }
        }

        public void Draw(SpriteBatch sprite)
        {
        }

        public override void Destroy(ref List<Object> objects)
        {
            this.objects.Clear();
            base.Destroy(ref objects);
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






    public class PhysicsObject : ColliderObject
    {

        public bool PhysicsDisabled { get { return physicsDisabled; } set { physicsDisabled = value; } }
        private bool physicsDisabled = false;

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        private Vector2 velocity = Vector2.Zero;

        public bool AffectedByGravity { get { return affectedByGravity; } set { affectedByGravity = value; } }
        private bool affectedByGravity = true;



        public bool OnFloor { get { return onFloor; } set { onFloor = value; } }
        private bool onFloor = false;

        public float TerminalVelocity {  get { return terminalVelocity; } set { terminalVelocity = value; } }
        private float terminalVelocity = 420.0f;





    }




    public class Tile
    {
        public int Width {get {return width;}}
        public int Height {get {return height;}}
        public Vector2 Size { get { return new Vector2(width, height); } }
        private int width = 8;
        private int height = 8;

        public bool Solid { get { return solid; } set { solid = value; } }
        private bool solid = false;


        enum TileTexture
        {
            NONE = 0,

        }
       
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

    public class ColliderObject : GameObject
    {
        public enum ObjectType
        {
            None = 0,
            Player = 1,
            Npc = 2,
            Enemy = 3,
            Projectile = 4,
            Item = 5,
        }
        public ObjectType Type { get { return type; } set { type = value; } }
        private ObjectType type = ObjectType.None;
    }

    public class Projectile : ColliderObject
    {

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




        private int direction = 1;
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } } 
        private float moveSpeed = 250.0f;

        private Item[] items = new Item[16];
        


        public Item[] Items { get { return items; }}
        public Item currentlyHeldItem = null;
        private Item currentItemInCursor = null;







        public bool UIHidden { get { return uiHidden; } set { uiHidden = value; } }
        private bool uiHidden = false;



        private GameObject rightHand;
        private Vector2 rightHandOffset = new Vector2(-2, 0);
        private Vector2 handPosition;

        enum PlayerState
        {
            None,
            Walking,
            Running,
            Jumping,
            Falling
        }

        private float LookAt(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }


        private PlayerState playerState = PlayerState.None;
        public override void Update(GameTime gameTime)
        {
            UpdateMusic(gameTime);
            if (currentItemInCursor != null)
            {
                if (currentItemInCursor is Item)
                {
                    var mouse = Mouse.GetState();
                    currentItemInCursor.Position = new Vector2(mouse.X, mouse.Y);
                }
            }

            rightHand.Position = Position + rightHandOffset;




            if (direction == 1)
            {
                Vector2 mousePos = Mouse.GetState().Position.ToVector2() + Main.camera.GetTopLeft();
                float angel = LookAt(mousePos - rightHand.Position);
                rightHand.Rotation = angel; 
            }
            else if (direction == -1)
            {

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




        public void DrawUI(SpriteBatch spriteBatch)
        {
            DrawMusicPlayer(spriteBatch);
        }
                                                                                                            



        private static Button musicPlayerExitButton;
        private static Texture2D musicPlayerPlayButtonTexture;
        private static Texture2D musicPlayerPauseButtonTexture;
        private static Button musicPlayerNextButton;
        private static Button musicPlayerPreviousButton;
        private static UIObject musicPlayerMusicIcon;
        private static UIObject musicPlayerPanel;
        private static Button musicPlayerMover;
        private static Button musicPlayerPlayPauseButton;
        private static bool musicPlayerPaused = true;
        private static bool musicPlayerHidden = true;
        private static SongCollection musics;
        private static Container musicPlayerContainer;


        private static Song currentSong;
        public void Initialize()
        {
            
            Console.WriteLine("Sucess!");
        }
        public void LoadContent(ContentManager content)
        {
            Console.WriteLine(rightHand + "<- null??");
            rightHand = new GameObject();
            rightHand.Texture = content.Load<Texture2D>("Assets/Textures/PlayerRightHand");

            rightHand.Origin = new Vector2(1, 1);
            handPosition = new Vector2(12, 1);

            Main.AddObject(rightHand);
        }
        public static void InitializeStatic()
        {                                                          
            InitializeMusicPlayer();
        }
        private static void InitializeMusicPlayer()
        {
            musicPlayerContainer = new Container();

            musicPlayerContainer.Position = new Vector2(200, 200);

            musicPlayerExitButton = new Button();
            musicPlayerNextButton = new Button();
            musicPlayerPreviousButton = new Button();
            musicPlayerMusicIcon = new UIObject();
            musicPlayerPanel = new UIObject();
            musicPlayerPlayPauseButton = new Button();
            musicPlayerMover = new Button();


            musicPlayerExitButton.Disabled = true;
            musicPlayerNextButton.Disabled = true;
            musicPlayerPreviousButton.Disabled = true;
            musicPlayerPlayPauseButton.Disabled = true;
            musicPlayerMover.Disabled = true;
        }

        private void UpdateMusic(GameTime gameTime)
        {
            if (Input.IsKeyPressed(Keys.F10)) 
            {
                if (musicPlayerHidden) 
                {
                    musicPlayerContainer.Show();
                    musicPlayerHidden = false;
                }
                else 
                { 
                    musicPlayerContainer.Hide();
                    musicPlayerHidden = true;
                }
            }
            if (musicPlayerHidden) { return; }
            musicPlayerContainer.Update(gameTime);


            if (MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Stopped)
            {
                musicPlayerPlayPauseButton.Texture = musicPlayerPaused ? musicPlayerPlayButtonTexture : musicPlayerPauseButtonTexture;
            }


            if (musicPlayerMover.Down)
            {
                musicPlayerPaused = MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Stopped;
                musicPlayerContainer.Position = Mouse.GetState().Position.ToVector2() + -(new Vector2(95, 31));
            }

            if (musicPlayerPreviousButton.Pressed)
            {
                try
                {
                    MediaPlayer.Play(currentSong);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }


            if (musicPlayerPlayPauseButton.Pressed)
            {
                if (MediaPlayer.State == MediaState.Stopped) 
                {
                    try
                    {
                        MediaPlayer.Play(currentSong);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else if (MediaPlayer.State == MediaState.Paused) { MediaPlayer.Resume(); }
                else if (MediaPlayer.State == MediaState.Playing) { MediaPlayer.Pause(); }
                musicPlayerPaused = MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Stopped;
                musicPlayerPlayPauseButton.Texture = musicPlayerPaused ? musicPlayerPlayButtonTexture : musicPlayerPauseButtonTexture;
            }


            if (musicPlayerExitButton.Pressed)
            {
                musicPlayerContainer.Hide();
                musicPlayerHidden = true;

            }
        }
        public static void LoadContentStatic(ContentManager content)
        {
            currentSong = content.Load<Song>("Assets/ForestDayMorning");
        }
        public static void LoadTextures()
        {
            LoadMusicPlayerTextures();
        }

        
        private static void LoadMusicPlayerTextures()
        {
            try
            {
                musicPlayerMusicIcon.Texture = AssetManager.GetTexture("MusicPlayerMusicIcon");
                musicPlayerExitButton.Texture = AssetManager.GetTexture("MusicPlayerExitButton");
                musicPlayerPlayButtonTexture = AssetManager.GetTexture("MusicPlayerPlayButton");
                musicPlayerPauseButtonTexture = AssetManager.GetTexture("MusicPlayerPauseButton");
                musicPlayerNextButton.Texture = AssetManager.GetTexture("MusicPlayerNextButton");
                musicPlayerPreviousButton.Texture = AssetManager.GetTexture("MusicPlayerPreviousButton");
                musicPlayerPanel.Texture = AssetManager.GetTexture("MusicPlayerPanel");
                musicPlayerPlayPauseButton.Texture = AssetManager.GetTexture("MusicPlayerPlayButton");

                musicPlayerMover.Texture = AssetManager.GetTexture("MusicPlayerMover");
                musicPlayerMover.Rectangle = new Rectangle(Point.Zero, musicPlayerMover.Texture.Bounds.Size);
                musicPlayerExitButton.Rectangle = new Rectangle(Point.Zero, musicPlayerExitButton.Texture.Bounds.Size);
                musicPlayerPlayPauseButton.Rectangle = new Rectangle(Point.Zero, musicPlayerPlayPauseButton.Texture.Bounds.Size);
                musicPlayerNextButton.Rectangle = new Rectangle(Point.Zero, musicPlayerNextButton.Texture.Bounds.Size);
                musicPlayerPreviousButton.Rectangle = new Rectangle(Point.Zero, musicPlayerPreviousButton.Texture.Bounds.Size);
            }
            catch (Exception e) { Console.WriteLine(e); }





            //musicPlayerContainer.Add(musicPlayerMusicIcon, new Vector2(2, 2));
            musicPlayerContainer.Add(musicPlayerPanel, new Vector2(0, 0));
            musicPlayerContainer.Add(musicPlayerExitButton, new Vector2(90, 2));
            musicPlayerContainer.Add(musicPlayerPlayPauseButton, new Vector2(55, 17));
            musicPlayerContainer.Add(musicPlayerPreviousButton, new Vector2(43, 19));
            musicPlayerContainer.Add(musicPlayerNextButton, new Vector2(71, 19));
            musicPlayerContainer.Add(musicPlayerMusicIcon, new Vector2(2, 2));

            musicPlayerContainer.Add(musicPlayerMover, new Vector2(90, 26));

            musicPlayerContainer.Hide();


            

        }

        public static void DrawMusicPlayer(SpriteBatch spriteBatch)
        {
            if (musicPlayerHidden) { return; }
            musicPlayerContainer.Draw(spriteBatch);   

        }


        private static void ReadFiles(string path)
        {
            musics.Clear();
            string musicDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            foreach (string music in Directory.GetFiles(musicDirectory))
            {
                try
                {
                    Song song = Song.FromUri(Path.GetFileNameWithoutExtension(music), new Uri(music));
                    musics.Add(song);
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
        }



    }

    public class Item : Object 
    {
        public Vector2 HandPos
        {
            get
            {
                return handPos;
            }
            set 
            {
                handPos = value;
            }
        }
        private Vector2 handPos;





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

        public ProgressBar(Texture2D texture,float width = 100.0f, float height = 8.0f, float min = 0.0f, float max = 100.0f)
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
        public bool Disabled 
        { 
            get 
            { 
                return disabled; 
            } 
            set 
            { 
                disabled = value;
                if (disabled)
                {
                    hovering = false;
                    pressed = false;
                    down = false;
                    wasDown = false;
                }
            } 
        }

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

            if (rectangle.Contains(Mouse.GetState().Position))
            {
                hovering = true;
                down = Mouse.GetState().LeftButton == ButtonState.Pressed;
                pressed = down && !wasDown;
                wasDown = down;

                /*
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
                */




            }
            else
            {
                hovering = false;
                down = false;
                pressed = false;
                //Mouse.SetCursor(MouseCursor.Arrow);
            }

            

        }

        public override string ToString()
        {
            return "Hovering: " + hovering.ToString() + "Pressed: " + pressed.ToString() + "Down: " + down.ToString() + "Disabled: " + disabled.ToString(); 
        }



    }


    public class QuadTree<T>
    {
        public struct Node
        {
            public static Queue<Node> nodePool = new Queue<Node>();

            public Rectangle rectangle = Rectangle.Empty;
            public int lvl;


            List<Node> children = new List<Node>();
            List<T> objects = new List<T>();


            public Node(int x, int y, int width, int height, int level)
            {
                rectangle = new Rectangle(x, y, width, height);
                lvl = level;
            }
            public Node(Rectangle rect, int level)
            {
                rectangle = rect;
                lvl = level;
            }


            public void Add(T obj)
            {
                if (objects.Count < 4 || lvl == Int32.MaxValue) { objects.Add(obj); }
                else
                {
                    if (children.Count == 0)
                    {
                        Node n1 = nodePool.First();
                        nodePool.Dequeue();
                        Node n2 = nodePool.First();
                        nodePool.Dequeue();
                        Node n3 = nodePool.First();
                        nodePool.Dequeue();
                        Node n4 = nodePool.First();
                        nodePool.Dequeue();


                        int x = rectangle.X;
                        int y = rectangle.Y;
                        int w = rectangle.Width;
                        int h = rectangle.Height;
                        n1.Set(x, y, w / 2, h / 2, lvl + 1);
                        children.Add(n1);
                        n2.Set(x + w / 2, y, w / 2, h / 2, lvl + 1);
                        children.Add(n2);
                        n3.Set(x, y + h, w / 2, h / 2, lvl + 1);
                        children.Add(n3);
                        n4.Set(x + w / 2, y + h / 2, w / 2, h / 2, lvl + 1);
                        children.Add(n4);


                        foreach (var o/*bject*/ in objects)
                        {
                            foreach (var n /*ode*/ in children)
                            {
                                if (n.Inside(o))
                                {
                                    n.Add(o);
                                }
                            }
                        }


                        foreach (var n /*ode*/ in children)
                        {
                            if (n.Inside(obj))
                            {
                                n.Add(obj);
                            }
                        }




                    }
                }
            }

            private bool Inside(T obj)
            {
                Rectangle rectA = this.rectangle;
                Rectangle rectB = (obj as Object).Rectangle;

                if (rectA.Intersects(rectB)) { return true; }
                return false;
            }

            private void Set(int x, int y, int w, int h, int level)
            {
                Set(new Rectangle(x, y, w, h), level);
            }
            private void Set(Rectangle rectangle, int level)
            {
                foreach (var child in children)
                {
                    nodePool.Enqueue(child);
                }
                children.Clear();
                objects.Clear();
                this.rectangle = rectangle;
                this.lvl = level;
            }

            public void ConstructQuadTree(Node root)
            {
                root.Set(0, 0, int.MaxValue, int.MaxValue, 0);
                for (int i = 0; i < 100; ++i)
                {
                    T obj = default(T);
                    root.Add(obj);
                }
            }


        }



    }
    public class World
    {

        public static Tile[,] tiles = new Tile[69420, 69420]; 


        public static void CreateWorld()
        {
            for (int x = 0; x < World.tiles.GetLength(0); ++x)
            {
                for (int y = 0; y < World.tiles.GetLength(1); ++y)
                {
                    
                }
            }
        }

    }

    public class Collision
    {
        public static bool CheckTileCollision(Object obj)
        {
            Vector2 position = obj.Position;

            int width = obj.Rectangle.Width;
            int height = obj.Rectangle.Height;
            int TL = (int)(position.X / 8.0) - 1;
            int TR = (int)((position.X + width) / 8.0) + 2;
            int BL = (int)(position.Y / 8.0) - 1;
            int BR = (int)(position.Y + height / 8.0) + 2;


            for (int x = TL; x < TR; ++x)
            {
                for (int y = BL; y < BR; ++y)
                {
                    Tile tile = World.tiles[x, y];
                    if (tile == null) { continue; }
                    Vector2 vector2;
                    vector2.X = (float)(x * 8.0);
                    vector2.Y = (float)(y * 8.0);
                    if (position.X + width > vector2.X && 
                        position.X < vector2.X + 8.0 && 
                        position.Y + height > vector2.Y && 
                        position.Y < vector2.Y + 8.0)
                    {
                        if (tile.Solid)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
            
    }
    public class GameObject : Object
    {

        public GameObject() : base()
        {

        }
        public Animation Animation
        {
            get 
            {
                return animation;
            }
            set
            {
                animation = value;
            }
        }
        private Animation animation;
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
        public float Rotation 
        { 
            get 
            { 
                // no radians
                return MathHelper.ToDegrees(rotation); 
            } 
            set 
            {
                // i hate math, if you want radians, convert it yourself
                float degree = MathHelper.ToRadians(value);
                rotation = degree; 
                rotation = MathHelper.WrapAngle(rotation); 
            } 
        }
        public Vector2 Origin { get { return origin; } set { origin = value; } }
        public Vector2 Scale { get { return scale; } set { scale = value; } }
        public SpriteEffects Effects { get { return effects; } set { effects = value; } }
        public float LayerDepth { get { return layerDepth; } set { layerDepth = value; } }
        public bool Hidden { get { return hidden; } set { hidden = value; } }
        public byte Opacity { get { return color.A; } set { color.A = value; } }
        public Rectangle Rectangle { get { return rectangle ?? Rectangle.Empty;} set { rectangle = value;}}
        private Rectangle? rectangle = null;
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
                    if (relativePath.Substring(relativePath.Length - 4) != ".xnb") continue;
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

        private static List<Object> objects = new List<Object>();


        private List<Object> objectsToUpdate = new List<Object>();
        private List<Object> objectsToDraw = new List<Object>();
        private List<Object> objectsToRemove = new List<Object>();

        private static Player player;
        private static ProgressBar progressBar;
        
        public static Camera camera;

        private GameObject Background;
        private Vector2 cameraPos = Vector2.Zero;


        private static Texture2D pixel;

        private Button button;


        private SpriteFont spriteFont;
        
        private QuadTree<Object>.Node quadTreeRoot;

        private GameObject test;

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
            test = new GameObject();
            player = new Player();
            player.Initialize();
            Player.InitializeStatic();
            quadTreeRoot.rectangle = new Rectangle(Int32.MinValue, Int32.MinValue, Int32.MaxValue, Int32.MaxValue);
            button = new Button();
            button.Position = new Vector2(100.0f, 100.0f);
            button.Rectangle = new Rectangle(100, 100, 100, 100);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            player = new Player();
            progressBar = new ProgressBar(CreateRectangleTexture(1, 1));
            Background = new GameObject();
            Background.Position = new Vector2(-100, -200);
            // TODO: Add your initialization logic here
            camera = new Camera(_graphics);
            Console.WriteLine("End of Initialization");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("Start of Loading Content");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            Player.LoadContentStatic(Content);
            try { spriteFont = this.Content.Load<SpriteFont>("Assets/Font"); }
            catch (Exception e) { Console.WriteLine(e); }
            AssetManager.LoadTextures(Content);
            test.Texture = AssetManager.GetTexture("Test");
            test.Origin = test.Texture.Bounds.Center.ToVector2();
            Player.LoadTextures();
            try
            {
                player.Texture = AssetManager.GetTexture("Player");                                                         
                Background.Texture = AssetManager.GetTexture("Background");
                Console.WriteLine("Setting textures successful!");
            }
            catch (Exception e) { Console.WriteLine(e); }
            player.LayerDepth = 0.01f;
            Background.Color = new Color(128, 0, 128, 128);
            Background.Scale = new Vector2(0.0325f);
            button.Texture = CreateRectangleTexture(button.Width, button.Height);
            objects.Add(player);
            objects.Add(Background);
            objects.Add(button);
            objects.Add(progressBar);
            objects.Add(test);
            // TODO: use this.Content to load your game content here
            Console.WriteLine("End of Loading Content");
        }
        protected override void Update(GameTime gameTime)
        {
            //float rot = test.Rotation % 360.0f;
            test.Rotation += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
           
            if (!this.IsActive) return;
            objectsToDraw.Clear();
            // process inputs -> process behaviors -> calculate velocities -> move entities -> resolve collisions -> render frame  
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // process inputs
            Input.UpdateInput();
            player.Update(gameTime);
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

                                //velocity.Y += (float)((98.1 * 2) * gameTime.ElapsedGameTime.TotalSeconds);
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
                if (obj.Texture == null)
                {
                    continue;
                }
                if (
                        (obj.Position.X + obj.Texture.Width < camera.GetTopLeft().X) ||
                        (obj.Position.Y + obj.Texture.Height < camera.GetTopLeft().Y) ||
                        (obj.Position.X > camera.GetTopLeft().X + _graphics.PreferredBackBufferWidth) ||
                        (obj.Position.Y > camera.GetTopLeft().Y + _graphics.PreferredBackBufferHeight)
                        )
                {
                    objectsToDraw.Remove(obj);
                }
            }


            cameraPos = player.Position;
            camera.MoveToward(cameraPos, (float)gameTime.ElapsedGameTime.TotalMilliseconds, 0.9999999f);


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
            GraphicsDevice.Clear(Color.Black); 
            // TODO: Add your drawing code here
            Matrix transform = Matrix.CreateTranslation(-camera.GetTopLeft().X, -camera.GetTopLeft().Y, 0);
            _spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: transform);
            DrawGameObjects(_spriteBatch);
            //DrawCollidersOutline<UIObject>(_spriteBatch);


            _spriteBatch.End();

            if (!player.UIHidden)
            {
                _spriteBatch.Begin();
                player.DrawUI(_spriteBatch);
                DrawUI(_spriteBatch);
                DrawCollidersOutline<UIObject>(_spriteBatch);
                int fps = (int)(1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
                progressBar.Value = fps / 60;
                _spriteBatch.DrawString(spriteFont, fps.ToString(), Vector2.Zero, Color.Magenta);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }




        public Texture2D CreateRectangleTexture(int w, int h, Color? color = null)
        {
            Color trueColor = color ?? new Color(255, 255, 255);
            Color[] colors = new Color[w * h];
            for (int i = 0; i < w * h; ++i)
            {
                colors[i] = trueColor;
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

        private void DrawColliders<T>(SpriteBatch spriteBatch)
        {
            foreach (var obj in objectsToDraw)
            {
                if (obj is not T)
                    continue;

                var collider = obj.Rectangle;
                if (collider == Rectangle.Empty) { continue; }

                spriteBatch.Draw(pixel, collider, new Color(255, 0, 0, 64));
            }
        }

        private void DrawCollidersOutline<T>(SpriteBatch spriteBatch)
        {
            foreach (var obj in objectsToDraw)
            {
                if (obj is not T)
                    continue;

                var collider = obj.Rectangle;
                if (collider.Size == Point.Zero || obj.Hidden) { continue; }
                DrawOutline(spriteBatch, collider, Color.Red);
            }
        }


        private void DrawOutline(SpriteBatch spriteBatch, Rectangle target, Color color)
        {               
            Texture2D tex = pixel;

            if (color == default)
                color = new Color(49, 84, 141) * 0.9f;

            var sourceCorner = new Rectangle(0, 0, 6, 6);
            var sourceEdge = new Rectangle(6, 0, 4, 6);
            var sourceCenter = new Rectangle(6, 6, 4, 4);


            int outline = 4;    
            byte a = color.A;
            Color cornerColor = color ;
            spriteBatch.Draw(tex, new Rectangle(target.X + outline, target.Y, target.Width - outline*2, outline), color);
            spriteBatch.Draw(tex, new Rectangle(target.X, target.Y - outline + target.Height, target.Height - outline*2, outline), sourceEdge, color, -(float)Math.PI * 0.5f, Vector2.Zero, 0, 0);
            spriteBatch.Draw(tex, new Rectangle(target.X - outline + target.Width, target.Y + target.Height, target.Width - outline * 2, outline), sourceEdge, color, (float)Math.PI, Vector2.Zero, 0, 0);
            spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y + outline, target.Height - outline * 2, outline), sourceEdge, color, (float)Math.PI * 0.5f, Vector2.Zero, 0, 0);

            spriteBatch.Draw(tex, new Rectangle(target.X, target.Y, outline, outline), sourceCorner, cornerColor, 0, Vector2.Zero, 0, 0);
            spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y, outline, outline), sourceCorner, cornerColor, (float)Math.PI * 0.5f, Vector2.Zero, 0, 0);
            spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y + target.Height, outline, outline), sourceCorner, cornerColor, (float)Math.PI, Vector2.Zero, 0, 0);
            spriteBatch.Draw(tex, new Rectangle(target.X, target.Y + target.Height, outline, outline), sourceCorner, cornerColor, (float)Math.PI * 1.5f, Vector2.Zero, 0, 0);
            /*
            int outline = 4;
            spriteBatch.Draw(pixel, new Rectangle(target.Location.X + (outline / 2), target.Location.Y, target.Width + outline, outline), color);


            spriteBatch.Draw(pixel, new Rectangle(target.Location.X, target.Location.Y + outline / 2, outline, target.Height + outline), color);
            spriteBatch.Draw(pixel, new Rectangle(target.Location.X + outline/2, target.Location.Y + target.Height, target.Width + outline, outline), color);
            spriteBatch.Draw(pixel, new Rectangle(target.Location.X + target.Width, target.Location.Y + outline / 2, outline, target.Height + outline), color);
            */
        }



        public static void AddObject(Object obj)
        {
            if (obj == null) throw new Exception("Failed to... idk");
            objects.Add(obj);
        }

        public static void AddObjects(Object[] obj)
        {
            objects.AddRange(obj.ToList());
        }




    }
}
