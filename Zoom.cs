using System;
using UnityEngine;

// Token: 0x020000EE RID: 238
[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
	// Token: 0x060005E6 RID: 1510 RVA: 0x00011B7D File Offset: 0x0000FD7D
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
		if (this.camera)
		{
			this.defaultFOV = this.camera.fieldOfView;
		}
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x000363D4 File Offset: 0x000345D4
	private void Update()
	{
		this.currentZoom += Input.mouseScrollDelta.y * this.sensitivity * 0.05f;
		this.currentZoom = Mathf.Clamp01(this.currentZoom);
		this.camera.fieldOfView = Mathf.Lerp(this.defaultFOV, this.maxZoomFOV, this.currentZoom);
	}

	// Token: 0x04000658 RID: 1624
	private Camera camera;

	// Token: 0x04000659 RID: 1625
	public float defaultFOV = 60f;

	// Token: 0x0400065A RID: 1626
	public float maxZoomFOV = 15f;

	// Token: 0x0400065B RID: 1627
	[Range(0f, 1f)]
	public float currentZoom;

	// Token: 0x0400065C RID: 1628
	public float sensitivity = 1f;
}
