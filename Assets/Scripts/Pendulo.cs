using UnityEngine;

public class Pendulo : MonoBehaviour
{
    private HingeJoint joint;
    private JointMotor myMotor;

    [SerializeField] private int angle = 30;
    [SerializeField] private float angularSpeed = 300;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joint = GetComponent<HingeJoint>();
        myMotor = joint.motor;
        myMotor.targetVelocity = angularSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        float pendulumAnlge = joint.angle;
        if (pendulumAnlge >= angle)
        {
            myMotor.targetVelocity = -angularSpeed;
        }else if (pendulumAnlge <= -angle)
        {
            myMotor.targetVelocity = angularSpeed;
        }

        joint.motor = myMotor;
    }
}
