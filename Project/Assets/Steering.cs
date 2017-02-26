using UnityEngine;
using System;
using System.Collections;
using EasyWiFi.Core;
using EasyWiFi.ServerBackchannels;

[AddComponentMenu("EasyWiFiController/Miscellaneous/Steering")]
public class Steering : MonoBehaviour {

    public FloatServerBackchannel floatBackchannel;

    Rigidbody myRigidbody;
    //maximum speed is velocity magnitude
    public float movementSpeed = 5f;
    //Need an acceleration variable  - how to do this? ramp functions?
    public float acceleration = 5f;
    //Handling parameter - between 0 and 10
    public int handling = 5;
    //Turret rotation speed
    public float RotationSpeed;
    //Current position 
    private Vector3 currentposition;

    //projectile info
    public Rigidbody rocket;
    public float rocket_speed = 100f;
    public float rocket_fire_rate = 1.0f;
    Boolean rocket_fired;
    private float hozW;
    private float verW;

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
        //call projectile fire script
        //InvokeRepeating("LaunchRocket", 2.0f, rocket_fire_rate);
    }

    void Update()
    {
        floatBackchannel.setValue(myRigidbody.velocity.magnitude);
    }


    //WANT TWIN STICK CONTROLLER
    // left joystick for moving
    //right joystick for moving turret and shooting
    public void SteeringScript(JoystickControllerType leftjoystick)
    {
        //Get input hoz and ver
        float hoz = leftjoystick.JOYSTICK_HORIZONTAL;
        float ver = leftjoystick.JOYSTICK_VERTICAL;
        //Get current velocity magniture
        float current_vel = myRigidbody.velocity.magnitude;

        //Lets translate based on the position of the joy stick
        //Sensitivity to how much joystick translation there is can be our max speed setting
        Vector3 moveDirection = new Vector3(hoz, 0, ver);

        transform.position += moveDirection * movementSpeed / 100f;

    }

    public void ShootateScript(JoystickControllerType rightjoystick)
    {
        // Want to use this to rotate based on the right joystick
        //Get input hoz and ver
        float hoz = rightjoystick.JOYSTICK_HORIZONTAL;
        float ver = rightjoystick.JOYSTICK_VERTICAL;
        hozW = hoz;
        verW = ver;

        // to stop reverting to zero, only want to calculate when there is input
        if(rightjoystick.JOYSTICK_HORIZONTAL != 0)
        {
            //get current rotation
            Vector3 currentrotation = transform.rotation.eulerAngles;
            //angle magic (comment TODO)
            float angle = Mathf.Atan2(hoz, ver) * Mathf.Rad2Deg;
            //interpolate between current and new rotation to get gradual rotation
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(currentrotation), Quaternion.Euler(0, angle + 90, 0), RotationSpeed/100f);

            //shooting in void start

        }

    }

 //   public void LaunchRocket()
 //   {
 //           //need to get transform.rotaiton
 //           Vector3 currentrotation = transform.rotation.eulerAngles;
 //           float angle = Mathf.Atan2(hozW, verW) * Mathf.Rad2Deg;

   //         Quaternion shootangle = Quaternion.Lerp(Quaternion.Euler(currentrotation), Quaternion.Euler(0, angle - 180, 0), RotationSpeed / 100f);
   ///         Vector3 rocketstartposition = transform.InverseTransformDirection(transform.position) + new Vector3(4, 0, 0);
   //         //Need to place object at barrel end to start instantiate from
   //         Rigidbody rocketClone = (Rigidbody)Instantiate(rocket, rocketstartposition, Quaternion.Euler(0,0,0));
   //     //need velocity vector components from shoot angle
   //     Debug.Log(shootangle.y + "sh");
   //         rocketClone.velocity = -transform.InverseTransformDirection(Vector3.forward) * rocket_speed;
   //         // You can also acccess other components / scripts of the clone
   //         // rocketClone.GetComponent<FireRocket>().DoSomething();
   // }

}
