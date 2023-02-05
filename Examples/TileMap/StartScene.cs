#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;
using System;

namespace Example.TileMap
{
    public class StartScene : GameScene
    {
        private ImageSprite explosionSprite;
        private ImageSprite awe;

        public StartScene()
        {
             
        }

        public override void Load()
        {
            Audio.LoadMP3File("sound1", "../resource/sanjinooyatsu.mp3", true);
            Audio.LoadMP3File("explosion", "../resource/explosion.mp3", false);
            Audio.Play("sound1");

            LoadTexture2dTile("explosion", "../resource/explosion.png", 120, 120);
            LoadTexture2dTile("magic", "../resource/magic.png", 120, 120);
            LoadTexture2d("awe", "../resource/awesomeface.png");
            explosionSprite = new ImageSprite(GetTexture("explosion"), 1);
            explosionSprite.AddComponent<AnimationTile>();
            explosionSprite.Disabled = true;

            ImageSprite magic = new ImageSprite(GetTexture("magic"), 1);
            magic.AddComponent<AnimationTile>();
            magic.AddComponent<MagicScript>();        

            awe = new ImageSprite(GetTexture("awe"), 1);
            BoxCollider collider = awe.AddComponent<BoxCollider>();
            collider.IsTrigger = true;
            awe.Disabled = true;

            ImageSprite sprite = new ImageSprite(LoadTexture2dTile("girl", "../resource/GirlTile001.png", 32, 32), 1);
            sprite.AddComponent<PlayerScript>();

            LoadTexture2dTile("map", "../resource/BrightForest.png", 32, 32);
            MapRenderer basemapRenderer = RendererManager.GetInstance().AddRenderer<MapRenderer>();
            ObstacleRenderer obstacleRenderer = RendererManager.GetInstance().AddRenderer<ObstacleRenderer>();
            TilemapSprite map = new TilemapSprite(GetTexture("map"), 1, basemapRenderer);
            TilemapSprite obstacle = new TilemapSprite(GetTexture("map"), 1, obstacleRenderer);
            obstacle.AddComponent<TilemapCollider>();

            AddElement("map", map);
            AddElement("obstacle", obstacle);
            AddElement("girl", sprite);
            AddElement("explosion", explosionSprite);
            AddElement("awe", awe);
            AddElement("magic", magic);

            TimeUtil.Delay(5000, DisplayAweface);
        }

        private void DisplayAweface()
        {
            if (awe.Disabled && explosionSprite.Disabled)
            {
                Random r = new Random();

                float x = (float)r.Next(-3, 4);
                float y = (float)r.Next(-3, 3);

                awe.SetPosition(x, y, 0.0f);
                awe.Disabled = false;
            }
            TimeUtil.Delay(5000, DisplayAweface);
        }


         public override void Update(double elapsed)
         {
            if (KeyState.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }
         }
    }
}
