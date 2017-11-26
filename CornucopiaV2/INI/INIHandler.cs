using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Generic;

namespace CornucopiaV2
{
	public class INIHandler
	{

		public string Section { get; set; }

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		private static extern int GetPrivateProfileString
			(string section
			, string key
			, string defaultValue
			, StringBuilder value
			, int size
			, string filePath
			)
			;

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetPrivateProfileString
			(string section
			, string key
			, string defaultValue
			, [In, Out] char[] value
			, int size
			, string filePath
			)
			;

#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
#pragma warning restore CA2101 // Specify marshaling for P/Invoke string arguments
		private static extern int GetPrivateProfileSection
			(string section
			, IntPtr keyValue
			, int size
			, string filePath
			)
			;

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool WritePrivateProfileString
			(string section
			, string key
			, string value
			, string filePath
			)
			;

		public int Capacity { get; private set; } = 512;

		public string FilePath { get; private set; }

		/// <summary>Record Constructor</summary>
		/// <param name="filePath"><see cref="FilePath"/></param>
		public INIHandler
			(string filePath
			)
		{
			FilePath = filePath;
		}

		public INIHandler
			(string filePath
			, string section
			)
		{
			FilePath = filePath;
			Section = section;
		}

		public bool WriteValue
			(string key
			, string value
			)
		{
			return
				WritePrivateProfileString
					(Section
					, key
					, value
					, FilePath
					)
					;
		}

		public string ReadValue
			(string section
			, string key
			, string defaultValue = ""
			)
		{
			int capacity = Capacity;
			StringBuilder value = new StringBuilder(capacity);
			GetPrivateProfileString
				(section
				, key
				, defaultValue
				, value
				, value.Capacity
				, FilePath
				)
				;
			return value.ToString();
		}

		public int GInt
			(string sectionKey
			, int defaultValue = 0
			)
		{
			string section = C.es;
			string key = C.es;
			SplitSectionKey(sectionKey, out section, out key);
			return
				int
				.Parse
				(ReadValue
					(section
					, key
					, defaultValue.ToString()
					)
				)
				;
		}

		public bool WInt
			(string sectionKey
			, int value
			)
		{
			string section = C.es;
			string key = C.es;
			SplitSectionKey(sectionKey, out section, out key);
			return
				WritePrivateProfileString
				(section
				, key
				, value.ToString()
				, FilePath
				)
				;
		}

		public long GLong
			(string sectionKey
			, long defaultValue = 0L
			)
		{
			string section = C.es;
			string key = C.es;
			SplitSectionKey(sectionKey, out section, out key);
			return
				long
				.Parse
				(ReadValue
					(section
					, key
					, defaultValue.ToString()
					)
				)
				;
		}

		public bool WLong
			(string sectionKey
			, long value
			)
		{
			string section = C.es;
			string key = C.es;
			SplitSectionKey(sectionKey, out section, out key);
			return
				WritePrivateProfileString
				(section
				, key
				, value.ToString()
				, FilePath
				)
				;
		}

		public float GFloat
			(string sectionKey
			, float defaultValue = 0F
			)
		{
			SplitSectionKey
				(sectionKey
				, out string section
				, out string key
				)
				;
			return
				float
					.Parse
					(ReadValue
						(section
						, key
						, defaultValue.ToString()
						)
					)
					;
		}

		public bool WFloat
			(string sectionKey
			, float value
			)
		{
			SplitSectionKey(sectionKey, out string section, out string key);
			return
				WritePrivateProfileString
				(section
				, key
				, value.ToString()
				, FilePath
				)
				;
		}

		public double GDouble
			(string key
			, double defaultValue = 0D
			) =>
			double
				.Parse
				(ReadValue
					(Section
					, key
					, defaultValue.ToString()
					)
				)
				;

		public decimal GDecimal
			(string key
			, decimal defaultValue = 0M
			) =>
			decimal
				.Parse
				(ReadValue
					(Section
					, key
					, defaultValue.ToString()
					)
				)
				;

		public string GString
			(string key
			, string defaultValue = ""
			) =>
				ReadValue
					(Section
					, key
					, defaultValue
					)
				;

		public Color GColor
			(string sectionKey
			, string defaultValueFromName = "ff000000"
			)
		{
			SplitSectionKey(sectionKey, out string section, out string key);
			return
				Color
				.FromName
				(ReadValue
					(section
					, key
					, defaultValueFromName
					)
				)
				;
		}

		public bool WColor
			(string sectionKey
			, Color color
			)
		{
			string section = C.es;
			string key = C.es;
			SplitSectionKey(sectionKey, out section, out key);
			return
				WritePrivateProfileString
				(section
				, key
				, color.Name
				, FilePath
				)
				;
		}

		public string[] ReadSections()
		{
			int capacity = Capacity;
			while (true)
			{
				char[] chars = new char[capacity];
				int size = GetPrivateProfileString(null, null, "", chars, capacity, FilePath);

				if (size == 0)
				{
					return null;
				}

				if (size < capacity - 2)
				{
					string result = new String(chars, 0, size);
					string[] sections = result.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
					return sections;
				}

				capacity *= 2;
			}
		}

		public string[] ReadKeys(string section)
		{
			int capacity = Capacity;
			// first line will not recognize if ini file is saved in UTF-8 with BOM 
			while (true)
			{
				char[] chars = new char[capacity];
				int size = GetPrivateProfileString(section, null, "", chars, capacity, FilePath);

				if (size == 0)
				{
					return null;
				}
				if (size < capacity - 2)
				{
					string result = new String(chars, 0, size);
					string[] keys = result.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
					return keys;
				}
				capacity *= 2;
			}
		}

		public List< Tuple<string,string>>ReadKeyValuePairs
			(string section
			)
		{
			int capacity = Capacity;
			while (true)
			{
				IntPtr returnedString = Marshal.AllocCoTaskMem(capacity * sizeof(char));
				int size = GetPrivateProfileSection(section, returnedString, capacity, FilePath);
				if (size == 0)
				{
					Marshal.FreeCoTaskMem(returnedString);
					return null;
				}

				if (size < capacity - 2)
				{
					string result = Marshal.PtrToStringAuto(returnedString, size - 1);
					Marshal.FreeCoTaskMem(returnedString);
					string[] keyValuePairs = result.Split('\0');
					List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
					foreach (string kvp in keyValuePairs)
					{
						string[] kvpx = kvp.Split("=", StringSplitOptions.RemoveEmptyEntries);
						Tuple<string, string> tuple;
						if (kvpx.Length>1)
						{
							tuple = new Tuple<string, string>(kvpx[0], kvpx[1]);
						}
						else
						{
							tuple = new Tuple<string, string>(kvpx[0], C.es);
						}
						tupleList.Add(tuple);
					}
					return tupleList;
				}
				Marshal.FreeCoTaskMem(returnedString);
				capacity *= 2;
			}
		}

		public bool DeleteSection(string section)
		{
			return WritePrivateProfileString(section, null, null, FilePath);
		}

		public bool DeleteKey(string section, string key)
		{
			return WritePrivateProfileString(section, key, null, FilePath);
		}

		private void SplitSectionKey
			(string sectionKey
			, out string section
			, out string key
			)
		{
			int pos;
			if ((pos = sectionKey.IndexOf("\\")) > 0)
			{
				section = sectionKey.Substring(0, pos);
				key = sectionKey.Substring(pos + 1);
			}
			else
			{
				section = Section;
				key = sectionKey;
			}
		}

	}
}