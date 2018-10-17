using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AIBehavior : MonoBehaviour {

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _alertDist;


    public class Room
    {
        public Transform transfLocation;       
        public List<Transform> interactablePoints;
        public string name;
    }
    public List<Room> path;   


    enum AIStates {

        movingNormal,
        movingAlert,
        interacting,

    }

    private void Start()
    {


        
        
    }


}
