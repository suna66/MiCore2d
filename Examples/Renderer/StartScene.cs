#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Renderer
{
    public class StartScene : GameScene
    {
        private ImageSprite backgorund = null;
        private int renderType = -1;
        private float blur = 0.0f;
        private float step = 0.05f;
        BlurRenderer blurRenderer = null!;
        WaveTextureRenderer waveRenderer = null!;
        SepiaTextureRenderer sepiaRenderer = null!;
        NoiseEffectRenderer noiseRendere = null!;
        SwipeRenderer swipeRenderer = null!;
        CircleEffectRenderer circleRenderer = null!;

        private float interval = 0.0f;
        
        public override void Load()
        {
            backgorund = new ImageSprite("../resource/park.jpg", 10);
            swipeRenderer = new SwipeRenderer(10, backgorund.AspectRatio);
            swipeRenderer.Scale = 0.0f;
            swipeRenderer.Direct = 1.0f;
            backgorund.SetRenderer(swipeRenderer);
            AddElement("back", backgorund);
        }

         public override void Update(double elapsed)
         {
            if (KeyStateInfo.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }
            if (KeyStateInfo.IsKeyDown(Keys.Space))
            {
                if (interval > 0.5f)
                {
                    backgorund.Alpha = 1.0f;
                    if (renderType == -1)
                    {
                        renderType = 0;
                        blurRenderer = new BlurRenderer(10, backgorund.AspectRatio);
                        backgorund.SetRenderer(blurRenderer);
                        blur = 0.0f;
                        blurRenderer.Blur = blur;
                    } else if (renderType == 0)
                    {
                        renderType = 1;
                        waveRenderer = new WaveTextureRenderer(10, backgorund.AspectRatio);
                        backgorund.SetRenderer(waveRenderer);
                        waveRenderer.Length = 10.0f;
                        waveRenderer.Width = 0.01f;
                        waveRenderer.Speed = 2.0f;
                    }
                    else if (renderType == 1)
                    {
                        renderType = 2;
                        sepiaRenderer = new SepiaTextureRenderer(10, backgorund.AspectRatio);
                        backgorund.SetRenderer(sepiaRenderer);
                    }
                    else if (renderType == 2)
                    {
                        renderType = 3;
                        noiseRendere = new NoiseEffectRenderer(10, backgorund.AspectRatio);
                        backgorund.SetRenderer(noiseRendere);
                    }
                    else if (renderType == 3)
                    {
                        renderType = 4;
                        circleRenderer = new CircleEffectRenderer(10, backgorund.AspectRatio);
                        backgorund.SetRenderer(circleRenderer);
                    }
                    else
                    {
                        renderType = -1;
                        swipeRenderer = new SwipeRenderer(10, backgorund.AspectRatio);
                        swipeRenderer.Scale = 0.0f;
                        swipeRenderer.Direct = 1.0f;
                        backgorund.SetRenderer(swipeRenderer);
                    }
                    interval = 0.0f;
                }
            }
            if (renderType == 0)
            {
                blur += step;
                if (blur >= 30.0f)
                {
                    step = -0.05f;
                }
                if (blur <= 0.0f)
                {
                    blur = 0.0f;
                    step = 0.05f;
                }
                blurRenderer.Blur = blur;
            }
            if (renderType == 1)
            {
                waveRenderer.Times += (float)elapsed;
            }
            if (renderType == 2)
            {
                backgorund.Alpha = MathF.Abs(MathF.Sin((float)CurrentTime * 0.3f));
            }
            if (renderType == 3)
            {
                noiseRendere.Times += (float)elapsed;
                backgorund.Alpha = MathF.Abs(MathF.Sin((float)CurrentTime * 0.3f));
            }
            if (renderType == 4)
            {
                circleRenderer.Radius = MathF.Abs(MathF.Sin((float)CurrentTime) * 2.0f);
                circleRenderer.Centor = new Vector2(MathF.Sin((float)CurrentTime) * 2.0f, MathF.Cos((float)CurrentTime) * 2.0f);
            }
            if (renderType == -1)
            {
                swipeRenderer.Scale += (float)elapsed * 0.5f;
                if (swipeRenderer.Scale >= 1.0f)
                {
                    swipeRenderer.Direct *= -1;
                    swipeRenderer.Scale = 0.0f;
                }
            }
            interval += (float)elapsed;
         }
    }
}
