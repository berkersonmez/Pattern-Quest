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

namespace Soomla.Implementation {
	public class Store : IStoreAssets{ // Make sure to change to your actual class name

		public int GetVersion() {
			return 0;
		}

		public VirtualCurrency[] GetCurrencies() {
	        return  new VirtualCurrency[] {
	            CRYSTAL_CURRENCY
	        };
		}

	    public VirtualGood[] GetGoods() {
	        return new VirtualGood[] {
	        };
		}

	    public VirtualCurrencyPack[] GetCurrencyPacks() {
	    	List<StoreCurrencyPack> packs = XmlParse.instance.getStoreCurrencyPacks();
			VirtualCurrencyPack[] vcps = new VirtualCurrencyPack[packs.Count];
	    	for (int i = 0; i < packs.Count; i++) {
				vcps[i] = new VirtualCurrencyPack(packs[i].name, packs[i].description, packs[i].itemID, packs[i].size, "currency_" + packs[i].type, 
				                                  new PurchaseWithMarket(new MarketItem(packs[i].productID, MarketItem.Consumable.CONSUMABLE, 0)));
	    	}
			return vcps;
		}

	    public VirtualCategory[] GetCategories() {
	        return new VirtualCategory[]{
	            GENERAL_CATEGORY
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
        
        
        
		public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory (
                "General", // name
                new List<string>(
                    new string[] {  }
                    )
                );


        /** Non Consumable Items **/
        

	}

}