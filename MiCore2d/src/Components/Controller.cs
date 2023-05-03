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
        /// getCollider. get collider component specified element.
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
        /// <param name="flowPos">flowing element position</param>
        /// <param name="fixedPos">fixed element postition</param>
        /// <param name="flowCollider">collision component flowing element</param>
        /// <param name="fixedCollider">collision component fixed element</param>
        protected void CalcBoxBox(Vector3 flowPos, Vector3 fixedPos, Collider flowCollider, Collider fixedCollider)
        {
            float distanceY = MathF.Max(flowPos.Y, fixedPos.Y) - MathF.Min(flowPos.Y, fixedPos.Y);
            float distanceX = MathF.Max(flowPos.X, fixedPos.X) - MathF.Min(flowPos.X, fixedPos.X);

            float cond_distanceX = flowCollider.WidthUnit * 0.5f + fixedCollider.WidthUnit * 0.5f;
            float cond_distanceY = flowCollider.HeightUnit * 0.5f + fixedCollider.HeightUnit * 0.5f;
            float sinkX = (cond_distanceX > distanceX) ? cond_distanceX - distanceX : 0.0f;
            float sinkY = (cond_distanceY > distanceY) ? cond_distanceY - distanceY : 0.0f;

            //if (distanceX < distanceY)
            if (sinkX > sinkY)
            {
                if (flowPos.Y < fixedPos.Y)
                {
                    //hit under
                    flowPos.Y = fixedPos.Y - fixedCollider.HeightUnit * 0.5f - flowCollider.HeightUnit * 0.5f;
                }
                else
                {
                    //hit upper
                    flowPos.Y = fixedPos.Y + fixedCollider.HeightUnit * 0.5f + flowCollider.HeightUnit * 0.5f;
                }
            }
            else
            {
                if (flowPos.X < fixedPos.X)
                {
                    //hit left
                    flowPos.X = fixedPos.X - fixedCollider.WidthUnit * 0.5f - flowCollider.WidthUnit * 0.5f;
                }
                else
                {
                    //hit right
                    flowPos.X = fixedPos.X + fixedCollider.WidthUnit * 0.5f + flowCollider.WidthUnit * 0.5f;
                }
            }
            flowCollider.SetPosition(flowPos);
        }

        /// <summary>
        /// CalcCircleCircle. Calculation position of circle collider object hitted circle collider object.
        /// </summary>
        /// <param name="flowPos">flowing element position</param>
        /// <param name="fixedPos">fixed element postition</param>
        /// <param name="flowCollider">collision component flowing element</param>
        /// <param name="fixedCollider">collision component fixed element</param>
        protected void CalcCircleCircle(Vector3 flowPos, Vector3 fixedPos, Collider flowCollider, Collider fixedCollider)
        {
            
            float x0 = 0.0f;
            float y0 = 0.0f;
            if (fixedPos.X > flowPos.X)
            {
                x0 = -(fixedPos.X - flowPos.X);
            }
            else
            {
                x0 = MathF.Abs(flowPos.X - fixedPos.X);
            }
            if (fixedPos.Y > flowPos.Y)
            {
                y0 = -(fixedPos.Y - flowPos.Y);
            }
            else
            {
                y0 = MathF.Abs(flowPos.Y - fixedPos.Y);
            }

            float radius0 = fixedCollider.RadiusUnit;
            float radian = MathF.Atan2(y0, x0);
            float radius1 = flowCollider.RadiusUnit;
            float x1 = (radius1 + radius0) * MathF.Cos(radian) + fixedPos.X;
            float y1 = (radius1 + radius0) * MathF.Sin(radian) + fixedPos.Y;

            //Log.Debug($"(x2, y2) = ({x2}, {y2})");

            flowPos.X = x1;
            flowPos.Y = y1;
            flowCollider.SetPosition(flowPos); 
        }

        /// <summary>
        /// CalcBoxCircle. Calculation position of box collider object hitted circle collider object.
        /// </summary>
        /// <param name="boxPos">flowing element position(box element)</param>
        /// <param name="circlePos">fixed element postition(circle element)</param>
        /// <param name="boxCollider">collision component flowing element</param>
        /// <param name="circleCollider">collision component fixed element</param>
        protected void CalcBoxCircle(Vector3 flowPos, Vector3 fixedPos, Collider flowCollider, Collider fixedCollider)
        {
            float x0 = 0.0f;
            float y0 = 0.0f;

            if (fixedPos.X > flowPos.X)
            {
                x0 = -(fixedPos.X - flowPos.X);
            }
            else
            {
                x0 = MathF.Abs(flowPos.X - fixedPos.X);
            }
            if (fixedPos.Y > flowPos.Y)
            {
                y0 = -(fixedPos.Y - flowPos.Y);
            }
            else
            {
                y0 = MathF.Abs(flowPos.Y - fixedPos.Y);
            }

            float radius0 = fixedCollider.RadiusUnit;
            float radian = MathF.Atan2(y0, x0);
            float x1 = radius0 * MathF.Cos(radian) + fixedPos.X;
            float y1 = radius0 * MathF.Sin(radian) + fixedPos.Y;

            float distanceY = MathF.Max(flowPos.Y, fixedPos.Y) - MathF.Min(flowPos.Y, fixedPos.Y);
            float distanceX = MathF.Max(flowPos.X, fixedPos.X) - MathF.Min(flowPos.X, fixedPos.X);

            float cond_distanceX = flowCollider.WidthUnit * 0.5f + radius0;
            float cond_distanceY = flowCollider.HeightUnit * 0.5f + radius0;
            float sinkX = (cond_distanceX > distanceX) ? cond_distanceX - distanceX : 0.0f;
            float sinkY = (cond_distanceY > distanceY) ? cond_distanceY - distanceY : 0.0f;

            if (sinkX > sinkY)
            {
                float halfHeight = flowCollider.HeightUnit * 0.5f;
                //hit upper or futter
                if (y1 >= (flowPos.Y - halfHeight) && y1 <= (flowPos.Y + halfHeight))
                {
                    float y2 = 0.0f;
                    if (flowPos.Y <= y1)
                    {
                        y2 = y1 - flowCollider.HeightUnit * 0.5f;
                    }
                    else
                    {
                        y2 = y1 + flowCollider.HeightUnit * 0.5f;
                    }
                    flowPos.Y = y2;
                }
            }
            else
            {
                float halfWidth = flowCollider.WidthUnit * 0.5f;
                //hit side
                if (x1 >= (flowPos.X - halfWidth) && x1 <= (flowPos.X + halfWidth))
                {
                    float x2 = 0.0f;
                    if (flowPos.X <= x1)
                    {
                        x2 = x1 - flowCollider.WidthUnit * 0.5f;
                    }
                    else
                    {
                        x2 = x1 + flowCollider.WidthUnit * 0.5f;
                    }
                    flowPos.X = x2;
                }
            }
            flowCollider.SetPosition(flowPos); 
        }

        /// <summary>
        /// CalcBoxCircle. Calculation position of box collider object hitted circle collider object.
        /// </summary>
        /// <param name="boxPos">flowing element position(circole element)</param>
        /// <param name="circlePos">fixed element postition(box element)</param>
        /// <param name="boxCollider">collision component flowing element</param>
        /// <param name="circleCollider">collision component fixed element</param>
        /// <param name="whitch">false: move box object, true: move circle object</param>
        protected void CalcCircleBox(Vector3 flowPos, Vector3 fixedPos, Collider flowCollider, Collider fixedCollider)
        {
            float x0 = 0.0f;
            float y0 = 0.0f;

            Vector3 currentPos = fixedPos;

            if (flowPos.X > fixedPos.X)
            {
                x0 = -(flowPos.X - fixedPos.X);
            }
            else
            {
                x0 = MathF.Abs(fixedPos.X - flowPos.X);
            }
            if (flowPos.Y > fixedPos.Y)
            {
                y0 = -(flowPos.Y - fixedPos.Y);
            }
            else
            {
                y0 = MathF.Abs(fixedPos.Y - flowPos.Y);
            }

            float radius0 = flowCollider.RadiusUnit;
            float radian = MathF.Atan2(y0, x0);
            float x1 = radius0 * MathF.Cos(radian) + flowPos.X;
            float y1 = radius0 * MathF.Sin(radian) + flowPos.Y;

            float distanceY = MathF.Max(fixedPos.Y, flowPos.Y) - MathF.Min(fixedPos.Y, flowPos.Y);
            float distanceX = MathF.Max(fixedPos.X, flowPos.X) - MathF.Min(fixedPos.X, flowPos.X);

            float cond_distanceX = fixedCollider.WidthUnit * 0.5f + radius0;
            float cond_distanceY = fixedCollider.HeightUnit * 0.5f + radius0;
            float sinkX = (cond_distanceX > distanceX) ? cond_distanceX - distanceX : 0.0f;
            float sinkY = (cond_distanceY > distanceY) ? cond_distanceY - distanceY : 0.0f;

            if (sinkX > sinkY)
            {
                float halfHeight = fixedCollider.HeightUnit * 0.5f;
                //hit upper or futter
                if (y1 >= (fixedPos.Y - halfHeight) && y1 <= (fixedPos.Y + halfHeight))
                {
                    float y2 = 0.0f;
                    if (fixedPos.Y <= y1)
                    {
                        y2 = y1 - fixedCollider.HeightUnit * 0.5f;
                    }
                    else
                    {
                        y2 = y1 + fixedCollider.HeightUnit * 0.5f;
                    }
                    fixedPos.Y = y2;
                }
            }
            else
            {
                float halfWidth = fixedCollider.WidthUnit * 0.5f;
                //hit side
                if (x1 >= (fixedPos.X - halfWidth) && x1 <= (fixedPos.X + halfWidth))
                {
                    float x2 = 0.0f;
                    if (fixedPos.X <= x1)
                    {
                        x2 = x1 - fixedCollider.WidthUnit * 0.5f;
                    }
                    else
                    {
                        x2 = x1 + fixedCollider.WidthUnit * 0.5f;
                    }
                    fixedPos.X = x2;
                }
            }
            Vector3 mob = fixedPos - currentPos;
            flowCollider.SetPosition(flowPos - mob);
        }

        /// <summary>
        /// OnSolidCollision. collided solid object.
        /// </summary>
        /// <param name="src">thie element collider</param>
        /// <param name="target">target element collider</param>
        /// <param name="collidedTargetPosition">collided vector3 position</param>
        protected virtual void OnSolidCollision(Collider src, Collider target, Vector3 collidedTargetPosition)
        {
            Collider flowCollider = null;
            Collider fixedCollider = null;
            int weight0 = src.GetElement().Weight;
            int weight1 = target.GetElement().Weight;
            Vector3 flowPos;
            Vector3 fixedPos;

            if (weight0 <= weight1)
            {
                flowCollider = src;
                fixedCollider = target;
                flowPos = src.GetPosition();
                fixedPos = collidedTargetPosition;
            }
            else
            {
                flowCollider = target;
                fixedCollider = src;
                flowPos = collidedTargetPosition;
                fixedPos = src.GetPosition();
            }

            if (flowCollider is CircleCollider && fixedCollider is CircleCollider)
            {
                CalcCircleCircle(flowPos, fixedPos, flowCollider, fixedCollider);
            }
            else if (flowCollider is BoxCollider && fixedCollider is CircleCollider )
            {
                CalcBoxCircle(flowPos, fixedPos, flowCollider, fixedCollider);
            }
            else if (flowCollider is CircleCollider)
            {
                CalcCircleBox(flowPos, fixedPos, flowCollider, fixedCollider);
            }
            else
            {
                CalcBoxBox(flowPos, fixedPos, flowCollider, fixedCollider);
            }
            
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