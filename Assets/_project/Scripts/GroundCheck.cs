using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private LayerMask _layerMask;
    // Start is called before the first frame update

    public bool IsGround()
    {
        if (Physics.Raycast(_origin.position, Vector3.down, out RaycastHit hit, 0.5f, _layerMask))
        {
            Debug.Log(gameObject.name + " ha toccato terra");
            return true;
        }
        else
            return false;
    }


}
