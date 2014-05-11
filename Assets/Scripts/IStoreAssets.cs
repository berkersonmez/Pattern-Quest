/*
 * Copyright (C) 2012 Soomla Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla;

namespace hmm.Soomla {
	public class Store : IStoreAssets{ // Make sure to change to your actual class name
		
		public int GetVersion() {
			return 0;
		}
		
		public VirtualCurrency[] GetCurrencies() {
			return  new VirtualCurrency[] {
				GOLD_CURRENCY, CRYSTAL_CURRENCY
			};
		}
		
		public VirtualGood[] GetGoods() {
			return new VirtualGood[] {
				/* SingleUseVGs     --> */    BAT_OF_PAIN_GOOD,
				/* LifetimeVGs      --> */    LIFE_DRAIN_GOOD,
				/* EquippableVGs    --> */
				/* SingleUsePackVGs --> */
				/* UpgradeVGs       --> */
			};
		}
		
		public VirtualCurrencyPack[] GetCurrencyPacks() {
			return new VirtualCurrencyPack[] {
				SMALL_CRYSTAL_PACK_PACK, MEDIUM_CRYSTAL_PACK_PACK, BIG_CRYSTAL_PACK_PACK
			};
		}
		
		public VirtualCategory[] GetCategories() {
			return new VirtualCategory[]{
				ITEMS_CATEGORY, SPELLS_CATEGORY, COMBOS_CATEGORY, POWERS_CATEGORY
			};
		}
		
		public NonConsumableItem[] GetNonConsumableItems() {
			return new NonConsumableItem[]{
				
			};
		}
		
		
		/** Static Final members **/
		
		// Currencies
		public const string GOLD_CURRENCY_ITEM_ID = "currency_gold";
		public const string CRYSTAL_CURRENCY_ITEM_ID = "currency_crystal";
		
		// Goods
		public const string LIFE_DRAIN_GOOD_ITEM_ID = "item_421";
		
		public const string BAT_OF_PAIN_GOOD_ITEM_ID = "item_456";
		
		
		// Currency Packs
		public const string SMALL_CRYSTAL_PACK_PACK_ITEM_ID = "item_192";
		#if UNITY_ANDROID
		public const string SMALL_CRYSTAL_PACK_PRODUCT_ID = "small_currency_pack";
		#else
		public const string SMALL_CRYSTAL_PACK_PRODUCT_ID = "small_currency_pack";
		#endif
		public const string MEDIUM_CRYSTAL_PACK_PACK_ITEM_ID = "item_182";
		#if UNITY_ANDROID
		public const string MEDIUM_CRYSTAL_PACK_PRODUCT_ID = "medium_currency_pack";
		#else
		public const string MEDIUM_CRYSTAL_PACK_PRODUCT_ID = "medium_currency_pack";
		#endif
		public const string BIG_CRYSTAL_PACK_PACK_ITEM_ID = "item_187";
		#if UNITY_ANDROID
		public const string BIG_CRYSTAL_PACK_PRODUCT_ID = "big_currency_pack";
		#else
		public const string BIG_CRYSTAL_PACK_PRODUCT_ID = "big_currency_pack";
		#endif
		
		// Non Consumables
		
		
		/** Virtual Currencies **/
		
		
		public static VirtualCurrency GOLD_CURRENCY = new VirtualCurrency(
			"GOLD", // name
			"", // description
			GOLD_CURRENCY_ITEM_ID); // item id
		
		public static VirtualCurrency CRYSTAL_CURRENCY = new VirtualCurrency(
			"CRYSTAL", // name
			"", // description
			CRYSTAL_CURRENCY_ITEM_ID); // item id
		
		
		
		/** Virtual Currency Packs **/
		
		
		public static VirtualCurrencyPack SMALL_CRYSTAL_PACK_PACK = new VirtualCurrencyPack(
			"Small Crystal Pack", // name
			"Small Crystal Pack", // description
			SMALL_CRYSTAL_PACK_PACK_ITEM_ID, // item id
			100, // number of currencies in the pack
			CRYSTAL_CURRENCY_ITEM_ID, // the associated currency
			new PurchaseWithMarket(new MarketItem(SMALL_CRYSTAL_PACK_PRODUCT_ID, MarketItem.Consumable.CONSUMABLE, 0.99))
			);
		
		public static VirtualCurrencyPack MEDIUM_CRYSTAL_PACK_PACK = new VirtualCurrencyPack(
			"Medium Crystal Pack", // name
			"Medium Crystal Pack", // description
			MEDIUM_CRYSTAL_PACK_PACK_ITEM_ID, // item id
			1000, // number of currencies in the pack
			CRYSTAL_CURRENCY_ITEM_ID, // the associated currency
			new PurchaseWithMarket(new MarketItem(MEDIUM_CRYSTAL_PACK_PRODUCT_ID, MarketItem.Consumable.CONSUMABLE, 4.99))
			);
		
		public static VirtualCurrencyPack BIG_CRYSTAL_PACK_PACK = new VirtualCurrencyPack(
			"Big Crystal Pack", // name
			"Big Crystal Pack", // description
			BIG_CRYSTAL_PACK_PACK_ITEM_ID, // item id
			5000, // number of currencies in the pack
			CRYSTAL_CURRENCY_ITEM_ID, // the associated currency
			new PurchaseWithMarket(new MarketItem(BIG_CRYSTAL_PACK_PRODUCT_ID, MarketItem.Consumable.CONSUMABLE, 14.99))
			);
		
		
		
		/** Virtual Goods **/
		
		/* SingleUseVGs */
		
		public static VirtualGood BAT_OF_PAIN_GOOD = new SingleUseVG(
			"Bat of Pain", // name
			"Bring pain to enemies with its high damage capability.", // description
			BAT_OF_PAIN_GOOD_ITEM_ID, // item id
			new PurchaseWithVirtualItem(GOLD_CURRENCY_ITEM_ID, 500)
			); // the way this virtual good is purchased
		
		/* LifetimeVGs */
		
		public static VirtualGood LIFE_DRAIN_GOOD = new LifetimeVG(
			"Life Drain", // name
			"Drains hp from enemy to you every turn for several turns.", // description
			LIFE_DRAIN_GOOD_ITEM_ID, // item id
			new PurchaseWithVirtualItem(CRYSTAL_CURRENCY_ITEM_ID, 100)
			); // the way this virtual good is purchased
		
		/* EquippableVGs */
		
		/* SingleUsePackVGs */
		
		/* UpgradeVGs */
		
		
		
		/** Virtual Categories **/
		
		
		public static VirtualCategory ITEMS_CATEGORY = new VirtualCategory (
			"Items", // name
			new List<string>(
			new string[] { BAT_OF_PAIN_GOOD_ITEM_ID }
		)
			);
		
		public static VirtualCategory SPELLS_CATEGORY = new VirtualCategory (
			"Spells", // name
			new List<string>(
			new string[] { LIFE_DRAIN_GOOD_ITEM_ID }
		)
			);
		
		public static VirtualCategory COMBOS_CATEGORY = new VirtualCategory (
			"Combos", // name
			new List<string>(
			new string[] {  }
		)
			);
		
		public static VirtualCategory POWERS_CATEGORY = new VirtualCategory (
			"Powers", // name
			new List<string>(
			new string[] {  }
		)
			);
		
		
		
		/** Non Consumable Items **/
		
		
	}
	
}