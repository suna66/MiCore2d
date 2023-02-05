#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// TilemapCollider.
    /// </summary>
    public class TilemapCollider : Collider
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public TilemapCollider()
        {
        }

        /// <summary>
        /// OnLoad. Initialize Component.
        /// </summary>
        public override void OnLoad()
        {
            WidthUnit = element.Scale.X / 2;
            HeightUnit = element.Scale.Y / 2;
        }

        /// <summary>
        /// GetTilemapData. getting tilemap data.
        /// </summary>
        /// <returns>tilemap array list</returns>
        public float[] GetTilemapData()
        {
            TilemapSprite sprite = (TilemapSprite)element;
            return sprite.GetPositionMap();
        }

        /// <summary>
        /// CheckCollidedMap. checking collition of each map data.
        /// </summary>
        /// <param name="target">target element collider</param>
        /// <param name="collidedPos">out parameter. collided map position</param>
        /// <returns>true: collided, false: not</returns>
        public bool CheckCollidedMap(Collider target, out Vector3 collidedPos)
        {
            collidedPos = Vector3.Zero;
            if (element is not TilemapSprite)
            {
                return false;
            }
            TilemapSprite sprite = (TilemapSprite)element;
            float[] map = sprite.GetPositionMap();
            int num = map.Length;
            bool isCollision = false;

            for (int i = 0; i < num; i += 4)
            {
                Vector3 tilePos = new Vector3(map[i], map[i + 1], map[i + 2]);
                Vector3 localPos = sprite.Position + tilePos;
                
                if  (target is BoxCollider)
                {
                    isCollision = checkCollision(localPos, (BoxCollider)target);
                }
                else if (target is CircleCollider)
                {
                    isCollision = checkCollision(localPos, (CircleCollider)target);
                }

                if (isCollision)
                {
                    collidedPos = localPos;
                    return isCollision;
                }
            }
            return false;
        }

        /// <summary>
        /// Collision. checking collision.
        /// </summary>
        /// <param name="target">target element collider</param>
        /// <returns>true: collided, false: not</returns>
        public override bool Collision(Collider target)
        {
            Vector3 pos;
            return CheckCollidedMap(target, out pos);
        }

        /// <summary>
        /// checkCollision. checking collision of box collider.
        /// </summary>
        /// <param name="localPos">map position</param>
        /// <param name="target">target element box collider.</param>
        /// <returns>true: collided, false: not</returns>
        private bool checkCollision(Vector3 localPos, BoxCollider target)
        {
            Vector3 targetPos = target.GetPosition();
            if (localPos.Z != targetPos.Z)
            {
                return false;
            }
            if (
                (localPos.X + WidthUnit > targetPos.X - target.WidthUnit)
                && (localPos.X - WidthUnit < targetPos.X + target.WidthUnit)
                && (localPos.Y + HeightUnit > targetPos.Y - target.HeightUnit)
                && (localPos.Y - HeightUnit < targetPos.Y + target.HeightUnit)
            )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// checkCollision. checking collision of circle collider.
        /// </summary>
        /// <param name="localPos">map position</param>
        /// <param name="target">target element circle collider.</param>
        ///  <returns>true: collided, false: not</returns>
        private bool checkCollision(Vector3 localPos, CircleCollider target)
        {
            Vector3 thisPos = localPos;
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