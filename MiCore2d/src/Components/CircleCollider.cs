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
        /// Collision. check collided to line.
        /// </summary>
        /// <param name="line">line</param>
        /// <returns>true: collided, false: not</returns>
        public override bool Collision(Line line)
        {
            return CollisionUtil.LineCircle(line, GetPosition(), RadiusUnit);
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
            return CollisionUtil.CircleCircle(thisPos, RadiusUnit, targetPos, target.RadiusUnit);

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
            return CollisionUtil.BoxCircle(targetPos, target.WidthUnit, target.HeightUnit, thisPos, RadiusUnit);
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