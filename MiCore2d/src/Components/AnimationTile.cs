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

        public double Interval { get; set; }

        public string AnimationName { get => animationName; }

        public bool StopAnimation {get; set;} = false;

        public bool IsOneShort { get; set; } = false;

        public void AddPattern(string key, int[] pattern)
        {
            animationPattern.Add(key, pattern);
        }

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

        public void RestartAnimation(bool oneShot = false)
        {
            StopAnimation = false;
            patternIndex = 0;
            IsOneShort = oneShot;
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

        public override void Dispose()
        { 
            base.Dispose();
            animationPattern = null;
            pattern = null;
        }
    }
}