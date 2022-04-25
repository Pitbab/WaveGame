using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFullLife : PowerUp
{
    private float timeToShift = 1f;
    private float emission;
    private Material mat;
    private Light light;

    

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        light = GetComponentInChildren<Light>();

        startingPos = transform.position;
        maxTranslation = 0.5f;
        rotationSpeed = 80f;
    }

    private void Update()
    {
        transform.position = startingPos + Vector3.up * maxTranslation * Mathf.Cos(Time.time);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        emission = Mathf.PingPong(Time.time, timeToShift);
        Color baseColor = Color.green;

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        mat.SetColor("_EmissionColor", finalColor);
        light.intensity = emission;


    }
    
    
    public override void TakePowerUp(PlayerLogic actor)
    {
        base.TakePowerUp(actor);
        actor.ChangeHp(actor.maxHealth);
    }
}
