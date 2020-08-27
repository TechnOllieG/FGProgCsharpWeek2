using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region InputIDs
    public string shoot = "Jump";
    #endregion InputIDs
    
    private Gun _gun;

    private void Awake()
    {
        _gun = GetComponent<Gun>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(shoot))
        {
            _gun.Shoot();
        }
    }
}
