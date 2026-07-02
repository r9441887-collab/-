using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

// Token: 0x02000440 RID: 1088
public class TypingSound : MonoBehaviour
{
	// Token: 0x06001EE7 RID: 7911 RVA: 0x0001BF85 File Offset: 0x0001A185
	private void Start()
	{
		this.TypingSoundSource.volume = 0f;
		this.PlaySound = false;
		this.TypingSoundSource.loop = true;
		this.TypingSoundSource.Play();
		this.Clear();
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x000DD184 File Offset: 0x000DB384
	private void Clear()
	{
		this.Clearing = true;
		this.LastTextCount = 0;
		this.minSpeed = float.MaxValue;
		this.maxSpeed = float.MinValue;
		this.ElapsedTime = Time.deltaTime;
		this.elapsedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x000DD1D4 File Offset: 0x000DB3D4
	private void Update()
	{
		if (this.LineText.text.Length < 1 && !this.Clearing)
		{
			this.Clear();
		}
		else if (this.LineText.text.Length > 1 && this.Clearing)
		{
			this.Clearing = false;
		}
		this.currTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		if (this.LineText.text.Length != this.LastTextCount)
		{
			if (!this.PlaySound)
			{
				this.PlaySound = true;
				DOVirtual.Float(0f, 1f, 0.2f, delegate(float f)
				{
					this.TypingSoundSource.volume = f;
				});
			}
			this.sampleTime = (float)(this.currTime - this.elapsedTime) * 2f;
			this.elapsedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			this.LastTextCount = this.LineText.text.Length;
		}
		if (this.PlaySound && (float)(this.currTime - this.elapsedTime) > this.sampleTime)
		{
			this.PlaySound = false;
			DOVirtual.Float(1f, 0f, 0.2f, delegate(float f)
			{
				this.TypingSoundSource.volume = f;
			});
		}
	}

	// Token: 0x04001D3D RID: 7485
	public TextMeshProUGUI LineText;

	// Token: 0x04001D3E RID: 7486
	public AudioSource TypingSoundSource;

	// Token: 0x04001D3F RID: 7487
	public float minSpeed;

	// Token: 0x04001D40 RID: 7488
	public float maxSpeed;

	// Token: 0x04001D41 RID: 7489
	public bool Clearing;

	// Token: 0x04001D42 RID: 7490
	public int LastTextCount;

	// Token: 0x04001D43 RID: 7491
	public bool PlaySound;

	// Token: 0x04001D44 RID: 7492
	public long elapsedTime;

	// Token: 0x04001D45 RID: 7493
	private float typingSpeed;

	// Token: 0x04001D46 RID: 7494
	private float timer;

	// Token: 0x04001D47 RID: 7495
	public float ElapsedTime;

	// Token: 0x04001D48 RID: 7496
	private long currTime;

	// Token: 0x04001D49 RID: 7497
	public float sampleTime;
}
