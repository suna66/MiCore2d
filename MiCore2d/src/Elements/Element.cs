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
        /// scale.
        /// </summary>
        protected Vector3 scale = Vector3.One;


        /// <summary>
        /// texture index.
        /// </summary>
        protected int textureIndex = 0;

        /// <summary>
        /// attached component list.
        /// </summary>
        private List<Component>? _componentList = null;

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
        /// RendererOrder
        /// </summary>
        /// <value>default zero</value>
        public int RendererOrder { get; set; } = 0;

        /// <summary>
        /// CurrentGameScene
        /// </summary>
        /// <value></value>
        public GameScene CurrentGameScene { get; set; } = null!;

        /// <summary>
        /// Alpha
        /// </summary>
        /// <value></value>
        public float Alpha { get; set; } = 1.0f;

        /// <summary>
        /// Unit
        /// </summary>
        /// <value></value>
        public float Unit { get; protected set; } = 1.0f;

        /// <summary>
        /// AspectRatio
        /// </summary>
        /// <value></value>
        public float AspectRatio {get; protected set; } = 1.0f;

        /// <summary>
        /// RelationElement
        /// </summary>
        /// <value></value>
        public Element? RelationElement {get; set;} = null;

        /// <summary>
        /// RadianX
        /// </summary>
        /// <value></value>
        public float RadianX {get; set; } = 0.0f;

        /// <summary>
        /// RadianY
        /// </summary>
        /// <value></value>
        public float RadianY {get; set; } = 0.0f;

        /// <summary>
        /// RadianZ
        /// </summary>
        /// <value></value>
        public float RadianZ {get; set; } = 0.0f;

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
            RadianX = e.RadianX;
            RadianY = e.RadianY;
            RadianZ = e.RadianZ;
            Alpha = e.Alpha;
            scale = Scale;
        }

        /// <summary>
        /// Position.
        /// </summary>
        /// <value>position</value>
        public Vector3 Position
        {
            get
            {
                if (RelationElement != null)
                {
                    return RelationElement.Position + position;
                }
                else
                {
                    return position;
                }

            }
            set
            {
                if (RelationElement != null)
                {
                    position = value - RelationElement.Position;
                }
                else
                {
                    position = value;
                }
            }
        }

        /// <summary>
        /// LocalPosition
        /// </summary>
        /// <value>local position</value>
        public Vector3 LocalPosition
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Position. 2D position.
        /// </summary>
        /// <value>position</value>
        public Vector2 Position2d
        {
            get
            {
                if (RelationElement != null)
                {
                    Vector3 wpos = RelationElement.Position + position;
                    return wpos.Xy;
                }
                else
                {
                    return position.Xy;
                }
            }
            set
            {
                if (RelationElement != null)
                {
                    Vector3 wpos = new Vector3(value.X, value.Y, Position.Z);
                    position = wpos - RelationElement.Position;
                }
                else
                {
                    position.X = value.X;
                    position.Y = value.Y;
                }
            }
        }

        /// <summary>
        /// LocalPosition2d
        /// </summary>
        /// <value>local position</value>
        public Vector2 LocalPosition2d
        {
            get
            {
                return position.Xy;
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
            if (RelationElement != null)
            {
                position = (new Vector3(x, y, z)) - RelationElement.Position;
            }
            else
            {
                position.X = x;
                position.Y = y;
                position.Z = z;
            }
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
        /// Width
        /// </summary>
        /// <value>width unit size</value>
        public float Width
        {
            get
            {
                return Unit * AspectRatio * scale.X * MathF.Abs(MathF.Cos(RadianY));
            }
        }

        /// <summary>
        /// Height
        /// </summary>
        /// <value>height unit size</value>
        public float Height
        {
            get
            {
                return Unit * scale.Y * MathF.Abs(MathF.Cos(RadianX));
            }
        }

        /// <summary>
        /// OriginWidth
        /// </summary>
        /// <value>original size of unit</value>
        public float OriginWidth
        {
            get
            {
                return Unit * AspectRatio;
            }
        }

        /// <summary>
        /// OriginHeight
        /// </summary>
        /// <value>original size of unit</value>
        public float OriginHeight
        {
            get
            {
                return Unit;
            }
        }

        /// <summary>
        /// Element Size
        /// </summary>
        /// <value>Vector3</value>
        public Vector3 Size
        {
            get
            {
                return new Vector3(Width, Height, 1.0f);
            }
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
            // float aspectRatio = 1.0f;
            // if (texture != null)
            //     aspectRatio = texture.Width / (float)texture.Height;
    
            // scale.X = Unit * aspectRatio * value;
            // scale.Y = Unit * value;
            // scale.Z = 1.0f;
            scale.X = value;
            scale.Y = value;
            scale.Z = 1.0f;
        }

        /// <summary>
        /// SetScaleX
        /// </summary>
        /// <param name="value">scale value</param>
        public void SetScaleX(float value)
        {
            //scale.X = Unit * value;
            scale.X = value;
        }

        /// <summary>
        /// SetScaleY
        /// </summary>
        /// <param name="value">scale value</param>
        public void SetScaleY(float value)
        {
            //scale.Y = Unit * value;
            scale.Y = value;
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
            get
            {
                rotation = Matrix4.Identity;
                if (RadianX != 0.0f)
                {
                    rotation *=  Matrix4.CreateRotationX(RadianX);
                }
                if (RadianY != 0.0f)
                {
                    rotation *=  Matrix4.CreateRotationY(RadianY);
                }
                if (RadianZ != 0.0f)
                {
                    rotation *=  Matrix4.CreateRotationZ(RadianZ);
                }
                return rotation;
            }
        }

        /// <summary>
        /// OriginVertex
        /// </summary>
        /// <value>non-rotated and original position shape vertexs</value>
        public Vector2[] OriginVertex
        {
            get
            {
                float x = Width * 0.5f;
                float y = Height * 0.5f;
                Vector2[] vertex = new Vector2[4];
                vertex[0] = new Vector2(-x,  y);
                vertex[1] = new Vector2( x,  y);
                vertex[2] = new Vector2( x, -y);
                vertex[3] = new Vector2(-x, -y);
                return vertex;
            }
        }

        /// <summary>
        /// Vertex
        /// </summary>
        /// <value>vertex position</value>
        public Vector2[] Vertex
        {
            get
            {
                int i = 0;
                Vector2[] vertex = new Vector2[4];
                Vector2[] origin = OriginVertex;
                foreach(Vector2 v in origin)
                {
                    //Z
                    float x = v.X * MathF.Cos(RadianZ) + v.Y * MathF.Sin(RadianZ);
                    float y = -v.X * MathF.Sin(RadianZ) + v.Y * MathF.Cos(RadianZ);
                    //x
                    y = y * MathF.Cos(RadianX);
                    //y
                    x = x * MathF.Cos(RadianY);
                    vertex[i++] = new Vector2(x + position.X, y + position.Y); 
                }
                return vertex;
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