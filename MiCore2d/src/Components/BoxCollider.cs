#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// BoxCollider. Box collision detector.
    /// </summary>
    public class BoxCollider : Collider
    {
        /// <summary>
        /// constructor.
        /// </summary>
        public BoxCollider()
        {
        }

        /// <summary>
        /// OnLoad. Initalize this Component.
        /// </summary>
        public override void OnLoad()
        {
            WidthUnit = element.Scale.X / 2;
            HeightUnit = element.Scale.Y / 2;
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
        /// checkCollision for BoxCollider.
        /// </summary>
        /// <param name="target">target element</param>
        /// <returns>true: collided. false: not collided</returns>
        private bool checkCollision(BoxCollider target)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();
            if (thisPos.Z != targetPos.Z)
            {
                return false;
            }
            
            if (
                (thisPos.X + WidthUnit > targetPos.X - target.WidthUnit)
                && (thisPos.X - WidthUnit < targetPos.X + target.WidthUnit)
                && (thisPos.Y + HeightUnit > targetPos.Y - target.HeightUnit)
                && (thisPos.Y - HeightUnit < targetPos.Y + target.HeightUnit)
            )
            {
                return true;
            }
            return false;
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

            float cond_distanceX = WidthUnit + target.RadiusUnit;
            float cond_distanceY = HeightUnit + target.RadiusUnit;

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
            if (distanceX <= WidthUnit)
                return true;
            if (distanceY <= HeightUnit)
                return true;

            float dist_sq = (distanceX - WidthUnit)*(distanceX - WidthUnit) + (distanceY - HeightUnit)*(distanceY - HeightUnit);
            return (dist_sq <= (target.RadiusUnit * target.RadiusUnit));
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            if (IsDynamic)
            {
                WidthUnit = element.Scale.X / 2;
                HeightUnit = element.Scale.Y / 2;
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