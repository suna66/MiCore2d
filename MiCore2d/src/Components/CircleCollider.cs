#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// CircleCollider. Circle Collision detector.
    /// </summary>
    public class CircleCollider : Collider
    {
        /// <summary>
        /// constructor.
        /// </summary>
        public CircleCollider()
        {
        }

        /// <summary>
        /// OnLoad. Initialize this component.
        /// </summary>
        public override void OnLoad()
        {
            RadiusUnit = element.Scale.Y / 2;
        }

        /// <summary>
        /// Collision. Check collision of targets.
        /// </summary>
        /// <param name="target">target element</param>
        /// <returns>true: collided. false: not collided</returns>
        public override bool Collision(Collider target)
        {
            bool is_collision = false;
            if (target is BoxCollider)
            {
                is_collision = checkCollision((BoxCollider)target);
            }
            else if (target is CircleCollider)
            {
                is_collision = checkCollision((CircleCollider)target);
            }
            return is_collision;
        }
        
        /// <summary>
        /// checkCollision for CircleCollider.
        /// </summary>
        /// <param name="target">target element</param>
        /// <returns>true: collided. false: not collided</returns>
        private bool checkCollision(CircleCollider target)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();
            float cond_distance = RadiusUnit + target.RadiusUnit;

            float maxY = MathF.Max(thisPos.Y, targetPos.Y);
            float minY = MathF.Min(thisPos.Y, targetPos.Y);
            float distanceY = maxY - minY;
            float maxX = MathF.Max(thisPos.X, targetPos.X);
            float minX = MathF.Min(thisPos.X, targetPos.X);
            float distanceX = maxX - minX;

            if (thisPos.Z != targetPos.Z)
            {
                return false;
            }

            return ((cond_distance * cond_distance) >= ((distanceX * distanceX) + (distanceY * distanceY)));

        }

        /// <summary>
        /// checkCollision for BoxCollider.
        /// </summary>
        /// <param name="target">target element</param>
        /// <returns>true: collided. false: not collided</returns>
        private bool checkCollision(BoxCollider target)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();

            float cond_distanceX = RadiusUnit + target.WidthUnit;
            float cond_distanceY = RadiusUnit + target.HeightUnit;

            float maxY = MathF.Max(thisPos.Y, targetPos.Y);
            float minY = MathF.Min(thisPos.Y, targetPos.Y);
            float distanceY = maxY - minY;
            float maxX = MathF.Max(thisPos.X, targetPos.X);
            float minX = MathF.Min(thisPos.X, targetPos.X);
            float distanceX = maxX - minX;

            if (thisPos.Z != targetPos.Z)
            {
                return false;
            }
            if (distanceX > cond_distanceX)
                return false;
            if (distanceY > cond_distanceY)
                return false;
            if (distanceX <= target.WidthUnit)
                return true;
            if (distanceY <= target.HeightUnit)
                return true;

            float dist_sq = (distanceX - target.WidthUnit)*(distanceX - target.WidthUnit) + (distanceY - target.HeightUnit)*(distanceY - target.HeightUnit);
            return (dist_sq <= (RadiusUnit * RadiusUnit));
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            if (IsDynamic)
            {
                RadiusUnit = element.Scale.Y / 2;
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}