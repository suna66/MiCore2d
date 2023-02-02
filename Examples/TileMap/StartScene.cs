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
            ImageSprite sprite = new ImageSprite(LoadTexture2dTile("girl", "../resource/GirlTile001.png", 32, 32), 1);

            LoadTexture2dTile("map", "../resource/BrightForest.png", 32, 32);
            MapRenderer renderer = RendererManager.GetInstance().AddRenderer<MapRenderer>();
            TilemapSprite map = new TilemapSprite(GetTexture("map"), 1, renderer);

            AddElement("map", map);
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
