#nullable disable warnings
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;

namespace MiCore2d
{
    /// <summary>
    /// HardBody. Thie component make element solided object.
    /// </summary>
    public class HardBody : Component
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HardBody()
        {
            Timeout = 0;
            Power = Vector3.Zero;
        }

        /// <summary>
        /// Timeout. timeout of action.
        /// </summary>
        /// <value></value>
        public float Timeout {get; set;}

        /// <summary>
        /// Power. direction and power value.
        /// </summary>
        /// <value>power</value>
        public Vector3 Power {get; set;}

        /// <summary>
        /// AddForce. Add force of moving this element.
        /// </summary>
        /// <param name="directPower">direction of power</param>
        /// <param name="msec">time of keeping power</param>
        public void AddForce(Vector3 directPower, float msec)
        {
            Timeout = msec/1000.0f;
            Power = directPower;
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            if (Timeout > 0.0)
            {
                element.Position += Power;
                Timeout -= (float)elapsed;
            }
            CheckCollision();
        }

        /// <summary>
        /// getCollider. getting collider component specified element.
        /// </summary>
        /// <param name="e">target element</param>
        /// <returns>collider component</returns>
        private Collider getCollider(Element e)
        {
            return e.GetComponent<Collider>();
        }

        /// <summary>
        /// CheckCollsion. checking collision.
        /// </summary>
        protected void CheckCollision()
        {
            Collider collider = getCollider(element);
            if (collider == null)
            {
                return;
            }

            Controller objScript = element.GetComponent<Controller>();
            IDictionaryEnumerator enumerator = gameScene.GetElementEnumerator();

            while (enumerator.MoveNext())
            {
                Element target = (Element)enumerator.Value;
                if (target.Disabled || target.Destroyed)
                {
                    continue;
                }
                if (element.Name != target.Name)
                {
                    Collider target_collider = getCollider(target);
                    if (target_collider != null)
                    {
                        bool is_collision = target_collider.Collision(collider);
                        if (is_collision)
                        {
                            if (target_collider.IsTrigger)
                            {
                                if (!collider.IsCollidedTarget(target))
                                {
                                    collider.AddCollidedTarget(target);
                                    OnEnterCollision(objScript, target);
                                }
                                else
                                {
                                    OnStayCollision(objScript, target);
                                }
                            }
                            if (target_collider.IsSolid)
                            {
                                OnSolidCollision(collider, target_collider);
                            }
                        }
                        else
                        {
                            if (target_collider.IsTrigger)
                            {
                                if (collider.IsCollidedTarget(target))
                                {
                                    collider.RemoveCollidedTarget(target);
                                    OnLeaveCollision(objScript, target);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// CalcObjectPosition. Calecration of position of collistion.
        /// </summary>
        /// <param name="thisPos">this element position</param>
        /// <param name="targetPos">target collided postition</param>
        /// <param name="src">collision component this element</param>
        /// <param name="target">collision component target element</param>
        protected virtual void CalcObjectPosition(Vector3 thisPos, Vector3 targetPos, Collider src, Collider target)
        {
            float distanceY = MathF.Max(thisPos.Y, targetPos.Y) - MathF.Min(thisPos.Y, targetPos.Y);
            float distanceX = MathF.Max(thisPos.X, targetPos.X) - MathF.Min(thisPos.X, targetPos.X);

            float cond_distanceX = src.WidthUnit + target.WidthUnit;
            float cond_distanceY = src.HeightUnit + target.HeightUnit;
            float sinkX = 0.0f;
            float sinkY = 0.0f;

            if (cond_distanceX > distanceX)
            {
                sinkX = cond_distanceX - distanceX;
            }

            if (cond_distanceY > distanceY)
            {
                sinkY = cond_distanceY - distanceY;
            }

            //if (distanceX < distanceY)
            if (sinkX > sinkY)
            {
                if (thisPos.Y < targetPos.Y)
                {
                    //hit under
                    thisPos.Y = targetPos.Y - target.HeightUnit - src.HeightUnit;
                }
                else
                {
                    //hit upper
                    thisPos.Y = targetPos.Y + target.HeightUnit + src.HeightUnit;
                }
            }
            else
            {
                if (thisPos.X < targetPos.X)
                {
                    //hit left
                    thisPos.X = targetPos.X - target.WidthUnit - src.WidthUnit;
                }
                else
                {
                    //hit right
                    thisPos.X = targetPos.X + target.WidthUnit + src.WidthUnit;
                }
            }
            element.Position = thisPos;
        }

        /// <summary>
        /// OnSolidCollision. collided solid object.
        /// </summary>
        /// <param name="src">thie element collider</param>
        /// <param name="target">target element collider</param>
        protected virtual void OnSolidCollision(Collider src, Collider target)
        {
            if (target is TilemapCollider)
            {
                TilemapCollider mapCollider = (TilemapCollider)target;
                Vector3 targetPos;
                bool isCollided = mapCollider.CheckCollidedMap(src, out targetPos);
                if (isCollided)
                {
                    Vector3 thisPos = src.GetPosition();
                    CalcObjectPosition(thisPos, targetPos, src, target);
                }
            }
            else
            {
                Vector3 thisPos = src.GetPosition();
                Vector3 targetPos = target.GetPosition();
                CalcObjectPosition(thisPos, targetPos, src, target);
            }
        }

        /// <summary>
        /// OnEnterCollision. called this function when collided target.
        /// </summary>
        /// <param name="objScript">controller component this element</param>
        /// <param name="target">collided element</param>
        public virtual void OnEnterCollision(Controller objScript, Element target)
        {
            if (objScript != null)
                objScript.OnEnterCollision(target);
        }

        /// <summary>
        /// OnEnterCollision. called this function while  colliding target.
        /// </summary>
        /// <param name="objScript">controller component this element</param>
        /// <param name="target">collided element</param>
        public virtual void OnStayCollision(Controller objScript, Element target)
        {
            if (objScript != null)
                objScript.OnStayCollision(target);
        }

        /// <summary>
        /// OnEnterCollision. called this function while  leaving target.
        /// </summary>
        /// <param name="objScript">controller component this element</param>
        /// <param name="target">collided element</param>
        public virtual void OnLeaveCollision(Controller objScript, Element target)
        {
            if (objScript != null)
                objScript.OnLeaveCollision(target);
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