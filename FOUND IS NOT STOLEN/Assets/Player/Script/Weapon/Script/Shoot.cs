
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour {

    // Use this for initialization

    private Transform _wTrans;
    private Vector3 _dirOrNull;
    public LayerMask ignoreMask;
    [SerializeField] private float shotDist = 10f;
    public List<GameObject> Projectiles = new List<GameObject>();
    public struct shotInputs
    {
        public bool firstShot;
        public bool secondShot;
    }    
    public shotInputs shotInp;
    PlayerInputHandler _inpHolder;  
    private GameObject collectable = null;
    float startTime = 0;

    void Start () {
        _wTrans = GetComponent<Transform>();        
        startTime = Time.time;
       
    }

    private void Update()
    {
        
        if (shotInp.firstShot)
            CollectorShot();

    }

    private void CollectorShot()
    {
        ShotRay();
        if (_dirOrNull != Vector3.zero)
        {
            foreach (GameObject i in Projectiles)
            {
                if (!i.activeSelf)
                {
                    AudioManager.instance.PlaySound("PiuSound");
                    i.SetActive(true);
                    i.GetComponent<ProjectileBehavior>().SetDir(_dirOrNull);
                    i.transform.SetParent(null);
                    break;
                }

            }
        }
        
     
    }
	
   
    private void ShotRay()
    {
        Camera cam = Camera.main;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        print(_dirOrNull);

        if (Physics.Raycast(ray, out hit, ignoreMask))
            _dirOrNull = hit.point;            
         else        
            _dirOrNull = Vector3.zero;        

        
    }

   
}
