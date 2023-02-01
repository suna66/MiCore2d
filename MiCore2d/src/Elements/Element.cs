#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{   
    public abstract class Element
    {
        protected Texture? texture = null;
        protected Vector3 position = Vector3.Zero;
        protected Matrix4 rotation = Matrix4.Identity;
        protected float alpha = 1.0f;
        protected Vector3 scale = Vector3.One;
        protected float unit = 1.0f;
        protected int textureIndex = 0;

        protected GameScene? parentGameScene = null;

        private List<Component>? _componentList = null;

        public Element()
        {
            Visible = true;
            Destroyed = false;
        }

        ~Element()
        {
        }

        public string Name { get; set; }

        public string Layer {get; set; } = "default";

        public bool Visible { get; set; }

        public bool Destroyed { get; set; }

        public Renderer? DrawRenderer { get; set; } = null;

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

        public void SetParentGameScene(GameScene scene)
        {
            parentGameScene = scene;
        }

        public GameScene GetParentGameScene()
        {
            return parentGameScene;
        }

        public virtual void UpdateComponents(double elapsed)
        {
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

        public void CopyPositions(Element e)
        {
            position = e.Position;
            rotation = e.Rotation;
            alpha = e.Alpha;
            SetScale(e.Scale.Y/e.Unit);
        }

        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
            }
        }

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

        public void SetPosition(float x, float y, float z)
        {
            position.X = x;
            position.Y = y;
            position.Z = z;
        }

        public void AddPositionX(float x)
        {
            position.X += x;
        }
    
        public void AddPositionY(float y)
        {
            position.Y += y;
        }

        public void AddPositionZ(float z)
        {
            position.Z += z;
        }

        public Vector3 Scale
        {
            get => scale;
        }

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

        public void SetScaleX(float value)
        {
            scale.X = unit * value;
        }

        public void SetScaleY(float value)
        {
            scale.Y = unit * value;
        }

        public float Alpha
        {
            get => alpha;
            set
            {
                alpha = value;
            }
        }

        public float Unit
        {
            get => unit;
            set
            {
                unit = value;
            }
        }

        public Texture Texture
        {
          get => texture;
          set {
            texture = value;
          }
        }

        public int TextureIndex
        {
            get => textureIndex;
            set
            {
                textureIndex = value;
            }
        }

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

        public Matrix4 Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
            }
        }

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

        public virtual void Dispose()
        {
            texture = null;
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