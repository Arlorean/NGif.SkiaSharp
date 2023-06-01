using System;
using System.IO;
using SkiaSharp;
using NGif.Components;

namespace Example
{
	class ExampleMain
	{
		[STAThread]
		static void Main(string[] args)
		{
			/* create Gif */
			//you should replace filepath
			String [] imageFilePaths = new String[]{"01.png","02.png","03.png"}; 
			String outputFilePath = "test.gif";
			AnimatedGifEncoder e = new AnimatedGifEncoder();
            
            // read file as memorystream
            MemoryStream memStream = new MemoryStream();
            e.Start(memStream);
			e.SetDelay(500);
			//-1:no repeat,0:always repeat
			e.SetRepeat(0);
			for (int i = 0, count = imageFilePaths.Length; i < count; i++ ) 
			{
				var bitmap = SKBitmap.Decode(imageFilePaths[i]);
				e.AddFrame( SKImage.FromBitmap(bitmap) );
			}
			e.Finish();
            File.WriteAllBytes(outputFilePath, memStream.ToArray());

            /* extract Gif */
            GifDecoder gifDecoder = new GifDecoder();
			gifDecoder.Read(outputFilePath);
			for ( int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++ ) 
			{
                SKBitmap frame = gifDecoder.GetFrame( i );  // frame i
                File.WriteAllBytes(outputFilePath + i + ".png", SKImage.FromBitmap(frame).Encode().ToArray());
            }
		}
	}
}
