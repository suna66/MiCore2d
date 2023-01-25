#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public abstract class Collider : Component
    {
        private List<Element>? _collidedList = null;

        public bool IsDynamic {get; set;} = false;

        public bool IsSolid { get; set; } = true;

        public bool IsTrigger {get; set;} = false;

        public float WidthUnit {get; set;} = 0.5f;

        public float HeightUnit {get; set;} = 0.5f;

        public float RadiusUnit {
            get
            {
                return WidthUnit;
            }
            set{
                WidthUnit = value;
                HeightUnit = value;
            }
        }

        public Collider()
        {
        }

        public virtual Vector3 GetPosition()
        {
            return element.Position;
        }

        public virtual bool Collision(Collider target)
        {
            return false;
        }

        public void AddCollidedTarget(Element target)
        {
            if (_collidedList == null)
            {
                _collidedList = new List<Element>();
            }
            _collidedList.Add(target);
        }

        public bool IsCollidedTarget(Element target)
        {
            if (_collidedList == null)
            {
                return false;
            }
            foreach(Element e in _collidedList)
            {
                if (e == target)
                    return true;
            }
            return false;
        }

        public void RemoveCollidedTarget(Element target)
        {
            if (_collidedList == null)
            {
                return;
            }
            _collidedList.Remove(target);
        }

        public override void Dispose()
        {
            if (_collidedList != null)
            {
                _collidedList = null;
            }
            base.Dispose();
        }
    }
}