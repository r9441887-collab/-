using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000411 RID: 1041
public class MailObject : MonoBehaviour
{
	// Token: 0x06001E07 RID: 7687 RVA: 0x0001B752 File Offset: 0x00019952
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(delegate
		{
			SingletoneBehaviour<MailManager>.Instance.SetMailContent(this);
			SingletoneBehaviour<MailManager>.Instance.ReadMail(base.gameObject);
			this.NoReadRedDot.SetActive(false);
			SingletoneBehaviour<MailManager>.Instance.SetReadMail(this.data.id);
			SingletoneBehaviour<MailManager>.Instance.ScrollUp();
		});
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x000D9314 File Offset: 0x000D7514
	public void SetData(MailData mailData, bool isRead)
	{
		this.data = mailData;
		this.profileImage.sprite = this.data.profileImage;
		this.profileImage.GetComponent<RectTransform>().localScale = Vector3.one * this.data.scale;
		Vector3 localPosition = this.profileImage.GetComponent<RectTransform>().localPosition;
		this.profileImage.GetComponent<RectTransform>().localPosition = new Vector3(localPosition.x, this.data.posY, localPosition.z);
		this.nameText.text = this.data.name;
		this.titleText.text = this.data.title;
		this.dateText.text = "20xx. xx. xx xx:xx";
		this.NoReadRedDot.SetActive(!isRead);
	}

	// Token: 0x04001C30 RID: 7216
	public MailData data = new MailData();

	// Token: 0x04001C31 RID: 7217
	[SerializeField]
	private Image profileImage;

	// Token: 0x04001C32 RID: 7218
	[SerializeField]
	private TextMeshProUGUI nameText;

	// Token: 0x04001C33 RID: 7219
	[SerializeField]
	private TextMeshProUGUI titleText;

	// Token: 0x04001C34 RID: 7220
	[SerializeField]
	private TextMeshProUGUI dateText;

	// Token: 0x04001C35 RID: 7221
	[SerializeField]
	private GameObject NoReadRedDot;

	// Token: 0x04001C36 RID: 7222
	private static bool firstMail = true;
}
