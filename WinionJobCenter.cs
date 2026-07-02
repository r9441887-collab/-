using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200042E RID: 1070
public class WinionJobCenter : MonoBehaviour
{
	// Token: 0x06001E98 RID: 7832 RVA: 0x0001BC23 File Offset: 0x00019E23
	private void Awake()
	{
		if (WinionJobCenter.Instance == null)
		{
			WinionJobCenter.Instance = this;
		}
	}

	// Token: 0x06001E99 RID: 7833 RVA: 0x000DBFE8 File Offset: 0x000DA1E8
	public void SetWinionJobPannel(bool value)
	{
		this.WinionJobPannel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		this.WinionJobPannel.SetActive(value);
		this.ClosePannel.SetActive(value);
		if (value)
		{
			for (int i = 0; i < GameManager.instance.gameData.WinionCount; i++)
			{
				Object.Instantiate<GameObject>(this.WinionStatusPrefab, this.WinionListContent);
			}
			return;
		}
		for (int j = 0; j < this.WinionListContent.childCount; j++)
		{
			Object.Destroy(this.WinionListContent.GetChild(j).gameObject);
		}
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x0000E32C File Offset: 0x0000C52C
	public void AllocWinionJob()
	{
	}

	// Token: 0x06001E9B RID: 7835 RVA: 0x0001BC38 File Offset: 0x00019E38
	public void MouseEnter(Image image)
	{
		image.color = new Color(0.5f, 0.5f, 0.5f, 0.45490196f);
	}

	// Token: 0x06001E9C RID: 7836 RVA: 0x0001BC59 File Offset: 0x00019E59
	public void MouseExit(Image image)
	{
		image.color = new Color(1f, 1f, 1f, 0.45490196f);
	}

	// Token: 0x04001CC6 RID: 7366
	public GameObject ClosePannel;

	// Token: 0x04001CC7 RID: 7367
	public GameObject WinionJobPannel;

	// Token: 0x04001CC8 RID: 7368
	public GameObject ScrollView;

	// Token: 0x04001CC9 RID: 7369
	public static WinionJobCenter Instance;

	// Token: 0x04001CCA RID: 7370
	public GameObject WinionStatusPrefab;

	// Token: 0x04001CCB RID: 7371
	public Transform WinionListContent;
}
