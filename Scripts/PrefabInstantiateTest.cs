using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiateTest : MonoBehaviour {

    public GameObject prefab;
    public GameObject parent;

    private void Awake() {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity).transform.parent = parent.transform;


    }
}
