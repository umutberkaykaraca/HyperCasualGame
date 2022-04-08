using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static int money = 0;
    private void Start()
    {
        money = 0;
    }
    private void Update()
    {
        GetComponent<UnityEngine.UI.Text>().text = money.ToString();
    }
}
