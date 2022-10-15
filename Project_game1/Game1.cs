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
        //background
        Texture2D bg;
        Texture2D bg2;
        Texture2D bg4;
        Vector2 bgPos = Vector2.Zero;
        Vector2 bgPos2 = Vector2.Zero;
        Vector2 bgPos4 = Vector2.Zero;
        
        //player
        AnimatedTexture player;
        Vector2 cameraPos = Vector2.Zero;        
        Vector2 playerPos = new Vector2(0, 467);
        Vector2 scroll_factor = new Vector2(1.0f, 1);

        bool isJumping;
        bool isGrounded;
        bool isGameOver;

        bool personHit;

        public int jumpSpeed;
        int force;

        //syringe
        Texture2D syringe;
        Vector2[] syringePosition = new Vector2[3];
        int[] syringePos = new int[3];

        //water
        Texture2D waterbottle;
        Vector2[] waterbottPosition = new Vector2[6];
        int[] waterPos = new int[6];

        //cloud
        Texture2D cloud;
        Vector2[] scaleCloud;
        Vector2[] cloudPos;
        int[] speed;

        Random r = new Random();

        //Texture2D key;
        //Vector2 keyPosition = new Vector2();
        //int keyPos = new int();

        int evidences = 0;
        Texture2D evidence;
        Vector2[] evidencePosition = new Vector2[5];
        int[] eviPos = new int[5];

        SpriteFont font;

        //AnimatedTexture ghost;
        //int ghostTimer = 0;

        //AnimatedTexture docter;
        //int docterTimer = 0;
        
        Texture2D barTexture;
        Vector2 barPos = new Vector2();
        int currentHeart;

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

            bg = Content.Load<Texture2D>("BG5");
            bg2 = Content.Load<Texture2D>("BG6");
            bg4 = Content.Load<Texture2D>("BG7");

            player.Load(Content, "player_walk2", 6, 2, 24);

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

            //key = Content.Load<Texture2D>("evidence1");
            evidence = Content.Load<Texture2D>("evidence2");

            barTexture = Content.Load<Texture2D>("HP_stamina");
            currentHeart = barTexture.Width - 5;


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

            waterbottPosition[0].X = 500;
            waterbottPosition[0].Y = 470;

            waterbottPosition[1].X = 1100;
            waterbottPosition[1].Y = 440;

            waterbottPosition[2].X = 1700;
            waterbottPosition[2].Y = 440;

            waterbottPosition[3].X = 2500;
            waterbottPosition[3].Y = 470;

            waterbottPosition[4].X = 3000;
            waterbottPosition[4].Y = 460;

            waterbottPosition[5].X = 3500;
            waterbottPosition[5].Y = 470;

            syringePosition[0].X = 700;
            syringePosition[0].Y = 440;

            syringePosition[1].X = 1800;
            syringePosition[1].Y = 440;

            syringePosition[2].X = 2900;
            syringePosition[2].Y = 440;

            evidencePosition[0].X = 1000;
            evidencePosition[0].Y = 470;

            evidencePosition[1].X = 2500;
            evidencePosition[1].Y = 440;

            evidencePosition[2].X = 2980;
            evidencePosition[2].Y = 470;

            evidencePosition[3].X = 3200;
            evidencePosition[3].Y = 440;

            evidencePosition[4].X = 3600;
            evidencePosition[4].Y = 470;

            //keyPosition.X = 3800;
            //keyPosition.Y = 480;

            font = Content.Load<SpriteFont>("ArialFont");

            //if (ghost.Visible == false)
            //{
            //    ghostTimer += 1;
            //}

            //if (ghostTimer == 200 && ghost.Visible == false)
            //{
            //    ghost.Visible = true;
            //    ghostTimer = 0;
            //}

            //if (docter.Visible == false)
            //{
            //    docterTimer += 1;
            //}

            //if (docterTimer == 200 && ghost.Visible == false)
            //{
            //    docter.Visible = true;
            //    docterTimer = 0;
            //}


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            player.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

            for (int i = 0; i < 2; i++)
            {
                cloudPos[i].X = cloudPos[i].X + speed[i];
                if (cloudPos[i].X > graphics.GraphicsDevice.Viewport.Width)
                {
                    cloudPos[i].X = r.Next(0, graphics.GraphicsDevice.Viewport.Height - cloud.Height);
                    cloudPos[i].Y = 100;
                    scaleCloud[i].X = scaleCloud[i].X;
                    scaleCloud[i].Y = r.Next(1, 2);
                }
                
            }
  

            if (!isJumping)
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

            if (playerPos.X < graphics.GraphicsDevice.Viewport.Width * 5 - 60)  
            {
                if (playerPos.X - cameraPos.X >= 300 && cameraPos.X < graphics.GraphicsDevice.Viewport.Width * 5)
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

            for (int i = 0; i < 3; i++)
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

            //Rectangle blockRectangle = new Rectangle((int)keyPosition.X, (int)keyPosition.Y, key.Width, key.Height);

            //if (playerRectangle.Intersects(blockRectangle) == true)
            //{
            //    personHit = true;

            //    keyPosition.X = -50;
            //    keyPosition.Y = 500;

            //}
            //else if (playerRectangle.Intersects(blockRectangle) == false)
            //{
            //    personHit = false;
            //}


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            //spriteBatch.Draw(bg, (bgPos - cameraPos) * scroll_factor, Color.White);
            spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor, Color.White);
            spriteBatch.Draw(bg, (bgPos - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width, 0), Color.White);
            spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 2, 0), Color.White);
            spriteBatch.Draw(bg, (bgPos - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 3, 0), Color.White);
            spriteBatch.Draw(bg4, (bgPos4 - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 4, 0), Color.White);
            
            player.DrawFrame(spriteBatch, (playerPos - cameraPos));

            for (int i = 0; i < syringePosition.Length; i++)
            {
                spriteBatch.Draw(syringe, syringePosition[i] - cameraPos, new Rectangle(24 * syringePos[i], 0, 24, 24), Color.White);
            }

            for (int i = 0; i < waterbottPosition.Length; i++)
            {
                spriteBatch.Draw(waterbottle, waterbottPosition[i] - cameraPos, new Rectangle(24 * waterPos[i], 0, 24, 24), Color.White);
            }

            for (int i = 0; i < evidencePosition.Length; i++)
            {
                spriteBatch.Draw(evidence, evidencePosition[i] - cameraPos, new Rectangle(24 * eviPos[i], 0, 24, 24), Color.White);
            }

            for (int i = 0; i < cloudPos.Length; i++)
            {
                spriteBatch.Draw(cloud, cloudPos[i], null, Color.White, 0, Vector2.Zero, scaleCloud[i], 0, 0);
            }

            spriteBatch.Draw(barTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barTexture.Width / 1, 5, barTexture.Width, 44), new Rectangle(0, 0, barTexture.Width - 4, 59), Color.White);
            if (currentHeart < barTexture.Width / 10 * 3)
            {
                spriteBatch.Draw(barTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barTexture.Width / 1, 5, currentHeart, 42), new Rectangle(0, 58, barTexture.Width - 10, 60), Color.DarkRed);
            }

            else
            {
                spriteBatch.Draw(barTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barTexture.Width / 1, 5, currentHeart, 42), new Rectangle(0, 58, barTexture.Width - 10, 60), Color.Green);
            }

            //spriteBatch.Draw(key, keyPosition - cameraPos, Color.White);

            if (!isGameOver)
            {
                string str;
                str = "Game Over";
                spriteBatch.DrawString(font, str, new Vector2(0, 5), Color.White);
            }

            string str;
            str = "Evidence : 0 ";
            spriteBatch.DrawString(font, str, new Vector2(0, 5), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RestartGame()
        {
            isJumping = false;
            isGrounded = true;
            isGameOver = false;
            evidences = 0;
            
        }
    }
}
