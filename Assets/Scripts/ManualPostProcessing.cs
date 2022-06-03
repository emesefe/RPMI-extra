using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ManualPostProcessing : MonoBehaviour
{
    // DE ESTE SCRIPT NO ES NECESARIO MODIFICAR NADA
    // PERO SÍ HACE FALTA ENTENDERLO PARA COMPLETAR UNO DE LOS PUNTOS DEL ENUNCIADO
    
    // Variable que guarda la referencia al script Volume, que contiene los efectos de post procesado
    private Volume volume;
    
    // Variable que guarda la cantidad de color que variamos
    private int colorVariation = 1;
    // Variable que guarda el tiempo entre variación y variación
    private float timeColorChange = 0.05f;

    private void Awake()
    {
        // Establecemos la referencia
        volume = GetComponent<Volume>();
    }

    private void Start()
    {
        // Empezamos la corrutina
        StartCoroutine(HueShiftCorroutine());
    }

    private IEnumerator HueShiftCorroutine()
    {
        // Accedemos al perfil de post procesado que hemos creado y miramos si existe el efecto ColorAdjustments
        volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments);
        
        // Mientras estemos dibujando la palabra
        while (!GameManager.sharedInstance.hasEndedDrawing)
        {
            // Esperamos un cierto número de segundos
            yield return new WaitForSeconds(timeColorChange);
            
            // * Si el valor de la variable hueShift llega a su mínimo, la variación de color es positiva (vamos de -180 a 180)
            // * Si el valor de la variable hueShift llega a su máximo, la variación de color es negativa (vamos de 180 a -180)
            // * Al final, con este cambio de signo, lo que hacemos es incrementar (sumar) hasta llegar al máximo y luego,
            // decrementar (restar) hasta llegar al mínimo y así conseguimos mantenernos en el intervalo [-180, 180]
            if (colorAdjustments.hueShift.value <= colorAdjustments.hueShift.min || 
                colorAdjustments.hueShift.value >= colorAdjustments.hueShift.max)
            {
                colorVariation *= -1;
            }
            
            // Aplicamos el incremento o decremento de variación de color, según corresponda
            colorAdjustments.hueShift.value += colorVariation;
        }
    }
}
