using System;
using System.Diagnostics;
using System.IO;

namespace CornucopiaV2
{
	static public class Out
	{
		static private object fileLock = new object();
		static public void TextDC(string text)
		{
			Debug.Print(text);
			Console.WriteLine(text);
		}
		static public void TextDC(string format, params object[] args)
		{
			Debug.Print(format, args);
			Console.WriteLine(format, args);
		}
		static public void TextDCF(string path, string format, params object[] args)
		{
			TextDCF(path, format.FormatWith(args));
		}
		static public void TextDCF(string path, string text)
		{
			Debug.Print(text);
			Console.WriteLine(text);
			lock (fileLock) File.WriteAllText(path, text + Environment.NewLine);
		}
		static public void TextDCFA(string path, string format, params object[] args)
		{
			TextDCFA(path, format.FormatWith(args));
		}
		static public void TextDCFA(string path, string text)
		{
			Debug.Print(text);
			Console.WriteLine(text);
			lock (fileLock) File.AppendAllText(path, text + Environment.NewLine);
		}
		static public void OutTextF(string path, string format, params object[] args)
		{
			lock (fileLock) File.WriteAllText(path, format.FormatWith(args) + Environment.NewLine);
		}
		static public void OutTextF(string path, string text)
		{
			lock (fileLock) File.WriteAllText(path, text + Environment.NewLine);
		}
		static public void OutTextFA(string path, string text)
		{
			lock (fileLock) File.AppendAllText(path, text + Environment.NewLine);
		}
	}
}
