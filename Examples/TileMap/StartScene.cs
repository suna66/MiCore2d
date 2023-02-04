using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.TileMap
{
    public class StartScene : GameScene
    {
        public StartScene()
        {
             
        }

        public override void Load()
        {
            Audio.LoadMP3File("sound1", "../resource/sanjinooyatsu.mp3", true);
            Audio.Play("sound1");

            LoadTexture2dTile("explosion", "../resource/explosion.png", 120, 120);
            LoadTexture2d("awe", "../resource/awesomeface.png");
            ImageSprite explosionSprite = new ImageSprite(GetTexture("explosion"), 1);
            explosionSprite.AddComponent<AnimationTile>();
            explosionSprite.Disabled = true;

            ImageSprite awe = new ImageSprite(GetTexture("awe"), 1);
            awe.AddComponent<BoxCollider>();
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
