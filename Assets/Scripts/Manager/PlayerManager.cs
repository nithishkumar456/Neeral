using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Sinleton 
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion


    public GameObject player;
}
