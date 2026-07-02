using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012B RID: 299
public class WinionDataForDebug : MonoBehaviour
{
	// Token: 0x06000724 RID: 1828 RVA: 0x0003DE1C File Offset: 0x0003C01C
	private void Start()
	{
		Button.ButtonClickedEvent buttonClickedEvent = new Button.ButtonClickedEvent();
		buttonClickedEvent.AddListener(delegate
		{
			SingletoneBehaviour<DebugWinion>.Instance.SetWinionDebugCenter(this);
		});
		base.GetComponent<Button>().onClick = buttonClickedEvent;
	}

	// Token: 0x04000812 RID: 2066
	[SerializeField]
	public DebugWinionContents winionContents;

	// Token: 0x04000813 RID: 2067
	public WinionHandler handler;
}
