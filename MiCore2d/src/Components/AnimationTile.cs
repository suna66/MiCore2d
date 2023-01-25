#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class AnimationTile : Component
    {
        private Dictionary<string, int[]>? animationPattern = null;
        private int[]? pattern = null;
        private int patternIndex = 0;
        protected string? animationName = null;

        protected double animationCurrentTime = 0.0f;

        public AnimationTile()
        {
            Interval = 0.1f;
            animationPattern = new Dictionary<string, int[]>();
        }

        public double Interval { get; set; }

        public string AnimationName { get => animationName; }

        public void AddPattern(string key, int[] pattern)
        {
            animationPattern.Add(key, pattern);
        }

        public void SwitchPattern(string key)
        {
            if (pattern != animationPattern[key])
            {
                pattern = animationPattern[key];
                patternIndex = 0;
                animationName = key;
            }
        }

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
                patternIndex++;
                if (patternIndex >= pattern.Length)
                {
                    patternIndex = 0;
                }
                element.TextureIndex = pattern[patternIndex];
            }
        }

        public override void Dispose()
        { 
            base.Dispose();
            animationPattern = null;
            pattern = null;
        }
    }
}