using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class zzTransparencyCaptureExample : MonoBehaviour
{
	// Token: 0x06001F65 RID: 8037 RVA: 0x0001C42C File Offset: 0x0001A62C
	private void Start()
	{
		this.lastMousePosition = Input.mousePosition;
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x0001C439 File Offset: 0x0001A639
	public IEnumerator capture()
	{
		Rect lRect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		if (this.capturedImage)
		{
			Object.Destroy(this.capturedImage);
		}
		yield return new WaitForEndOfFrame();
		this.capturedImage = zzTransparencyCapture.capture(lRect);
		yield break;
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x000DEF78 File Offset: 0x000DD178
	public void Update()
	{
		if (Input.GetKeyDown(99))
		{
			base.StartCoroutine(this.capture());
		}
		if (Input.GetKeyDown(115))
		{
			Object.Destroy(this.capturedImage);
		}
		Vector3 vector = Input.mousePosition - this.lastMousePosition;
		vector *= 0.15f;
		this.cameraTransform.Translate(vector);
		this.lastMousePosition = Input.mousePosition;
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x000DEFE4 File Offset: 0x000DD1E4
	private void OnGUI()
	{
		if (this.capturedImage)
		{
			GUI.DrawTexture(new Rect((float)Screen.width * 0.1f, (float)Screen.height * 0.1f, (float)Screen.width * 0.8f, (float)Screen.height * 0.8f), this.capturedImage, 2, true);
			GUI.color = Color.green;
			GUILayout.Label("press S to clear", Array.Empty<GUILayoutOption>());
		}
		GUI.color = Color.black;
		GUILayout.Label("Press C to do transparent capturing, please capture those boxes", Array.Empty<GUILayoutOption>());
		GUILayout.Label("The result won't include background color, and the transparency (alpha value) in scene objects, is also can be captured", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x04001DCF RID: 7631
	public Texture2D capturedImage;

	// Token: 0x04001DD0 RID: 7632
	public Transform cameraTransform;

	// Token: 0x04001DD1 RID: 7633
	private Vector3 lastMousePosition;
}
