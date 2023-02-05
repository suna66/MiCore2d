#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Collider. Base class of collision detector.
    /// </summary>
    public abstract class Collider : Component
    {
        /// <summary>
        /// _collidedList. managed collided element list.
        /// </summary>
        private List<Element>? _collidedList = null;

        /// <summary>
        /// IsDynamic.
        /// </summary>
        /// <value>true: change range of collision area dynamicly</value>
        public bool IsDynamic {get; set;} = false;

        /// <summary>
        /// IsSolid. solid object or not.
        /// </summary>
        /// <value>true: solid object, false: not</value>
        public bool IsSolid { get; set; } = true;

        /// <summary>
        /// IsTrigger. calling trigger function when collided element.
        /// </summary>
        /// <value>true: calling trigger function, false: not</value>
        public bool IsTrigger {get; set;} = false;

        /// <summary>
        /// WidthUnit. element size from origin.
        /// </summary>
        /// <value>width size from origin.</value>
        public float WidthUnit {get; set;} = 0.5f;

        /// <summary>
        /// HeightUnit element size from origin.
        /// </summary>
        /// <value>height size from origin</value>
        public float HeightUnit {get; set;} = 0.5f;

        /// <summary>
        /// RadiusUnit. element size of radius.
        /// </summary>
        /// <value>size of radius</value>
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

        /// <summary>
        /// constructor.
        /// </summary>
        public Collider()
        {
        }

        /// <summary>
        /// GetPosition. get element position.
        /// </summary>
        /// <returns>position</returns>
        public virtual Vector3 GetPosition()
        {
            return element.Position;
        }

        /// <summary>
        /// Collision. check collided to target.
        /// </summary>
        /// <param name="target">target element</param>
        /// <returns>true: collided, false: not</returns>
        public virtual bool Collision(Collider target)
        {
            return false;
        }

        /// <summary>
        /// AddCollidedTarget. add collided element to management list. 
        /// </summary>
        /// <param name="target">collided target</param>
        public void AddCollidedTarget(Element target)
        {
            if (_collidedList == null)
            {
                _collidedList = new List<Element>();
            }
            _collidedList.Add(target);
        }

        /// <summary>
        /// IsCollidedTarget. management list has the target.
        /// </summary>
        /// <param name="target">target element</param>
        /// <returns>true: has target, false: not</returns>
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

        /// <summary>
        /// RemoveCollidedTarget. remove the target from collided management list.
        /// </summary>
        /// <param name="target">target element</param>
        public void RemoveCollidedTarget(Element target)
        {
            if (_collidedList == null)
            {
                return;
            }
            _collidedList.Remove(target);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
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