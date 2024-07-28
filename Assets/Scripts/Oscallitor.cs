using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscallitor : MonoBehaviour
{
    private Vector3 _startingPosition;
    [SerializeField] private Vector3 movementPosition;
    [SerializeField] [Range(0, 1)] private float movementFactor; //Factor = çarpan

    [SerializeField] private float period = 2f;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        ObstacleCycle();
        
        Vector3 movingPosition = movementPosition * movementFactor;
        transform.position = _startingPosition + movingPosition;
    }

    private void ObstacleCycle()
    {
        if (period <= Mathf.Epsilon)
        {
            //anyValue + Epsilon = anyValue
            // anyValue - Epsilon = anyValue
            // 0 + Epsilon = Epsilon
            // 0 - Epsilon = -Epsilon
            return;
        }
        
        float cycles = Time.time / period; // örneğin zamanım 1 periodum 2 saniye bu bize 1/2 yi verir (yarım tur) - zamana bağlı çalışır
        const float tau = Mathf.PI * 2; // 2pi radyan'ı bize veriyor. radyan cinsinden - mathf.sin() kullanabilmek için lazım
        //const value = 6.283
        
        float rawSinWave = Mathf.Sin(cycles * tau); // tau = 2pi (360 derece) (1 tam tur) temsil eder * cycles (anlık döngüyü verir) 
        // mathf.sin radyan cinsinden değer alır
        // cyles = 1/2 * tau 2pi = yarım tur = pi = 180 derece
        
        movementFactor = (rawSinWave + 1f) / 2f; // 0-1 arasında istediğimiz için ve sin -1 1 arasında değer aldığı için böyle bir eşitlemeye ihtiyaç duyduk.
        Debug.Log(rawSinWave);

    }
}
