using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance; // Variable que guarda la referencia a esta clase, este script (único)
    
    public string name; // Variable que guarda la palabra a dibujar
    
    // Variables necesarias para la configuración de la palabra (NO ES NECESARIO MODIFICAR NADA)
    public const int distanceBetweenLetters = 10;
    public bool hasEndedDrawing;

    // Variable necesaria para la configuración de la palabra (NO ES NECESARIO MODIFICAR NADA)
    [SerializeField] private GameObject[] alphabet;
    
    // Referencia al Game Object Canvas
    [SerializeField] private GameObject canvas;

    // Variables necesarias para la configuración de la palabra (NO ES NECESARIO MODIFICAR NADA)
    private Dictionary<string, int> alphabetDict;

    private Vector3 startPosition = Vector3.zero;

    private void Awake()
    {
        // Configuración de Singleton
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
        // Función necesaria para la configuración de la palabra (NO ES NECESARIO MODIFICAR NADA)
        AlphabetDictConstructor();
        
        // ALGO SE TENDRÁ QUE AÑADIR AQUÍ
        name = "murcielago";
        InstantiateName(name);

        hasEndedDrawing = false;
        canvas.SetActive(false);
    }

    private void AlphabetDictConstructor()
    {
        // Función necesaria para la configuración de la palabra (NO ES NECESARIO MODIFICAR NADA)
        
        // Creamos un nuevo diccionario
        alphabetDict = new Dictionary<string, int>();
        
        // Asociamos cada letra con su índice en un diccionario
        for (int i = 0; i < alphabet.Length; i++)
        {
            alphabetDict.Add(alphabet[i].name, i);
        }
    }

    private void InstantiateLetter(string l, Vector3 pos)
    {
        // Función necesaria para la configuración de la palabra (NO ES NECESARIO MODIFICAR NADA)
        // * Accedemos al índice de la letra
        int idx = alphabetDict[l];
        // * Accedemos a la letra desactivada en la escena Game
        GameObject letter = alphabet[idx];
        // * Indicamos la posición que debe ocupar
        letter.transform.position = pos;
        // * Activamos la letra
        letter.SetActive(true);
        
    }

    private void InstantiateName(string s)
    {
        Vector3 position = startPosition;
        foreach (char letter in s)
        {
            InstantiateLetter(letter.ToString(), position);
            position += distanceBetweenLetters * Vector3.right;
        }
    }
    
    public void GoToMenuScene()
    {
        SceneManager.LoadScene("MENU");
    }

    public void End()
    {
        hasEndedDrawing = true;
        // ALGO SE TENDRÁ QUE AÑADIR AQUÍ
    }
}
