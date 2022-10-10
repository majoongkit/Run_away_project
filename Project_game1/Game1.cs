using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project_game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D bg;
        Texture2D bg2;
        AnimatedTexture player;
        Vector2 cameraPos = Vector2.Zero;
        Vector2 bgPos = Vector2.Zero;
        Vector2 bgPos2 = Vector2.Zero;
        Vector2 playerPos = new Vector2(0, 490);
        Vector2 scroll_factor = new Vector2(1.0f, 1);

        bool isJumping;
        bool isGrounded;
        bool isGameOver;

        bool personHit;

        public int jumpSpeed;
        int force;

        Texture2D syringe;
        Vector2[] syringePosition = new Vector2[4];
        int[] syringePos = new int[4];

        Texture2D waterbottle;
        Vector2[] waterbottPosition = new Vector2[6];
        int[] waterPos = new int[6];

        Texture2D cloud;
        //Texture2D cloud2;
        //Texture2D cloud3;
        Vector2[] scaleCloud;
        Vector2[] cloudPos;
        int[] speed;

        Random r = new Random();
        

        Texture2D key;
        Vector2 keyPosition = new Vector2();
        int keyPos = new int();

        Texture2D evidence;
        Vector2[] evidencePosition = new Vector2[5];
        int[] eviPos = new int[5];

        //Random rand = new Random();

        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            player = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 1.0f);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            jumpSpeed = -10; 
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            RestartGame();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //bg = Content.Load<Texture2D>("BG4");
            //bg2 = Content.Load<Texture2D>("BG3");
            player.Load(Content, "player_walk", 6, 2, 24);

            syringe = Content.Load<Texture2D>("syringe");
            waterbottle = Content.Load<Texture2D>("waterbottle");

            cloud = Content.Load<Texture2D>("Clound_1_fix_again");
            cloudPos = new Vector2[2];
            scaleCloud = new Vector2[2];
            speed = new int[2];
            for (int i = 0; i < 2; i++)
            {
                cloudPos[i].Y = r.Next(0, graphics.GraphicsDevice.Viewport.Height - cloud.Height);
                scaleCloud[i].X = r.Next(1, 2);
                scaleCloud[i].Y = 0; /*scaleCloud[i].X;*/
                speed[i] = r.Next(2, 3);
            }

            key = Content.Load<Texture2D>("evidence1");
            evidence = Content.Load<Texture2D>("evidence2");


            // TODO: use this.Content to load your game content here

            //for (int i = 0; i < 5; i++)
            //{
            //    syringePosition[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - syringe.Width / 1);
            //    syringePosition[i].Y = 450;
            //}

            /*
            for (int i =0; i < 7; i++)
            {
                waterbottPosition[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - waterbottle.Width / 1);
                waterbottPosition[i].Y = 450;
            }
            */

            waterbottPosition[0].X = 300;
            waterbottPosition[0].Y = 450;

            waterbottPosition[1].X = 950;
            waterbottPosition[1].Y = 450;

            waterbottPosition[2].X = 1500;
            waterbottPosition[2].Y = 450;

            waterbottPosition[3].X = 2600;
            waterbottPosition[3].Y = 455;

            waterbottPosition[4].X = 2800;
            waterbottPosition[4].Y = 455;

            waterbottPosition[5].X = 3000;
            waterbottPosition[5].Y = 450;

            syringePosition[0].X = 600;
            syringePosition[0].Y = 450;

            syringePosition[1].X = 1300;
            syringePosition[1].Y = 450;

            syringePosition[2].X = 1900;
            syringePosition[2].Y = 450;

            syringePosition[3].X = 2200;
            syringePosition[3].Y = 450;
 

            evidencePosition[0].X = 750;
            evidencePosition[0].Y = 470;

            evidencePosition[1].X = 1250;
            evidencePosition[1].Y = 450;

            evidencePosition[2].X = 2100;
            evidencePosition[2].Y = 450;

            evidencePosition[3].X = 2500;
            evidencePosition[3].Y = 455;

            evidencePosition[4].X = 2950;
            evidencePosition[4].Y = 455;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            for (int i = 0; i < 2; i++)
            {
                cloudPos[i].X = cloudPos[i].X + speed[i];
                if (cloudPos[i].X > graphics.GraphicsDevice.Viewport.Width)
                {
                    cloudPos[i].X = r.Next(0, graphics.GraphicsDevice.Viewport.Height - cloud.Height);
                    cloudPos[i].Y = 0;
                    scaleCloud[i].X = scaleCloud[i].X;
                    scaleCloud[i].Y = r.Next(1, 2);
                }

            }


            player.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

            if(!isJumping)
            {
                isGrounded = true;
            }

            if (isJumping == true && force < 0)
            {
                isJumping = false;
            }

            if (isJumping == true)
            {
                jumpSpeed = -10;
                force -= 5;
                isGrounded = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && isGrounded)
            {
                isJumping = true;
                isGrounded = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && isGameOver == true)
            {
                //playerPos -= new Vector2(2, 0);
                RestartGame();
            }


            if (playerPos.X < graphics.GraphicsDevice.Viewport.Width * 4 - 60)  
            {
                if (playerPos.X - cameraPos.X >= 300 && cameraPos.X < graphics.GraphicsDevice.Viewport.Width * 4)
                {
                    cameraPos += new Vector2(2, 0);
                }
                player.Play();
                playerPos += new Vector2(2, 0);

            }

            else
            {
                player.Pause(0, 0);
            }


            System.Console.WriteLine("Player Pos (x, y)" + playerPos);
            System.Console.WriteLine("Camera Player Pos (x, Y)" + (playerPos - cameraPos));

            Rectangle playerRectangle = new Rectangle((int)playerPos.X, (int)playerPos.Y, 24, 24);

            for (int i = 0; i < 4; i++)
            {
                Rectangle blockRectangle = new Rectangle((int)syringePosition[i].X, (int)syringePosition[i].Y, syringe.Width, syringe.Height);

                if (playerRectangle.Intersects(blockRectangle) == true)
                {
                    personHit = true;

                    //syringePosition[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - syringe.Width / 1);
                    syringePosition[i].X = -50;
                    syringePosition[i].Y = 500;
                    //syringePos[i] = rand.Next(1);

                }
                else if (playerRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }
            }


            for (int i = 0; i < 6; i++)
            {
                Rectangle blockRectangle = new Rectangle((int)waterbottPosition[i].X, (int)waterbottPosition[i].Y, waterbottle.Width, waterbottle.Height);

                if (playerRectangle.Intersects(blockRectangle) == true)
                {
                    personHit = true;

                    //int x = ((int)cameraPos.X);
                    //waterbottPosition[i].X = rand.Next(x + graphics.GraphicsDevice.Viewport.Width - waterbottle.Width / 1);
                    waterbottPosition[i].X = -50;
                    waterbottPosition[i].Y = 500;
                    //waterPos[i] = rand.Next(1);
                }
                else if (playerRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                Rectangle blockRectangle = new Rectangle((int)evidencePosition[i].X, (int)evidencePosition[i].Y, evidence.Width, evidence.Height);

                if (playerRectangle.Intersects(blockRectangle) == true)
                {
                    personHit = true;

                    evidencePosition[i].X = -50;
                    evidencePosition[i].Y = 500;

                }
                else if (playerRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }

                
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            //spriteBatch.Draw(bg, (bgPos - cameraPos) * scroll_factor, Color.White);
            //spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor, Color.White);
            //spriteBatch.Draw(bg, (bgPos - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width, 0), Color.White);
            //spriteBatch.Draw(bg, (bgPos - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 2, 0), Color.White);
            //spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 3, 0), Color.White);
            player.DrawFrame(spriteBatch, (playerPos - cameraPos));

            for (int i = 0; i < syringePosition.Length; i++)
            {
                spriteBatch.Draw(syringe, syringePosition[i] - cameraPos, new Rectangle(24 * syringePos[i], 0, 24, 24), Color.White);
            }

            for (int i = 0; i < syringePosition.Length; i++)
            {
                spriteBatch.Draw(waterbottle, waterbottPosition[i] - cameraPos, new Rectangle(24 * waterPos[i], 0, 24, 24), Color.White);
            }

            for (int i = 0; i < evidencePosition.Length; i++)
            {
                spriteBatch.Draw(evidence, evidencePosition[i] - cameraPos, new Rectangle(24 * eviPos[i], 0, 24, 24), Color.White);
            }

            for (int i = 0; i < 2; i++)
            {
                spriteBatch.Draw(cloud, cloudPos[i], null, Color.White, 0, Vector2.Zero, scaleCloud[i], 0, 0);
            }


            spriteBatch.End();


            base.Draw(gameTime);
        }

        private void RestartGame()
        {
            isJumping = false;
            isGrounded = true;
            isGameOver = false;
            
        }
    }
}
