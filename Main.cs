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
using System.Text;
using System.Text.RegularExpressions;

namespace MyGame
{
    
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
        private static Vector2 mousePosition = Vector2.Zero;
        public static Vector2 MousePosition { get { return mousePosition; } }
        public static Vector2 GlobalMousePosition 
        { 
            get 
            {
                return mousePosition + Main.camera.GetTopLeft();
            } 
        }
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


            var pressedKeys = keyboardState.GetPressedKeys();
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

        


        public static MouseState mouseState;
        private static MouseState previousMouseState;


        public enum MouseButton
        {
            Left,
            Right,
            Middle,
            X1,
            X2
        }

        public static KeyboardState keyboardState;  
        private static KeyboardState previousKeyboardState;
        public static bool HoveringUI { get { return hoveringUI; } set { hoveringUI = value; } }
        private static bool hoveringUI = false;
        public static bool IsAnyKeyPressed()
        {
            return keyboardState.GetPressedKeys().Length != 0;
        }
        public static bool IsKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }


        public static bool IsKeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        public static bool IsMouseButtonPressed(MouseButton mouseButton)
        {
            if (mouseButton == MouseButton.Left)
            {
                return mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && !hoveringUI;
            }
            else if (mouseButton == MouseButton.Right)
            {
                return mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released && !hoveringUI;
            }
            else if (mouseButton == MouseButton.Middle)
            {
                return mouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released && !hoveringUI;
            }
            else if (mouseButton == MouseButton.X1)
            {
                return mouseState.XButton1 == ButtonState.Pressed && previousMouseState.XButton1 == ButtonState.Released && !hoveringUI;
            }
            else if (mouseButton == MouseButton.X2)
            {
                return mouseState.XButton2 == ButtonState.Pressed && previousMouseState.XButton2 == ButtonState.Released && !hoveringUI;
            }
            return false;
        }

        public static bool IsMouseButtonDown(MouseButton mouseButton)
        {
            if (mouseButton == MouseButton.Left)
            {
                return mouseState.LeftButton == ButtonState.Pressed;
            }
            else if (mouseButton == MouseButton.Right)
            {
                return mouseState.RightButton == ButtonState.Pressed;
            }
            else if (mouseButton == MouseButton.Middle)
            {
                return mouseState.MiddleButton == ButtonState.Pressed;
            }
            else if (mouseButton == MouseButton.X1)
            {
                return mouseState.XButton1 == ButtonState.Pressed;
            }
            else if (mouseButton == MouseButton.X2)
            {
                return mouseState.XButton2 == ButtonState.Pressed;
            }
            return false;
        }









        public static void UpdateInput()
        {
            
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);
            hoveringUI = false;
            ScanForKonamiCode();
            //Diagnostics.Debug.WriteLine("Hello world");
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
                obj.Key.Hidden = true;
            }
        }
        public void Show()
        {
            foreach (var obj in objects)
            {
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

        public override void Destroy()
        {
            this.objects.Clear();
            base.Destroy();
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

            if (movePercentage != 1.0f)
                differenceInPosition *= movePercentage;
            
            var fractionOfPassedTime = deltaTimeInMs / 10;

            Center += differenceInPosition * fractionOfPassedTime;

            if ((target - Center).Length() < movePercentage || movePercentage != 1.0f)  
            {
                Center = target;
            }
        }

        public void MoveToward(Vector2 target)
        {

            Center = target;
        
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

        public float GravityScale { get { return gravityScale; } set { gravityScale = value; } }
        private float gravityScale = 1.0f;


        public bool OnFloor { get { return onFloor; } set { onFloor = value; } }
        private bool onFloor = false;

        public bool WasOnFloor { get { return wasOnFloor; } set { wasOnFloor = value; } }
        private bool wasOnFloor = false;

        public float TerminalVelocity {  get { return terminalVelocity; } set { terminalVelocity = value; } }
        private float terminalVelocity = 420.0f;

        public bool CanCollideWithTiles
        {
            get { return canCollideWithTiles; }
            set { canCollideWithTiles = value; }
        }
        private bool canCollideWithTiles = true;

        public virtual void OnTileCollide(Tile tile)
        {
            if (tile != null)
            tile.color = Color.Red;
        }



    }
    public class LightMap
    {
        public static List<LightSource> lightSources = new List<LightSource>();
    }
    public class LightSource
    {
        public int x;
        public int y;
        public Color color;
        public static Texture2D texture = null;
        public Vector2 scale;
        static LightSource()
        {
            texture = AssetManager.GetTexture("LightSource");
        }
        public LightSource(int x, int y, Vector2 scale, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.scale = scale;
            LightMap.lightSources.Add(this);
        }

    }

    public class Tile
    {
        public int Width {get {return width;}}
        public int Height {get {return height;}}
        public Vector2 Size { get { return new Vector2(width, height); } }
        private int width = 8;
        private int height = 8;
        private int x;
        private int y;
        public Texture2D Texture {  get { return texture;  } set { texture = value; } }
        private Texture2D texture;
        public Color color = Color.White;
        public bool Solid { get { return solid; } set { solid = value; } }
        private bool solid = false;

        public Tile(int id)
        {

        }

        public Tile(TileType tileType, int x, int y)
        {
            this.tileType = tileType;
            this.x = x;
            this.y = y;
            switch (tileType)
            {
                case TileType.NONE:
                    break;
                case TileType.GRASS:
                    texture = AssetManager.GetTexture("GrassBlock");
                    solid = true;
                    break;
                case TileType.DIRT:
                    texture = AssetManager.GetTexture("DirtBlock");
                    solid = true;
                    break;
                case TileType.STONE:
                    texture = AssetManager.GetTexture("StoneBlock");
                    solid = true;
                    break;
                default:
                    break;
            }
            
        }
        public TileType Type { get { return tileType; } set { tileType = value; } }
        private TileType tileType = TileType.NONE;
        public enum TileType
        {
            NONE = 0,
            GRASS = 1,
            DIRT = 2,
            STONE = 3,

        }

    }









    public class AI
    {

    }


    
    public class Enemy1 : Enemy
    {
        public Enemy1(Texture2D texture, Vector2 position, float health) : this(health, position)
        {
            this.Texture = texture;
        }
        public Enemy1(float health, Vector2? position = null, Vector2? velocity = null) : base(health, position, velocity)
        {
            this.Rectangle = new Rectangle(Point.Zero, new Point(16, 32));

        }
        
        public override void Damage(float value)
        {
            base.Damage(value);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
    
    public class Enemy : Character
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void OnCollide(ColliderObject otherCollider)
        {
            if (otherCollider == this)
            {
                return;
            }

            if (otherCollider is Character character)
            {
                character.Damage(ContactDamage);
            }
            base.OnCollide(otherCollider);
        }
        public Enemy(float health, Vector2? position = null, Vector2? velocity = null) : base(health, position, velocity)
        { }
        public Enemy(float health) : base(health)
        {
            this.MaxHealth = health;
        }
    }
    public class Character : PhysicsObject
    {
        
        public float Health { get { return health; } set { health = value; } }
        private float health = 100.0f;
        
        public float PreviousHealth { get { return previousHealth; } set { previousHealth = value; } }
        private float previousHealth;
        public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        private float maxHealth = 100.0f;

        public int ContactDamage { get { return contactDamage; } set { contactDamage = value; } }
        private int contactDamage = 67;

        public bool Invisible { get { return invisible; } set { invisible = value; } }
        private bool invisible = false;
        protected float invisbleOpacity = 0.1f;
        private float friction = 0.85f;
        public float Friction { get { return friction; } set { friction = value; } }
        public AI AI { get { return aI; } set { aI = value; } }
        private AI aI; 
        public int Direction
        {
            get { return direction; }
            set
            {
                direction = value;
            }
        }
        private int direction = 1;











        public float healthBarTimer = 5.0f;
        public bool healthBarStartedCountingDown = false;
        public bool healthBarFading = false;

        public byte healthBarOpacity = (byte)0;



        public Character(float health, Vector2? position = null, Vector2? velocity = null)
        {
            this.Position = position ?? Vector2.Zero;
            this.Velocity = velocity ?? Vector2.Zero;
            this.Health = health;
            this.MaxHealth = health;
        }
 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
        public virtual void Damage(float value)
        {
            if (value < 0) return;
            health -= value;
            CombatText combatText = new CombatText(this, (int)value);
            if (health <= 0) Destroy();
        }
        public void Die()
        {

        }
    }


    public class Animation
    {


        /*
        public Animation(GameObject target, Texture2D texture, int frameCount, int fps = 10)
        {
            this.target = target;
            this.texture = texture;
            maxFrames = frameCount;
            this.fps = fps;
            durationPerFrame = 1000.0f / fps;
            animations.Add(this);
        }
        */
        public Animation(GameObject target)
        {
            this.target = target;
            this.texture = target.Texture;
            animations.Add(this);
        }

        public int Frame { get { return frame; } }
        public int FrameCount { get { return frameCount; } }



        private static List<Animation> animations = new List<Animation>();
        private int frame = 0;
        private int frameCount = 1;
        private int frameWidth = 0;
        private float elapsed = 0.0f;
        private int fps = 10;
        public float durationPerFrame = 100.0f;
        private GameObject target;
        private Texture2D texture;
        private int y = 0;
        private bool looped = true;

        private int[] transition_linear = { 1, 2, 3, 4, 5, 6, 7, 8 };

        private static void Initialize()
        {

        }

        public void PlayAnimation(Texture2D texture, int frameCount, int y = 0,int fps = 10, bool looped = true)
        {
            this.looped = looped;
            this.texture = texture;
            this.frameCount = frameCount;
            this.fps = fps;
            this.frameWidth = texture.Width / frameCount;
            this.elapsed = 0.0f;
            this.looped = looped;
            durationPerFrame = 1000.0f / fps;
            this.y = 0;
            frame = 0;
            elapsed = 0.0f;
        }
        public void PlayAnimation(int y, int fps = 10, bool looped = true)
        {
            this.looped = looped;
            this.fps = fps;
            this.frameWidth = texture.Width / frameCount;
            this.elapsed = 0.0f;
            this.looped = looped;
            durationPerFrame = 1000.0f / fps;
            this.y = 0;
            frame = 0;
            elapsed = 0.0f;
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
            if (frameCount <= 1)
            {
                target.SourceRectangle = null;
                return;
            }
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            if (elapsed > durationPerFrame)
            {
                ++frame;
                frame %= frameCount;
                elapsed -= durationPerFrame;
                
            }

            target.SourceRectangle = new Rectangle(frameWidth * frame, texture.Height * y, frameWidth, texture.Height);
        }
    }

    public class ColliderObject : GameObject
    {
        // ColliderObjects are things that will collide in a quadtree or something, i will add the quadtree implementation soon™
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

        public bool CollisionDisabled { get { return collisionDisabled; } set { collisionDisabled = value; } }
        private bool collisionDisabled = false;




        public virtual void OnCollide(ColliderObject otherCollider)
        {

        }
    }

    public static class Settings
    {
        public static GraphicsDeviceManager graphics;
        public static int screenWidth = 640;
        public static int screenHeight = 480;

        public static void SetSettings()
        {
            
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

        public Player(float health) : base(health)
        {
            this.SetSize(32, 32);
        }

        
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } } 
        private float moveSpeed = 500.0f;


        public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
        private float maxSpeed = 250.0f;
        public float JumpPower { get { return jumpPower; } set { jumpPower = value; } }
        private float jumpPower = -400f;

        private int selectedItemIndex = 0;
        public ItemSlot[] itemSlots = new ItemSlot[16];

        private bool attacking = false;
        private bool aiming = false;
        public Item currentlyHeldItem = null;
        public Item currentItemInCursor = null;

        private Vector2[] bodyPartOffsetsRight =
        {
            new Vector2(0, -10),
            new Vector2(0, 0),
            new Vector2(20, 11),
            new Vector2(12, 11),
            new Vector2(0, 10)
        };
        private Vector2[] bodyPartOffsetsLeft =
        {
            new Vector2(0, -10),
            new Vector2(0, 0),
            new Vector2(20, 11),
            new Vector2(12, 11),
            new Vector2(0, 10)
        };
        private enum Part
        {
            Head = 0,
            Torso = 1,
            LeftHand = 2,
            RightHand = 3,
            Legs = 4
        }


    

        public bool UIHidden { get { return uiHidden; } set { uiHidden = value; } }
        private bool uiHidden = false;



        private bool isMale = true;



        private Character reference;
        private GameObject head;
        private GameObject torso;
        private GameObject rightHand;
        private GameObject leftHand;
        private GameObject legs;
        private Vector2 handPosition;
        private Vector2 handOffset = new Vector2(0, 10);
        public enum PlayerState
        {
            None,
            Idle,
            Walking,
            Jumping,
            Falling
        }
        
        private PlayerState playerState = PlayerState.None;
        private PlayerState lastPlayerState = PlayerState.None;


        public void Initialize()
        {
            try
            {
                Console.WriteLine("Sucess!");
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to initialize player: " + e);
            }
        }
        public void LoadContent(ContentManager content)
        {
            LightSource = new LightSource((int)this.Position.X, (int)this.Position.Y, new Vector2(1.0f),Color.White);
            isMale = false;
            if (isMale)
            {
                Rectangle = new Rectangle(0, 0, 18, 34);
                RectangleOffset = new Vector2(8, 0);
            }
            else
            {
                Rectangle = new Rectangle(0, 0, 16, 32);
                RectangleOffset = new Vector2(10, 0);
            }

            InitializeMusicPlayer();
            currentSong = content.Load<Song>("Assets/ForestDayMorning");
            rightHand = new GameObject();
            rightHand.Texture = AssetManager.GetTexture("PlayerRightHand1");
            rightHand.Animation = new Animation(rightHand);
            rightHand.Origin = rightHand.GetSize()/2;

            leftHand = new GameObject();
            leftHand.Texture = AssetManager.GetTexture("PlayerLeftHand1");
            leftHand.Animation = new Animation(leftHand);
            leftHand.Origin = leftHand.GetSize() / 2;
            
            
            handPosition = new Vector2(12, 1) + Position;


            head = new GameObject();
            head.Texture = AssetManager.GetTexture("PlayerHead1");
            head.Animation = new Animation(head);
            legs = new GameObject();
            legs.Texture = AssetManager.GetTexture("PlayerLegs1");
            legs.Animation = new Animation(legs);
            torso = new GameObject();
            torso.Texture = AssetManager.GetTexture("PlayerTorso1");
            torso.Animation = new Animation(torso);


            reference = new Character(UInt16.MaxValue, new Vector2(420, 420));
            reference.Rectangle = new Rectangle(67, 67, 16, 32);
            reference.Texture = AssetManager.GetTexture("Reference");
            reference.Animation = new Animation(reference);
            Main.AddObject(reference);


            Main.AddObject(legs);
            Main.AddObject(leftHand);
            Main.AddObject(torso);
            Main.AddObject(head);
            Main.AddObject(rightHand);

            


            int x = 0;
            int y = 0;
            for (int i = 0; i < itemSlots.Length; i++)
            {
                ItemSlot itemSlot = new ItemSlot();
                itemSlot.Texture = ItemSlot.texture;
                itemSlot.index = i;
                x = 24 * (i % 4);
                y = i % 4 == 0 ? y + 24 : y;
                itemSlot.Position = new Vector2(x + 16, y);
                itemSlot.RectangleOffset = new Vector2(2, 2);
                itemSlots[i] = itemSlot;
                Main.AddObject(itemSlot);
            }


            Pistol pistol = new Pistol();
            itemSlots[0].AddItem(pistol);

            Sword sword = new Sword();
            itemSlots[1].AddItem(sword);

        }
        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine(Projectile.projectileCount);
            UpdateMusic(gameTime);
            if (Input.IsKeyPressed(Keys.F11)) uiHidden = !uiHidden;

            Vector2 velocity = Velocity;
            Vector2 position = Position;


            if (Input.IsKeyDown(Keys.A))
            {
                velocity.X -= (float)(MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                playerState = PlayerState.Walking;
            }
            else if (Input.IsKeyDown(Keys.D))
            {
                velocity.X += (float)(MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                playerState = PlayerState.Walking;
            }
           
            else
            {
                velocity.X *= Friction;
                if (Math.Abs(velocity.X) < 1f)
                    velocity.X = 0f;
                playerState = PlayerState.Idle;
            }


            /*
            if (Input.IsKeyDown(Keys.W))
            {
                velocity.Y -= (float)(MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (Input.IsKeyDown(Keys.S))
            {
                velocity.Y += (float)(MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                //.Y -= (float)(MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds) * Direction;
                velocity.Y = 0;
            }
            */


            
            if (Input.IsKeyPressed(Keys.Space) && OnFloor)
            {
                velocity.Y = JumpPower;
                position.Y -= 8;

            }

            

            velocity.X = Math.Clamp(velocity.X, -MaxSpeed, MaxSpeed);
            velocity.Y = Math.Clamp(velocity.Y, -MaxSpeed, MaxSpeed);
            this.Velocity = velocity;
            this.Position = position;



            if (Input.IsKeyPressed(Keys.D1)) { selectedItemIndex = 0; }
            else if (Input.IsKeyPressed(Keys.D2)) { selectedItemIndex = 1; }
            else if (Input.IsKeyPressed(Keys.D3)) { selectedItemIndex = 2; }
            else if (Input.IsKeyPressed(Keys.D4)) { selectedItemIndex = 3; }
            else if (Input.IsKeyPressed(Keys.D5)) { selectedItemIndex = 4; }
            else if (Input.IsKeyPressed(Keys.D6)) { selectedItemIndex = 5; }
            else if (Input.IsKeyPressed(Keys.D7)) { selectedItemIndex = 6; }
            else if (Input.IsKeyPressed(Keys.D8)) { selectedItemIndex = 7; }
            else if (Input.IsKeyPressed(Keys.D9)) { selectedItemIndex = 8; }
            else if (Input.IsKeyPressed(Keys.D0)) { selectedItemIndex = 9; }

            currentlyHeldItem = itemSlots[selectedItemIndex].item;
            Vector2 facingOffset = (Direction == -1) ? new Vector2(-handOffset.X, handOffset.Y) : handOffset;
            Vector2 rotatedOffset = Vector2.Transform(facingOffset, Matrix.CreateRotationZ(rightHand.Rotation));
            handPosition = rightHand.Position + rotatedOffset;
            //handPosition = new Vector2(12, 1) + Main.camera.GetTopLeft();

            /*
            for (int i = 0; i < itemSlots.Length; ++i)
            {
                if (itemSlots[i].Hovering)
                {
                    if (itemSlots[i].item != null)
                    {
                        Item temp = currentItemInCursor;
                        itemSlots[i].item = temp;
                        currentItemInCursor = itemSlots[i].item;

                    }
                    else
                    {
                        itemSlots[i].item = currentItemInCursor;
                        currentItemInCursor = null;
                    }
                }

            }

            */
            if (currentItemInCursor != null)
            {
                currentItemInCursor.Position = new Vector2(Input.mouseState.X, Input.mouseState.Y) + currentItemInCursor.GetSize() / 2.0f;

                for (int i = 0; i < itemSlots.Length; ++i)
                {
                    if (itemSlots[i].item != null)

                        if (itemSlots[i] == itemSlots[selectedItemIndex])
                        {
                            itemSlots[i].Resize(1.1f);
                            //itemSlots[i].Scale = new Vector2(2.0f);
                        }
                        else
                        {
                            itemSlots[i].Resize(1.0f);
                        }


                    if (itemSlots[i].Pressed)
                    {
                        if (itemSlots[i].item != null)
                        {
                            Item temp = currentItemInCursor;
                            currentItemInCursor = itemSlots[i].item;
                            itemSlots[i].item = temp;
                            

                        }
                        else
                        {
                            itemSlots[i].item = currentItemInCursor;
                            currentItemInCursor = null;
                        }
                    }

                }



            }
            else
            {
                for (int i = 0; i < itemSlots.Length; ++i)
                {
                    if (itemSlots[i].item != null)

                        if (itemSlots[i] == itemSlots[selectedItemIndex])
                        {
                            itemSlots[i].Resize(1.1f);
                        }
                        else
                        {
                            itemSlots[i].Resize(1.0f);
                        }


                    if (itemSlots[i].Pressed)
                    {
                        if (itemSlots[i].item != null)
                        {
                            currentItemInCursor = itemSlots[i].item;
                            itemSlots[i].item = null;
                        }
                    }

                }

            }

            Vector2 headOffset = (Direction == -1) ? bodyPartOffsetsLeft[(int)Part.Head] : bodyPartOffsetsRight[(int)Part.Head];
            head.Position = Position + headOffset;
            Vector2 torsoOffset = (Direction == -1) ? bodyPartOffsetsLeft[(int)Part.Torso] : bodyPartOffsetsRight[(int)Part.Torso];
            torso.Position = Position + torsoOffset;
            Vector2 leftHandOffset = (Direction == -1) ? bodyPartOffsetsLeft[(int)Part.LeftHand] : bodyPartOffsetsRight[(int)Part.LeftHand];
            leftHand.Position = Position + leftHandOffset;
            Vector2 rightHandOffset = (Direction == -1) ? bodyPartOffsetsLeft[(int)Part.RightHand] : bodyPartOffsetsRight[(int)Part.RightHand];
            rightHand.Position = Position + rightHandOffset;
            Vector2 legsOffset = (Direction == -1) ? bodyPartOffsetsLeft[(int)Part.Legs] : bodyPartOffsetsRight[(int)Part.Legs];
            legs.Position = Position + legsOffset;


            head.Effects = (Direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            torso.Effects = (Direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            leftHand.Effects = (Direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            rightHand.Effects = (Direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            legs.Effects = (Direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;


            if (currentlyHeldItem != null)
            {
                currentlyHeldItem.Position = Position + new Vector2(16, 12);
                aiming = currentlyHeldItem.holdStyle == Item.HoldStyle.Aim;
                if (Direction == 1)
                {
                    //currentlyHeldItem.Position = handPosition; //+ currentlyHeldItem.HandPos;

                }
                else if (Direction == -1)
                {

                    //Vector2 flippedHandPos = new Vector2(currentlyHeldItem.HandPos.X, currentlyHeldItem.HandPos.Y);
                    //currentlyHeldItem.Position = handPosition - flippedHandPos;
                }
                if (currentlyHeldItem.holdStyle == Item.HoldStyle.None)
                {

                }
                else if (currentlyHeldItem.holdStyle == Item.HoldStyle.Front)
                {

                }
                else if (currentlyHeldItem.holdStyle == Item.HoldStyle.Aim)
                {
                    currentlyHeldItem.Effects = (Direction == -1) ? SpriteEffects.FlipVertically : SpriteEffects.None;
                }
                else if (currentlyHeldItem.holdStyle == Item.HoldStyle.Back)
                {
                    if (!attacking)
                    {
                        currentlyHeldItem.PositionOffset = new Vector2(-12, -12);
                        currentlyHeldItem.Effects = (Direction == 1) ? SpriteEffects.FlipVertically : SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                        currentlyHeldItem.LayerDepth = 1.0f;
                    }
                }




                if (currentlyHeldItem.clickType == Item.ClickType.Press)
                {
                    if (Input.IsMouseButtonPressed(Input.MouseButton.Left))
                    {
                        currentlyHeldItem.Use(this);
                        if (currentlyHeldItem.useStyle == Item.UseStyle.None)
                        {

                        }
                        else if (currentlyHeldItem.useStyle == Item.UseStyle.Aim)
                        {

                        }
                        else if (currentlyHeldItem.useStyle == Item.UseStyle.Swing)
                        {

                            attacking = true;
                            
                            currentlyHeldItem.Effects = (Direction == -1) ? SpriteEffects.FlipVertically : SpriteEffects.None;
                            
                            if (Direction == 1)
                            {
                                currentlyHeldItem.RotationDegrees = 0;
                                currentlyHeldItem.Effects = SpriteEffects.None;
                                Vector2 origin = currentlyHeldItem.Origin;
                                origin.X = 0f;
                                origin.Y = GetSize().Y;
                                currentlyHeldItem.Origin = origin;
                            }
                            else
                            {
                                currentlyHeldItem.Effects = SpriteEffects.FlipVertically;
                                Vector2 origin = currentlyHeldItem.Origin;
                                origin.X = 0.0f;
                                origin.Y = 0.0f;
                                currentlyHeldItem.Origin = origin;
                                
                            }
                            
                        }
                    }
                }
                else if (currentlyHeldItem.clickType == Item.ClickType.Hold)
                {
                    if (Input.IsMouseButtonDown(Input.MouseButton.Left))
                    {
                        currentlyHeldItem.Use(this);
                    }
                }



                if (attacking)
                {
                    if (Direction == 1)
                    {
                        currentlyHeldItem.RotationDegrees += ((float)gameTime.ElapsedGameTime.TotalSeconds * 256) * Direction;
                    }
                    else if (Direction == -1)
                    {
                        currentlyHeldItem.RotationDegrees += ((float)gameTime.ElapsedGameTime.TotalSeconds * 256) * Direction;

                    }
                    Console.WriteLine(currentlyHeldItem.RotationDegrees);
                }

                if (Direction == 1)
                {
                    if (currentlyHeldItem.RotationDegrees >= 90.0f)
                    {
                        attacking = false;
                        currentlyHeldItem.RotationDegrees = 0.0f;
                    }
                }
                else if (Direction == -1)
                {
                    if (currentlyHeldItem.RotationDegrees >= 0.0f)
                    {
                        attacking = false;
                        currentlyHeldItem.RotationDegrees = 0.0f;
                    }
                }
               

            }
            else
            {
                aiming = false;
            }

            if (Direction == 1)
            {
                head.LayerDepth = 0.15f;
                leftHand.LayerDepth = 0.1f;
                rightHand.LayerDepth = 0.2f;
                torso.LayerDepth = 0.15f;
                legs.LayerDepth = 0.15f;
                
                if (currentlyHeldItem != null)
                {
                    if (currentlyHeldItem.holdStyle == Item.HoldStyle.Aim)
                    {
                        if (aiming)
                        {
                            Vector2 mousePos = Input.mouseState.Position.ToVector2() + Main.camera.GetTopLeft();
                            float angel = LookAt(mousePos - rightHand.Position);
                            rightHand.Rotation = angel + MathHelper.ToRadians(-90.0f);
                            currentlyHeldItem.Rotation = angel;
                        }
                        else
                        {
                            rightHand.Rotation = 0.0f;
                        }

                    }
                    else if (currentlyHeldItem.holdStyle == Item.HoldStyle.Front)
                    {
                    }
                }

                    
            }
            else if (Direction == -1)
            {
                head.LayerDepth = 0.15f;
                leftHand.LayerDepth = 0.1f;
                rightHand.LayerDepth = 0.2f;
                torso.LayerDepth = 0.15f;
                legs.LayerDepth = 0.15f;


                if (currentlyHeldItem != null)
                {
                    if (currentlyHeldItem.holdStyle == Item.HoldStyle.Aim)
                    {
                        if (aiming)
                        {
                            Vector2 mousePos = Input.mouseState.Position.ToVector2() + Main.camera.GetTopLeft();
                            float angel = LookAt(mousePos - rightHand.Position);
                            rightHand.Rotation = angel + MathHelper.ToRadians(-90.0f);
                            currentlyHeldItem.Rotation = angel;
                        }
                        else
                        {
                            rightHand.Rotation = 0.0f;
                        }
                    }
                    else if (currentlyHeldItem.holdStyle == Item.HoldStyle.Front)
                    {
                    }
                }
            }
            else
            {
                Console.WriteLine("ARE YOU TRYING TO MODIFY DIRECTION????????????");
                Direction = 1;
            }

            if (playerState != lastPlayerState)
            {
                if (playerState == PlayerState.None)
                {

                }
                else if (playerState == PlayerState.Idle)
                {
                    head.Texture = AssetManager.GetTexture("PlayerHead1");
                    torso.Texture = AssetManager.GetTexture("PlayerTorso1");
                    leftHand.Texture = AssetManager.GetTexture("PlayerLeftHand1");
                    rightHand.Texture = AssetManager.GetTexture("PlayerRightHand1");
                    legs.Texture = AssetManager.GetTexture("PlayerLegs1");
                    head.Animation.PlayAnimation(head.Texture, 1);
                    leftHand.Animation.PlayAnimation(leftHand.Texture, 1);
                    rightHand.Animation.PlayAnimation(rightHand.Texture, 1);
                    torso.Animation.PlayAnimation(torso.Texture, 1);
                    legs.Animation.PlayAnimation(legs.Texture, 1);





                }
                else if (playerState == PlayerState.Walking)
                {
                    head.Texture = AssetManager.GetTexture("PlayerWalkingHead1");
                    torso.Texture = AssetManager.GetTexture("PlayerWalkingTorso1");
                    leftHand.Texture = AssetManager.GetTexture("PlayerWalkingLeftHand1");
                    rightHand.Texture = AssetManager.GetTexture("PlayerWalkingRightHand1");
                    legs.Texture = AssetManager.GetTexture("PlayerWalkingLegs1");
                    head.Animation.PlayAnimation(head.Texture, 8);
                    torso.Animation.PlayAnimation(torso.Texture, 8);
                    leftHand.Animation.PlayAnimation(leftHand.Texture, 8);
                    rightHand.Animation.PlayAnimation(rightHand.Texture, 8);
                    legs.Animation.PlayAnimation(legs.Texture, 8);
                }

            }
            lastPlayerState = playerState;
            //Console.WriteLine(playerState);




            if (attacking || aiming)
            {
                Direction = Input.MousePosition.X + Main.camera.GetTopLeft().X < Position.X ? -1 : 1;
            }
            else
            {
                if (Velocity.X < 0.0f) Direction = -1;
                else if (Velocity.X > 0.0f) Direction = 1; ;
            }


            head.Color = this.Color;
            torso.Color = this.Color;
            leftHand.Color = this.Color;
            rightHand.Color = this.Color;
            legs.Color = this.Color;




            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentlyHeldItem != null && currentlyHeldItem.Texture != null)
            {
                spriteBatch.Draw(
                        currentlyHeldItem.Texture,
                        currentlyHeldItem.Position + currentlyHeldItem.PositionOffset,
                        currentlyHeldItem.SourceRectangle,
                        this.Color,
                        currentlyHeldItem.Rotation,
                        currentlyHeldItem.Origin,
                        currentlyHeldItem.Scale,
                        currentlyHeldItem.Effects,
                        currentlyHeldItem.LayerDepth
                        );
                
            }
            base.Draw(spriteBatch, gameTime);
        }




        public string[] deathMessages =
        {
            "skill issue",
            "MASSIVE skill issue"
        };
        public override void Destroy()
        {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Health = MaxHealth;
        }
        public Vector2 GetCenter()
        {
            return Position + new Vector2(16, 16);
        }
        private float LookAt(Vector2 Direction)
        {
            return (float)Math.Atan2(Direction.Y, Direction.X);
        }






        public void DrawUI(SpriteBatch spriteBatch)
        {
            DrawMusicPlayer(spriteBatch);
        }
                                                                                                            



        private Button musicPlayerExitButton;
        private Texture2D musicPlayerPlayButtonTexture;
        private Texture2D musicPlayerPauseButtonTexture;
        private Button musicPlayerNextButton;
        private Button musicPlayerPreviousButton;
        private UIObject musicPlayerMusicIcon;
        private UIObject musicPlayerPanel;
        private Button musicPlayerMover;
        private Button musicPlayerPlayPauseButton;
        private bool musicPlayerPaused = true;
        private bool musicPlayerHidden = true;
        private SongCollection musics;
        private Container musicPlayerContainer;


        private Song currentSong;
       
        public void InitializeStatic()
        {                                                          
            InitializeMusicPlayer();
        }
        private void InitializeMusicPlayer()
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


            musicPlayerExitButton.Hidden = true;
            musicPlayerNextButton.Hidden = true;
            musicPlayerPreviousButton.Hidden = true;
            musicPlayerPlayPauseButton.Hidden = true;
            musicPlayerMover.Hidden = true;
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

            musicPlayerPaused = MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Stopped;
            if (musicPlayerPaused)
            {
                musicPlayerPlayPauseButton.Texture = musicPlayerPaused ? musicPlayerPlayButtonTexture : musicPlayerPauseButtonTexture;
                
            }


            if (musicPlayerMover.Down)
            {
               
                musicPlayerContainer.Position = Input.mouseState.Position.ToVector2() + -(new Vector2(95, 31));
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
  
        public void LoadTextures()
        {
            LoadMusicPlayerTextures();
        }

        
        private void LoadMusicPlayerTextures()
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
                musicPlayerMover.Rectangle = new Rectangle(Point.Zero, musicPlayerMover.GetSize().ToPoint());
                musicPlayerExitButton.Rectangle = new Rectangle(Point.Zero, musicPlayerExitButton.GetSize().ToPoint());
                musicPlayerPlayPauseButton.Rectangle = new Rectangle(Point.Zero, musicPlayerPlayPauseButton.GetSize().ToPoint());
                musicPlayerNextButton.Rectangle = new Rectangle(Point.Zero, musicPlayerNextButton.GetSize().ToPoint());
                musicPlayerPreviousButton.Rectangle = new Rectangle(Point.Zero, musicPlayerPreviousButton.GetSize().ToPoint());
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

        public void DrawMusicPlayer(SpriteBatch spriteBatch)
        {
            if (musicPlayerHidden) { return; }
            musicPlayerContainer.Draw(spriteBatch);   

        }


        private void ReadFiles(string path)
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



    public class Text
    {
        private struct Char
        {

        }
        private static string[] emojis =
        {
            ":heart:",
            ":broken_heart:",
            ":wilted_rose",
        };
        public static Text operator +(string text, Text text2)
        {
            return new Text("aaaa" + text);
        }
        public Text(string text)
        {
            Console.WriteLine(text);
        }
        public static void Test(string text = "MASSIVE skill issue :broken_heart::wilted_rose:")
        {
            string result = "";
            result = text;
            string[] split = text.Split();
            for (int i = 0; i < split.Length; ++i)
            {
                bool start = false;
                bool end = false;
                if (split[i][0] != ':' && split[i][split[i].Length - 1] != ':')
                {
                    continue;
                }
                start = true;

                for (int j = 0; j < emojis.Length; ++j)
                {
                    if (split[i] == emojis[j])
                    {
                        string emojiName = split[i].Replace(":", "");
                        Texture2D emoji = AssetManager.GetTexture(emojiName);
                        
                    }
                }



            }
            Console.WriteLine(result);
        }
        public static Text DrawText(string text)
        {


            return null;
        }
        private void RenderEmoji(string text)
        {

        }
    }





    public class ItemSlot : Button
    {
        public static Texture2D texture = null;
        public string itemName;
        public string itemStats;
        public Item item;
        public int index = -1;
        public bool favorite = false;

        public ItemSlot()
        {
            Rectangle = new Rectangle(0, 0, 16, 16);
            
        }

        public void AddItem(Item item)
        {
            this.item = item;
            itemName = item.name;
        }
        public void AddItem(Item item, int amount)
        {
            this.item = item;
            item.amount += amount;
            itemName = item.name;
        }


    }

    public class Bullet : Projectile
    {
        public Bullet(ColliderObject whoShotTheProjectile, Vector2 position, float rotation) :base()
        {
            if (AssetManager.LoadedTextures)
            {
                Texture = AssetManager.GetTexture("Bullet");
            }
            this.Position = position;
            LightSource = new LightSource((int)position.X, (int)position.Y, new Vector2(0.2f), Color.Orange);
            this.Rotation = rotation;
            this.Origin = GetSize() /2.0f;
            this.Velocity = Vector2.Transform(new Vector2(1000, 0), Matrix.CreateRotationZ(rotation));
            this.Rectangle = new Rectangle(0, 0, 2, 2);
            //RectangleOffset = new Vector2(4, 0);
            baseRectangleOffset = new Vector2(GetSize().X / 2.0f , 0f);
                
            this.WhoShotTheProjectile = whoShotTheProjectile;
        }   
        public override void Update(GameTime gameTime)
        {
            Rotation = (float)MathF.Atan2(Velocity.Y,Velocity.X);
            Vector2 rotatedOffset = Vector2.Transform(baseRectangleOffset, Matrix.CreateRotationZ(Rotation));
            RectangleOffset = rotatedOffset;
            base.Update(gameTime);
        }
    }
    public class Projectile : PhysicsObject
    {
        public ColliderObject WhoShotTheProjectile { get { return whoShotTheProjectile; } set { whoShotTheProjectile = value; } }
        private ColliderObject whoShotTheProjectile;
        public bool canCollideWithSameType = false;
        protected Vector2 baseRectangleOffset;
        private bool destroyed = false;
        public float LifeTime { get { return lifeTime; } set { lifeTime = value; } }
        private float lifeTime = 5.0f;
        public static int projectileCount = 0;
        public int Damage { get { return damage; }set { damage = value; } }
        private int damage = 67;
        protected int piercingCount = 0;
        public Projectile() :base()
        {
            this.CanCollideWithTiles = false;
            ++projectileCount;
        }
        public override void OnTileCollide(Tile tile)
        {
            Destroy();
            base.OnTileCollide(tile);
        }
        public override void Destroy()
        {
            if (destroyed) 
            {
                return;
            }
            destroyed = true;
            --projectileCount;
            base.Destroy();
        }
        public override void Update(GameTime gameTime)
        {
            if (destroyed) return;
            lifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lifeTime <= 0.0f)
            {
                Destroy();
                return;
            }
            /*
            if (Collision.CheckProjectileCollision(this))
            {
                Destroy();
                return;
            }
            */
            base.Update(gameTime);
        }
        public override void OnCollide(ColliderObject otherCollider)
        {
            bool collided = false;
            if (destroyed) return;
            if (otherCollider == whoShotTheProjectile)
            {
                return;
            }
            /*
            if (otherCollider.GetType().Name != this.GetType().Name)
            {
                return;
            }*/
            if (otherCollider is Character character)
            {
                character.Damage((float)Damage);
                collided = true;
            }
            --piercingCount;
            if (collided && piercingCount < 0) Destroy();
            //Destroy();
            base.OnCollide(otherCollider);
        }
    }

    public class Sword : BaseSword
    {
        public override string name => "Sword";
        protected override string texturePath => "Sword";
        public Sword() : base()
        {
            if (AssetManager.LoadedTextures)
            {
                icon = AssetManager.GetTexture("SwordIcon");
                Texture = AssetManager.GetTexture("Sword");
            }
            Vector2 origin = this.Origin;
            origin.Y = GetSize().Y;
            this.Origin = origin;

        }
    }
    public class Pistol : BaseGun
    {

        public override string name => "Pistol";
        protected override string texturePath => "Gun";
        public Pistol()
        {
            if (AssetManager.LoadedTextures)
            {
                icon = AssetManager.GetTexture("GunIcon");
                Texture = AssetManager.GetTexture(texturePath);
            }
            Vector2 origin = this.Origin;
            origin.Y = GetSize().Y / 2;
            this.Origin = origin;
            HandPos = new Vector2(2, 11);
            useStyle = UseStyle.Aim;
            holdStyle = HoldStyle.Aim;
            clickType = ClickType.Hold;
        }
        
    }


    public abstract class BaseSword : Item
    {
        public BaseSword()
        {
            this.useStyle = Item.UseStyle.Swing;
            this.holdStyle = Item.HoldStyle.Back;
            this.clickType = Item.ClickType.Press;
            
        }
        public override void OnUse(Character user)
        {
            RotationDegrees = 90.0f;
            base.OnUse(user);
        }
    }

    public abstract class BaseGun : Item
    {
        public override void OnUse(Character user)
        {

            Bullet bullet = new Bullet(user, Position, Rotation);


            Main.AddObject(bullet);
            base.OnUse(user);
        }
    }
    public class DroppedItem : ColliderObject
    {
        public Item item;
        public DroppedItem(Item item)
        {
            this.item = item;
            Texture = item.Texture;
            Rectangle = item.Rectangle;
        }

        public virtual void OnPickup()
        {
            
        }
    }
    public abstract class Item : GameObject 
    {
        public abstract string name { get; }
        protected abstract string texturePath { get; }
        public string description { get; private set; }

        public string customDescription = "";

        public bool consumable = false;
        public int amount = 1;
        public float useTime = 0.1f;
        public Texture2D icon; // must be 16x16
        public bool stackable = true;
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
        private Vector2 handPos = Vector2.Zero;

        public enum ClickType
        {
            None,
            Press,
            Hold
        }


      

        public enum UseStyle
        {
            None,
            Aim,
            Swing,
        }

        public enum HoldStyle
        {
            None,
            Front,
            Aim,
            Back,
        }


        public UseStyle useStyle = UseStyle.None;
        public HoldStyle holdStyle = HoldStyle.None;
        public ClickType clickType = ClickType.None;


        public virtual void OnUse(Character user)
        {
            
        }

        public virtual void Use(Character user)
        {
            OnUse(user);
        }

        public void Drop()
        {
            --amount;
            if (amount <= 0)
            {
                amount = 0;
            }
        }

        public void DropAll()
        {
            amount = 0;
        }

    }
    public class ToolTip : UIObject
    {
        public string Title { get { return title; } set { title = value; } }
        private string title = "";
        public string Description { get { return description; } set { description = value; } }
        private string description = "";
        public Color TitleColor { get { return titleColor; } set { titleColor = value; } }
        private Color titleColor = Color.White;
        public Color DescriptionColor { get { return descriptionColor; } set { descriptionColor = value; } }
        private Color descriptionColor = Color.LightGray;

        public ToolTip()
        {
        }
        public void SetToolTip(Item item)
        {
            if (item == null) return;
            title = item.name;
            description = item.description;


        }
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



        public bool Hovering { get { return hovering; } }
        private bool hovering = false;

        public override void Update(GameTime gameTime)
        {
            if (Rectangle.Contains(Input.mouseState.Position))
            {
                hovering = true;
            }
            else
            {
                hovering = false;
            }
            base.Update(gameTime);
        }


    }

    public class HealthBar : UIObject
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

        private static Texture2D healthBar1 = null;
        private static Texture2D healthBar2 = null;

        public HealthBar(float width = 100.0f, float height = 8.0f, float max = 100.0f, float min = 0.0f)
        {
            
            this.min = min;
            this.max = max;
            this.Max = max;
            w = width;
            h = height;
        }
        static HealthBar()
        {
            if (AssetManager.LoadedTextures)
            {
                healthBar1 = AssetManager.GetTexture("HealthBar1");
                healthBar2 = AssetManager.GetTexture("HealthBar2");
            }
        }
        public override void Update(GameTime gameTime)
        {
            float clmap = Math.Clamp(Value, Min, Max);
            float nromalize = (clmap - Min) / (Max - Min);
            percent = (int)(nromalize * 100);
            base.Update(gameTime);
        }
        public static void DrawHealthBar(
            SpriteBatch spriteBatch,
            float x, 
            float y,
            int w, 
            int h,
            float health,
            float maxHealth,
            ref float previousHealth,
            ref float timer,
            ref byte opacity,
            ref bool startedCountingDown,
            ref bool fading,
            GameTime gameTime
            )
        {
            if (health <= 0.0f)
            {
                return;
            }
            bool wasHit = health < previousHealth;
            if (wasHit)
            {
                startedCountingDown = true;
                fading = false;
                opacity = 255;
                timer = 5.0f;
            }
            if (startedCountingDown && !fading)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    fading = true;
                    startedCountingDown = false;
                }    
            }
            
            if (fading)
            {
                float alpha = (opacity / 255.0f);
                alpha -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (alpha <= 0)
                {
                    alpha = 0.0f;
                    opacity = (byte)0;
                    fading = false;
                }
                else
                {
                    opacity = (byte)(alpha * 255.0f);
                }
            }
            if (opacity <= 0)
            {
                opacity = 0;
                fading = false;
            }
            if (opacity > 0)
            {
                float percentage = health / maxHealth;
                if (percentage > 1.0f) percentage = 1.0f;
                float r = 1.0f - percentage;
                float g = percentage;
                float alpha = opacity / 255.0f;
                Color color1 = new Color(1.0f * alpha, 1.0f * alpha, 1.0f * alpha, alpha);
                Color color2 = new Color(r * alpha, g * alpha, 0.0f, alpha);
                int hBW = 16;
                int hBH = 4;
                Vector2 healthBarPos = new Vector2(x + (w / 2 - hBW / 2), y + (h + hBH));
                Rectangle sourceRect = new Rectangle(0, 0, (int)(hBW * percentage), hBH);
                spriteBatch.Draw(healthBar1, healthBarPos, null, color1, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.01f);
                spriteBatch.Draw(healthBar2, healthBarPos, sourceRect, color2, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
            }
            previousHealth = health;
        }
        public int ToPercent()
        {
            return percent;
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

        public ProgressBar(Texture2D texture,float width = 100.0f, float height = 8.0f, float max = 100.0f, float min = 0.0f)
        {
            Texture = texture;
            this.min = min;
            this.max = max;
            this.Max = max;
            w = width;
            h = height;
        }
        public override void Update(GameTime gameTime)
        {
            float clmap = Math.Clamp(Value, Min, Max);
            float nromalize = (clmap - Min) / (Max - Min);
            percent = (int)(nromalize * 100);
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
        private Color color = Color.White;
        private Vector2 position;
        private float lifeTime = 5.0f;
        enum Type
        {
            NONE,
            
        }
        public static void CreateText(string text, Vector2 worldPos, Color? color = null, Vector2? scale = null, float life = 5.0f)
        {
            CombatText combatText = new CombatText(text, color, scale);
            combatText.position = worldPos;
            combatText.lifeTime = life;
            combatTexts.Add(combatText);
        }
        public static void CreateText(string text, Color? color = null, Vector2? scale = null)
        {
            CreateText(text, Vector2.Zero, color, scale, 5.0f);
        }
        public static void CreateText(string text, Color? color = null, float? scale = null)
        {
            CreateText(text, Vector2.Zero, color, scale.HasValue? new Vector2(scale.Value) : (Vector2?) null , 5.0f);
        }
        private CombatText(string text, Color? color = null, Vector2? scale = null)
        {
            this.text = text;
            this.color = color ?? Color.White;
            this.scale = scale ?? Vector2.One;
            this.lifeTime = 5.0f;
        }
        
        public CombatText(Object obj, int damage)
        {
            this.text = damage.ToString();
            this.position = obj.Position - new Vector2(0, 8);
            this.color = Color.White;
            this.scale = Vector2.One;
            this.lifeTime = 1.0f;
            combatTexts.Add(this);
        }
         
        public static void DrawTexts(SpriteBatch spriteBatch,SpriteFont spriteFont ,GameTime gameTime)
        {
            for (int i = 0; i < combatTexts.Count; ++i)
            {
                CombatText combatText = combatTexts[i];
                float combatTextSize = 0.1f;
                spriteBatch.DrawString(spriteFont, combatText.text, combatText.position - new Vector2(combatTextSize*5), Color.Red, 0.0f, Vector2.Zero, combatText.scale + new Vector2(combatTextSize), SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(spriteFont, combatText.text, combatText.position, combatText.color, 0.0f, Vector2.Zero, combatText.scale, SpriteEffects.None, 0.0f);

                combatText.position.Y -= 20.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                combatText.lifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (combatText.lifeTime <= 0)
                {
                    combatTexts.Remove(combatText);
                }
            }
        }
    }
    public class Button : UIObject
    {


        public bool Pressed { get { return pressed; } set { pressed = value; } }
        public bool Down { get { return down; } set { down = value; } }
        public new bool Hidden { 
            get 
            { 
                return base.Hidden; 
            } 
            set 
            { 
                base.Hidden = value;
                if (value)
                {
                    pressed = false;
                    down = false;
                    wasDown = false;
                }
            }
        }
                                                                                                
        public bool Disabled { get { return disabled; } set { disabled = value; } }
   
        private bool pressed = false;
        private bool down = false;
        private bool disabled = false;

        private bool wasDown = false;
        public Button()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Hidden)
            {
                return;
            }

            Rectangle rectangle = this.Rectangle;
            if (Hovering)
            {
                Input.HoveringUI = true;
                down = Input.mouseState.LeftButton == ButtonState.Pressed;
                pressed = down && !wasDown;
                wasDown = down;
            }
            else
            {
                down = false;
                pressed = false;
            }
           

            

        }

        public override string ToString()
        {
            return "Hovering: " + Hovering.ToString() + "Pressed: " + pressed.ToString() + "Down: " + down.ToString() + "Disabled: " + disabled.ToString(); 
        }



    }



    /*
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


                        foreach (var o in objects)
                        {
                            foreach (var n in children)
                            {
                                if (n.Inside(o))
                                {
                                    n.Add(o);
                                }
                            }
                        }


                        foreach (var n  in children)
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
*/


    public class QuadTree
    {
        public static int currentMaxLevel = 0;
        private int maxObjects = 4;
        private int maxLevel = 67;

        private int level;
        private List<ColliderObject> objects;
        private Rectangle bounds;
        private QuadTree[] nodes;
        public Rectangle Bounds { get { return bounds; } }
        public QuadTree(int level, Rectangle bounds)
        {
            this.level = level;
            this.objects = new List<ColliderObject>();
            this.bounds = bounds;
            nodes = new QuadTree[4];
            if (level > currentMaxLevel) currentMaxLevel = level;
        }

        public void Clear()
        {
            objects.Clear();
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            int subWidth = bounds.Width / 2;
            int subHeight = bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            nodes[0] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight)); 
            nodes[1] = new QuadTree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[2] = new QuadTree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        private int GetIndex(Rectangle rectangle)
        {
            int index = -1;

            float verticalMidpoint = bounds.X + (bounds.Width / 2);
            float horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            bool topQuadrant = (rectangle.Y < horizontalMidpoint && rectangle.Y + rectangle.Height < horizontalMidpoint);
            bool bottomQuadrant = (rectangle.Y > horizontalMidpoint);

            if (rectangle.X < verticalMidpoint && rectangle.X + rectangle.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }

            }
            else if (rectangle.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }



                return index;
        }

        public void Insert(ColliderObject colliderObject)//Rectangle rectangle)
        {
            Rectangle rectangle = colliderObject.Rectangle;
            if (nodes[0] != null)
            {
                int index = GetIndex(rectangle);

                if (index != -1)
                {
                    nodes[index].Insert(colliderObject);
                    return;
                }
                else
                {
                    return;
                }
            }
            objects.Add(colliderObject);

            if (objects.Count > maxObjects && level < maxLevel)
            {
                if (nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < objects.Count)
                {
                    int index = GetIndex(objects[i].Rectangle);
                    if (index != -1)
                    {
                        nodes[index].Insert(objects[i]);
                        objects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

            }
        }
        public List<ColliderObject> Retrieve(List<ColliderObject> returnObjects, Rectangle rectangle)
        {
            int index = GetIndex(rectangle);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].Retrieve(returnObjects, rectangle);
            }
            returnObjects.AddRange(objects);
            return returnObjects;
        }
            

        public void DrawRectangleOutline(SpriteBatch spriteBatch, Color color)
        {
            Color outlineColor = new Color(color.R, color.G, color.B, (byte)255);
            Rectangle target = bounds;


            int outline = 1;
            spriteBatch.Draw(Main.pixel, new Rectangle(target.X, target.Y, target.Width, outline), outlineColor);
            spriteBatch.Draw(Main.pixel, new Rectangle(target.X, target.Y, outline, target.Height), outlineColor);
            spriteBatch.Draw(Main.pixel, new Rectangle(target.X, target.Y + target.Height - outline, target.Width, outline), outlineColor);
            spriteBatch.Draw(Main.pixel, new Rectangle(target.X + target.Width - outline, target.Y, outline, target.Height), outlineColor);



            foreach (var node in nodes)
            {
                if (node != null)
                {
                    node.DrawRectangleOutline(spriteBatch, color);
                }
            }
        }



    }
    public class Background
    {
        Texture2D currentBackground;
        Texture2D nextBackground;
        Texture2D previousBackground;

        public void Update(GameTime gameTime)
        {

        }
    }

    
    public class World
    {

        public static Tile[,] tiles = new Tile[2048, 2048]; 


        public static void CreateWorld()
        {
            File.WriteAllText("world.femboy", "");
            StringBuilder stringBuilder = new StringBuilder();
            string[] content =  new string[2048];
            string line = "";
            for (int x = 0; x < World.tiles.GetLength(0); ++x)
            {
                stringBuilder.Clear();
                for (int y = 100; y < World.tiles.GetLength(1); ++y)
                {
                    Tile.TileType type;
    
                    type = (Tile.TileType)Main.rand.Next(1, 3);
                    Tile tile = new Tile(type, x, y);
                    tiles[x, y] = tile;
                    stringBuilder.Append(((int)type).ToString());
                }
                line = stringBuilder.ToString();
                content[x] = line;
            }
            File.WriteAllLines("world.femboy", content);

        }

        public static void LoadWorld(string path)
        {
            string[] content = File.ReadAllLines(path);
            Console.WriteLine(content);
            for (int x = 0; x < content.Length; ++x)
            {
                string line = content[x];
                for (int y = 0; y < line.Length; ++y)
                {
                    Tile.TileType type = (Tile.TileType)int.Parse(line[y].ToString());                                                   
                    tiles[x, y] = new Tile(type, x, y);
                }
            }

        }
    }

    public class Collision
    {
        public static bool CheckTileCollision(ColliderObject obj)
        {
            Vector2 position = obj.Position + obj.RectangleOffset;
            int w = obj.Rectangle.Width;
            int h = obj.Rectangle.Height;


            int leftTile = Math.Max(0, (int)(position.X / 8));
            int rightTile = Math.Min(World.tiles.GetLength(0) - 1, (int)((position.X + w - 1 )/8));
            int topTile = Math.Max(0, (int)(position.Y / 8));
            int bottomTile = Math.Min(World.tiles.GetLength(1) - 1, (int)((position.Y + h - 1) / 8));

            for (int x = leftTile; x <= rightTile; ++x)
            {
                for (int y = topTile; y <= bottomTile; ++y)
                {
                    Tile tile = World.tiles[x, y];
                    if (tile == null || !tile.Solid) { continue; }
                    Rectangle tileRectangle = new Rectangle(x * 8, y * 8, 8, 8);
                    Rectangle objRectangle = new Rectangle((int)position.X, (int)position.Y, w, h);
                    if (objRectangle.Intersects(tileRectangle))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static Vector2 ResolveCollision(PhysicsObject obj, Vector2 velocity, GameTime gameTime)
        {
            Vector2 newVelocity = velocity;
            Vector2 position = obj.Position + obj.RectangleOffset;
            int w = obj.Rectangle.Width;
            int h = obj.Rectangle.Height;
            Vector2 nextPosX = position + new Vector2(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            if (CheckCollisionAtPosition(nextPosX, w, h))
            {
                newVelocity.X = 0;
                if (velocity.X > 0)
                {
                    int rightTile = (int)((nextPosX.X + w - 1) / 8);
                    obj.Position = new Vector2(rightTile * 8 - w - obj.RectangleOffset.X, obj.Position.Y);
                }
                else if (velocity.X < 0)
                {
                    int leftTile = (int)(nextPosX.X / 8);
                    obj.Position = new Vector2((leftTile + 1) * 8 - obj.RectangleOffset.X, obj.Position.Y);
                }
            }
            else
            {
                try
                {
                    Tile tile = World.tiles[Math.Min(World.tiles.GetLength(0) - 1, (uint)position.X / 8), Math.Min(World.tiles.GetLength(1) - 1, (uint)position.Y / 8)];
                    obj.OnTileCollide(tile);
                }
                catch 
                {
                    
                }
            }

            Vector2 currentPos = obj.Position + obj.RectangleOffset;
            Vector2 nextPosY = position + new Vector2(0, velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (CheckCollisionAtPosition(nextPosY, w, h))
            {
                newVelocity.Y = 0;

                if (velocity.Y > 0)
                {
                    int bottomTile = (int)((nextPosY.Y + h - 1) / 8);
                    obj.Position = new Vector2(obj.Position.X, bottomTile * 8 - h - obj.RectangleOffset.Y);
                    obj.OnFloor = true;
                    newVelocity.Y = 0.0f;
                }
                else if (velocity.Y < 0)
                {
                    int topTile = (int)(nextPosY.Y / 8);
                    obj.Position = new Vector2(obj.Position.X, (topTile + 1) * 8 - obj.RectangleOffset.Y);
                    newVelocity.Y = 0.0f;
                }
            }
            else
            {

                Vector2 groundPos = currentPos + new Vector2(0, 1);
                if (!CheckCollisionAtPosition(groundPos, w, h))
                {
                    obj.OnFloor = false;
                }
                else
                {
                    try
                    {
                        Tile tile = World.tiles[Math.Min(World.tiles.GetLength(0) - 1, (int)groundPos.X / 8), Math.Min(World.tiles.GetLength(1) - 1, (int)groundPos.Y / 8)];
                        obj.OnTileCollide(tile);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            }
            /*
            Vector2 groundPosition = currentPos + new Vector2(0, 1);
            if (CheckCollisionAtPosition(groundPosition, w, h))
            {
                Tile tile = World.tiles[Math.Min(World.tiles.GetLength(0) - 1, (int)groundPosition.X/8), Math.Min(World.tiles.GetLength(1) - 1, (int)groundPosition.Y/8)];
                obj.OnTileCollide(tile);
            }
            */
            return newVelocity;
        }

        public static bool CheckCollisionAtPosition(Vector2 position, int w, int h)
        {
            return CheckCollisionAtPosition(position, w, h, out _, out _);
        }
        public static bool CheckCollisionAtPosition(Vector2 position, int w, int h, out Vector2? tilePos, out Tile tile)
        {
            int leftTile = Math.Max(0, (int)(position.X / 8));
            int rightTile = Math.Min(World.tiles.GetLength(0) - 1, (int)((position.X + w - 1) / 8));
            int topTile = Math.Max(0, (int)(position.Y / 8));
            int bottomTile = Math.Min(World.tiles.GetLength(1) - 1, (int)((position.Y + h - 1) / 8));


            for (int x = leftTile; x <= rightTile; ++x)
            {
                for (int y = topTile; y <= bottomTile; ++y)
                {
                    Tile currentTile = World.tiles[x, y];
                    if (currentTile == null || !currentTile.Solid) { continue; }
                    Rectangle tileRectangle = new Rectangle(x * 8, y * 8, 8, 8);
                    Rectangle objRectangle = new Rectangle((int)position.X, (int)position.Y, w, h);
                    if (objRectangle.Intersects(tileRectangle))
                    {
                        tilePos = new Vector2(x, y);
                        tile = currentTile;
                        return true; 
                    }
                        
                }
            }



            tilePos = null;
            tile = null;
            return false;
        }
        

       public static bool CheckProjectileCollision(Projectile projectile)
       {
            Vector2 position = projectile.Position + projectile.RectangleOffset;
            int tileX = (int)(position.X / 8);
            int tileY = (int)(position.Y / 8);

            if (tileX < 0 || tileX >= World.tiles.GetLength(0) ||
                tileY < 0 || tileY >= World.tiles.GetLength(1)) return true;

            Tile tile = World.tiles[tileX, tileY];
            return tile != null && tile.Solid;
       }
    }

    public class GameObject : Object
    {

        public LightSource LightSource { get { return lightSource; } set { lightSource = value; } }
        private LightSource lightSource;
        public GameObject() : base()
        {

        }
        
        public virtual void OnHover()
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
        public override void Destroy()
        {
            LightMap.lightSources.Remove(this.LightSource);
            base.Destroy();
        }


        
    }
    public class Object
    {
        public Texture2D Texture
        {
            get
            {
                if (texture == null)
                {
                    if (this is Projectile)
                    {
                        return AssetManager.MissingTexture3;
                    }
                    else if (this is Character)
                    {
                        return AssetManager.MissingTexture5;
                    }
                }
                return texture;
            }
            set
            {
                if (value == null)
                {
                    //throw new System.Exception("Failed to set Texture" + " " + this.GetType().Name);
                    if (this is Projectile)
                    {
                        texture = AssetManager.MissingTexture3;
                        return;
                    }
                    else if (this is Character)
                    {
                        texture = AssetManager.MissingTexture4;
                        return;
                    }
                    else
                    {
                        texture = AssetManager.MissingTexture1;
                        return;
                    }
                }
                texture = value;
                width = texture.Width;
                height = texture.Height;

            }
        }
        public Vector2 GetSize()
        {
            if (animation != null)
            {
                int width = 0;
                int height = 0;

                width = Texture.Bounds.Width / animation.FrameCount;
                height = Texture.Bounds.Height;
                return new Vector2(width, height);
            }
            return new Vector2(width, height);
        }
        protected void SetSize(int x, int y)
        {
            width = x;
            height = y;
        }
        private int width;
        private int height;
        public Vector2 PositionOffset { get { return positionOffset; } set { positionOffset = value; } }
        private Vector2 positionOffset = Vector2.Zero;
        public Vector2 Position { get { return position; } set { position = value; } }
        public Rectangle? SourceRectangle { get { return sourceRectangle; } set { sourceRectangle = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public float Rotation { get{ return rotation; } set { rotation = MathHelper.WrapAngle(value); } }
        public float RotationDegrees { get { return MathHelper.ToDegrees(Rotation); } set { Rotation = MathHelper.ToRadians(value); } }


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

        public Animation Animation { get { return animation; } set { animation = value; } }
        private Animation animation = null;

        public Vector2 RectangleOffset { get { return rectangleOffset; } set { rectangleOffset = value; } }
        private Vector2 rectangleOffset;
        public virtual void Update(GameTime gameTime) { }
        public virtual void Destroy()
        {
            Main.RemoveObject(this);
        }
        public void Resize(float value)
        {
            if (Texture == null)
            {
                Scale = new Vector2(value);
                return;
            }
            Vector2 newScale = new Vector2(value);

            Vector2 size = GetSize();
            Vector2 oldSize = size * this.Scale;
            Vector2 newSize = size * newScale;
            this.Position += (oldSize - newSize) / 2.0f;
            Scale = newScale;

        }

    }
    public static class AssetManager
    {
        public static bool LoadedTextures { get { return loadedTextures; } }
        private static bool loadedTextures = false;
        private static Dictionary<String, Texture2D> textures = new Dictionary<string, Texture2D>();


        public static Texture2D MissingTexture1 = null;
        public static Texture2D MissingTexture2 = null;
        public static Texture2D MissingTexture3 = null;
        public static Texture2D MissingTexture4 = null;
        public static Texture2D MissingTexture5 = null;
        public static Texture2D MissingTexture6 = null;


        static AssetManager()
        {
            if (loadedTextures)
            {
                MissingTexture1 = GetTexture("MissingTexture1");
                MissingTexture2 = GetTexture("MissingTexture2");
                MissingTexture3 = GetTexture("MissingTexture3");
                MissingTexture4 = GetTexture("MissingTexture4");
                MissingTexture5 = GetTexture("MissingTexture5");
                MissingTexture6 = GetTexture("MissingTexture6");
            }
        }
        public static Dictionary<String, Texture2D> GetTextures()
        {
            return textures;
        }
        public static Texture2D GetTexture(string name)
        {
            textures.TryGetValue(name, out var texture);
            /*
            if (texture == null)
            {
                return MissingTexture2;
                //throw new System.Exception("Failed to get texture!");
            }
            */
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
            loadedTextures = true;
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
        private static GraphicsDeviceManager graphics;
        private static SpriteBatch spriteBatch;

        private static List<Object> objects = new List<Object>();


        private List<Object> objectsToUpdate = new List<Object>();
        private List<Object> objectsToDraw = new List<Object>();
        private static List<Object> objectsToRemove = new List<Object>();

        public static Player player;
        private static ProgressBar progressBar;
        
        public static Camera camera;

        private Vector2 cameraPos = Vector2.Zero;

        private QuadTree quadTree;
        public static Texture2D pixel;
        public static Random rand;
        private Button button;

        private Enemy1 enemy;
        private SpriteFont spriteFont;
        private GameObject test;
        private static bool shouldExit = false;
        private static GameObject currentObjectInCursor = null;



        public static Vector2 screenSize;


        public Main()
        {
            Console.WriteLine("Start of Main Construction");
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = true;
            //graphics.IsFullScreen = true;
            //graphics.HardwareModeSwitch = false;

            /*
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 360;
            */
            
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            
            graphics.IsFullScreen = true;
            graphics.HardwareModeSwitch = false;
            
            Window.Title = "Catmeow";
            //graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferMultiSampling = true;

            graphics.ApplyChanges();
            Console.WriteLine("End of Main Construction");
        }

        protected override void Initialize()
        {
            Console.WriteLine("Start of Initialization");
            screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            rand = new Random(67);
            quadTree = new QuadTree(0, new Rectangle(-UInt16.MaxValue, -UInt16.MaxValue, UInt16.MaxValue*2, UInt16.MaxValue*2));
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            test = new GameObject();
            player = new Player(UInt16.MaxValue);
            player.Position = new Vector2(1024 * 8f, 1365.33333f * 8f);
            player.Initialize();
            button = new Button();
            button.Position = new Vector2(100.0f, 100.0f);
            button.Rectangle = new Rectangle(100, 100, 100, 100);
            
            
            // TODO: Add your initialization logic here
            camera = new Camera(graphics);
            Console.WriteLine("End of Initialization");
            base.Initialize();
        }

        protected override void LoadContent()    
        {
            Console.WriteLine("Start of Loading Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetManager.LoadTextures(Content);
            ItemSlot.texture = AssetManager.GetTexture("ItemSlot");
            try { spriteFont = this.Content.Load<SpriteFont>("Assets/Font"); }
            catch (Exception e) { Console.WriteLine(e); }
            player.LoadContent(Content);
            test.Texture = AssetManager.GetTexture("Test");
            test.Origin = test.GetSize()/2.0f;
            player.LoadTextures();
            progressBar = new ProgressBar(pixel);
            progressBar.Position = new Vector2(0, graphics.PreferredBackBufferHeight - 8.0f);
            try
            {                                              
                Console.WriteLine("Setting textures successful!");
            }
            catch (Exception e) { Console.WriteLine(e); }
            player.LayerDepth = 0.01f;
            button.Texture = CreateRectangleTexture(button.Width, button.Height);
            enemy = new Enemy1(AssetManager.GetTexture("Reference"), new Vector2(128, 128), UInt16.MaxValue);
            objects.Add(enemy);
            if (File.Exists("world.femboy"))
                World.LoadWorld("world.femboy");
            else
                World.CreateWorld();
            objects.Add(player);
            objects.Add(button);
            objects.Add(progressBar);
            objects.Add(test);
            // TODO: use this.Content to load your game content here
            Console.WriteLine("End of Loading Content");
        }
        protected override void Update(GameTime gameTime)
        {
            //float rot = test.Rotation % 360.0f;
            test.RotationDegrees += 100.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            if (!this.IsActive) return;
            objectsToDraw.Clear();
            // process inputs -> process behaviors -> calculate velocities -> move entities -> resolve collisions -> render frame  
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || /*Keyboard.GetState().IsKeyDown(Keys.Escape) ||*/ shouldExit)
                Exit();
            // process inputs
            Input.UpdateInput();
            /*
            foreach (var ui in objects.OfType<UIObject>())
            {
                ui.Update(gameTime);            
            }
            */
            // process behaviors
            // calculate velocities
            

            if (button.Pressed)
            {
                player.Position = Vector2.Zero;
                player.Velocity = Vector2.Zero;
            }
            // TODO: Add your update logic here
            quadTree.Clear();
            objectsToDraw = objects.ToList();
            List<ColliderObject> returnObjects = new List<ColliderObject>();
            foreach (var obj in objects.ToList())
            {
                obj.Update(gameTime);
                Rectangle cursorRectangle = new Rectangle((int)Input.MousePosition.X + (int)camera.GetTopLeft().X, (int)Input.MousePosition.Y + (int)camera.GetTopLeft().Y, 1, 1);
                if (obj is GameObject)
                {
                    GameObject gameObject = (GameObject)obj;
                    LightSource light = gameObject.LightSource;

                    if (light != null)
                    {
                        light.x = (int)gameObject.Position.X + (int)gameObject.RectangleOffset.X;
                        light.y = (int)gameObject.Position.Y + (int)gameObject.RectangleOffset.Y;
                        gameObject.LightSource = light;
                    }
                    if (gameObject.Rectangle.Intersects(cursorRectangle))
                    {
                        gameObject.OnHover();
                        currentObjectInCursor = gameObject;
                        currentObjectInCursor.Color = Color.Yellow;
                    }
                    else
                    {
                        gameObject.Color = Color.White;
                    }

                }
                if (obj is Character character)
                {
                    character.PreviousHealth = character.Health;
                }
                if (obj is PhysicsObject)
                {
                    PhysicsObject physicsObject = (PhysicsObject)obj;
                    // move entities
                    if (!physicsObject.PhysicsDisabled)
                    {
                        bool wasOnFloor = physicsObject.OnFloor;
                        Vector2 velocity = physicsObject.Velocity;
                        if (physicsObject.AffectedByGravity && (!physicsObject.OnFloor || velocity.Y < 0.0f))
                        {
                            velocity.Y += (float)((98.1 * 6.7) * gameTime.ElapsedGameTime.TotalSeconds);
                        }
                        else if (wasOnFloor)
                        {
                            velocity.Y = 0f;
                        }
                        if (physicsObject.CanCollideWithTiles)
                        {
                            physicsObject.Velocity = Collision.ResolveCollision(physicsObject, velocity, gameTime);
                        }
                        else
                        {
                            physicsObject.Velocity = velocity;
                        }
                            physicsObject.Position += physicsObject.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
             
                    }

                }
                // resolve collision
                
                if (obj is ColliderObject colliderObject)
                { 
                    if (quadTree.Bounds.Contains(colliderObject.Rectangle))
                    {
                        quadTree.Insert(colliderObject);
                    }
                    else
                    {
                        if (colliderObject != player)
                            RemoveObject(colliderObject);
                    }
                    returnObjects.Clear();
                    quadTree.Retrieve(returnObjects, colliderObject.Rectangle);
                }

                if (obj is ColliderObject colliderObject1)
                {
                    foreach (ColliderObject colliderObject2 in returnObjects)
                    {
                        if (colliderObject1 is Projectile projectile1)
                        {
                            if (colliderObject2 is Projectile projectile2)
                            {
                                continue;
                            }
                            if (colliderObject1 == projectile1.WhoShotTheProjectile)
                            {
                                continue;
                            }

                        }
                        if (colliderObject1.Rectangle.Intersects(colliderObject2.Rectangle))
                        {
                            colliderObject1.OnCollide(colliderObject2);
                            colliderObject2.OnCollide(colliderObject1);
                        }
                    }
                }
                /*
                if (obj is ColliderObject colliderObject1)
                {
                    Projectile projectile1 = null;
                    if (colliderObject1 is Projectile)
                    {
                        projectile1 = (Projectile)colliderObject1;
                    }

                    foreach (ColliderObject colliderObject2 in objects.OfType<ColliderObject>())
                    {
                        if (colliderObject1 == colliderObject2) continue;
                        Projectile projectile2 = null;
                        if (colliderObject2 is Projectile)
                        {
                            projectile2 = (Projectile)colliderObject2;
                        }
                        if (projectile1 == projectile2) continue;
                        if (projectile1 != null)
                        {
                            if (projectile2 != null)
                            {
                                Rectangle a = projectile1.Rectangle;
                                Rectangle b = projectile2.Rectangle;
                                Vector2 aPos = projectile1.Position;
                                Vector2 bPos = projectile2.Position;

                                if (a.Intersects(b))
                                {
                                    projectile1.OnCollide(projectile2);
                                    projectile2.OnCollide(projectile1);
                                }

                                else if (b.Intersects(a))
                                {
                                    projectile2.OnCollide(projectile1);
                                    projectile1.OnCollide(projectile2);

                                }
                            }
                            else
                            {
                                Rectangle a = projectile1.Rectangle;
                                Rectangle b = colliderObject2.Rectangle;
                                Vector2 aPos = projectile1.Position;
                                Vector2 bPos = colliderObject2.Position;
                                bool selfHit = projectile1.WhoShotTheProjectile == colliderObject2;
                                if (a.Intersects(b))
                                {
                                    if (!selfHit)
                                    {
                                        projectile1.OnCollide(colliderObject2);
                                        colliderObject2.OnCollide(projectile1);
                                    }
                                }

                                else if (b.Intersects(a))
                                {
                                    if (!selfHit)
                                    {
                                        colliderObject2.OnCollide(projectile1);
                                        projectile1.OnCollide(colliderObject2);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Rectangle a = colliderObject1.Rectangle;
                            Rectangle b = colliderObject2.Rectangle;
                            Vector2 aPos = colliderObject1.Position;
                            Vector2 bPos = colliderObject2.Position;

                            if (a.Intersects(b))
                            {
                                colliderObject1.OnCollide(colliderObject2);
                                colliderObject2.OnCollide(colliderObject1);
                            }

                            else if (b.Intersects(a))
                            {
                                colliderObject2.OnCollide(colliderObject1);
                                colliderObject1.OnCollide(colliderObject2);

                            }
                        }

                    }
                }








                */











                /*
                if (obj is ColliderObject)
                {
                    ColliderObject colliderObject = (ColliderObject)obj;

               
                    if (!colliderObject.CollisionDisabled)
                    {
                        Projectile projectile = null;
                        if (colliderObject is Projectile)
                        {
                            projectile = (Projectile)colliderObject;
                        }
                        // I WILL IMPLEMENT THE QUAD TREE SOON, FOR NOW, I WILL DO THE WORST COLLISION DETECTION ALGORITHM FOR NOW
                        foreach (var obj2 in objects.OfType<ColliderObject>())
                        {
                            ColliderObject obj1 = (ColliderObject)obj;
                            if (obj1 == obj2) { continue; }

                            Rectangle a = obj1.Rectangle;
                            Rectangle b = obj2.Rectangle;
                            Vector2 aPos = obj1.Position;
                            Vector2 bPos = obj2.Position;
                            if (projectile != null)
                            {
                                if (projectile.WhoShotTheProjectile == obj2) continue;
                                if (a.Intersects(b))
                                {
                                    obj1.OnCollide(obj2);
                                    obj2.OnCollide(obj1);
                                    objectsToRemove.Add(obj1);
                                }
                            }
                            else
                            {
                                if (a.Intersects(b))
                                {
                                    obj1.OnCollide(obj2);
                                    Rectangle intersect = Rectangle.Intersect(a, b);
                                    aPos.X -= intersect.Width / 2;
                                    aPos.Y -= intersect.Height / 2;
                                    bPos.X += intersect.Width / 2;
                                    bPos.Y += intersect.Height / 2;

                                }

                                else if (b.Intersects(a))
                                {
                                    obj2.OnCollide(obj1);
                                    Rectangle intersect = Rectangle.Intersect(b, a);
                                    aPos.X += intersect.Width / 2;
                                    aPos.Y += intersect.Height / 2;
                                    bPos.X -= intersect.Width / 2;
                                    bPos.Y -= intersect.Height / 2;

                                }
                            }
                            obj1.Position = aPos;
                            obj2.Position = bPos;

                        }
                    }
                }
                */



                Rectangle rect = obj.Rectangle;
                Vector2 pos = obj.Position;
                Vector2 offset = obj.RectangleOffset;
                rect.X = (int)(pos.X + offset.X);
                rect.Y = (int)(pos.Y + offset.Y);

                obj.Rectangle = rect;
                obj.Position = pos;

                if (obj is not UIObject)
                {

                
                    if (obj.Texture != null)
                    {
                        if (
                                (obj.Position.X + obj.Texture.Width < camera.GetTopLeft().X) ||
                                (obj.Position.Y + obj.Texture.Height < camera.GetTopLeft().Y) ||
                                (obj.Position.X > camera.GetTopLeft().X + graphics.PreferredBackBufferWidth) ||
                                (obj.Position.Y > camera.GetTopLeft().Y + graphics.PreferredBackBufferHeight)
                                )
                        {

                            objectsToDraw.Remove(obj);
                        }
                    }
                }
            }
            
            //camera.MoveToward(cameraPos + new Vector2(16, 16), (float)gameTime.ElapsedGameTime.TotalMilliseconds, 1.0f);
            
            Animation.UpdateAnimations(gameTime);
            cameraPos = player.Position;
            camera.MoveToward(cameraPos + new Vector2(16, 16), (float)gameTime.ElapsedGameTime.TotalMilliseconds, 1.0f);


            foreach (var obj in objectsToRemove.ToList())
            { 
                objects.Remove(obj);
                objectsToRemove.Remove(obj);
            }

            base.Update(gameTime);

        }

        private int virtualWidth = 640;
        private int virtualHeight = 360;
        private float time = 0.0f;
        private int fps = 0;
        private bool gameObjectsHidden = false;
        protected override void Draw(GameTime gameTime)
        {
            if (Input.IsKeyPressed(Keys.F12))
                gameObjectsHidden = !gameObjectsHidden;
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            Matrix transform = Matrix.CreateTranslation(-(float)Math.Round(camera.GetTopLeft().X), -(float)Math.Round(camera.GetTopLeft().Y), 0);//* Matrix.CreateScale(0.5f);



            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointWrap, transformMatrix: transform);
            int startX = Math.Max(0, (int)camera.GetTopLeft().X / 8);
            int startY = Math.Max(0, (int)camera.GetTopLeft().Y / 8);
            int endX = Math.Min(World.tiles.GetLength(0), (int)(camera.GetTopLeft().X + graphics.PreferredBackBufferWidth) / 8 + 1);
            int endY = Math.Min(World.tiles.GetLength(1), (int)(camera.GetTopLeft().Y + graphics.PreferredBackBufferHeight) / 8 + 1);
            for (int x = startX; x < endX; ++x)
            {
                for (int y = startY; y < endY; ++y)
                {
                    Tile tile = World.tiles[x, y];
                    if (tile == null) { continue; }
                    if (tile.Texture == null) { continue; }
                    Vector2 pos = new Vector2(x * 8.0f, y * 8.0f);
                    /*if (
                        (pos.X + 8.0f < camera.GetTopLeft().X) ||
                        (pos.Y + 8.0f < camera.GetTopLeft().Y) ||
                        (pos.X > camera.GetTopLeft().X + graphics.PreferredBackBufferWidth) ||
                        (pos.Y > camera.GetTopLeft().Y + graphics.PreferredBackBufferHeight)
                        )
                    {
                        continue;
                    }*/
                    spriteBatch.Draw(tile.Texture, pos, null, tile.color, 0.0f ,Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);
                }
            }

            foreach (GameObject obj in objectsToDraw.OfType<GameObject>())
            {
                if (!gameObjectsHidden)
                {
                    obj.Draw(spriteBatch, gameTime);

                    if (obj is Character character)
                    {
                        if (!player.UIHidden)
                        {
                            float previousHealth = character.PreviousHealth;
                            HealthBar.DrawHealthBar(spriteBatch, character.Position.X, character.Position.Y, (int)character.GetSize().X, (int)character.GetSize().Y, character.Health, character.MaxHealth, ref previousHealth, ref character.healthBarTimer, ref character.healthBarOpacity, ref character.healthBarStartedCountingDown, ref character.healthBarFading, gameTime);
                            character.PreviousHealth = previousHealth;

                        }
                    }
                    if (obj.Hidden || obj.Texture == null) { continue; }


                    spriteBatch.Draw(
                        obj.Texture,
                        obj.Position + obj.PositionOffset,
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
            





           





            //DrawColliders(spriteBatch);
            CombatText.DrawTexts(spriteBatch, spriteFont, gameTime);
            spriteBatch.End();



            spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: transform);
            spriteBatch.Draw(pixel, new Rectangle((int)camera.GetTopLeft().X, (int)camera.GetTopLeft().Y, graphics.PreferredBackBufferWidth + 1, graphics.PreferredBackBufferHeight + 1), Color.Black * 0.8f);
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, null, null, null, transform);

            foreach (var light in LightMap.lightSources)
            {
                if (light.color.A == 0) continue;
                Vector2 origin = new Vector2(LightSource.texture.Width / 2f, LightSource.texture.Height / 2f);
                Color tint = light.color * 0.75f;
                spriteBatch.Draw(LightSource.texture, new Vector2(light.x, light.y), null, tint, 0f, origin, light.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();


            /*
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointWrap, null, null, null, transform);
            quadTree.DrawRectangleOutline(spriteBatch, Color.Red);
            spriteBatch.End();
            */
            if (!player.UIHidden)
            {

                spriteBatch.Begin(samplerState: SamplerState.PointWrap);
                //player.DrawUI(spriteBatch);
                DrawUI(spriteBatch);
                for (int i = 0; i < player.itemSlots.Length; i++)
                {
                    //spriteBatch.Draw(ItemSlot.texture, player.itemSlots[i].Position, Color.White);
                    if (player.itemSlots[i].item == null) continue;
                    Texture2D tex = player.itemSlots[i].item.icon ?? player.itemSlots[i].item.Texture;
                    //spriteBatch.Draw(tex, player.itemSlots[i].Position + new Vector2(2, 2), Color.White);

                    
                    ItemSlot itemSlot = player.itemSlots[i];
                    spriteBatch.Draw(
                    itemSlot.item.icon ?? itemSlot.item.Texture,
                    itemSlot.Position + new Vector2(2, 2),
                    null,
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    itemSlot.Scale,
                    0.0f,
                    0.0f
                    );
                    /*
                    if (player.itemSlots[i].item == null) continue;
                    
                    spriteBatch.Draw(   
                    (itemSlot.item.icon != null) ? itemSlot.item.icon : itemSlot.item.Texture,
                    itemSlot.Position + new Vector2(2, 2),
                    itemSlot.item.Color
                    );
                    */
                }

                //DrawCollidersOutline<UIObject>(spriteBatch);
                //DrawColliders<UIObject>(spriteBatch);
                if (player.currentItemInCursor != null)
                {
                    if (player.currentItemInCursor.Texture != null)
                    {
                        Texture2D tex = player.currentItemInCursor.icon != null ? player.currentItemInCursor.icon : player.currentItemInCursor.Texture;
                        spriteBatch.Draw(tex, player.currentItemInCursor.Position, Color.White);
                    }
                }
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (time >= 1.0f)
                {
                    fps = (int)(1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
                    progressBar.Value = (fps / 60) * 100;
                    time = 0.0f;
                }

                spriteBatch.DrawString(spriteFont, fps.ToString(), new Vector2(0, graphics.PreferredBackBufferHeight - 16.0f), Color.Magenta);
                spriteBatch.DrawString(spriteFont, player.Position.ToPoint().ToString(), new Vector2(0, graphics.PreferredBackBufferHeight - 32.0f), Color.Magenta);
                spriteBatch.DrawString(spriteFont, player.Velocity.ToPoint().ToString(), new Vector2(0, graphics.PreferredBackBufferHeight - 48.0f), Color.Magenta);
                spriteBatch.End();



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

            


        private void DrawColliders(SpriteBatch spriteBatch)
        {
            foreach (var obj in objectsToDraw)
            {
                if (obj is UIObject) continue;
                var collider = obj.Rectangle;
                if (collider.Size == Point.Zero || obj.Hidden) { continue; }

                spriteBatch.Draw(pixel, collider, new Color(255, 0, 0, 67));
            }
        }
        private void DrawColliders<T>(SpriteBatch spriteBatch)
        {
            foreach (var obj in objectsToDraw)
            {
                if (obj is not T)
                    continue;

                var collider = obj.Rectangle;
                if (collider.Size == Point.Zero || obj.Hidden) { continue; }

                spriteBatch.Draw(pixel, collider, new Color(255, 0, 0, 64));
            }
        }

        public static void Quit()
        {
            shouldExit = true;
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



            int outline = 2;    
            byte a = color.A;
            Color cornerColor = new Color((byte)(color.R * 0.5), (byte)(color.G * 0.5), (byte)(color.B * 0.5), (byte)255);
            spriteBatch.Draw(tex, new Rectangle(target.X + outline, target.Y, target.Width - outline*2, outline), color);
            spriteBatch.Draw(tex, new Rectangle(target.X, target.Y - outline + target.Height, target.Height - outline*2, outline), color);
            spriteBatch.Draw(tex, new Rectangle(target.X - outline + target.Width, target.Y + target.Height, target.Width - outline * 2, outline), color);
            spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y + outline, target.Height - outline * 2, outline), color);

            spriteBatch.Draw(tex, new Rectangle(target.X, target.Y, outline, outline), cornerColor);
            spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y, outline, outline), cornerColor);
            spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y + target.Height, outline, outline), cornerColor);
            spriteBatch.Draw(tex, new Rectangle(target.X, target.Y + target.Height, outline, outline), cornerColor);
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
            ArgumentNullException.ThrowIfNull(obj);
            if (objects.Contains(obj)) return;
            objects.Add(obj);
        }

        public static void AddObjects(Object[] obj)
        {
            objects.AddRange(obj.ToList());
        }

        public static void RemoveObject(Object obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            if (!objects.Contains(obj)) return;
            objectsToRemove.Add(obj);
        }




    }
}
