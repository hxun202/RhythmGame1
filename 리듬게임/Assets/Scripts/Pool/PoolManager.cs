using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    private List<PoolItem> poolItems = new();

    private Dictionary<NoteType, Queue<GameObject>> pools = new();

    private Dictionary<NoteType, GameObject> prefabs = new();

    private Transform NoteParent;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (PoolItem item in poolItems)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            prefabs[item.noteType] = item.prefab;

            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = Instantiate(item.prefab, NoteParent);

                obj.SetActive(false);

                queue.Enqueue(obj);
            }

            pools[item.noteType] = queue;
        }
    }

    public GameObject Get(NoteType type)
    {
        Queue<GameObject> queue = pools[type];

        if (queue.Count == 0)
        {
            GameObject obj = Instantiate(prefabs[type]);

            obj.SetActive(false);

            queue.Enqueue(obj);
        }

        GameObject note = queue.Dequeue();

        note.SetActive(true);

        return note;
    }

    public void Return(NoteType type, GameObject obj)
    {
        obj.SetActive(false);

        pools[type].Enqueue(obj);
    }
}