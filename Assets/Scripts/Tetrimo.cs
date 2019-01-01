﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimo : MonoBehaviour
{

    public float fallspeed = 1;
    public float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkUserInput();
    }


    public Vector2 round(Vector3 input) {
        return new Vector2(Mathf.Round(input.x),Mathf.Round(input.y));

    }


    bool isValid(Transform tetrimo) {

        foreach(Transform minimo in tetrimo){

            Vector2 pos = round(minimo.position);

            if (!FindObjectOfType<Game>().isInGrid(pos)) {
                return false;
            }

            if (FindObjectOfType<Game>().getTransform(pos) != null && FindObjectOfType<Game>().getTransform(pos).parent != tetrimo)
            {
                return false;
            }


        }

        return true;
    }




    void checkUserInput() {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            if (isValid(transform))
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += Vector3.right;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            if (isValid(transform))
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += Vector3.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);
            transform.position = round(transform.position);
            if (isValid(transform))
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.Rotate(0, 0, -90);
                transform.position = round(transform.position);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - time >= fallspeed) {
            transform.position += Vector3.down;
            if (isValid(transform))
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else {
                transform.position += Vector3.up;
                enabled = false;
                FindObjectOfType<Game>().clearRows();
                
                FindObjectOfType<Game>().spawnNextTetrimo();
            }

            time = Time.time;
        }
    }

    
}
