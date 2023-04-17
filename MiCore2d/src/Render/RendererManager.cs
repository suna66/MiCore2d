using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// RendererManager.
    /// Now, This class is not used.
    /// </summary>
    public class RendererManager
    {
    //     private static RendererManager? _instance = null;
    //     private List<Renderer> _rendererList;

    //     /// <summary>
    //     /// GetInstance.
    //     /// </summary>
    //     /// <returns>instance</returns>
    //     public static RendererManager GetInstance()
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = new RendererManager();
    //             _instance.AddRenderer<TextureRenderer>();
    //             _instance.AddRenderer<SepiaTextureRenderer>();
    //             _instance.AddRenderer<TextureArrayRenderer>();
    //             _instance.AddRenderer<PolygonRenderer>();
    //         }
    //         return _instance;
    //     }

    //     /// <summary>
    //     /// Constructor.
    //     /// </summary>
    //     private RendererManager()
    //     {
    //         _rendererList = new List<Renderer>();
    //     }

    //     /// <summary>
    //     /// AddRenderer.
    //     /// </summary>
    //     /// <typeparam name="T">renderer type</typeparam>
    //     /// <returns>renderer instance</returns>
    //     public T AddRenderer<T>() where T : new()
    //     {
    //         T obj = new T();
    //         if (obj is Renderer)
    //         {
    //             Renderer renderer = (Renderer)(object)obj;
    //             _rendererList.Add(renderer);
    //             return obj;
    //         }
    //         throw new InvalidCastException("cannot convert to Renderer Object");
    //     }

    //     /// <summary>
    //     /// GetRenderer.
    //     /// </summary>
    //     /// <typeparam name="T">renderer type</typeparam>
    //     /// <returns>renderer instance</returns>
    //     public T GetRenderer<T>()
    //     {
    //         foreach(Renderer c in _rendererList)
    //         {
    //             if (c is T)
    //             {
    //                 return (T)(object)c;
    //             }
    //         }
    //         return (T)(object)null!;
    //     }

    //     /// <summary>
    //     /// GetCount.
    //     /// </summary>
    //     /// <returns>number of renderers</returns>
    //     public int GetCount()
    //     {
    //         return _rendererList.Count;
    //     }

    //     /// <summary>
    //     /// Clear.
    //     /// </summary>
    //     /// <param name="renew">renew instance or not</param>
    //     public void Clear(bool renew)
    //     {
    //         foreach (Renderer renderer in _rendererList)
    //         {
    //             renderer.Dispose();
    //         }
    //         if (renew)
    //             _rendererList = new List<Renderer>();
    //         else
    //             _rendererList = null!;
    //     }

    //     /// <summary>
    //     /// Dispose.
    //     /// </summary>
    //     public void Dispose()
    //     {
    //         Clear(false);
    //         GC.SuppressFinalize(this);
    //     }
     }
}