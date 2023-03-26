#nullable disable warnings
using OpenTK.Mathematics;
using System.Collections;

namespace MiCore2d
{
    /// <summary>
    /// Controller. Base class of controller of element.
    /// </summary>
    public abstract class Controller : Component
    {
        /// <summary>
        ///  _start. status of called start method.
        /// </summary>
        private bool _start = false;

        /// <summary>
        /// TargetLayer.
        /// </summary>
        /// <value></value>
        public List<string> TargetLayer = new List<string>();

        /// <summary>
        /// IsEnableCollsionDetect
        /// </summary>
        /// <value>true: call collision detact function when updating Controller component</value>
        public bool IsEnableCollsionDetect {get; set;} = false;

        /// <summary>
        ///  constructor.
        /// </summary>
        public Controller()
        {
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            if (!_start)
            {
                Start();
                _start = true;
            }
            else
            {
                Update(elapsed);
                if (IsEnableCollsionDetect)
                {
                    CollisionDetector();
                }
            }
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
        /// CollisionDetector.
        /// </summary>
        public void CollisionDetector()
        {
            Collider collider = getCollider(element);
            if (collider == null)
            {
                return;
            }
            if (element.Disabled || element.Destroyed)
            {
                collider.RemoveColidedAll();
                return;
            }

            IDictionaryEnumerator enumerator = gameScene.GetElementEnumerator();
            while (enumerator.MoveNext())
            {
                Element target = (Element)enumerator.Value;
                if (target.Disabled || target.Destroyed)
                {
                    continue;
                }
                if (TargetLayer.Count > 0)
                {
                    if (!TargetLayer.Contains(target.Layer))
                    {
                        continue;
                    }
                }
                if (element.Name != target.Name)
                {
                    Vector3 collidedTargetPosition;
                    Collider target_collider = getCollider(target);
                    if (target_collider != null)
                    {
                        bool is_collision = target_collider.Collision(collider, out collidedTargetPosition);
                        if (is_collision)
                        {
                            if (target_collider.IsTrigger)
                            {
                                if (!collider.IsCollidedTarget(target))
                                {
                                    collider.AddCollidedTarget(target);
                                    OnEnterCollision(target, collidedTargetPosition);
                                }
                                else
                                {
                                    OnStayCollision(target, collidedTargetPosition);
                                }
                            }
                            if (target_collider.IsSolid)
                            {
                                OnSolidCollision(collider, target_collider, collidedTargetPosition);
                            }
                        }
                        else
                        {
                            if (target_collider.IsTrigger)
                            {
                                if (collider.IsCollidedTarget(target))
                                {
                                    collider.RemoveCollidedTarget(target);
                                    OnLeaveCollision(target);
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

            float cond_distanceX = src.WidthUnit/2 + target.WidthUnit/2;
            float cond_distanceY = src.HeightUnit/2 + target.HeightUnit/2;
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
                    thisPos.Y = targetPos.Y - target.HeightUnit/2 - src.HeightUnit/2;
                }
                else
                {
                    //hit upper
                    thisPos.Y = targetPos.Y + target.HeightUnit/2 + src.HeightUnit/2;
                }
            }
            else
            {
                if (thisPos.X < targetPos.X)
                {
                    //hit left
                    thisPos.X = targetPos.X - target.WidthUnit/2 - src.WidthUnit/2;
                }
                else
                {
                    //hit right
                    thisPos.X = targetPos.X + target.WidthUnit/2 + src.WidthUnit/2;
                }
            }
            //element.Position = thisPos;
            src.SetPosition(thisPos);
        }

        /// <summary>
        /// OnSolidCollision. collided solid object.
        /// </summary>
        /// <param name="src">thie element collider</param>
        /// <param name="target">target element collider</param>
        /// <param name="collidedTargetPosition>collided vector3 position</param>
        protected virtual void OnSolidCollision(Collider src, Collider target, Vector3 collidedTargetPosition)
        {
            //Console.WriteLine($"position {collidedPosition}");
            Vector3 thisPos = src.GetPosition();
            CalcObjectPosition(thisPos, collidedTargetPosition, src, target);
        }


        /// <summary>
        /// OnEnterCollision. called this function when collided target.
        /// </summary>
        /// <param name="target">collided element</param>
        /// <param name="collidedPosition">collieded vecto3 position</param>
        public virtual void OnEnterCollision(Element target, Vector3 collidedTargetPosition)
        {
            OnEnterCollision(target);
        }

        /// <summary>
        /// OnEnterCollision. called this function while  colliding target.
        /// </summary>
        /// <param name="target">collided element</param>
        /// <param name="collidedPosition">collieded vecto3 position</param>
        public virtual void OnStayCollision(Element target, Vector3 collidedTargetPosition)
        {
            OnStayCollision(target);
        }

        /// <summary>
        /// OnEnterCollision. called this function while  leaving target.
        /// </summary>
        /// <param name="target">collided element</param>
        public virtual void OnLeaveCollision(Element target)
        {
        }

        /// <summary>
        /// OnEnterCollision. called this method when element collided target element.
        /// </summary>
        /// <param name="target">target element</param>
        public virtual void OnEnterCollision(Element target)
        {
        }

        /// <summary>
        /// OnStayCollision. called this method while element is colliding target element.
        /// </summary>
        /// <param name="target">target element</param>
        public virtual void OnStayCollision(Element target)
        {
        }

        /// <summary>
        /// Start. Abstruct method. Callded first time of Update function.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Update. Abstruct method. Callded this method  each frame time.
        /// </summary>
        /// <param name="elapsed">elpased time of frame</param>
        public abstract void Update(double elapsed);

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}