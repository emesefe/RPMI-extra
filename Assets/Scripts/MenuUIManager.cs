using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    // Variable que guarda la referencia al Input Field
    [SerializeField] private TMP_InputField nameInputField;

    private bool IsValid(string s)
    {
        // Comprobamos si se trata de una única palabra formada por letras (NO ES NECESARIO MODIFICAR NADA)
        if (!System.Text.RegularExpressions.Regex.IsMatch(s, "[^A-Za-z]"))
        {
            // Lista que va a guardar los caracteres únicos
            List<char> unique = new List<char>();
            foreach (var c in s)
            {
                // Si el caracter no se repite, lo agregamos a la lista
                if (!unique.Contains(c))
                {
                    unique.Add(c);
                }
                // Si algún caracter se repite, la palabra deja de ser válida
                else
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }
    private void SaveWord(string s)
    {
        // ALGO SE TENDRÁ QUE AÑADIR AQUÍ
    }

    public void GoToGameScene()
    {
        // Guardamos la palabra introducida por el usuario
        string word = nameInputField.text;
        // Si la palabra es válida
        if (IsValid(word))
        {
            // * Guardamos la palabra con persistencia de datos
            // * Pasamos a la siguiente escena
            SaveWord(word);
            SceneManager.LoadScene("Menu");
        }
        // Si la palabra no es válida, te dejo improvisar algo =P
        else
        {
            Debug.Log("¡No conquistas nada, con una ensalada!");
        }
        
    }
}
