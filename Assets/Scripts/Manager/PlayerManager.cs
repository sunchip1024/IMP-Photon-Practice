using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

/// <summary>
///Manage Network player if isLocalPlayer variable is false
/// or Local player if isLocalPlayer variable is true.
/// </summary>

public class PlayerManager : MonoBehaviour
{
	public PhotonView PV;
	public string id;
	public Text PlayerName;

	public bool isOnline;

	public bool isLocalPlayer;

	public float verticalSpeed = 3.0f;

	public float rotateSpeed = 150f;
	public Camera PlayerCamera;


	float h;
	float v;

    public void Awake()
    {
		isLocalPlayer = PV.IsMine;
	}

	public void SetName(string name)
    {
		PV.RPC("SetNameRPC", RpcTarget.All, name);
    }

    [PunRPC]
    public void SetNameRPC(string name)
	{
		PlayerName.text = name;
	}

	private void Start()
	{
		isLocalPlayer = PV.IsMine;
		PlayerCamera.enabled = isLocalPlayer;
	}


	// Update is called once per frame
	void FixedUpdate()
	{
		if(isLocalPlayer) Move();
	}

	void Move()
	{


		// Store the input axes.
		h = Input.GetAxisRaw("Horizontal");

		v = Input.GetAxisRaw("Vertical");

		var x = h * Time.deltaTime * verticalSpeed;
		var y = h * Time.deltaTime * rotateSpeed;
		var z = v * Time.deltaTime * verticalSpeed;

		transform.Rotate(0, y, 0);

		transform.Translate(0, 0, z);

		if (h != 0 || v != 0)
		{

		}


	}

}