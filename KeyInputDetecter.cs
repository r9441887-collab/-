using System;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class KeyInputDetecter : MonoBehaviour
{
	// Token: 0x060008BC RID: 2236 RVA: 0x000442B0 File Offset: 0x000424B0
	public void KeyUp(KeyboardId keyCode)
	{
		this.KeyboardObject[(int)keyCode].GetComponent<SpriteRenderer>().sprite = this.UpSprites[(int)keyCode];
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x000442DC File Offset: 0x000424DC
	public void KeyDown(KeyboardId keyCode)
	{
		this.KeyboardObject[(int)keyCode].GetComponent<SpriteRenderer>().sprite = this.DownSprites[(int)keyCode];
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00044308 File Offset: 0x00042508
	private void Update()
	{
		if (Input.GetKeyUp(276))
		{
			this.KeyUp(KeyboardId.LeftArrow);
		}
		if (Input.GetKeyUp(275))
		{
			this.KeyUp(KeyboardId.RightArrow);
		}
		if (Input.GetKeyUp(273))
		{
			this.KeyUp(KeyboardId.UpArrow);
		}
		if (Input.GetKeyUp(274))
		{
			this.KeyUp(KeyboardId.DownArrow);
		}
		if (Input.GetKeyDown(276))
		{
			this.KeyDown(KeyboardId.LeftArrow);
		}
		if (Input.GetKeyDown(275))
		{
			this.KeyDown(KeyboardId.RightArrow);
		}
		if (Input.GetKeyDown(273))
		{
			this.KeyDown(KeyboardId.UpArrow);
		}
		if (Input.GetKeyDown(274))
		{
			this.KeyDown(KeyboardId.DownArrow);
		}
	}

	// Token: 0x040009A3 RID: 2467
	public Sprite[] UpSprites;

	// Token: 0x040009A4 RID: 2468
	public Sprite[] DownSprites;

	// Token: 0x040009A5 RID: 2469
	public GameObject[] KeyboardObject;
}
