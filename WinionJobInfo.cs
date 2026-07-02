using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000120 RID: 288
public class WinionJobInfo : MonoBehaviour
{
	// Token: 0x060006DE RID: 1758 RVA: 0x0003CC60 File Offset: 0x0003AE60
	private void Start()
	{
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = 0;
		entry.callback.AddListener(delegate(BaseEventData eventData)
		{
			this.winionBackground.color = new Color(0.5f, 0.5f, 0.5f, 0.45490196f);
		});
		base.GetComponent<EventTrigger>().triggers.Add(entry);
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = 1;
		entry2.callback.AddListener(delegate(BaseEventData eventData)
		{
			this.winionBackground.color = new Color(1f, 1f, 1f, 0.45490196f);
		});
		base.GetComponent<EventTrigger>().triggers.Add(entry2);
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x040007B9 RID: 1977
	public Image winionImage;

	// Token: 0x040007BA RID: 1978
	public Image winionBackground;

	// Token: 0x040007BB RID: 1979
	public TextMeshProUGUI winionName;

	// Token: 0x040007BC RID: 1980
	public TextMeshProUGUI winionLevel;
}
