#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Animation. Animation Component class.
    /// </summary>
    public class Animation : Component
    {
        /// <summary>
        /// textureDic. management of pattern of animation textures.
        /// </summary>
        protected Dictionary<string, Texture>? textureDic = null;

        /// <summary>
        /// main_texture. default texture instance.
        /// </summary>
        protected Texture? main_texture = null;

        /// <summary>
        /// animationName. current animation texture name.
        /// </summary>
        protected string? animationName = null;

        /// <summary>
        /// animationCurrentTime. elapsed animating time.
        /// </summary>
        protected double animationCurrentTime = 0.0f;

        /// <summary>
        /// constructor.
        /// </summary>
        public Animation()
        {
            Interval = 0.1f;
        }

        /// <summary>
        /// Interval. interval time to change next texture index.
        /// </summary>
        /// <value>time of interval(second)</value>
        public double Interval { get; set; }

        /// <summary>
        /// AnimationName.
        /// </summary>
        /// <value>animation name of current running</value>
        public string AnimationName { get => animationName; }

        /// <summary>
        /// StopAnimation.
        /// </summary>
        /// <value>true: Stop animation, false: Running animation</value>
        public bool StopAnimation {get; set;} = false;

        /// <summary>
        /// IsOneShort. 
        /// </summary>
        /// <value>true: animating once, false: animating repeatedly</value>
        public bool IsOneShort { get; set; } = false;

        /// <summary>
        /// SwitchTexture. Switch animation texture.
        /// </summary>
        /// <param name="key">animation name</param>
        /// <param name="oneShot">once or repeatedly</param>
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

        /// <summary>
        /// RestartAnimation
        /// </summary>
        /// <param name="oneShot">once or repeatedly</param>
        public void RestartAnimation(bool oneShot = false)
        {
            StopAnimation = false;
            element.TextureIndex = 0;
            IsOneShort = oneShot;
        }

        /// <summary>
        /// AddTexture. Add animation texture to this component.
        /// </summary>
        /// <param name="key">animation name</param>
        /// <param name="tex">animation texture</param>
        public void AddTexture(string key, Texture tex)
        {
            if (textureDic == null)
            {
                textureDic = new Dictionary<string, Texture>();
            }
            textureDic.Add(key, tex);
        }

        /// <summary>
        /// UpdateComponent. called by game engine to animate this element.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
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

        /// <summary>
        /// Dispose
        /// </summary>
        public override void Dispose()
        {
            main_texture = null;
            textureDic = null;
            base.Dispose();
        }
    }
}