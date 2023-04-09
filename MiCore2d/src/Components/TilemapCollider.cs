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
        /// Tilemap Positions
        /// </summary>
        /// <value></value>
        public Vector3[] PositionList {get; set;} = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TilemapCollider()
        {
            IsDynamic = false;
        }

        /// <summary>
        /// OnLoad. Initialize Component.
        /// </summary>
        public override void OnLoad()
        {
            WidthUnit = element.Scale.X;
            HeightUnit = element.Scale.Y;
            PositionList = GetTilemapVector();
        }

        /// <summary>
        /// GetTilemapData. getting tilemap data.
        /// </summary>
        /// <returns>tilemap array list</returns>
        public Vector4[] GetTilemapData()
        {
            if (element is not TilemapSprite)
            {
                return null;
            }
            TilemapSprite sprite = element as TilemapSprite;
            return sprite?.TileMap;
        }

        /// <summary>
        /// GetTilemapVector
        /// </summary>
        /// <returns>Vector3 from tilemap</returns>
        private Vector3[] GetTilemapVector()
        {
            Vector4[] map = GetTilemapData();
            if (map == null)
            {
                return null;
            }
            int num = map.Length;
            Vector3[] list = new Vector3[num];
            //int index = 0;
            for (int i = 0; i < num; i++) {
                //Vector3 tilePos = new Vector3(map[i], map[i + 1], map[i + 2]);
                list[i] = map[i].Xyz;
            }
            return list;
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
            bool isCollision = false;

            foreach(Vector3 pos in PositionList ?? new Vector3[0])
            {
                if (pos.Z != 0.0f)
                {
                    continue;
                }
                
                if (target is BoxCollider)
                {
                    isCollision = checkCollision(pos, target as BoxCollider);
                }
                else if (target is CircleCollider)
                {
                    isCollision = checkCollision(pos, target as CircleCollider);
                }
                
                if (isCollision)
                {
                    collidedPos = pos;
                    return isCollision;
                }

            }
            return false;
        }

        /// <summary>
        /// Collision. checking collision.
        /// </summary>
        /// <param name="target">target element collider</param>
        /// <param name="collidedElementPosition">collision element position</param>
        /// <returns>true: collided, false: not</returns>
        public override bool Collision(Collider target, out Vector3 collidedElementPosition)
        {
            if (IsDynamic)
            {
                WidthUnit = element.Scale.X;
                HeightUnit = element.Scale.Y;
                PositionList = GetTilemapVector();
            }
            collidedElementPosition = Vector3.Zero;
            bool isCollided = CheckCollidedMap(target, out collidedElementPosition);
            return isCollided;
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
                WidthUnit = element.Scale.X;
                HeightUnit = element.Scale.Y;
                PositionList = GetTilemapVector();
            }
            foreach(Vector3 pos in PositionList)
            {
                if (pos.Z != 0.0f)
                {
                    continue;
                }
                bool collided = CollisionUtil.LineBox(line, pos, WidthUnit, HeightUnit);
                if (collided)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Collision(Vector2 point)
        {
            if (IsDynamic)
            {
                WidthUnit = element.Scale.X;
                HeightUnit = element.Scale.Y;
                PositionList = GetTilemapVector();
            }
            foreach(Vector3 pos in PositionList)
            {
                if (pos.Z != 0.0f)
                {
                    continue;
                }
                bool collided = CollisionUtil.PointBox(point, pos, WidthUnit, HeightUnit);
                if (collided)
                {
                    return true;
                }
            }
            return false;
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
            return CollisionUtil.BoxBox(localPos, WidthUnit, HeightUnit, targetPos, target.WidthUnit, target.HeightUnit);
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