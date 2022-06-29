using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class Settings{
        [SerializeField]
        public Material material;
        [SerializeField]
        public float zoom = 18f;
        [SerializeField]
        public int size = 640;
        [SerializeField]
        public float scale = 1f;
        [SerializeField]
        public string key;
        [SerializeField]
        public string style = "emerald";
    }
}
