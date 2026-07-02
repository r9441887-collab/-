using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public class TransparencyCaptureToFile : MonoBehaviour
{
	// Token: 0x06001F70 RID: 8048 RVA: 0x0001C45F File Offset: 0x0001A65F
	public IEnumerator capture()
	{
		yield return new WaitForEndOfFrame();
		zzTransparencyCapture.captureScreenshot("capture.png");
		yield break;
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x0001C467 File Offset: 0x0001A667
	public void Update()
	{
		if (Input.GetKeyDown(99))
		{
			base.StartCoroutine(this.capture());
		}
	}
}
