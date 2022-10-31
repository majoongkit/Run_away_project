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
        //mainmenu
        Texture2D title;
        bool isTitle;
        
        //Texture2D gameplay;
        bool isGameplay;

        //scenes gameover
        Texture2D GameOver;
        bool isGameOver;

        //scenes dead
        Texture2D yourDead;
        bool isDead;

        //scenes Pause
        Texture2D gamePause;
        bool isGamePause;

        //scenes win
        Texture2D gameWin;
        bool isGameWin;

        //background
        Texture2D bg2;
        Texture2D bg4;
        Vector2 bgPos = Vector2.Zero;
        Vector2 bgPos2 = Vector2.Zero;
        Vector2 bgPos4 = Vector2.Zero;
        
        //player
        AnimatedTexture player;
        AnimatedTexture playerJump;
        AnimatedTexture playerSlide;
        Vector2 cameraPos = Vector2.Zero;        
        Vector2 playerPos = new Vector2(0, 467);
        Vector2 scroll_factor = new Vector2(1.0f, 1);

        float _countDownJump = 1;
        float _currentTimeJump = 0;

        float countDownSlide = 0.5f;
        float currentTimeSlide = 0;

        bool isJumping;
        bool isSlide;
        bool isGrounded;        

        bool personHit;

        int jumpSpeed;
        int force;

        bool speedUp;
        float countdownSpeed = 2;
        float currentCountdownspeed;
        float playerSpeed = 2;

        //HP
        Texture2D barHeartTexture;
        //Vector2 barPos = new Vector2();
        float currentHeart = 10;
        float countdownHeart = 0.5f;

        //stamina
        Texture2D barStaminaTexture;
        float currentStamina = 10;
        float countdownStamina = 0.5f;

        //syringe
        Texture2D syringe;
        Vector2[] syringePosition = new Vector2[4];
        int[] syringePos = new int[4];

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
 
        //evidences
        Texture2D evidence;
        Vector2[] evidencePosition = new Vector2[5];
        int[] eviPos = new int[5];
        int evidences = 0;

        SpriteFont font;

        //ghost
        //AnimatedTexture ghost;
        //int ghostTimer = 0;

        //docter
        //AnimatedTexture docter;
        //int docterTimer = 0;

        //police man
        Texture2D police_man;
        Vector2 policePos = new Vector2(5200, 467);
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            player = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 1.0f);
            playerJump = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 1.0f);
            playerSlide = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 1.0f);

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            jumpSpeed = -20; 
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //gameplay = Content.Load<Texture2D>("BG5");
            bg2 = Content.Load<Texture2D>("BG6");
            bg4 = Content.Load<Texture2D>("BG7");

            title = Content.Load<Texture2D>("Title3");
            isTitle = true;
            isGameplay = false;

            GameOver = Content.Load<Texture2D>("GameOver");
            isGameOver = false;

            yourDead = Content.Load<Texture2D>("Your_dead");
            isDead = false;

            gamePause = Content.Load<Texture2D>("Pause");
            isGamePause = false;

            gameWin = Content.Load<Texture2D>("You_win");
            isGameWin = false;

            player.Load(Content, "player_walk2", 6, 2, 24);
            playerJump.Load(Content, "player_jump", 5, 1, 6);
            playerSlide.Load(Content, "player_slide", 4, 1, 24);

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

            evidence = Content.Load<Texture2D>("evidence2");

            barHeartTexture = Content.Load<Texture2D>("HP_stamina");
            currentHeart = barHeartTexture.Width - 5;

            barStaminaTexture = Content.Load<Texture2D>("HP_stamina");
            currentStamina = barStaminaTexture.Width - 5;

            police_man = Content.Load<Texture2D>("police_man");


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

            waterbottPosition[0].X = 900;
            waterbottPosition[0].Y = 430;

            waterbottPosition[1].X = 2000;
            waterbottPosition[1].Y = 480;

            waterbottPosition[2].X = 3000;
            waterbottPosition[2].Y = 480;

            waterbottPosition[3].X = 3900;
            waterbottPosition[3].Y = 430;

            waterbottPosition[4].X = 4600;
            waterbottPosition[4].Y = 480;

            waterbottPosition[5].X = 5000;
            waterbottPosition[5].Y = 480;

            syringePosition[0].X = 700;
            syringePosition[0].Y = 430;

            syringePosition[1].X = 1800;
            syringePosition[1].Y = 480;

            syringePosition[2].X = 2900;
            syringePosition[2].Y = 480;

            syringePosition[3].X = 3900;
            syringePosition[3].Y = 480;

            evidencePosition[0].X = 1200;
            evidencePosition[0].Y = 430;

            evidencePosition[1].X = 2200;
            evidencePosition[1].Y = 480;

            evidencePosition[2].X = 3200;
            evidencePosition[2].Y = 440;

            evidencePosition[3].X = 4200;
            evidencePosition[3].Y = 460;

            evidencePosition[4].X = 5200;
            evidencePosition[4].Y = 430;


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
            if (isGameplay == true)
            {
                UpdateGameplay(gameTime);
            }

            else if (isTitle == true)
            {
                UpdateTitle();
            }

            else if (isGamePause == true)
            {
                UpdateGamePause();
            }

            else if (isGameOver == true)
            {
                UpdateGameOver(gameTime);
            }

            else if (isGameWin == true)
            {
                UpdateGameWin(gameTime);
            }

            else if (isDead == true)
            {
                UpdateyourDead();
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            if (isGameplay == true)
            {
                DrawGameplay();
            }

            else if (isTitle == true)
            {
                DrawTitle();
            }

            else if (isGamePause == true)
            {
                DrawGamePause();
            }

            else if (isGameOver == true)
            {
                DrawGameOver();
            }

            else if (isGameWin == true)
            {
                DrawGameWin();
            }

            else if (isDead == true)
            {
                DrawyourDead();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateGameplay(GameTime gameTime)
        {
            //player
            player.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            playerJump.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            playerSlide.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (playerPos.X < graphics.GraphicsDevice.Viewport.Width * 8 - 60)
            {
                if (playerPos.X - cameraPos.X >= 300 && cameraPos.X < graphics.GraphicsDevice.Viewport.Width * 8)
                {
                    cameraPos += new Vector2(playerSpeed, 0);
                }

                player.Play();
                playerJump.Pause();
                playerSlide.Pause();
                playerPos += new Vector2(playerSpeed, 0);

            }
            else
            {
                player.Pause(0, 0);
            }

            if (isJumping == false)
            {
                isGrounded = true;
            }

            if (isJumping && force < 0)
            {
                jumpSpeed = -20;
                force -= 5;
                isGrounded = false;
            }

            else if (isSlide)
            {
                isJumping = false;
                //isGrounded = true;
            }

            //////////////////////////////// Y
            if (isJumping)
            {
                playerSlide.Pause();
                player.Pause();
                playerJump.Play();
                playerPos = new Vector2(playerPos.X, 430);
                _currentTimeJump -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_currentTimeJump < 0)
                    isJumping = false;
            }
            
            else if (isSlide)
            {
                player.Pause();
                playerJump.Pause();
                playerSlide.Play();
                playerPos = new Vector2(playerPos.X, 497);
                currentTimeSlide -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (currentTimeSlide < 0)
                    isSlide = false;
            }
            else
            {
                playerPos = new Vector2(playerPos.X, 467);
            }

            //keyboard 
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                isJumping = true;
                isGrounded = false;
                _currentTimeJump = _countDownJump;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isSlide = true;
                isJumping = false;
                isGrounded = false;
                currentTimeSlide = countDownSlide;
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && isGameOver == true)
            //{
            //    //playerPos -= new Vector2(2, 0);
            //    RestartGame();
            //}

            currentHeart -= 0.3f;
            currentStamina = 0;
            

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

                    speedUp = true;
                    currentCountdownspeed = countdownSpeed;
                    playerSpeed = 5;

                    currentStamina += 100;
                    
                    //syringePos[i] = rand.Next(1);

                }
                else if (playerRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }
            }

            if (speedUp == true)
            {
                if (currentCountdownspeed > 0)
                {
                    currentCountdownspeed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    playerSpeed = 2;
                    speedUp = false;
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

                    currentHeart += 100;

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

                    evidences++;

                }
                else if (playerRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }


            }

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

            if (currentHeart <= 0)
            {
                isDead = true;
                isGameOver = false;
                isGameplay = false;
                isGamePause = false;
                isTitle = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) == true)
            {
                isGameplay = false;
                isGamePause = true;
            }

        }

        private void UpdateTitle()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                isTitle = false;              
                isGameplay = true;
            }
        }

        private void UpdateGamePause()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {                                
                isGameplay = true;
                isGamePause = false;
            }
        }

        private void UpdateGameOver(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && isGameOver == true)
            {
                //playerPos -= new Vector2(2, 0);
                RestartGame();
            }

        }

        private void UpdateGameWin(GameTime gameTime)
        {
            
        }

        private void UpdateyourDead()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && isDead == true)
            {
                //playerPos -= new Vector2(2, 0);
                RestartGame();
            }
        }

        private void DrawGameplay()
        {
            //spriteBatch.Draw(gameplay, Vector2.Zero, Color.White);

            //spriteBatch.Draw(bg, (bgPos - cameraPos) * scroll_factor, Color.White);
            spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor, Color.White);
            
            //spriteBatch.Draw(gameplay, (bgPos - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width, 0), Color.White);
            spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 2, 0), Color.White);
            //spriteBatch.Draw(gameplay, (bgPos - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 3, 0), Color.White);
            spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 4, 0), Color.White);
            spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 5, 0), Color.White);
            spriteBatch.Draw(bg2, (bgPos2 - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 6, 0), Color.White);
            spriteBatch.Draw(bg4, (bgPos4 - cameraPos) * scroll_factor + new Vector2(graphics.GraphicsDevice.Viewport.Width * 7, 0), Color.White);

            
            if (isJumping)
            {
                playerJump.DrawFrame(spriteBatch, (playerPos - cameraPos));
            }
            else if (isSlide)
            {
                playerSlide.DrawFrame(spriteBatch, (playerPos - cameraPos));
            }
            else
            {
                player.DrawFrame(spriteBatch, (playerPos - cameraPos));
            }

            

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

            spriteBatch.Draw(barHeartTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barHeartTexture.Width / 1, 5, barHeartTexture.Width, 44), new Rectangle(0, 0, barHeartTexture.Width - 4, 59), Color.White);
            if (currentHeart < barHeartTexture.Width / 10 * 3)
            {
                spriteBatch.Draw(barHeartTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barHeartTexture.Width / 1, 5, (int)currentHeart, 42), new Rectangle(0, 58, barHeartTexture.Width - 10, 60), Color.DarkRed);
            }

            else
            {
                spriteBatch.Draw(barHeartTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barHeartTexture.Width / 1, 5, (int)currentHeart, 42), new Rectangle(0, 58, barHeartTexture.Width - 10, 60), Color.Green);
            }

            spriteBatch.Draw(barStaminaTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barStaminaTexture.Width / 1, 50, barStaminaTexture.Width, 44), new Rectangle(0, 0, barStaminaTexture.Width - 4, 59), Color.White);
            if (currentStamina < barStaminaTexture.Width / 10 * 3)
            {
                spriteBatch.Draw(barStaminaTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barStaminaTexture.Width / 1, 50, (int)currentStamina, 42), new Rectangle(0, 58, barStaminaTexture.Width - 10, 60), Color.DarkBlue);
            }

            else
            {
                spriteBatch.Draw(barStaminaTexture, new Rectangle(GraphicsDevice.Viewport.Width / 1 - barStaminaTexture.Width / 1, 50, (int)currentStamina, 42), new Rectangle(0, 58, barStaminaTexture.Width - 10, 60), Color.Blue);
            }

            //spriteBatch.Draw(key, keyPosition - cameraPos, Color.White);

            //if (!isGameOver)
            //{
            //    spriteBatch.DrawString(font, str, new Vector2(0, 5), Color.White);

            //}

            string str;
            str = "Evidence : " + evidences;
            spriteBatch.DrawString(font, str, new Vector2(5, 5), Color.White);

            string str2;
            str2 = "Press LeftAlt to Pause Game";
            spriteBatch.DrawString(font, str2, new Vector2(5, 25), Color.White);

            spriteBatch.Draw(police_man, policePos, Color.White);
        }

        private void DrawTitle()
        {
            spriteBatch.Draw(title, Vector2.Zero, Color.White);

            //bg
            //text1
            //text2
            //logo
        }

        private void DrawGamePause()
        {
            spriteBatch.Draw(gamePause, Vector2.Zero, Color.White);
        }

        private void DrawGameOver()
        {
            spriteBatch.Draw(GameOver, Vector2.Zero, Color.White);
        }

        private void DrawGameWin()
        {
            spriteBatch.Draw(gameWin, Vector2.Zero, Color.White);
        }

        private void DrawyourDead()
        {
            spriteBatch.Draw(yourDead, Vector2.Zero, Color.White);
        }

        private void RestartGame()
        {
            isGrounded = true;
            isTitle = true;

            isGameplay = false;
            isJumping = false;            
            isGameOver = false;
            isDead = false;
            
            evidences = 0;
            
            currentHeart = barHeartTexture.Width - 5;
            currentStamina = barStaminaTexture.Width - 5;
            
        }
    }
}
