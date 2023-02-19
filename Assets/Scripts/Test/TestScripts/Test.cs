using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private int private_int;

    [SerializeField]
    protected int protected_int;

    [SerializeField]
    private int m_int;

    [ReadOnly]
    public int ReadOnlyPublicInt;

    [SerializeField, ReadOnly]
    private int ReadOnlyPrivateInt;
}
