using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CornucopiaV2
{
	[AttributeUsage
	  (AttributeTargets.Field
	  , AllowMultiple = false
	  , Inherited = false
	  )
	]
	public abstract class AEnumText
	   : Attribute
	{
		public string Description { get; set; }
		public AEnumText()
		{
			Description = "Unknown";
		}
		public AEnumText(string description)
		{
			Description = description;
		}
	}
	public sealed class EnumDescription
	   : AEnumText
	{
		public EnumDescription()
		   : base()
		{
			Description = "Unknown";
		}
		public EnumDescription
		  (string description
		  )
		   : base
		   (description
		   )
		{
		}
	}
	public class EnumShortDescription
	  : AEnumText
	{
		public EnumShortDescription()
		   : base()
		{
		}
		public EnumShortDescription
		  (string description
		  )
		   : base
		   (description
		   )
		{
		}
	}
	public class EnumLongDescription
	  : AEnumText
	{
		public EnumLongDescription()
		   : base()
		{
		}
		public EnumLongDescription
		  (string description
		  )
		   : base
		   (description
		   )
		{
		}
	}
	public static class EnumExtention
	{
		public static T GetAttribute<T>
		  (this Enum value
		  )
		{
			T returnValue = default(T);
			value
			   .GetType()
			   .GetField(value.ToString())
			   .GetCustomAttributes(typeof(T), false)
			   .Each(attribute => returnValue = (T)attribute)
			   ;
			return returnValue;
		}

		private static string EnumText<T>
		 (this Enum value
		 )
		 where T : AEnumText
		{
			T attribute = value.GetAttribute<T>();
			if (attribute == null)
			{
				throw
				   new ArgumentException
					 ("Enum value " + value.ToString()
					 + " does not have an Attribute called "
					 + typeof(T).ToString()
					 )
					 ;
			}
			return attribute.Description;
		}

		public static string EnumDescription
		  (this Enum value
		  )
		{
			return value.EnumText<EnumDescription>();
		}
		public static string EnumShortDescription
		  (this Enum value
		  )
		{
			return value.EnumText<EnumShortDescription>();
		}
		public static string EnumLongDescription
		  (this Enum value
		  )
		{
			return value.EnumText<EnumLongDescription>();
		}
		public static void EnumEach<T>
		  (Action<T> method
		  )
		{
			if (method != null)
			{
				Enum
				  .GetValues(typeof(T))
				  .Cast<T>()
				  .Each
				  (value =>
				  {
					  method.Invoke(value);
				  }
				  )
				  ;
			}
		}
		private enum StringType
		{
			ToString = 0,
			EnumDescription = 1,
			EnumShortDescription = 2,
			EnumLongDescription = 3,
		}
		//private static T ParseText<T>
		//   (this string toString
		//   , T defaultValue
		//   , bool throwExceptionOnParseFail
		//   , StringType stringType
		//   )
		//  where T : struct, IConvertible
		//{
		//   if (!typeof(T).IsEnum)
		//   {
		//      throw
		//         new ArgumentException
		//            ("T in ParseToString<T> must be an enumerated type (enum)"
		//            )
		//            ;
		//   }
		//   T returnEnumValue = defaultValue;
		//   bool found = false;
		//   EnumExtention
		//      .EnumEach<T>
		//      (enumValue =>
		//      {
		//         string stringValue = string.Empty;
		//         switch (stringType)
		//         {
		//            case StringType.ToString:
		//               stringValue = enumValue.ToString();
		//               break;
		//            case StringType.EnumDescription:
		//               stringValue = enumValue.EnumDescription();
		//               break;
		//            case StringType.EnumShortDescription:
		//               stringValue = enumValue.GetAttribute<EnumShortDescription>().Description;
		//               break;
		//            case StringType.EnumLongDescription:
		//               stringValue = enumValue.GetAttribute<EnumLongDescription>().Description;
		//               break;
		//         }
		//         if (stringValue.ToLower().Equals(toString.ToLower()))
		//         {
		//            found = true;
		//            returnEnumValue = enumValue;
		//            return;
		//         }
		//      }
		//     )
		//     ;
		//   if ((!found) && throwExceptionOnParseFail)
		//   {
		//      throw
		//         new Exception
		//            ("The value '"
		//            + toString
		//            + "' could not be associated with an enum entry in "
		//            + typeof(T).Name
		//            )
		//            ;
		//   }
		//   return returnEnumValue;
		//}
		public static T ParseToString<T>
		  (this string toString
		  , T defaultValue
		  , bool throwExceptionOnParseFail
		  )
		  where T : struct, IConvertible
		{
			if (!typeof(T).IsEnum)
			{
				throw
				   new ArgumentException
					  ("T in ParseToString<T> must be an enumerated type"
					  )
					  ;
			}
			T returnEnumValue = defaultValue;
			bool found = false;
			EnumExtention.EnumEach<T>
			  (enumValue =>
			  {
				  if (enumValue.ToString() == toString)
				  {
					  found = true;
					  returnEnumValue = enumValue;
					  return;
				  }
			  }
			  )
			  ;
			if ((!found) && throwExceptionOnParseFail)
			{
				throw
				   new Exception
					  ("The value '"
					  + toString
					  + "' could not be associated with an enum entry in "
					  + typeof(T).Name
					  )
					  ;
			}
			return returnEnumValue;
		}
		public static IEnumerable<T> EnumValuesAll<T>
		   (this T someValue
		   )
		{
			foreach (T enumValue in Enum.GetValues(typeof(T)))
			{
				yield return enumValue;
			}
		}
		public static IEnumerable<int> EnumValuesAll<T>()
		{
			foreach (int enumValue in Enum.GetValues(typeof(T)).Cast<int>())
			{
				yield return enumValue;
			}
		}

		public static IEnumerable<int> EnumValuesSplit<T>
		   (this int enumCombinedValues
		   )
		{
			foreach (int enumValue in EnumValuesAll<T>())
			{
				if (enumValue == (enumValue & enumCombinedValues))
				{
					yield return enumValue;
				}
			}
		}

		//public static T ParseEnumDescription
		public static TOut GetAttributeValue<T, TOut>
			(this Enum enumeration
			, Func<T, TOut> expression
			)
			where T : Attribute
		{
			T attribute =
			  enumeration
				.GetType()
				.GetMember(enumeration.ToString())
				.Where(member => member.MemberType == MemberTypes.Field)
				.FirstOrDefault()
				.GetCustomAttributes(typeof(T), false)
				.Cast<T>()
				.SingleOrDefault();

			if (attribute == null)
				return default(TOut);

			return expression(attribute);
		}

		public static T GetAttributeX<T>(this Enum value) where T : Attribute
		{
			Type type = value.GetType();
			MemberInfo[] memberInfo = type.GetMember(value.ToString());
			object[] attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
			return (T)attributes[0];
		}

	}
}