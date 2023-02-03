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
