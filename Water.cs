using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
[HelpURL("https://nvjob.github.io/unity/nvjob-water-shader")]
[AddComponentMenu("#NVJOB/Water Shader/Water Shader - simple and fast")]
[ExecuteInEditMode]
public class Water : MonoBehaviour
{
	// Token: 0x06000019 RID: 25 RVA: 0x0000E349 File Offset: 0x0000C549
	private void Awake()
	{
		this.lwVector = Vector2.zero;
		this.lwNVector = Vector2.zero;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000E361 File Offset: 0x0000C561
	private void OnEnable()
	{
		if (this.depthTextureModeOn)
		{
			Camera.main.depthTextureMode = 1;
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
	private void LateUpdate()
	{
		this.lwVector = Quaternion.AngleAxis(Time.time * this.UvRotateSpeed, Vector3.forward) * Vector2.one * this.UvRotateDistance;
		this.lwNVector = Quaternion.AngleAxis(Time.time * this.UvBumpRotateSpeed, Vector3.forward) * Vector2.one * this.UvBumpRotateDistance;
		Shader.SetGlobalFloat("_WaterLocalUvX", this.lwVector.x);
		Shader.SetGlobalFloat("_WaterLocalUvZ", this.lwVector.y);
		Shader.SetGlobalFloat("_WaterLocalUvNX", this.lwNVector.x);
		Shader.SetGlobalFloat("_WaterLocalUvNZ", this.lwNVector.y);
	}

	// Token: 0x04000022 RID: 34
	[Header("Settings")]
	public float UvRotateSpeed = 0.4f;

	// Token: 0x04000023 RID: 35
	public float UvRotateDistance = 2f;

	// Token: 0x04000024 RID: 36
	public float UvBumpRotateSpeed = 0.4f;

	// Token: 0x04000025 RID: 37
	public float UvBumpRotateDistance = 2f;

	// Token: 0x04000026 RID: 38
	public bool depthTextureModeOn = true;

	// Token: 0x04000027 RID: 39
	[Header("Information")]
	public string HelpURL = "nvjob.github.io/unity/nvjob-water-shader";

	// Token: 0x04000028 RID: 40
	public string ReportAProblem = "nvjob.github.io/support";

	// Token: 0x04000029 RID: 41
	public string Patrons = "nvjob.github.io/patrons";

	// Token: 0x0400002A RID: 42
	private Vector2 lwVector;

	// Token: 0x0400002B RID: 43
	private Vector2 lwNVector;
}
