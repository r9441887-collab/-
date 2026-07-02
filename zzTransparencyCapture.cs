using System;
using System.IO;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public class zzTransparencyCapture
{
	// Token: 0x06001F79 RID: 8057 RVA: 0x000DF164 File Offset: 0x000DD364
	public static Texture2D capture(Rect pRect)
	{
		Camera main = Camera.main;
		CameraClearFlags clearFlags = main.clearFlags;
		Color backgroundColor = main.backgroundColor;
		main.clearFlags = 2;
		main.backgroundColor = Color.black;
		main.Render();
		Texture2D texture2D = zzTransparencyCapture.captureView(pRect);
		main.backgroundColor = Color.white;
		main.Render();
		Texture2D texture2D2 = zzTransparencyCapture.captureView(pRect);
		for (int i = 0; i < texture2D2.width; i++)
		{
			for (int j = 0; j < texture2D2.height; j++)
			{
				Color pixel = texture2D.GetPixel(i, j);
				Color pixel2 = texture2D2.GetPixel(i, j);
				if (pixel != Color.clear)
				{
					texture2D2.SetPixel(i, j, zzTransparencyCapture.getColor(pixel, pixel2));
				}
			}
		}
		texture2D2.Apply();
		Texture2D texture2D3 = texture2D2;
		Object.DestroyImmediate(texture2D);
		main.backgroundColor = backgroundColor;
		main.clearFlags = clearFlags;
		return texture2D3;
	}

	// Token: 0x06001F7A RID: 8058 RVA: 0x0001C496 File Offset: 0x0001A696
	public static Texture2D captureScreenshot()
	{
		return zzTransparencyCapture.capture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x000DF244 File Offset: 0x000DD444
	public static void captureScreenshot(string pFileName)
	{
		Texture2D texture2D = zzTransparencyCapture.captureScreenshot();
		try
		{
			using (FileStream fileStream = new FileStream(pFileName, FileMode.Create))
			{
				new BinaryWriter(fileStream).Write(ImageConversion.EncodeToPNG(texture2D));
			}
		}
		finally
		{
			Object.DestroyImmediate(texture2D);
		}
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x000DF2A0 File Offset: 0x000DD4A0
	private static Color getColor(Color pColorWhenBlack, Color pColorWhenWhite)
	{
		float alpha = zzTransparencyCapture.getAlpha(pColorWhenBlack.r, pColorWhenWhite.r);
		return new Color(pColorWhenBlack.r / alpha, pColorWhenBlack.g / alpha, pColorWhenBlack.b / alpha, alpha);
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
	private static float getAlpha(float pColorWhenZero, float pColorWhenOne)
	{
		return 1f + pColorWhenZero - pColorWhenOne;
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x0001C4C3 File Offset: 0x0001A6C3
	private static Texture2D captureView(Rect pRect)
	{
		Texture2D texture2D = new Texture2D((int)pRect.width, (int)pRect.height, 5, false);
		texture2D.ReadPixels(pRect, 0, 0, false);
		return texture2D;
	}
}
