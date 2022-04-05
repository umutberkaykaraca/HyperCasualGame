using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public LayerMask layer;
    public Vector3 currentPos;
    public Animator anim;
    public Transform backPos;
    [Range(1, 200)]
    public float rayDistance;
    [Range(1, 50)]
    public int moveSpeed, rotSpeed;
    

    private int yPos=2;
   
 

    void Update()
    {
        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Vector3 direction = (currentPos - transform.position).normalized;

        if (Physics.Raycast(ray, out hit, rayDistance, layer))
        {
            currentPos = new Vector3(hit.point.x, 0.1f, hit.point.z);
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("Running", true);
                transform.position = Vector3.MoveTowards(transform.position, currentPos, moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            }
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("Running", false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            AddBack(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bridge"))
        {
            Bridge(other.gameObject);
        }
        if (other.gameObject.CompareTag("Power"))
        {
            Power(other.gameObject);
        }
        
    }

    public void Power(GameObject powerObj) 
    {
        
        
            if(Money.money > 0){
                moveSpeed++;
                Money.money--;
            }
        
       
    }

    public void Bridge(GameObject bridgeObj) 
    {

        
        if (backPos.transform.childCount > 0)
        {
            
            Money.money++;
            MeshRenderer mesh = bridgeObj.GetComponent<MeshRenderer>();
            mesh.enabled = false;
            mesh.material.color = Color.red;
            int obstacleNumber = backPos.transform.childCount - 1;
            Destroy(backPos.GetChild(obstacleNumber).gameObject);
            yPos -= 1;
            
        }
       
    }
    
    private void AddBack(GameObject obj)
    {
        obj.transform.SetParent(backPos.transform);
        obj.transform.rotation = backPos.rotation;
        obj.transform.position = new Vector3(backPos.position.x,yPos,backPos.position.z);
        yPos += 1;
    }
}