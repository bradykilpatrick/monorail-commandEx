using UnityEngine;
using System.Collections;
using System;

public abstract class TrainMove
{

    public abstract void move(Transform t, Rigidbody rb);
    public Collider[] randomAssign(Collider[] arrColliders)
    {
        arrColliders[0] = null;
        arrColliders[1] = null;
        arrColliders[2] = null;

        for (int i = 0; i < 3;)
        {
            int j = UnityEngine.Random.Range(0, 3);
            if (arrColliders[j] == null)
            {
                if (i == 0)
                {
                    arrColliders[j] = GameObject.FindGameObjectWithTag("Fore").GetComponent<Collider>();
                    i++;
                }
                else if (i == 1)
                {
                    arrColliders[j] = GameObject.FindGameObjectWithTag("Back").GetComponent<Collider>();
                    i++;
                }
                else if(i == 2)
                {
                    arrColliders[j] = GameObject.FindGameObjectWithTag("Stop").GetComponent<Collider>();
                    i++;
                }
            }
        }
        return arrColliders;
    }
}

public class MoveForward : TrainMove
{
    
    public override void move(Transform t, Rigidbody rb)
    {
        //rb = GetComponent<Rigidbody>();
        rb.AddForce(t.forward * 100);
    }
}
public class MoveBackward : TrainMove
{
    
    public override void move(Transform t, Rigidbody rb)
    {
        //rb = GetComponent<Rigidbody>();
        rb.AddForce(t.forward * -100);
    }
}
public class StopTrain : TrainMove
{

    public override void move(Transform t, Rigidbody rb)
    {
        //rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }
}
public class Random : TrainMove
{
    public override void move (Transform t, Rigidbody rb) { }
}

public class Train : MonoBehaviour
{
    public TrainMove forwardMove = new MoveForward();
    public TrainMove backwardMove = new MoveBackward();
    public TrainMove stopTrain = new StopTrain();
    public Rigidbody rb;
    public TrainMove RandomMove = new Random();
    GameObject rail;

    Collider[] arrColliders = new Collider[3];

    public Collider getMouseClick(string tag)
    {
        Collider temp = new Collider();

        temp = GameObject.FindGameObjectWithTag(tag).GetComponent<Collider>();
        
        return temp;
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rail = GameObject.FindGameObjectWithTag("Rail");
        arrColliders[0] = getMouseClick("Fore");
        arrColliders[1] = getMouseClick("Back");
        arrColliders[2] = getMouseClick("Stop");

    }

    void Update ()
    {
  
        transform.rotation = Quaternion.Euler(0, 90, 0);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            
            
            Collider RandomCollider = getMouseClick("Random");
            Collider PlusCollider = getMouseClick("Plus");
            Collider MinusCollider = getMouseClick("Minus");

            if (arrColliders[0].Raycast(ray, out hit, 100.0F))
            {
                
                forwardMove.move(transform, rb);
            }
            else if (arrColliders[1].Raycast(ray, out hit, 100.0F))
            {
                backwardMove.move(transform, rb);
            }
            else if (arrColliders[2].Raycast(ray, out hit, 100.0F))
            {
                stopTrain.move(transform, rb);
            }
            else if(RandomCollider.Raycast(ray, out hit, 100.0F))
            {
                arrColliders = RandomMove.randomAssign(arrColliders);
            }
            else if(PlusCollider.Raycast(ray, out hit, 100.0f))
            {
                rail.transform.localScale += new Vector3(.3f, 0, 0);
            }
            else if(MinusCollider.Raycast(ray, out hit, 100.0f))
            {
                rail.transform.localScale -= new Vector3(.3f, 0, 0);
            }

        }
        
    }
}