using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string PoolName { get { return GetType().Name; } }

    public virtual void Push()
    {
        ObjectPooler.Instance.Push(this);
    }
}
