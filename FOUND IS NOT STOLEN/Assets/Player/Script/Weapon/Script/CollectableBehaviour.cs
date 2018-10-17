using UnityEngine;

public class CollectableBehaviour : MonoBehaviour {

    // Use this for initialization
    private bool isHit;
    private Transform target = null;
    private float sTime;
    private float maxDist = 2f;
    private Vector3 startPosition;

    // Update is called once per frame

    private void Start()
    {
        startPosition = this.transform.position;
    }
    void LateUpdate () {

        if (isHit)
        {
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Renderer>().material.SetFloat("_EdgeWidth", 0.2f);
            UpdatePosition();
        }          
            

        if(target != null)
            DestroyObject();
	}

    public void Hit(Transform transf)
    {
        isHit = true;
        this.target = transf;
        this.sTime = Time.time ;
    }

    private void DestroyObject()
    {
        if (Vector3.Distance(target.position, this.transform.position) < maxDist)
            Destroy(this.gameObject);
    }


    private void UpdatePosition()
    {
        float speed = 0.1f;

        float journeyLength = Vector3.Distance(transform.position, target.position);       

        float distCovered = (Time.time - sTime) * speed;
        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        this.transform.position = Vector3.Lerp(this.transform.position, target.position, fracJourney);

        float currentDist = Vector3.Distance(this.transform.position, target.position);
        float startDist = Vector3.Distance(startPosition, target.position);
        float myThreshold = 1 - (currentDist / startDist);
        

        // Set our position as a fraction of the distance between the markers.  
        Vector3 dir = (target.position - this.transform.position).normalized;
        this.GetComponent<Renderer>().material.SetVector("_FlyDirection", dir);
        this.GetComponent<Renderer>().material.SetFloat("_Threshold", myThreshold);
        this.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.3f, 0.3f, 0.3f),  myThreshold);
        
    }
}
