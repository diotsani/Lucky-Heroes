using UnityEngine;

namespace Interfaces
{
    public interface IObjectPool
    {
        Component Get(Component prefab);
        void Release(Component obj);
        void Prewarm(int count);
    }
}