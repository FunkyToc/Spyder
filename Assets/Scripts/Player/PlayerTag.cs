using UnityEngine;

public class PlayerTag : MonoBehaviour, TagInterface 
{
    string TagInterface.Help()
    {
        return "Player Tag is on Player main object";
    }
}