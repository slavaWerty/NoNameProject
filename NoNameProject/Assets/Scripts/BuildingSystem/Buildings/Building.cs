using UnityEngine;

namespace BuildingSystem 
{
    public abstract class Building : MonoBehaviour
    {
        public int Id;
        public string TypeId;
        public int Level;
        public abstract string BuildingKey { get;}

        public Renderer MainRenderer;
        public Vector2Int Size = Vector2Int.one;

        public void Initzialize(Renderer rendered, Vector2Int size)
        {
            MainRenderer = rendered;
            Size = size;
        }

        public virtual void SetTransparent(bool available)
        {
            if (available)
                MainRenderer.material.color = Color.green;
            else
                MainRenderer.material.color = Color.red;
        }

        public virtual void SetNormal()
        {
            MainRenderer.material.color = Color.white;
        }

        private void OnDrawGizmos()
        {
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                    else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                    Gizmos.DrawCube(transform.position + new Vector3(x, y, 0), new Vector3(1, 1, .1f));
                }
            }
        }
    }
}

