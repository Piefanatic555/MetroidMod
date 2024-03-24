using System;
using System.Collections;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MetroidMod.Content.MorphBallAddons;

namespace MetroidMod.ID
{
    public static class MorphBallID
    {
        public sealed class Util
		{
			[AttributeUsage(AttributeTargets.Field)]
			public class SlotAttribute : System.Attribute
			{
				public int Slot { get; set; }

				public SlotAttribute() { }
				public SlotAttribute(int slot)
				{
					this.Slot = slot;
				}

				public override string ToString()
				{
					return ((SlotType) this.Slot).ToString();
				}
			}

			[AttributeUsage(AttributeTargets.Field)]
			public class ItemIDAttribute : System.Attribute
			{
				public int ItemID { get; set; }

				public ItemIDAttribute() { }
				public ItemIDAttribute(int itemID)
				{
					this.ItemID = itemID;
				}

				public override string ToString()
				{
					return this.ItemID;
				}
			}

			[AttributeUsage(AttributeTargets.Enum)]
			public class EnumEnforcementAttribute : System.Attribute
			{
				public enum EnforcementTypeEnum
				{
					ThrowException,
					DefaultToValue
				}
				public EnforcementTypeEnum EnforcementType { get; set; }
				public EnumEnforcementAttribute()
				{
					this.EnforcementType = EnforcementTypeEnum.DefaultToValue;
				}
				public EnumEmforcementAttribute(EnforcementTypeEnum enforcementType)
				{
					this.EnforcementType = enforcementType;
				}
			}

			public static int GetItemID(this Enum value)
			{
				return GetItemID(value);
			}
			public static int GetItemID<T>(object value)
			{
				return GetItemID(value);
			}
			public static int GetItemID(object value)
			{
				if (value == null) 
					return null;

				Type type = value.GetType();

				if (!type.IsEnum)
					throw new ApplicationException("Value parameter must be an enum.");

				FieldInfo fieldInfo = type.GetField(value.ToString());
				object[] itemIDAttributes = fieldInfo.GetCustomAttributes(typeof(ItemIDAttribute), false);

				if (itemIDAttributes == null || itemIDAttributes.Length == 0)
				{
					object[] enforcementAttributes = fieldInfo.GetCustomAttributes(typeof(EnumEnforcementAttribute), false);

					if (enforcementAttributes != null && enforcementAttributes.Length == 1)
					{
						EnumEnforcementAttribute enforcementAttribute = (EnumEnforcementAttribute)enforcementAttributes[0];
						if (enforcementAttribute.EnforcementType == EnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)
							throw new ApplicationException("No Slot attributes exist in enforced enum of type '" + type.Name + "', value '" + value.ToString() + "'.");
					}
					return -1;  // ITEM DOESN'T HAVE A DEFINED ITEMID BY ATTRIBUTES
				}
				else if (itemIDAttributes.Length > 1)
					throw new ApplicationException("Too many ItemID attributes exist in enum of type '" + type.Name + "', value '" + value.ToString() + "'.");
				return itemIDAttributes[0].ItemID;
			}

		}
    }

    public enum SlotType
    {
        None = -1,
        Drill,
        Weapon,
        Special,
        Utility,
        Boost
    }
}