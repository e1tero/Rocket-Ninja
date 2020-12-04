using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }
}
