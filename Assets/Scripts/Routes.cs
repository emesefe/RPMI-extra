using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Routes : MonoBehaviour
{
    public static Routes sharedInstance; // Variable que guarda la referencia a esta clase, este script (único)
    
    // Arrays que guardan las rutas de cada letra
    public Transform[] routeA;
    public Transform[] routeB;
    public Transform[] routeC;
    public Transform[] routeD;
    public Transform[] routeE;
    public Transform[] routeF;
    public Transform[] routeG;
    public Transform[] routeH;
    public Transform[] routeI;
    public Transform[] routeJ;
    public Transform[] routeK;
    public Transform[] routeL;
    public Transform[] routeM;
    public Transform[] routeN;
    public Transform[] routeO;
    public Transform[] routeP;
    public Transform[] routeQ;
    public Transform[] routeR;
    public Transform[] routeS;
    public Transform[] routeT;
    public Transform[] routeU;
    public Transform[] routeV;
    public Transform[] routeW;
    public Transform[] routeX;
    public Transform[] routeY;
    public Transform[] routeZ;

    // Diccionario que une cada letra con su ruta
    public Dictionary<string, Transform[]> routeDict;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Configuramos el diccionario que une cada letra con su ruta
        // Lo hacemos en el Start porque las rutas están instanciadas en la escena
        routeDict = new Dictionary<string, Transform[]>
        {
            {"A", routeA},
            {"B", routeB},
            {"C", routeC},
            {"D", routeD},
            {"E", routeE},
            {"F", routeF},
            {"G", routeG},
            {"H", routeH},
            {"I", routeI},
            {"J", routeJ},
            {"K", routeK},
            {"L", routeL},
            {"M", routeM},
            {"N", routeN},
            {"O", routeO},
            {"P", routeP},
            {"Q", routeQ},
            {"R", routeR},
            {"S", routeS},
            {"T", routeT},
            {"U", routeU},
            {"V", routeV},
            {"W", routeW},
            {"X", routeX},
            {"Y", routeY},
            {"Z", routeZ}
        };
    }
    
    // DE ESTE SCRIPT NO ES NECESARIO MODIFICAR NADA =)
}
