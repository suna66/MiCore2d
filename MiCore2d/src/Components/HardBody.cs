#nullable disable warnings
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;

namespace MiCore2d
{
    public class HardBody : Component
    {
        public HardBody()
        {
            Timeout = 0;
            Power = Vector3.Zero;
        }

        public float Timeout {get; set;}
        public Vector3 Power {get; set;}

        public void AddForce(Vector3 directPower, float msec)
        {
            Timeout = msec/1000.0f;
            Power = directPower;
        }

        public override void UpdateComponent(double elapsed)
        {
            if (Timeout > 0.0)
            {
                element.Position += Power;
                Timeout -= (float)elapsed;
            }
            CheckCollision();
        }

        private Collider getCollider(Element e)
        {
            return e.GetComponent<Collider>();
        }

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

        public virtual void OnEnterCollision(Controller objScript, Element target)
        {
            if (objScript != null)
                objScript.OnEnterCollision(target);
        }

        public virtual void OnStayCollision(Controller objScript, Element target)
        {
            if (objScript != null)
                objScript.OnStayCollision(target);
        }

        public virtual void OnLeaveCollision(Controller objScript, Element target)
        {
            if (objScript != null)
                objScript.OnLeaveCollision(target);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}