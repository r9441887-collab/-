using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
[HelpURL("https://nvjob.github.io/unity/nvjob-dynamic-sky-lite")]
[AddComponentMenu("#NVJOB/Dynamic Sky/Dynamic Sky Lite")]
public class DynamicSkyLite : MonoBehaviour
{
	// Token: 0x06000007 RID: 7 RVA: 0x0000E29F File Offset: 0x0000C49F
	private void Awake()
	{
		this.ssgVector = Vector2.zero;
		this.tr = base.transform;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
	private void LateUpdate()
	{
		this.ssgVector = Quaternion.AngleAxis(Time.time * this.ssgUvRotateSpeed, Vector3.forward) * Vector2.one * this.ssgUvRotateDistance;
		Shader.SetGlobalFloat("_SkyShaderUvX", this.ssgVector.x);
		Shader.SetGlobalFloat("_SkyShaderUvZ", this.ssgVector.y);
		this.tr.position = new Vector3(this.player.position.x, this.tr.position.y, this.player.position.z);
	}

	// Token: 0x04000003 RID: 3
	[Header("Settings")]
	public float ssgUvRotateSpeed = 1f;

	// Token: 0x04000004 RID: 4
	public float ssgUvRotateDistance = 1f;

	// Token: 0x04000005 RID: 5
	public Transform player;

	// Token: 0x04000006 RID: 6
	[Header("Information")]
	public string HelpURL = "nvjob.github.io/unity/nvjob-dynamic-sky-lite";

	// Token: 0x04000007 RID: 7
	public string ReportAProblem = "nvjob.github.io/support";

	// Token: 0x04000008 RID: 8
	public string Patrons = "nvjob.github.io/patrons";

	// Token: 0x04000009 RID: 9
	private Vector2 ssgVector;

	// Token: 0x0400000A RID: 10
	private Transform tr;
}
