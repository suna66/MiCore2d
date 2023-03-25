#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{   
    /// <summary>
    /// Element. Base class of element.
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// texture. texture instance this element.
        /// </summary>
        protected Texture? texture = null;

        /// <summary>
        /// position.
        /// </summary>
        protected Vector3 position = Vector3.Zero;

        /// <summary>
        /// rotation.
        /// </summary>
        protected Matrix4 rotation = Matrix4.Identity;

        /// <summary>
        /// alpha.
        /// </summary>
        protected float alpha = 1.0f;

        /// <summary>
        /// scale.
        /// </summary>
        protected Vector3 scale = Vector3.One;

        /// <summary>
        /// unit size.
        /// </summary>
        protected float unit = 1.0f;

        /// <summary>
        /// texture index.
        /// </summary>
        protected int textureIndex = 0;

        /// <summary>
        /// game scene instance.
        /// </summary>
        protected GameScene? parentGameScene = null;

        /// <summary>
        /// attached component list.
        /// </summary>
        private List<Component>? _componentList = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Element()
        {
        }

        /// <summary>
        /// Destructor.
        /// </summary> 
        ~Element()
        {
        }

        /// <summary>
        /// Name. element name.
        /// </summary>
        /// <value>name</value>
        public string Name { get; set; }

        /// <summary>
        /// Layer. layer name.
        /// </summary>
        /// <value>name</value>
        public string Layer {get; set; } = "default";

        /// <summary>
        /// Disabled. disable this element or not.
        /// </summary>
        /// <value>true: disable, false: eneble</value>
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// Visibled. visible this element or not.
        /// </summary>
        /// <value>true: visible, false: unvisible</value>
        public bool Visibled { get; set; } = true;

        /// <summary>
        /// Destroyed. destroyed this element.
        /// </summary>
        /// <value>true: destroyed. false: not</value>
        public bool Destroyed { get; set; } = false;

        /// <summary>
        /// DrawRenderer.
        /// </summary>
        /// <value>renderer instance</value>
        public Renderer? DrawRenderer { get; protected set; } = null;

        /// <summary>
        /// GetRenderer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Renderer</returns>
        public T GetRenderer<T>()
        {
            return (T)(object)(DrawRenderer);
        }

        /// <summary>
        /// SetRenderer
        /// </summary>
        /// <param name="renderer">Renderer</param>
        public void SetRenderer(Renderer renderer)
        {
            if (DrawRenderer != null)
            {
                DrawRenderer.Dispose();
            }
            DrawRenderer = renderer;
        }

        /// <summary>
        /// AddComponent.
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>Component</returns>
        public T AddComponent<T>() where T : new()
        {
            T obj = new T();
            if (obj is Component)
            {
                if (_componentList == null)
                {
                    _componentList = new List<Component>();
                }
                Component component = (Component)(object)obj;
                component.SetParent(this);
                component.OnLoad();
                _componentList.Add(component);
                return obj;
            }
            throw new InvalidCastException("cannot convert to Component Object");
        }

        /// <summary>
        /// RemoveComponent.
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        public void RemoveComponent<T>()
        {
            if (_componentList == null)
            {
                return;
            }
            foreach(Component c in _componentList)
            {
                if (c is T)
                {
                    c.Destroyed = true;
                }
            }
        }

        /// <summary>
        /// GetComponent.
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>component</returns>
        public T GetComponent<T>()
        {
            if (_componentList == null)
            {
                return (T)(object)null!;
            }
            foreach(Component c in _componentList)
            {
                if (c is T)
                {
                    return (T)(object)c;
                }
            }
            return (T)(object)null!;
        }

        /// <summary>
        /// GetComponents
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>list of components</returns>
        public List<T> GetComponents<T>()
        {
            List<T> list = new List<T>();
             if (_componentList == null)
            {
                return list;
            }
            foreach(Component c in _componentList)
            {
                if (c is T)
                {
                    list.Add((T)(object)c);
                }
            }
            return list;
        }

        /// <summary>
        /// SetParentGameScene.
        /// </summary>
        /// <param name="scene">game scene</param>
        public void SetParentGameScene(GameScene scene)
        {
            parentGameScene = scene;
        }

        /// <summary>
        /// GetParentGameScene
        /// </summary>
        /// <returns>game scene</returns>
        public GameScene GetParentGameScene()
        {
            return parentGameScene;
        }

        /// <summary>
        /// UpdateComponents. called this function from Scene.
        /// </summary>
        /// <param name="elapsed">elapsed time</param>
        public virtual void UpdateComponents(double elapsed)
        {
            if (Disabled)
                return;
            
            bool hasDestoryed = false;
            if (_componentList != null)
            {
                for (int i = 0; i < _componentList.Count; i++)
                {
                    if (!_componentList[i].Destroyed)
                    {
                        _componentList[i].UpdateComponent(elapsed);
                    }
                    else
                    {
                        hasDestoryed = true;
                        _componentList[i].Dispose();
                    }
                }

                if (hasDestoryed)
                {
                    _componentList.RemoveAll(p => p.Destroyed == true);
                }
            }
        }

        /// <summary>
        /// CopyPosition.
        /// </summary>
        /// <param name="e">target element</param>
        public void CopyPositions(Element e)
        {
            position = e.Position;
            rotation = e.Rotation;
            alpha = e.Alpha;
            SetScale(e.Scale.Y/e.Unit);
        }

        /// <summary>
        /// Position.
        /// </summary>
        /// <value>position</value>
        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// Position. 2D position.
        /// </summary>
        /// <value>position</value>
        public Vector2 Position2d
        {
            get
            {
                return new Vector2(position.X, position.Y);
            }
            set
            {
                position.X = value.X;
                position.Y = value.Y;
            }
        }

        /// <summary>
        /// SetPosition.
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <param name="z">position z</param>
        public void SetPosition(float x, float y, float z)
        {
            position.X = x;
            position.Y = y;
            position.Z = z;
        }

        /// <summary>
        /// AddPositionX.
        /// </summary>
        /// <param name="x">add position x</param>
        public void AddPositionX(float x)
        {
            position.X += x;
        }
    
        /// <summary>
        /// AddPositionY.
        /// </summary>
        /// <param name="y">add position y</param>
        public void AddPositionY(float y)
        {
            position.Y += y;
        }

        /// <summary>
        /// AddPositionZ.
        /// </summary>
        /// <param name="z">add position z</param>
        public void AddPositionZ(float z)
        {
            position.Z += z;
        }

        /// <summary>
        /// AddPosition
        /// </summary>
        /// <param name="pos">position</param>
        public void AddPosition(Vector3 pos)
        {
            position += pos;
        }

        /// <summary>
        /// Scale.
        /// </summary>
        /// <value>scale value</value>
        public Vector3 Scale
        {
            get => scale;
        }

        /// <summary>
        /// SetScale.
        /// </summary>
        /// <param name="value">scale value</param>
        public void SetScale(float value)
        {
            if (value < 0.0f)
            {
                value = MathF.Abs(value);
            }
            float aspectRatio = 1.0f;
            if (texture != null)
                aspectRatio = texture.Width / (float)texture.Height;
    
            scale.X = unit * aspectRatio * value;
            scale.Y = unit * value;
            scale.Z = 1.0f;
        }

        /// <summary>
        /// SetScaleX
        /// </summary>
        /// <param name="value">scale value</param>
        public void SetScaleX(float value)
        {
            scale.X = unit * value;
        }

        /// <summary>
        /// SetScaleY
        /// </summary>
        /// <param name="value">scale value</param>
        public void SetScaleY(float value)
        {
            scale.Y = unit * value;
        }

        /// <summary>
        /// Alpha.
        /// </summary>
        /// <value>alpha value</value>
        public float Alpha
        {
            get => alpha;
            set
            {
                alpha = value;
            }
        }

        /// <summary>
        /// Unit.
        /// </summary>
        /// <value>unit size</value>
        public float Unit
        {
            get => unit;
            set
            {
                unit = value;
            }
        }

        /// <summary>
        /// Texture.
        /// </summary>
        /// <value>texture</value>
        public Texture Texture
        {
          get => texture;
          set {
            texture = value;
          }
        }

        /// <summary>
        /// TextureIndex.
        /// </summary>
        /// <value>index of texture</value>
        public int TextureIndex
        {
            get => textureIndex;
            set
            {
                textureIndex = value;
            }
        }

        /// <summary>
        /// TextureCount.
        /// </summary>
        /// <value>index number of texture</value>
        public int TextureCount
        {
            get
            {
                return texture.TextureCount;
            }
        }

        /// <summary>
        /// IncrementTextureIndex.
        /// </summary>
        public void IncrementTextureIndex()
        {
            if (texture == null)
                return;

            textureIndex++;
            if (textureIndex >= texture.TextureCount)
            {
                textureIndex = 0;
            }
        }

        /// <summary>
        /// Rotation.
        /// </summary>
        /// <value>rotated matrix</value>
        public Matrix4 Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
            }
        }

        /// <summary>
        /// SetRotation
        /// </summary>
        /// <param name="x">rotation x</param>
        /// <param name="y">rotation y</param>
        /// <param name="z">rotation z</param>
        public void SetRotation(float x, float y, float z)
        {
            rotation = Matrix4.Identity;
            if (x != 0.0f)
            {
                rotation *= Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(x));
            }
            if (y != 0.0f)
            {
                rotation *= Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(y));
            }
            if (z != 0.0f)
            {
                rotation *= Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(z));
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            if (DrawRenderer != null)
            {
                DrawRenderer?.Dispose();
            }
            if (texture != null)
            {
                texture.Dispose();
            }
            if (_componentList != null)
            {
                for (int i = 0; i < _componentList.Count; i++)
                {
                    _componentList[i].Dispose();
                }
                _componentList = null;
            }
        }
    }
}