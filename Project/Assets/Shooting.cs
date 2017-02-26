using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    //need to get public variable from another script later? TODO
    public float rocket_fire_rate = 1.0f;
    public Rigidbody rocket;
    public float rocket_speed = 100f;

    private float lastShot = 0.0f;

    public float rocket_life_time = 4.0f;

    // Use this for initialization
    void Start () {
    //    InvokeRepeating("LaunchRocket", 2.0f, rocket_fire_rate);

    }

    // Update is called once per frame
    void Update () {
        LaunchRocket();
        if (gameObject.name == ("RocketColne"))
        {
           // Destroy(gameObject, 4f);
        }
    }

    public void LaunchRocket()
    {
        if (Time.time > rocket_fire_rate + lastShot)
        {
        //Need to place object at barrel end to start instantiate from
        Rigidbody rocketClone = (Rigidbody)Instantiate(rocket, transform.position, Quaternion.identity);
        //GameObject rocketClone = (GameObject)Instantiate(rocket, transform.position, Quaternion.identity);

            //need velocity vector components from shoot angle
            rocketClone.velocity = transform.InverseTransformDirection(Vector3.right) * rocket_speed;
            // rocketClone.GetComponent<Rigidbody>().velocity = transform.InverseTransformDirection(Vector3.right) * rocket_speed;

            //rocketClone.velocity = -transform.InverseTransformDirection(Vector3.forward) * rocket_speed;
            //        StartCoroutine(Wait());
            Destroy(rocketClone.gameObject, rocket_life_time);        
            //Destroy(rocketClone.GetComponent<Rigidbody>(), 4.0f);

            lastShot = Time.time;

        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(5);
    }

}
