using System;
using UnityEngine;

// Token: 0x02000376 RID: 886
public class EventSpeech_Chapter02Event08 : MonoBehaviour
{
	// Token: 0x06001A97 RID: 6807 RVA: 0x000192AB File Offset: 0x000174AB
	private void OnEnable()
	{
		base.Invoke("ActiveResetBubble", Random.Range(1f, 2f));
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x0000EE27 File Offset: 0x0000D027
	public void ActiveResetBubble()
	{
		base.gameObject.SetActive(false);
	}
}
