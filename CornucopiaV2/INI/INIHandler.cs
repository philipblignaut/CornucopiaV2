using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CornucopiaV2
{
	public class INIHandler
	{

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
		public INIHandler(string filePath)
		{
			FilePath = filePath;
		}

		public bool WriteValue
			(string section
			, string key
			, string value
			)
		{
			return
				INIHandler
					.WritePrivateProfileString
					(section
					, key
					, value
					, FilePath
					)
					;
		}

		public string ReadValue
			(string section
			, string key
			,  string defaultValue = ""
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

		public string[] ReadKeyValuePairs(string section)
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
					return keyValuePairs;
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

	}
}
