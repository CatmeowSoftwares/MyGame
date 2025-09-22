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
                return mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
            }
            else if (mouseButton == MouseButton.Right)
            {
                return mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
            }
            else if (mouseButton == MouseButton.Middle)
            {
                return mouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released;
            }
            else if (mouseButton == MouseButton.X1)
            {
                return mouseState.XButton1 == ButtonState.Pressed && previousMouseState.XButton1 == ButtonState.Released;
            }
            else if (mouseButton == MouseButton.X2)
            {
                return mouseState.XButton2 == ButtonState.Pressed && previousMouseState.XButton2 == ButtonState.Released;
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

            if (movePercentage != 1.0f)
                differenceInPosition *= movePercentage;

            var fractionOfPassedTime = deltaTimeInMs / 10;

            Center += differenceInPosition * fractionOfPassedTime;

            if ((target - Center).Length() < movePercentage || movePercentage != 1.0f)  
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





        public virtual void OnCollide()
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




        private int direction = 1;
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



        private GameObject reference;
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


            reference = new GameObject();
            reference.Position = this.Position;
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


            UpdateMusic(gameTime);
            if (Input.IsKeyPressed(Keys.F11)) uiHidden = !uiHidden;
            if (Input.IsKeyDown(Keys.A))
            {
                Vector2 velocity = Velocity;
                velocity.X -= (float)(MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                velocity.X = Math.Max(-maxSpeed, velocity.X);
                Velocity = velocity;
                playerState = PlayerState.Walking;
            }
            else if (Input.IsKeyDown(Keys.D))
            {
                Vector2 velocity = Velocity;
                velocity.X += (float)(MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                velocity.X = Math.Min(maxSpeed, velocity.X);
                Velocity = velocity;
                playerState = PlayerState.Walking;
            }
            else
            {
                Vector2 velocity = Velocity;
                velocity.X -= (float)(MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds) * direction;
                velocity.X = (direction == 1) ? Math.Max(0, velocity.X) : Math.Min(0, velocity.X);
                Velocity = velocity;
                playerState = PlayerState.Idle;
            }
            if (Input.IsKeyPressed(Keys.Space))
            {
                Vector2 velocity = Velocity;
                velocity.Y = -400;
                Velocity = velocity;

            }


            if (attacking || aiming)
            {
                direction = Input.MousePosition.X + Main.camera.GetTopLeft().X < Position.X ? -1 : 1;
            }
            else
            {
                if (Velocity.X < 0.0f) direction = -1;
                else if (Velocity.X > 0.0f) direction = 1; ;
            }
            


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
            Vector2 facingOffset = (direction == -1) ? new Vector2(-handOffset.X, handOffset.Y) : handOffset;
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

            Vector2 headOffset = (direction == -1) ? bodyPartOffsetsLeft[(int)Part.Head] : bodyPartOffsetsRight[(int)Part.Head];
            head.Position = Position + headOffset;
            Vector2 torsoOffset = (direction == -1) ? bodyPartOffsetsLeft[(int)Part.Torso] : bodyPartOffsetsRight[(int)Part.Torso];
            torso.Position = Position + torsoOffset;
            Vector2 leftHandOffset = (direction == -1) ? bodyPartOffsetsLeft[(int)Part.LeftHand] : bodyPartOffsetsRight[(int)Part.LeftHand];
            leftHand.Position = Position + leftHandOffset;
            Vector2 rightHandOffset = (direction == -1) ? bodyPartOffsetsLeft[(int)Part.RightHand] : bodyPartOffsetsRight[(int)Part.RightHand];
            rightHand.Position = Position + rightHandOffset;
            Vector2 legsOffset = (direction == -1) ? bodyPartOffsetsLeft[(int)Part.Legs] : bodyPartOffsetsRight[(int)Part.Legs];
            legs.Position = Position + legsOffset;


            head.Effects = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            torso.Effects = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            leftHand.Effects = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            rightHand.Effects = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            legs.Effects = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;


            if (currentlyHeldItem != null)
            {
                currentlyHeldItem.Position = Position + new Vector2(16, 12);
                currentlyHeldItem.Effects = (direction == -1) ? SpriteEffects.FlipVertically : SpriteEffects.None;
                aiming = currentlyHeldItem.holdStyle == Item.HoldStyle.Aim;
                if (direction == 1)
                {
                    //currentlyHeldItem.Position = handPosition; //+ currentlyHeldItem.HandPos;

                }
                else if (direction == -1)
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

                }




                if (currentlyHeldItem.clickType == Item.ClickType.Press)
                {
                    if (Input.IsMouseButtonPressed(Input.MouseButton.Left))
                    {
                        currentlyHeldItem.OnUse();
                        if (currentlyHeldItem.useStyle == Item.UseStyle.None)
                        {

                        }
                        else if (currentlyHeldItem.useStyle == Item.UseStyle.Aim)
                        {

                        }
                    }
                }
                else if (currentlyHeldItem.clickType == Item.ClickType.Hold)
                {
                    if (Input.IsMouseButtonDown(Input.MouseButton.Left))
                    {
                        currentlyHeldItem.OnUse();
                    }
                }
                
            }
            else
            {
                aiming = false;
            }


            if (direction == 1)
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
            else if (direction == -1)
            {
                head.LayerDepth = 0.15f;
                leftHand.LayerDepth = 0.2f;
                rightHand.LayerDepth = 0.15f;
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
                direction = 1;
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
    
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentlyHeldItem != null && currentlyHeldItem.Texture != null)
            {
                spriteBatch.Draw(
                        currentlyHeldItem.Texture,
                        currentlyHeldItem.Position,
                        currentlyHeldItem.SourceRectangle,
                        currentlyHeldItem.Color,
                        currentlyHeldItem.Rotation,
                        currentlyHeldItem.Origin,
                        currentlyHeldItem.Scale,
                        currentlyHeldItem.Effects,
                        currentlyHeldItem.LayerDepth
                        );
                
            }
            base.Draw(spriteBatch, gameTime);
        }

        public Vector2 GetCenter()
        {
            return Position + new Vector2(16, 16);
        }
        private float LookAt(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }

        public override void Destroy(ref List<Object> objects)
        {

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
        public void UpdateSlot()
        {
            // idk
        }

    }

    public class Bullet : Projectile
    {
        public Bullet(Vector2 position, float rotation)
        {
            if (AssetManager.LoadedTextures)
            {
                Texture = AssetManager.GetTexture("Bullet");
            }
            this.Position = position;
            this.Rotation = rotation;
            Vector2 origin = this.Origin;
            origin.Y = GetSize().Y / 2;
            this.Origin = origin;
            this.Velocity = Vector2.Transform(new Vector2(1000, 0), Matrix.CreateRotationZ(rotation));

        }
        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
    }
    public class Projectile : PhysicsObject
    {
        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
        public override void OnCollide()
        {
            Main.RemoveObject(this);
            base.OnCollide();
        }
    }

    public class Sword : BaseSword
    {
        public Sword()
        {
            if (AssetManager.LoadedTextures)
            {
                icon = AssetManager.GetTexture("SwordIcon");
                Texture = AssetManager.GetTexture("Sword");
            }
        }
    }
    public class Pistol : BaseGun
    {
        public Pistol()
        {
            if (AssetManager.LoadedTextures)
            {
                icon = AssetManager.GetTexture("GunIcon");
                Texture = AssetManager.GetTexture("Gun");
            }
            Vector2 origin = this.Origin;
            origin.Y = GetSize().Y / 2;
            this.Origin = origin;
            HandPos = new Vector2(2, 11);
            useStyle = UseStyle.Aim;
            holdStyle = HoldStyle.Aim;
            clickType = ClickType.Press;
        }
        public override void OnUse()
        {

            Bullet bullet = new Bullet(Position, Rotation);



            Main.AddObject(bullet);
            base.OnUse();
        }
    }


    public class BaseSword : Item
    {

    }

    public class BaseGun : Item
    {

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
    public class Item : GameObject 
    {
        public string name { get; private set; }
        public string description { get; private set; }

        public string customDescription = "";

        public bool consumable = false;
        public int amount = 1;

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
        }

        public enum HoldStyle
        {
            None,
            Front,
            Aim,
        }


        public UseStyle useStyle = UseStyle.None;
        public HoldStyle holdStyle = HoldStyle.None;
        public ClickType clickType = ClickType.None;


        public virtual void OnUse()
        {
            
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
        public virtual void SetStats()
        {
            name = "";
            icon = null;

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

            if (rectangle.Contains(Input.mouseState.Position))
            {
                hovering = true;
                down = Input.mouseState.LeftButton == ButtonState.Pressed;
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
        
        public virtual void OnHover()
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        
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
            return Texture.Bounds.Size.ToVector2();
            return Texture.Bounds.Size
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint().ToVector2().ToPoint()
                .ToVector2()

                ;
        }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Rectangle? SourceRectangle { get { return sourceRectangle; } set { sourceRectangle = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public float Rotation { get{ return rotation; } set { rotation = value; } }
        public float RotationDegrees { get { return MathHelper.ToDegrees(rotation); } set { rotation = MathHelper.ToRadians(value); } }


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
        public virtual void Destroy(ref List<Object> objects)
        {

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
        private static GraphicsDeviceManager _graphics;
        private static SpriteBatch spriteBatch;

        private static List<Object> objects = new List<Object>();


        private List<Object> objectsToUpdate = new List<Object>();
        private List<Object> objectsToDraw = new List<Object>();
        private static List<Object> objectsToRemove = new List<Object>();

        private static Player player;
        private static ProgressBar progressBar;
        
        public static Camera camera;

        private Vector2 cameraPos = Vector2.Zero;


        private static Texture2D pixel;

        private Button button;


        private SpriteFont spriteFont;
        
        private QuadTree<Object>.Node quadTreeRoot;

        private GameObject test;
        private static bool shouldExit = false;
        private static GameObject currentObjectInCursor = null;
        public Main()
        {
            Console.WriteLine("Start of Main Construction");
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            //_graphics.IsFullScreen = true;
            //_graphics.HardwareModeSwitch = false;
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 360;
            Window.Title = "Catmeow";
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferMultiSampling = true;

            _graphics.ApplyChanges();
            Console.WriteLine("End of Main Construction");
        }

        protected override void Initialize()
        {
            Console.WriteLine("Start of Initialization");
            test = new GameObject();
            player = new Player();
            player.Initialize();
            quadTreeRoot.rectangle = new Rectangle(Int32.MinValue, Int32.MinValue, Int32.MaxValue, Int32.MaxValue);
            button = new Button();
            button.Position = new Vector2(100.0f, 100.0f);
            button.Rectangle = new Rectangle(100, 100, 100, 100);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            progressBar = new ProgressBar(CreateRectangleTexture(1, 1));
            progressBar.Position = new Vector2(0, _graphics.PreferredBackBufferHeight - 8.0f);
            // TODO: Add your initialization logic here
            camera = new Camera(_graphics);
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
            try
            {                                              
                Console.WriteLine("Setting textures successful!");
            }
            catch (Exception e) { Console.WriteLine(e); }
            player.LayerDepth = 0.01f;
            button.Texture = CreateRectangleTexture(button.Width, button.Height);
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
            
            if (!this.IsActive) return;
            objectsToDraw.Clear();
            // process inputs -> process behaviors -> calculate velocities -> move entities -> resolve collisions -> render frame  
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || shouldExit)
                Exit();
            // process inputs
            Input.UpdateInput();
            player.Update(gameTime);
            Animation.UpdateAnimations(gameTime);
            foreach (var ui in objects.OfType<UIObject>())
            {
                ui.Update(gameTime);            
            }
            // process behaviors
            // calculate velocities
            

            if (button.Pressed)
            {
                player.Position = Vector2.Zero;
                player.Velocity = Vector2.Zero;
            }
            // TODO: Add your update logic here
            
            objectsToDraw = objects.ToList();
            foreach (var obj in objects)
            {
                Rectangle cursorRectangle = new Rectangle((int)Input.MousePosition.X, (int)Input.MousePosition.Y, 1, 1);
                if (obj is GameObject)
                {
                    GameObject gameObject = (GameObject)obj;
                    if (gameObject.Rectangle.Intersects(cursorRectangle))
                    {
                        gameObject.OnHover();
                        currentObjectInCursor = gameObject;
                    }

                }
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
                    if (!physicsObject.CollisionDisabled)
                    {
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
                }

                Rectangle rect = obj.Rectangle;
                Vector2 pos = obj.Position;
                Vector2 offset = obj.RectangleOffset;
                rect.X = (int)(pos.X + offset.X);
                rect.Y = (int)(pos.Y + offset.Y);

                obj.Rectangle = rect;
                obj.Position = pos;

                if (obj is UIObject)
                {
                    continue;
                }
                if (obj.Texture != null)
                {
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
            }


            cameraPos = player.Position;
            camera.MoveToward(cameraPos + new Vector2(16, 16), (float)gameTime.ElapsedGameTime.TotalMilliseconds, 1.0f);


            foreach (var obj in objectsToRemove.ToList())
            {
                obj.Destroy(ref objects);
                objects.Remove(obj);
                objectsToRemove.Remove(obj);
            }

            base.Update(gameTime);

        }

        private int virtualWidth = 640;
        private int virtualHeight = 360;
        private float time = 0.0f;
        private int fps = 0;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            Matrix transform = Matrix.CreateTranslation(-camera.GetTopLeft().X, -camera.GetTopLeft().Y, 0);
            spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointWrap, transformMatrix: transform);


            foreach (GameObject obj in objectsToDraw.OfType<GameObject>())
            {
                obj.Draw(spriteBatch, gameTime);
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
            
                //DrawColliders(spriteBatch);
                //DrawCollidersOutline<UIObject>(spriteBatch);


                spriteBatch.End();


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
                        spriteBatch.Draw(player.currentItemInCursor.Texture, player.currentItemInCursor.Position, Color.White);
                    }
                }
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (time >= 1.0f)
                {
                    fps = (int)(1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
                    progressBar.Value = fps / 60;
                    time = 0.0f;
                }

                spriteBatch.DrawString(spriteFont, fps.ToString(), new Vector2(0, _graphics.PreferredBackBufferHeight - 16.0f), Color.Magenta);
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

                spriteBatch.Draw(pixel, collider, new Color(255, 0, 0, 64));
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

            var sourceCorner = new Rectangle(0, 0, 6, 6);
            var sourceEdge = new Rectangle(6, 0, 4, 6);
            var sourceCenter = new Rectangle(6, 6, 4, 4);


            int outline = 2;    
            byte a = color.A;
            Color cornerColor = new Color((byte)(color.R * 0.5), (byte)(color.G * 0.5), (byte)(color.B * 0.5), (byte)255);
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
