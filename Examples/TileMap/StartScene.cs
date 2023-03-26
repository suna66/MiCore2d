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

        private float[] _baseMap = {
                -3.0f,  2.0f,  0.0f,  6.0f,
                -2.0f,  2.0f,  0.0f,  6.0f,
                -1.0f,  2.0f,  0.0f,  6.0f,
                 0.0f,  2.0f,  0.0f,  6.0f,
                 1.0f,  2.0f,  0.0f,  6.0f,
                 2.0f,  2.0f,  0.0f,  6.0f,
                 3.0f,  2.0f,  0.0f,  6.0f,
                -3.0f,  1.0f,  0.0f,  6.0f,
                -2.0f,  1.0f,  0.0f,  0.0f,
                -1.0f,  1.0f,  0.0f,  0.0f,
                 0.0f,  1.0f,  0.0f,  0.0f,
                 1.0f,  1.0f,  0.0f,  0.0f,
                 2.0f,  1.0f,  0.0f,  0.0f,
                 3.0f,  1.0f,  0.0f,  6.0f,
                -3.0f,  0.0f,  0.0f,  6.0f,
                -2.0f,  0.0f,  0.0f,  0.0f,
                -1.0f,  0.0f,  0.0f,  0.0f,
                 0.0f,  0.0f,  0.0f,  0.0f,
                 1.0f,  0.0f,  0.0f,  0.0f,
                 2.0f,  0.0f,  0.0f,  0.0f,
                 3.0f,  0.0f,  0.0f,  6.0f,
                -3.0f, -1.0f,  0.0f,  6.0f,
                -2.0f, -1.0f,  0.0f,  0.0f,
                -1.0f, -1.0f,  0.0f,  0.0f,
                 0.0f, -1.0f,  0.0f,  0.0f,
                 1.0f, -1.0f,  0.0f,  0.0f,
                 2.0f, -1.0f,  0.0f,  0.0f,
                 3.0f, -1.0f,  0.0f,  6.0f,
                -3.0f, -2.0f,  0.0f,  6.0f,
                -2.0f, -2.0f,  0.0f,  0.0f,
                -1.0f, -2.0f,  0.0f,  0.0f,
                 0.0f, -2.0f,  0.0f,  0.0f,
                 1.0f, -2.0f,  0.0f,  0.0f,
                 2.0f, -2.0f,  0.0f,  0.0f,
                 3.0f, -2.0f,  0.0f,  6.0f,
                -3.0f, -3.0f,  0.0f,  6.0f,
                -2.0f, -3.0f,  0.0f,  6.0f,
                -1.0f, -3.0f,  0.0f,  6.0f,
                 0.0f, -3.0f,  0.0f,  6.0f,
                 1.0f, -3.0f,  0.0f,  6.0f,
                 2.0f, -3.0f,  0.0f,  6.0f,
                 3.0f, -3.0f,  0.0f,  6.0f
                 //3.0f, -3.0f,  0.0f, 102.0f
            };
        
        private float[] _obstacleMap = {
                -4.0f,  3.0f,  0.0f, 102.0f,
                -3.0f,  3.0f,  0.0f, 102.0f,
                -2.0f,  3.0f,  0.0f, 102.0f,
                -1.0f,  3.0f,  0.0f, 102.0f,
                 0.0f,  3.0f,  0.0f, 102.0f,
                 1.0f,  3.0f,  0.0f, 102.0f,
                 2.0f,  3.0f,  0.0f, 102.0f,
                 3.0f,  3.0f,  0.0f, 102.0f,
                 4.0f,  3.0f,  0.0f, 102.0f,
                -4.0f,  2.0f,  0.0f, 102.0f,
                -4.0f,  1.0f,  0.0f, 102.0f,
                -4.0f,  0.0f,  0.0f, 102.0f,
                -4.0f, -1.0f,  0.0f, 102.0f,
                -4.0f, -2.0f,  0.0f, 102.0f,
                -4.0f, -3.0f,  0.0f, 102.0f,
                 4.0f,  2.0f,  0.0f, 102.0f,
                 4.0f,  1.0f,  0.0f, 102.0f,
                 4.0f,  0.0f,  0.0f, 102.0f, 
                 4.0f, -1.0f,  0.0f, 102.0f,
                 4.0f, -2.0f,  0.0f, 102.0f,
                 4.0f, -3.0f,  0.0f, 102.0f,
                -4.0f, -4.0f,  0.0f, 102.0f,
                -3.0f, -4.0f,  0.0f, 102.0f,
                -2.0f, -4.0f,  0.0f, 102.0f,
                -1.0f, -4.0f,  0.0f, 102.0f,
                 0.0f, -4.0f,  0.0f, 102.0f,
                 1.0f, -4.0f,  0.0f, 102.0f,
                 2.0f, -4.0f,  0.0f, 102.0f,
                 3.0f, -4.0f,  0.0f, 102.0f,
                 4.0f, -4.0f,  0.0f, 102.0f
            };

        public StartScene()
        {
             
        }

        public override void Load()
        {
            Audio.LoadMP3File("sound1", "../resource/sanjinooyatsu.mp3", true);
            Audio.LoadMP3File("explosion", "../resource/explosion.mp3", false);
            Audio.LoadMP3File("magic", "../resource/magic.mp3", false);
            Audio.Play("sound1");

            explosionSprite = new ImageSprite("../resource/explosion.png", 120, 120, 1);
            explosionSprite.AddComponent<AnimationTile>();
            explosionSprite.Disabled = true;

            ImageSprite magic = new ImageSprite("../resource/magic.png", 120, 120, 1);
            magic.AddComponent<AnimationTile>();
            magic.AddComponent<MagicScript>();
            //magic.AddComponent<HardBody>();
            BoxCollider m_collider = magic.AddComponent<BoxCollider>();
            m_collider.IsSolid = false;

            awe = new ImageSprite("../resource/awesomeface.png", 1);
            BoxCollider collider = awe.AddComponent<BoxCollider>();
            collider.IsSolid = false;
            collider.IsTrigger = true;
            awe.Disabled = true;

            ImageSprite sprite = new ImageSprite("../resource/GirlTile001.png", 32, 32, 1);
            sprite.AddComponent<PlayerScript>();

            // MapRenderer basemapRenderer = RendererManager.GetInstance().AddRenderer<MapRenderer>();
            // ObstacleRenderer obstacleRenderer = RendererManager.GetInstance().AddRenderer<ObstacleRenderer>();
            TilemapSprite map = new TilemapSprite("../resource/BrightForest.png", 32, 32, 1, _baseMap, false);
            TilemapSprite obstacle = new TilemapSprite("../resource/BrightForest.png", 32, 32, 1, _obstacleMap, false);
            obstacle.AddComponent<TilemapCollider>();

            AddElement("map", map);
            AddElement("obstacle", obstacle);
            AddElement("magic", magic);
            AddElement("girl", sprite);
            AddElement("explosion", explosionSprite);
            AddElement("awe", awe, "enemy");

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
    }
}
