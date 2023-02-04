#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class Animation : Component
    {
        protected Dictionary<string, Texture>? textureDic = null;
        protected Texture? main_texture = null;
        protected string? animationName = null;

        protected double animationCurrentTime = 0.0f;

        public Animation()
        {
            Interval = 0.1f;
        }

        public double Interval { get; set; }

        public string AnimationName { get => animationName; }

        public bool StopAnimation {get; set;} = false;

        public bool IsOneShort { get; set; } = false;

        public void SwitchTexture(string? key, bool oneShot = false)
        {
            if (key == null)
            {
                if (main_texture != null)
                {
                    element.Texture = main_texture;
                }
                animationName = null;
                return;
            }
            if (textureDic != null)
            {
                if (main_texture == null)
                {
                    main_texture = element.Texture;
                }
                if ((textureDic.ContainsKey(key)))
                {
                    element.Texture = textureDic[key];
                    animationName = key;
                }
            }
            IsOneShort = oneShot;
        }

        public void RestartAnimation(bool oneShot = false)
        {
            StopAnimation = false;
            element.TextureIndex = 0;
            IsOneShort = oneShot;
        }

        public void AddTexture(string key, Texture tex)
        {
            if (textureDic == null)
            {
                textureDic = new Dictionary<string, Texture>();
            }
            textureDic.Add(key, tex);
        }

        public override void UpdateComponent(double elapsed)
        {
            animationCurrentTime += elapsed;
            if (animationCurrentTime > Interval)
            {
                animationCurrentTime = 0;
                if (!StopAnimation)
                {
                    element.IncrementTextureIndex();
                    if (IsOneShort)
                    {
                        if (element.TextureIndex == element.TextureCount - 1)
                        {
                            StopAnimation = true;
                        }
                    }
                }
            }
        }

        public override void Dispose()
        {
            main_texture = null;
            textureDic = null;
            base.Dispose();
        }
    }
}