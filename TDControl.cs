using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[AddComponentMenu("#NVJOB/Tools/TDControl")]
public class TDControl : MonoBehaviour
{
	// Token: 0x06000030 RID: 48 RVA: 0x0001EA88 File Offset: 0x0001CC88
	private void Awake()
	{
		this.tr = base.transform;
		this.rotationStart = this.tr.eulerAngles;
		this.positionStart = this.tr.position;
		this.cameraStart = this.camTransform.localPosition;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000E478 File Offset: 0x0000C678
	private void LateUpdate()
	{
		this.Rotation();
		this.CameraTransform();
		if (this.liftOn)
		{
			this.Lift();
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0001EAD4 File Offset: 0x0001CCD4
	private void Rotation()
	{
		if (Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y")) > 0f)
		{
			this.mouseX += this.rotationSpeed * 0.01f * Input.GetAxis("Mouse X") * 0.3f;
			this.mouseY -= this.rotationSpeed * 0.01f * Input.GetAxis("Mouse Y") * 0.3f;
		}
		this.mouseY = Mathf.Clamp(this.mouseY, this.mouseVerticaleClamp.x, this.mouseVerticaleClamp.y);
		this.tr.rotation = Quaternion.Slerp(this.tr.rotation, Quaternion.Euler(this.mouseY, this.mouseX + this.rotationStart.y, 0f), this.smoothMouse * Time.deltaTime);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x0001EBCC File Offset: 0x0001CDCC
	private void Lift()
	{
		this.upCh += Input.GetAxis("Vertical") * 0.2f;
		this.upCh = Mathf.Clamp(this.upCh, this.liftClamp.x, this.liftClamp.y);
		this.upChCur = Mathf.SmoothDamp(this.upChCur, this.upCh, ref this.upChVel, this.smoothLift);
		this.tr.position = this.tr.TransformDirection(new Vector3(0f, this.positionStart.y + this.upChCur, 0f));
	}

	// Token: 0x06000034 RID: 52 RVA: 0x0001EC78 File Offset: 0x0001CE78
	private void CameraTransform()
	{
		this.camCh += Input.mouseScrollDelta.y * 0.5f;
		this.camCh = Mathf.Clamp(this.camCh, this.camClamp.x, this.camClamp.y);
		this.camChCur = Mathf.SmoothDamp(this.camChCur, this.camCh, ref this.camhVel, this.smoothCam);
		this.camTransform.localPosition = new Vector3(this.cameraStart.x, this.cameraStart.y, this.cameraStart.z + this.camChCur);
	}

	// Token: 0x04000045 RID: 69
	[Header("Rotation Settings")]
	public float rotationSpeed = 180f;

	// Token: 0x04000046 RID: 70
	public Vector2 mouseVerticaleClamp = new Vector2(-20f, 20f);

	// Token: 0x04000047 RID: 71
	public float smoothMouse = 3f;

	// Token: 0x04000048 RID: 72
	[Header("Lift Settings")]
	public bool liftOn = true;

	// Token: 0x04000049 RID: 73
	public Vector2 liftClamp = new Vector2(-20f, 20f);

	// Token: 0x0400004A RID: 74
	public float smoothLift = 0.5f;

	// Token: 0x0400004B RID: 75
	[Header("Camera Settings")]
	public Transform camTransform;

	// Token: 0x0400004C RID: 76
	public Vector2 camClamp = new Vector2(-20f, 20f);

	// Token: 0x0400004D RID: 77
	public float smoothCam = 0.5f;

	// Token: 0x0400004E RID: 78
	private Transform tr;

	// Token: 0x0400004F RID: 79
	private Vector3 rotationStart;

	// Token: 0x04000050 RID: 80
	private Vector3 positionStart;

	// Token: 0x04000051 RID: 81
	private Vector3 cameraStart;

	// Token: 0x04000052 RID: 82
	private Vector3 velocity;

	// Token: 0x04000053 RID: 83
	private Vector3 target;

	// Token: 0x04000054 RID: 84
	private float mouseX;

	// Token: 0x04000055 RID: 85
	private float mouseY;

	// Token: 0x04000056 RID: 86
	private float upCh;

	// Token: 0x04000057 RID: 87
	private float upChCur;

	// Token: 0x04000058 RID: 88
	private float upChVel;

	// Token: 0x04000059 RID: 89
	private float camhVel;

	// Token: 0x0400005A RID: 90
	private float camCh;

	// Token: 0x0400005B RID: 91
	private float camChCur;
}
