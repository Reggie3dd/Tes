using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
	public float speed = 5;
	public GameObject player;
	public Transform target;

	RaycastHit hit;
	Ray MyRay;
	public Vector3 offset;

	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public KeyCode up = KeyCode.W;
	public KeyCode down = KeyCode.S;
	public KeyCode rotCamA = KeyCode.Q;
	public KeyCode rotCamB = KeyCode.E;

	public Transform startPoint;
	public int rotationX = 70;
	public int rotationLimit = 360;

	public float maxHeight = 15.0f;
	public float minHeight = 5.0f;
	public float rotationSpeed = 0.05f;

	private float camRotation;
	private float height;
	private float tmpHeight;
	private float h, v;
	

	[SerializeField]private bool selectedPlayer = false;

	private bool L, R, U, D;

	void Start()
	{
		height = (maxHeight + minHeight) / rotationSpeed;
		tmpHeight = height;
		camRotation = rotationLimit / rotationSpeed;
		transform.position = new Vector3(startPoint.position.x, height, startPoint.position.z);
	}

	public void CursorTriggerEnter(string triggerName)
	{
		switch (triggerName)
		{
			case "L":
				L = true;
				break;
			case "R":
				R = true;
				break;
			case "U":
				U = true;
				break;
			case "D":
				D = true;
				break;
		}
	}

	public void CursorTriggerExit(string triggerName)
	{
		switch (triggerName)
		{
			case "L":
				L = false;
				break;
			case "R":
				R = false;
				break;
			case "U":
				U = false;
				break;
			case "D":
				D = false;
				break;
		}
	}
    private void LateUpdate()
    {
		FollowPlayer();


		if (Input.GetKey(left) || L)
		{
			h = -1;
			selectedPlayer = false;
		}
		else if (Input.GetKey(right) || R)
		{  
			h = 1;
			selectedPlayer = false;
		}
		else h = 0;
		if (Input.GetKey(down) || D)
		{
			v = -1;
			selectedPlayer = false;
		}
		else if (Input.GetKey(up) || U)
		{
			v = 1;
			selectedPlayer = false;
		}
		else v = 0;

		if (Input.GetKey(rotCamB)) camRotation -= rotationSpeed; else if (Input.GetKey(rotCamA)) camRotation += rotationSpeed;
		camRotation = Mathf.Clamp(camRotation, 0, rotationLimit);

		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (height < maxHeight) tmpHeight += 1;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (height > minHeight) tmpHeight -= 1;
		}

		tmpHeight = Mathf.Clamp(tmpHeight, minHeight, maxHeight);
		height = Mathf.Lerp(height, tmpHeight, 3 * Time.deltaTime);

		Vector3 direction = new Vector3(h, v, 0);
		transform.Translate(direction * speed * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
		transform.rotation = Quaternion.Euler(rotationX, camRotation, 0);
		if (selectedPlayer)
		{
			transform.LookAt(target);

			offset = (transform.position - player.transform.position).normalized* 10 + new Vector3(0, height,0);
			transform.position = player.transform.position + offset;

		}

	}

	void Update()
	{


		
	}
	public void FollowPlayer()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			MyRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector2 oldPosition = new Vector2(transform.localPosition.x, transform.localPosition.z);
			Debug.Log("RayCast On");
			if (Physics.Raycast(MyRay, out hit))
			{
                if (hit.collider.gameObject.CompareTag("Player"))
                {
					selectedPlayer = true;
					player = hit.collider.gameObject;
					target = hit.collider.transform;
				}
					
				//	offset = transform.position - player.transform.position;
			}
			
		}

	}
}
