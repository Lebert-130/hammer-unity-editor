using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tools : MonoBehaviour {
    Vector3 worldPosition;
    public Camera TopView;
    public new GameObject blockObject;
    public GameObject previewObject;
    GameObject preview_Object;
    bool creatingBlocks;
    Vector3 tempPosition;
    bool gotTemp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Ray ray = TopView.ScreenPointToRay(Input.mousePosition);

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        GameObject right = blockObject.transform.GetChild(0).gameObject;
        meshGenerator mesh_right = right.GetComponent<meshGenerator>();

        GameObject left = blockObject.transform.GetChild(1).gameObject;
        meshGenerator mesh_left = left.GetComponent<meshGenerator>();

        GameObject back = blockObject.transform.GetChild(2).gameObject;
        meshGenerator mesh_back = back.GetComponent<meshGenerator>();

        GameObject bottom = blockObject.transform.GetChild(4).gameObject;
        meshGenerator mesh_bottom = bottom.GetComponent<meshGenerator>();

        GameObject top = blockObject.transform.GetChild(5).gameObject;
        meshGenerator mesh_top = top.GetComponent<meshGenerator>();

        Vector3 spawnPosition = new Vector3(Mathf.Ceil(worldPosition.x), Mathf.Floor(worldPosition.y), Mathf.Floor(worldPosition.z));

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mesh_left.vertex1 = new Vector3(spawnPosition.x, 0, 0);
            mesh_left.vertex2 = new Vector3(spawnPosition.x, -1, 0);

            mesh_back.vertex3 = new Vector3(spawnPosition.x, 0, 0);
            mesh_back.vertex4 = new Vector3(spawnPosition.x, -1, 0);

            mesh_bottom.vertex2 = new Vector3(spawnPosition.x, -1, 0);

            mesh_top.vertex1 = new Vector3(spawnPosition.x, 0, 0);

            preview_Object = Instantiate(previewObject, spawnPosition, new Quaternion(0, 0, 0, 0));

            tempPosition = spawnPosition;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            mesh_top.vertex3.x = spawnPosition.x;
            mesh_top.vertex4.x = spawnPosition.x;

            mesh_right.vertex3.x = spawnPosition.x;
            mesh_right.vertex4.x = spawnPosition.x;

            mesh_bottom.vertex3.x = spawnPosition.x;
            mesh_bottom.vertex4.x = spawnPosition.x;

            if (spawnPosition.z == 0)
            {
                mesh_left.vertex3.z = 1;
                mesh_left.vertex4.z = 1;

                mesh_top.vertex2.z = 1;
                mesh_top.vertex4.z = 1;

                mesh_right.vertex1.z = 1;
                mesh_right.vertex2.z = 1;

                mesh_bottom.vertex1.z = 1;
                mesh_bottom.vertex3.z = 1;
            }
            else
            {
                mesh_left.vertex3.z = spawnPosition.z;
                mesh_left.vertex4.z = spawnPosition.z;

                mesh_top.vertex2.z = spawnPosition.z;
                mesh_top.vertex4.z = spawnPosition.z;

                mesh_right.vertex1.z = spawnPosition.z;
                mesh_right.vertex2.z = spawnPosition.z;

                mesh_bottom.vertex1.z = spawnPosition.z;
                mesh_bottom.vertex3.z = spawnPosition.z;
            }
            

            preview_Object.transform.localScale = new Vector3((spawnPosition.x) - (tempPosition.x), 1, (spawnPosition.z) - (tempPosition.z));
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameObject clone;

            Destroy(preview_Object);

            clone = Instantiate(blockObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            TopView.gameObject.transform.Translate(mouseX,0,0);
            TopView.gameObject.transform.Translate(0, mouseY, 0);
        }

        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        TopView.gameObject.transform.Translate(0, 0, mouseWheel*4);
	}
}
