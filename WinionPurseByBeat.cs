using System;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class WinionPurseByBeat : PurseByBeat
{
	// Token: 0x060008A5 RID: 2213 RVA: 0x000139C0 File Offset: 0x00011BC0
	public override void Init()
	{
		base.Init();
		this.defaultSprite = this._rend.sprite;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00043F64 File Offset: 0x00042164
	public override void Pulse()
	{
		base.Pulse();
		SpriteRenderer rend = this._rend;
		Sprite[] array = this.sprites;
		int num = this.index;
		this.index = num + 1;
		rend.sprite = array[num];
		this.index %= this.sprites.Length;
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x000139D9 File Offset: 0x00011BD9
	public override void SetDefault()
	{
		base.SetDefault();
		this._rend.sprite = this.defaultSprite;
	}

	// Token: 0x04000987 RID: 2439
	public SpriteRenderer _rend;

	// Token: 0x04000988 RID: 2440
	public Sprite defaultSprite;

	// Token: 0x04000989 RID: 2441
	public Sprite[] sprites;

	// Token: 0x0400098A RID: 2442
	public int index;
}
