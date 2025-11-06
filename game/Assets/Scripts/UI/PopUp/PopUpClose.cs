using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpClose : MonoBehaviour
{
    public void Close()
    {
        Destroy(transform.parent.gameObject);
    }
}
