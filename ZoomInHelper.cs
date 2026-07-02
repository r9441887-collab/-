using System;
using UnityEngine;

// Token: 0x02000431 RID: 1073
public class ZoomInHelper : SingletoneBehaviour<ZoomInHelper>
{
	// Token: 0x06001EA5 RID: 7845 RVA: 0x0001BCB8 File Offset: 0x00019EB8
	private void Start()
	{
		this.zoom = Camera.main.orthographicSize;
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x000DC1AC File Offset: 0x000DA3AC
	private void Update()
	{
		float axis = Input.GetAxis("Mouse ScrollWheel");
		this.zoom -= axis * this.zoomMultiplier;
		this.zoom = Mathf.Clamp(this.zoom, this.minZoom, this.maxZoom);
		this.currentScale = this.defaultScale / this.zoom;
		this.CanvasParent.localScale = Vector3.one * this.currentScale;
		Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, this.zoom, ref this.velocity, this.smoothTime);
	}

	// Token: 0x04001CD1 RID: 7377
	[Header("기본 배율")]
	public float defaultScale = 3f;

	// Token: 0x04001CD2 RID: 7378
	[Header("현재 확대 배율")]
	public float currentScale = 1f;

	// Token: 0x04001CD3 RID: 7379
	[Header("현재 카메라 사이즈")]
	public float zoom = 1f;

	// Token: 0x04001CD4 RID: 7380
	[SerializeField]
	private Transform CanvasParent;

	// Token: 0x04001CD5 RID: 7381
	public float zoomMultiplier = 1f;

	// Token: 0x04001CD6 RID: 7382
	public float minZoom = 0.1f;

	// Token: 0x04001CD7 RID: 7383
	public float maxZoom = 3f;

	// Token: 0x04001CD8 RID: 7384
	public float velocity;

	// Token: 0x04001CD9 RID: 7385
	public float smoothTime = 0.25f;
}
