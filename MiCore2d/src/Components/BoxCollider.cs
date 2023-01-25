#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class BoxCollider : Collider
    {
        public BoxCollider()
        {
        }

        public override void OnLoad()
        {
            WidthUnit = element.Scale.X / 2;
            HeightUnit = element.Scale.Y / 2;
        }

        public virtual Box2 GetBox2()
        {
            return new Box2(element.Position.X - WidthUnit, element.Position.Y - HeightUnit, element.Position.X + WidthUnit, element.Position.Y + HeightUnit);
        }

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

        private void doSolidCollision(BoxCollider target)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();
        }

        private void doSolidCollision(CircleCollider target)
        {
            
        }

        private bool checkCollision(BoxCollider target)
        {
            Vector3 thisPos = GetPosition();
            Vector3 targetPos = target.GetPosition();
            if (thisPos.Z != targetPos.Z)
            {
                return false;
            }
            return GetBox2().Contains(target.GetBox2());
        }

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

        public override void UpdateComponent(double elapsed)
        {
            if (IsDynamic)
            {
                WidthUnit = element.Scale.X / 2;
                HeightUnit = element.Scale.Y / 2;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}