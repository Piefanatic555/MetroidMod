using System;
using System.Collections;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MetroidMod.Content.SuitAddons;
using MetroidMod.Content.Items.Tiles;

namespace MetroidMod.ID
{
        public static class SuitID
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

                public enum SlotType
                {
                        None = -1,
                        Tank_Energy,
                        Tank_Reserve,
                        Missile_Expansion,      // Will be used to reconfigure where missiles are tracked/stored.
                        Varia,
                        Utility,                // Dark Suit, Gravity Suit, P.E.D. Suit
                        Utility2,               // Light Suit, Terra Gravity Suit, Phazon Suit, Hazard Shield
                        Lunar,
                        Grip,
                        Attack,
                        Boots,                  // Hi-Jump Boots, Space Jump Boots
                        Jump,                   // Space Jump
                        Speed,                  // Speed Booster
                        Visor_Scan,
                        Visor_Utility,          // Thermal Visor, Dark Visor, Command Visor
                        Visor_Utility2          // XRay Visor, Echo Visor
                }
                
                [Util.EnumEnforcement(Util.EnumEnforcement.EnumEnforcementType.DefaultToValue)]
                public enum Addon
                {
                        [Util.ItemID(ModContent.ItemType<EnergyTank>()), Util.Slot(SlotType.Tank_Energy)]               
                        Tank_Energy,            
                        [Util.ItemID(ModContent.ItemType<ReserveTank>()), Util.Slot(SlotType.Tank_Reserve)]             
                        Tank_Reserve,           
                        [Util.ItemID(ModContent.ItemType<MissileExpansion>()), Util.Slot(SlotType.Missile_Expansion)]   // Will be used to reconfigure where missiles are tracked/stored.
                        Missile_Expansion,      
                        [Util.ItemID(ModContent.ItemType<VariaSuitAddon>()), Util.Slot(SlotType.Varia)]                 
                        Varia,                  
                        [Util.ItemID(ModContent.ItemType<VariaSuitV2Addon>()), Util.Slot(SlotType.Varia)]
                        VariaV2,
                        [Util.ItemID(ModContent.ItemType<DarkSuitAddon>()), Util.Slot(SlotType.Utility)]                
                        Dark_Suit,              
                        [Util.ItemID(ModContent.ItemType<GravitySuitAddon>()), Util.Slot(SlotType.Utility)]
                        Gravity,
                        [Util.ItemID(ModContent.ItemType<PEDSuitAddon>()), Util.Slot(SlotType.Utility)]
                        PED,
                        [Util.ItemID(ModContent.ItemType<LightSuitAddon>()), Util.Slot(SlotType.Utility2)]              
                        Light, 
                        [Util.ItemID(ModContent.ItemType<TerraGravitySuitAddon>()), Util.Slot(SlotType.Utility2)]                 
                        TerraGravity,
                        [Util.ItemID(ModContent.ItemType<PhazonSuitAddon>()), Util.Slot(SlotType.Utility2)]
                        Phazon,
                        [Util.ItemID(ModContent.ItemType<HazardShieldAddon>()), Util.Slot(SlotType.Utility2)]
                        HazardShield,
                        [Util.ItemID(ModContent.ItemType<SolarAugment>()), Util.Slot(SlotType.Lunar)]                   
                        Solar, 
                        // [Util.ItemID(ModContent.ItemType<StardustAugment>()), Util.Slot(SlotType.Lunar)]                 
                        Stardust,
                        [Util.ItemID(ModContent.ItemType<NebulaAugment>()), Util.Slot(SlotType.Lunar)]
                        Nebula,
                        [Util.ItemID(ModContent.ItemType<VortexAugment>()), Util.Slot(SlotType.Lunar)]
                        Vortex,
                        [Util.ItemID(ModContent.ItemType<PolarGrip>()), Util.Slot(SlotType.Grip)]                       
                        PowerGrip, 
                        [Util.ItemID(ModContent.ItemType<ScrewAttack>()), Util.Slot(SlotType.Attack)]                   
                        ScrewAttack,   
                        [Util.ItemID(ModContent.ItemType<HiJumpBoots>()), Util.Slot(SlotType.Boots)]                    
                        HiJump,                 
                        [Util.ItemID(ModContent.ItemType<SpaceJumpBoots>()), Util.Slot(SlotType.Boots)]
                        SpaceJump_Boots,
                        [Util.ItemID(ModContent.ItemType<SpaceJump>()), Util.Slot(SlotType.Jump)]                       
                        SpaceJump,           
                        [Util.ItemID(ModContent.ItemType<SpeedBooster>()), Util.Slot(SlotType.Speed)]   
                        SpeedBooster,         
                        [Util.ItemID(ModContent.ItemType<ScanVisor>()), Util.Slot(SlotType.Visor_Scan)]
                        Scan,                   
                        // [Util.ItemID(ModContent.ItemType<ThermalVisor>()), Util.Slot(SlotType.Visor_Utility)]
                        Thermal,       
                        // [Util.ItemID(ModContent.ItemType<DarkVisor>()), Util.Slot(SlotType.Visor_Utility)]         
                        Dark_Visor,
                        // [Util.ItemID(ModContent.ItemType<CommandVisor>()), Util.Slot(SlotType.Visor_Utility)]
                        Command,
                        [Util.ItemID(ModContent.ItemType<XRayScope>()), Util.Slot(SlotType.Visor_Utility2)]
                        XRay,                   
                        // [Util.ItemID(ModContent.ItemType<EchoVisor>()), Util.Slot(SlotType.Visor_Utility2)]
                        Echo                        
                }
                
        }
}
