using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
namespace TestScaleRotation
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D vaisseau_Sprite;
        Vector2 position;
        Vector2 origImg;
        float speed = 200.0f;
        float angle=270.0f;
        float scale = 1.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            Debug.WriteLine("..Loading Game1..OK !! ");
        }

       
        protected override void Initialize()
        {
            vaisseau_Sprite = Content.Load<Texture2D>("ship");
            font = Content.Load<SpriteFont>("kenvector_future");

            origImg = new Vector2(vaisseau_Sprite.Width / 2, vaisseau_Sprite.Height / 2);
            position = new Vector2(300, 320);
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                angle = (angle - 90.0f * dt) % 360;
                Console.WriteLine("angle : {0}", angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                angle = (angle + 90.0f * dt) % 360;
                Console.WriteLine("angle : {0}", angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position.X += (float)Math.Cos(MathHelper.ToRadians(angle)) * speed * dt;
                position.Y += (float)Math.Sin(MathHelper.ToRadians(angle)) * speed * dt;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                scale += dt;
                if (scale > 7.0f) scale = 7.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                scale -= dt;
                if (scale < 0.2f) scale = 0.2f;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            //Vector2 origImg = new Vector2(vaisseau_Sprite.Width / 2, vaisseau_Sprite.Height / 2);
            // la rotation et le scale en temps reel fonctionnent correctement
            // param1 : la texture
            // param2 : la position
            // param3 : null
            // param4 : Color.White
            // param5 : la rotation
            // param6 : le scale en X et Y
            // param7 : l'effet a appliquer sur le sprite
            // param8 : le layer

            spriteBatch.Draw(vaisseau_Sprite, new Vector2(position.X, position.Y), null, Color.White, MathHelper.ToRadians(angle), origImg, new Vector2(scale, scale), SpriteEffects.None,1.0f);
            spriteBatch.DrawString(font, "angle : " + ((int)angle).ToString() + "\nX : " + ((int)position.X).ToString() + "\nY : " + ((int)position.Y).ToString() + "\nScale : " + scale.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
