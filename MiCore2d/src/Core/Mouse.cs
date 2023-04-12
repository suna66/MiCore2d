using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace MiCore2d
{
    public struct MousePosition
    {
        /// <summary>
        /// Position
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Local Position
        /// </summary>
        public Vector2 LocalPosition; 

        /// <summary>
        /// Mouse move delta X
        /// </summary>
        public float DeltaX;

        /// <summary>
        /// Mouse move delta Y
        /// </summary>
        public float DeltaY;

        /// <summary>
        /// Mouse Wheel offset X
        /// </summary>
        public float OffsetX;

        /// <summary>
        /// Mouse Wheel offset Y
        /// </summary>
        public float OffsetY;

        /// <summary>
        /// Init
        /// </summary>
        public void Init()
        {
            Position = Vector2.Zero;
            LocalPosition = Vector2.Zero;
            DeltaX  = 0.0f;
            DeltaY = 0.0f;
            OffsetX = 0.0f;
            OffsetY = 0.0f;
        }
    }

    public struct MouseButtonState
    {
        /// <summary>
        /// Pressed
        /// </summary>
        public bool[] Pressed;

        /// <summary>
        /// Init
        /// </summary>
        public void Init()
        {
            Pressed = new bool[(int)MouseButton.Last];
            for (int i = 0; i < Pressed.Length; i++)
            {
                Pressed[i] = false;
            }
        }

        /// <summary>
        /// GetState
        /// </summary>
        /// <param name="button">Button type</param>
        /// <returns></returns>
        public bool GetState(MouseButton button)
        {
            return Pressed[(int)button];
        }

        /// <summary>
        /// Press
        /// </summary>
        /// <param name="button">button type</param>
        /// <param name="pressed">state</param>
        public void Press(MouseButton button, bool pressed)
        {
            Pressed[(int)button] = pressed;
        }


        /// <summary>
        /// IsAnyPress
        /// </summary>
        /// <returns></returns>
        public bool IsAnyPress
        {
            get
            {
                foreach(bool value in Pressed)
                {
                    if (value)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// this[]
        /// </summary>
        /// <returns></returns>
        public bool this[MouseButton button] { get => Pressed[(int)button]; }
    }
}