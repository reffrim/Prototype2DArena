using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethAnimate : MonoBehaviour
{
    public GameObject PowerUpManagerReference;
    Animator AnimatorReference;


    // Start is called before the first frame update
    void Start()
    {
        AnimatorReference = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isPowered = PowerUpManagerReference.GetComponent<PowerUpManager>().IsPowered;
        AnimatorReference.SetBool("IsPoweredUp", isPowered);
    }
}
