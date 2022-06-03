using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;


public class Agent : MonoBehaviour
{
    private NavMeshAgent agent; // Guarda la referencia de la componente NavMeshAgent
    private TrailRenderer trail; // Guarda la referencia de la componente TrailRenderer (rastro)
    
    // Variables necesarias para la configuración de la ruta a seguir (NO ES NECESARIO MODIFICAR NADA)
    private Transform[] points;
    private float speed = 10f;
    private int totalPoints;
    private int nextPoint;
    private float threshold = 0.5f;
    
    // Variable que indica si estamos en estado de dibujar letra (true) o no (false)
    private bool drawingLetter;

    // Variables necesarias para el cambio de letra (NO ES NECESARIO MODIFICAR NADA)
    private string name;
    private int nameLength;
    private int letterIndex;
    private string currentLetter;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        // Hacemos que al principio el rastro no se muestre, pues nunca empezamos en el mismo sitio
        trail.emitting = false;
        
        name = GameManager.sharedInstance.name;
        nameLength = name.Length;

        StartCoroutine(FirstLetter());
    }

    private void Update()
    {
        if (drawingLetter)
        {
            DrawLetter();
        }else if (letterIndex > 0 && nextPoint == 0)
        {
            // Aquí estamos pasando a una nueva letra
            if (Vector3.Distance(transform.position, points[nextPoint].position) < threshold)
            {
                // Una vez llegamos al primer punto de la nueva letra, 
                // * indicamos que volvemos a estar en el estado de dibujar letra
                // * volvemos a activar el rastro
                drawingLetter = true;
                trail.emitting = false;
            }
        }
    }

    private void DrawLetter()
    {
        // Si hemos llegado al siguiente punto
        if (Vector3.Distance(transform.position, points[nextPoint].position) < threshold)
        {
            // Cambiamos el objetivo al nuevo siguiente punto
            nextPoint++;

            
            if (nextPoint == totalPoints)
            {
                // Si hemos llegado al último punto, pasamos a la siguiente letra
                // * Dejamos de dibujar letra pues pasamos al estado de transición a una nueva letra
                // * Dejamos de emitir el rastro
                drawingLetter = false;
                trail.emitting = false;
                
                if (letterIndex < nameLength - 1)
                {
                    // Si todavía no hemos acabado la palabra, pasamos a una nueva letra
                    NextLetter();
                }
                else
                {
                    // Hemos acabado la palabra
                    // ALGO SE TENDRÁ QUE AÑADIR AQUÍ
                    GameManager.sharedInstance.End();
                }
            }
            else
            {
                // Si todavía no hemos acabado la letra, pasamos al siguiente punto de la misma
                agent.SetDestination(points[nextPoint].position);
            }
        }
    }

    private IEnumerator FirstLetter()
    {
        // (NO ES NECESARIO MODIFICAR NADA)
        // El caso de la primera letra de la palabra es un tanto especial
        // * Indicamos que vamos por la primera letra de la palabra y guardamos cuál es
        letterIndex = 0;
        currentLetter = name[letterIndex].ToString();

        // * Indicamos cuál es la ruta correspondiente y el total de puntos que tiene
        points = Routes.sharedInstance.routeDict[currentLetter];
        totalPoints = points.Length;
        
        // * Le indicamos al agente que su primer objetivo es el primer punto de la ruta
        // * De hecho, situamos al agente en esa posición
        nextPoint = 0;
        transform.position = points[nextPoint].position;
        
        // * Esperamos 2 segundos
        yield return new WaitForSeconds(2);
        
        // * Activamos el estado de dibujar letra y el rastro 
        drawingLetter = true;
        trail.emitting = true;

    }

    private void NextLetter()
    {
        // (NO ES NECESARIO MODIFICAR NADA)
        // Esta función se dedica a cambiar de letra
        // * Indicamos que ahora la letra es la siguiente y guardamos la letra en cuestión
        letterIndex++;
        currentLetter = name[letterIndex].ToString();

        // * Indicamos cuál es la ruta correspondiente y el total de puntos que tiene
        points = Routes.sharedInstance.routeDict[currentLetter];
        totalPoints = points.Length;
        
        // * Le indicamos al agente que su siguiente objetivo es el primer punto de la ruta de la nueva letra
        // * Le configuramos ese nuevo objetivo mediante la componente NavMeshAgent
        nextPoint = 0;
        agent.SetDestination(points[nextPoint].position);
    }

}
