using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BD RID: 189
public class WinionFile : MonoBehaviour
{
	// Token: 0x060004A4 RID: 1188 RVA: 0x00030FF8 File Offset: 0x0002F1F8
	private void Awake()
	{
		this.image = base.transform.GetChild(0).GetComponent<Image>();
		this.isSelect = false;
		this.image.enabled = this.isSelect;
		base.GetComponent<Button>().onClick.AddListener(delegate
		{
			this.SelectWinion(true);
		});
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00031050 File Offset: 0x0002F250
	public void SelectWinion(bool value)
	{
		if (!SingletoneBehaviour<WinionFolderManager>.Instance.CanMoveFolder)
		{
			return;
		}
		this.isSelect = value;
		this.image.enabled = this.isSelect;
		if (value)
		{
			SingletoneBehaviour<WinionFolderManager>.Instance.windows[(int)this.folder].GetComponent<WinionFileSelector>().SetRemoveTarget(base.gameObject);
			SingletoneBehaviour<WinionFolderManager>.Instance.windows[(int)this.folder].GetComponent<WinionFileSelector>().Remove(true);
			if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
			{
				Debug.Log("위니언 초기화");
				WinionHandler winionHandler = GameManager.instance.GetWinionHandlers()[(int)this.winion];
				winionHandler.winionBehaviour.RemoveComponent<WinionDance>();
				winionHandler.winionBehaviour.RemoveComponent<WinionGetBackStage>();
				winionHandler.winionMouseEvent.canMouseEnter = true;
				winionHandler.winionDragAndDrop.canDragAndDrop = true;
			}
		}
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00011017 File Offset: 0x0000F217
	public void SetInfo(Winion _folder, Winion _winion)
	{
		this.folder = _folder;
		this.winion = _winion;
	}

	// Token: 0x04000532 RID: 1330
	public Winion folder;

	// Token: 0x04000533 RID: 1331
	public Winion winion;

	// Token: 0x04000534 RID: 1332
	public bool isSelect;

	// Token: 0x04000535 RID: 1333
	private Image image;
}
