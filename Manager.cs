using SdlDotNet.Audio;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alien_Terrorist_Killer
{   
    public class Manager
    {        
        private Surface mVideo;
        private Hero hero;
        private Vijand vijand1, vijand2, vijand3, vijand4;
        private Lasers lasers;
        private Slimes slimes;
        private HeartContainer heartContainer;
        private Rectangle spikes1_ColRectangle;
        private Rectangle groundColRectangle;
        private Rectangle platform1_ColRectangle, platform2_ColRectangle, platform3_ColRectangle, platform4_ColRectangle;
        private Point laserPoint;
        private Point slimePoint;
        private Point heartPoint, heartPoint2, heartPoint3;
        private Point heartContainerPoint;
        private Point enemy_1, enemy_2, enemy_3, enemy_4;
        Level level1, level2, activelevel;
        private int shootTimer;
        private int immuneTimer;
        private int drawTimer;
        private Surface heart = new Surface("heart.png");
        private Surface background = new Surface("background.png");
        Thread audioThread = new Thread(new ThreadStart(AudioPlaybackThread));
        Sound sndLaser = new Sound("laser.wav");
        Sound sndPain = new Sound("pain.wav");        
        private Startscreen startscreen;
        private Endscreen endscreen;
        public Manager()
        {
            mVideo = Video.SetVideoMode(794, 494);           
            Console.Title = "Alien Terrorist Killer";
            hero = new Hero(mVideo, SdlDotNet.Input.Key.A, SdlDotNet.Input.Key.D, SdlDotNet.Input.Key.Space, SdlDotNet.Input.MouseButton.PrimaryButton); // QWERTY dus A = Q           
            enemy_1 = new Point(10, 305);
            enemy_2 = new Point(570, 305);
            enemy_3 = new Point(340, 185);
            enemy_4 = new Point(650, 65);
            heartContainerPoint = new Point(725, 185);
            heartPoint = new Point(20, 20);
            heartPoint2 = new Point(70, 20);
            heartPoint3 = new Point(120, 20);
            vijand1 = new Vijand(mVideo, enemy_1, 100, 290);
            vijand2 = new Vijand(mVideo, enemy_2, 570, 680);
            vijand3 = new Vijand(mVideo, enemy_3, 340, 570);
            vijand4 = new Vijand(mVideo, enemy_4, 650, 700);
            groundColRectangle = new Rectangle(0, 480, 794, 24); //486 = 510(17*30) - 24(Ground height) --- 794 (Ground width) 24 (Ground height)
            platform1_ColRectangle = new Rectangle(0, 360, 350, 15); 
            platform2_ColRectangle = new Rectangle(570, 360, 350, 15);
            platform3_ColRectangle = new Rectangle(300, 240, 350, 15); // x = 10, y = 8 --> 10  *30 = 300 /// 8 * 30 = 240  /// x en y vanaf 0 tellen in array! 
            platform4_ColRectangle = new Rectangle(660, 120, 350, 15);
            spikes1_ColRectangle = new Rectangle(420, 420, 85, 71);
            lasers = new Lasers();
            slimes = new Slimes();
            level1 = new Level1(mVideo);
            level2 = new Level2(mVideo);
            startscreen = new Startscreen(mVideo);
            endscreen = new Endscreen(mVideo);
            activelevel = startscreen;
            immuneTimer = 0;
            drawTimer = 0;
            shootTimer = 0;
            audioThread.Start();
            sndLaser.Volume = sndPain.Volume = 15;           
                     
            Events.Tick += Events_Tick;
            Events.Run();
        }

        private void Events_Tick(object sender, TickEventArgs e)
        {            
                
            //Clear
            mVideo.Blit(background);
               
            //Update
            if (startscreen.keypress == true && activelevel == startscreen)
                activelevel = level1;
            if (endscreen.keypress == true && activelevel == endscreen)
                Environment.Exit(0);

            if (hero.health == 3)
            {
                mVideo.Blit(heart, heartPoint);
                mVideo.Blit(heart, heartPoint2);
                mVideo.Blit(heart, heartPoint3);
            }
            if (hero.health == 2)
            {
                mVideo.Blit(heart, heartPoint);
                mVideo.Blit(heart, heartPoint2);
            }
            if (hero.health == 1)
            {
                mVideo.Blit(heart, heartPoint);
            }
            hero.Update();
            lasers.UpdateRight();            
            lasers.UpdateLeft();
            slimes.UpdateLeft();
            slimes.UpdateRight();
            if (vijand1 != null)
            vijand1.Update();
            if (vijand2 != null)
            vijand2.Update();
            if (vijand3 != null)
                vijand3.Update();
            if (vijand4 != null)
                vijand4.Update();

            //Draw
            if (hero.immune == false)
            hero.Draw();
            else // HERO FLICKERS WHEN LOSING HEALTH
            {
                drawTimer++;
                if (drawTimer == 6)
                    drawTimer = 0;
                if (drawTimer >= 3)                
                    hero.Draw();              
            }
            lasers.Draw();
            slimes.Draw();
            if (vijand1 != null)
            vijand1.Draw();
            if (vijand2 != null)
            vijand2.Draw();
            if (vijand3 != null)
                vijand3.Draw();
            if (vijand4 != null)
                vijand4.Draw();
            if (activelevel == level2 && heartContainer != null)
                heartContainer.Draw();

            activelevel.DrawWorld();

            //COLLISION ETC

            //HERO ENTERS LEVEL 2
            if (activelevel == level1 && hero.colRectangle.Bottom < 130 && hero.colRectangle.Right >= 788)
                RestartLvl2();

            //HERO COMPLETES GAME
            if (activelevel == level2 && hero.colRectangle.Top > 380 && hero.colRectangle.Right >= 788)
                activelevel = endscreen;

            //HERO-HEARTCONTAINER COLLISION
            if (activelevel == level2)
            {
                if (heartContainer != null)
                {
                    if (heartContainer.colRectangle.IntersectsWith(hero.colRectangle))
                    {
                        heartContainer = null;
                        if (hero.health < 3)
                        hero.health++;
                    }
                }               
            }

            //HERO-SPIKES COLLISION
            if (spikes1_ColRectangle.IntersectsWith(hero.colRectangle))
            {
                if (hero.immune == false)
                {
                    sndPain.Play();
                    hero.health--;
                    if (hero.health == 0)
                        hero.dead = true;
                    hero.immune = true;
                }                
            }
  
            //HERO SHOOTS
            if (hero.shoot && activelevel != startscreen && activelevel != endscreen)
            {
                sndLaser.Play();
                if (hero.facingDirection == "right")
                    laserPoint = new Point(hero.position.X + 35, hero.position.Y + 35);
                if (hero.facingDirection == "left")
                    laserPoint = new Point(hero.position.X, hero.position.Y + 35);
                Laser laser = new Laser(mVideo, laserPoint, lasers, hero.facingDirection);
                hero.shoot = false;
            }
           
            //ENEMY DEAD CHECK
            if (vijand1 != null)
            {
                if (vijand1.dead)
                    vijand1 = null;
            }            
            if (vijand2 != null)
            {          
                if (vijand2.dead)
                    vijand2 = null;
            }
            if (vijand3 != null)
            {
                if (vijand3.dead)
                    vijand3 = null;
            }
            if (vijand4 != null)
            {
                if (vijand4.dead)
                    vijand4 = null;
            }

            

            //HERO DEAD CHECK
            if (hero.dead == true)
            {
                if (activelevel == level1)
                    RestartLvl1();
                if (activelevel == level2)
                    RestartLvl2();               
            }

            //LASER COLLISION
            lasers.CheckHitPlatform(platform1_ColRectangle);
            lasers.CheckHitPlatform(platform2_ColRectangle);
            lasers.CheckHitPlatform(platform3_ColRectangle);
            lasers.CheckHitPlatform(platform4_ColRectangle);
            lasers.CheckHitSpikes(spikes1_ColRectangle);
            if (vijand1 != null)
            lasers.CheckHitEnemy(vijand1);
            if (vijand2 != null)
            lasers.CheckHitEnemy(vijand2);
            if (vijand3 != null)
            lasers.CheckHitEnemy(vijand3);
            if (vijand4 != null)
            lasers.CheckHitEnemy(vijand4);

            //SLIME COLLISION
            slimes.CheckHitHero(hero);
            slimes.CheckHitPlatform(platform1_ColRectangle);
            slimes.CheckHitPlatform(platform2_ColRectangle);
            slimes.CheckHitPlatform(platform3_ColRectangle);
            slimes.CheckHitPlatform(platform4_ColRectangle);
            slimes.CheckHitSpikes(spikes1_ColRectangle);

            //ENEMYSIGHT-HERO COLLISION (--> FOR WHEN TO SHOOT)
            checkSightCollision(vijand1);
            checkSightCollision(vijand2);
            checkSightCollision(vijand3);
            checkSightCollision(vijand4); 

            //HERO-ENEMY COLLISION               
            checkEnemyCollision(vijand1);                                        
            checkEnemyCollision(vijand2);            
            checkEnemyCollision(vijand3);        
            checkEnemyCollision(vijand4);     
            
            //IMMUNE TIMER (zodat hero even immune is als hij een leven verliest)
            if (hero.immune == true)
            {
                immuneTimer++;
                if (immuneTimer == 100) {
                    immuneTimer = 0;
                    hero.immune = false;
                }
            }

            //PLATFORM COLLSION
            if (platform1_ColRectangle.IntersectsWith(hero.colRectangle))
                checkPlatformCollision(platform1_ColRectangle);
            else if (platform2_ColRectangle.IntersectsWith(hero.colRectangle)) {
                checkPlatformCollision(platform2_ColRectangle);
            }
            else if (platform3_ColRectangle.IntersectsWith(hero.colRectangle))
            {
                checkPlatformCollision(platform3_ColRectangle);
            }
            else if (platform4_ColRectangle.IntersectsWith(hero.colRectangle))
            {
                checkPlatformCollision(platform4_ColRectangle);
            }   
            else 
            {
                hero.down = true;                 
            }            

            //GROUND COLLISION
           if (hero.colRectangle.IntersectsWith(groundColRectangle))
                    hero.down = false;   
 
            mVideo.Update();
        }

        ////////////////////////////////////////////////////////////////////////////////////

        // METHODES
        public void checkPlatformCollision(Rectangle platform)
        {           
                if (hero.colRectangle.Bottom < platform.Bottom) //hero stands on top of platform
                    hero.down = false;
                else //bumped against sides/bottom of platform
                {
                    hero.left = hero.right = hero.jump = false;
                    hero.force = 20;
                }
        }

         private void checkEnemyCollision(Vijand enemy)
        {
            if (enemy != null && hero.immune == false)
            {
                if (hero.colRectangle.IntersectsWith(enemy.colRectangle))
                {
                    sndPain.Play();
                    hero.health--;
                    Console.WriteLine(hero.health);
                    if (hero.health == 0)
                        hero.dead = true;
                    hero.immune = true;
                }       
            }                                                                   
        }

         private void checkSightCollision(Vijand vijand)
         {
             if (vijand != null)
             {
                 if (vijand.sight.IntersectsWith(hero.colRectangle))
                 {       //SHOOT
                     if (shootTimer == 40)
                     {
                         shootTimer = 0;
                         if (vijand.direction == "right")
                             slimePoint = new Point(vijand.position.X + 50, vijand.position.Y + 10);
                         if (vijand.direction == "left")
                             slimePoint = new Point(vijand.position.X, vijand.position.Y + 10);
                         Slime slime = new Slime(mVideo, slimePoint, slimes, vijand.direction);
                     }
                     else
                     {
                         shootTimer++;
                     }
                 }
             }
         }

        private void EnemyDeadCheck(Vijand vijand) {
            if (vijand != null)
            {
                if (vijand.dead)
                    vijand = null;
            }
            }     

         public void RestartLvl1()
         {
             hero = new Hero(mVideo, SdlDotNet.Input.Key.A, SdlDotNet.Input.Key.D, SdlDotNet.Input.Key.Space, SdlDotNet.Input.MouseButton.PrimaryButton);
             
             vijand1 = new Vijand(mVideo, enemy_1, 100, 290);
             vijand2 = new Vijand(mVideo, enemy_2, 570, 680);
             vijand3 = new Vijand(mVideo, enemy_3, 340, 570);
             vijand4 = new Vijand(mVideo, enemy_4, 650, 700);
         }
         public void RestartLvl2()
         {
             hero = new Hero(mVideo, SdlDotNet.Input.Key.A, SdlDotNet.Input.Key.D, SdlDotNet.Input.Key.Space, SdlDotNet.Input.MouseButton.PrimaryButton);
             activelevel = level2;
             hero.position = hero.lvl2Position;
             heartContainer = new HeartContainer(mVideo, heartContainerPoint);

             spikes1_ColRectangle = new Rectangle(180, 420, 85, 71);

             platform1_ColRectangle = new Rectangle(0, 180, 350, 15);
             platform2_ColRectangle = new Rectangle(720, 240, 350, 15);
             platform3_ColRectangle = new Rectangle(330, 330, 350, 15);
             platform4_ColRectangle = new Rectangle(450, 330, 350, 15);

             enemy_1 = new Point(180, 125);
             enemy_2 = new Point(450, 275);
             enemy_3 = new Point(300, 425);
             enemy_4 = new Point(650, 425);
             vijand1 = new Vijand(mVideo, enemy_1, 180, 230);
             vijand2 = new Vijand(mVideo, enemy_2, 450, 600);
             vijand3 = new Vijand(mVideo, enemy_3, 300, 400);
             vijand4 = new Vijand(mVideo, enemy_4, 650, 700);
         }

         private static void AudioPlaybackThread()
         {
             //background music
             Music bgMusic = new Music("background.wav");
             MusicPlayer.Volume = 20;
             MusicPlayer.Load(bgMusic);
             MusicPlayer.Play(true); //true --> loop    
         }
    }
}
