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
            // WidthUnit = element.Scale.X;
            // HeightUnit = element.Scale.Y;
            WidthUnit = element.Width;
            HeightUnit = element.Height;

        }

        /// <summary>
        /// Collision. Check collision of targets.
        /// </summary>
        /// <param name="target">target element</param>
        /// <param name="collidedElementPosition">collision vector3 position</param>
        /// <returns>true: collided. false: not collided</returns>
        public override bool Collision(Collider target, out Vector3 collidedElementPosition)
        {
            collidedElementPosition = Vector3.Zero;
            bool is_collision = false;
            if (IsDynamic)
            {
                // WidthUnit = element.Scale.X;
                // HeightUnit = element.Scale.Y;
                WidthUnit = element.Width;
                HeightUnit = element.Height;
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
                // WidthUnit = element.Scale.X;
                // HeightUnit = element.Scale.Y;
                WidthUnit = element.Width;
                HeightUnit = element.Height;
            }
            return CollisionUtil.LineBox(line, GetPosition(), WidthUnit, HeightUnit);
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
                // WidthUnit = element.Scale.X;
                // HeightUnit = element.Scale.Y;
                WidthUnit = element.Width;
                HeightUnit = element.Height;
            }
            return CollisionUtil.PointBox(point, GetPosition(), WidthUnit, HeightUnit);
        }

        /// <summary>
        /// checkCollision for BoxCollider.
        /// </summary>
        /// <param name="target">target element</param>
        /// <param name="collidedElementPosition">collision element position</param>
        /// <returns>true: collided. false: not collided</returns>
        private bool checkCollision(BoxCollider target, out Vector3 collidedElementPosition)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();
            collidedElementPosition = thisPos;
            return CollisionUtil.BoxBox(thisPos, WidthUnit, HeightUnit, targetPos, target.WidthUnit, target.HeightUnit);
        }

        /// <summary>
        /// checkCollision for CircleCollider.
        /// </summary>
        /// <param name="target">target element</param>
        /// <param name="collidedElementPosition">collision element position</param>
        /// <returns>true: collided. false: not collided</returns>
        private bool checkCollision(CircleCollider target, out Vector3 collidedElementPosition)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();
            collidedElementPosition = thisPos;
            return CollisionUtil.BoxCircle(thisPos, WidthUnit, HeightUnit, targetPos, target.RadiusUnit);
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