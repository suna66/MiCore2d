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
            //RadiusUnit = element.Scale.Y / 2;
            RadiusUnit = element.Height / 2;
        }

        /// <summary>
        /// Collision. Check collision of targets.
        /// </summary>
        /// <param name="target">target element</param>
        /// <param name="collidedElementPosition">collision element position</param>
        /// <returns>true: collided. false: not collided</returns>
        public override bool Collision(Collider target, out Vector3 collidedElementPosition)
        {
            collidedElementPosition = Vector3.Zero;
            bool is_collision = false;
            if (IsDynamic)
            {
                //RadiusUnit = element.Scale.Y / 2;
                RadiusUnit = element.Height / 2;
            }

            if (target is BoxCollider)
            {
                is_collision = checkCollision(target as BoxCollider, out collidedElementPosition);
            }
            else if (target is CircleCollider)
            {
                is_collision = checkCollision(target as CircleCollider, out collidedElementPosition);
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
            if (IsDynamic)
            {
                //RadiusUnit = element.Scale.Y / 2;
                RadiusUnit = element.Height / 2;
            }
            return CollisionUtil.LineCircle(line, GetPosition(), RadiusUnit);
        }

        /// <summary>
        /// Collision. check collided to point.
        /// </summary>
        /// <param name="point">point</param>
        /// <returns>true: collided, false: not</returns>
        public override bool Collision(Vector2 point)
        {
            if (IsDynamic)
            {
                //RadiusUnit = element.Scale.Y / 2;
                RadiusUnit = element.Height / 2;
            }
            return CollisionUtil.PointCircle(point, GetPosition(), RadiusUnit);
        }
        
        /// <summary>
        /// checkCollision for CircleCollider.
        /// </summary>
        /// <param name="target">target element</param>
        /// <param name="collidedElementPosition">collision vector3 position</param>
        /// <returns>true: collided. false: not collided</returns>
        private bool checkCollision(CircleCollider target, out Vector3 collidedElementPosition)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();
            collidedElementPosition = thisPos;
            return CollisionUtil.CircleCircle(thisPos, RadiusUnit, targetPos, target.RadiusUnit);

        }

        /// <summary>
        /// checkCollision for BoxCollider.
        /// </summary>
        /// <param name="target">target element</param>
        /// <param name="collidedElementPosition">collision vector3 position</param>
        /// <returns>true: collided. false: not collided</returns>
        private bool checkCollision(BoxCollider target, out Vector3 collidedElementPosition)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();
            collidedElementPosition = thisPos;
            return CollisionUtil.BoxCircle(targetPos, target.WidthUnit, target.HeightUnit, thisPos, RadiusUnit);
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
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