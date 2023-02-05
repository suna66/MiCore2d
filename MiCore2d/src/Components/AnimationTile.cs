#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// AnimationTile. animating for tilemap texture.
    /// </summary>
    public class AnimationTile : Component
    {
        /// <summary>
        /// animationPattern. management pattern data of animation.
        /// </summary>
        private Dictionary<string, int[]>? animationPattern = null;

        /// <summary>
        /// pattan. current animation pattern.
        /// </summary>
        private int[]? pattern = null;

        /// <summary>
        /// patternIndex. current index of animation pattern array.
        /// </summary>
        private int patternIndex = 0;

        /// <summary>
        /// animationName. current animation name.
        /// </summary>
        protected string? animationName = null;

        /// <summary>
        /// animationCurrentTime.elapsed animating time.
        /// </summary>
        protected double animationCurrentTime = 0.0f;

        /// <summary>
        /// constructor.
        /// </summary>
        public AnimationTile()
        {
            Interval = 0.1f;
            animationPattern = new Dictionary<string, int[]>();
        }

        /// <summary>
        /// OnLoad. Initialize AnimationTile instance.
        /// </summary>
        public override void OnLoad()
        {
            base.OnLoad();
            int indexCount = element.TextureCount;
            pattern = new int[indexCount];
            for (int i = 0; i < indexCount; i++)
            {
                pattern[i] = i;
            }
            AddPattern("default", pattern);
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
        /// AddPattern. add animation pattern of this tilemap texture data.
        /// </summary>
        /// <param name="key">animation name</param>
        /// <param name="pattern">animation pattern array</param>
        public void AddPattern(string key, int[] pattern)
        {
            animationPattern.Add(key, pattern);
        }

        /// <summary>
        /// SwitchPattern. switch animation pattern.
        /// </summary>
        /// <param name="key">animation name</param>
        /// <param name="oneShot">Once or repeatedly</param>
        public void SwitchPattern(string key, bool oneShot = false)
        {
            if (pattern != animationPattern[key])
            {
                pattern = animationPattern[key];
                patternIndex = 0;
                animationName = key;
                IsOneShort = oneShot;
            }
        }

        /// <summary>
        /// RestartAnimation
        /// </summary>
        /// <param name="oneShot">once or repeatedly</param>
        public void RestartAnimation(bool oneShot = false)
        {
            StopAnimation = false;
            patternIndex = 0;
            IsOneShort = oneShot;
        }

        /// <summary>
        /// UpdateComponent. called by game engine to animate this element.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            if (pattern == null)
            {
                return;
            }
            animationCurrentTime += elapsed;
            if (animationCurrentTime > Interval)
            {
                animationCurrentTime = 0;
                if (!StopAnimation)
                {
                    patternIndex++;
                    if (patternIndex >= pattern.Length)
                    {
                        if (IsOneShort)
                        {
                            patternIndex = pattern.Length - 1;
                            StopAnimation = true;
                        }
                        else
                        {
                            patternIndex = 0;
                        }
                    }
                    element.TextureIndex = pattern[patternIndex];
                }
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public override void Dispose()
        { 
            base.Dispose();
            animationPattern = null;
            pattern = null;
        }
    }
}