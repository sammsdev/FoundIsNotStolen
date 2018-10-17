using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    private float projectileSpeed = 30f;
    private Vector3 dir;
    [SerializeField] private Transform WeaponNib;
	// Use this for initialization
    
	void Start () {
        WeaponNib = GameObject.FindGameObjectWithTag("Weapon_Nib").GetComponent<Transform>();

    }
	
	// Update is called once per frame
	void MoveProjectile (Vector3 dir) {

        this.transform.LookAt(dir);
        GetComponent<Rigidbody>().velocity = this.transform.forward * projectileSpeed;
        Invoke("ResetProjectile", 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectableBehaviour isCollectable = other.GetComponent<CollectableBehaviour>();
        if (isCollectable)
        {
            isCollectable.Hit(WeaponNib);
            ResetProjectile();
        } else
        {
            ResetProjectile();
        }
        
    }

    public void SetDir (Vector3 newDir)
    {
        this.dir = newDir;
        MoveProjectile(dir);
    }

    private void ResetProjectile()
    {
        this.transform.parent = WeaponNib;
        this.transform.position = this.transform.parent.position;
        this.gameObject.SetActive(false);
    }
    
}
