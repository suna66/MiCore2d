#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class CircleCollider : Collider
    {
        public CircleCollider()
        {
        }

        public override void OnLoad()
        {
            RadiusUnit = element.Scale.Y / 2;
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

        }

        private void doSolidCollision(CircleCollider target)
        {

        }
        
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

        public override void UpdateComponent(double elapsed)
        {
            if (IsDynamic)
            {
                RadiusUnit = element.Scale.Y / 2;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}