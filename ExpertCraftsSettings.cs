using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ff14bot.Helpers;

namespace NavigationTest.ExpertCrafts
{
    public class ExpertCraftsSettings : JsonSettings
    {
        private static ExpertCraftsSettings _settings;
        
        private ExpertMode _expertMode;

        private List<SkybuildersBuyItem> _itemsToBuy;
        
        public static List<SkybuildersBuyItem> defaultList = new List<SkybuildersBuyItem>()
        {
            new SkybuildersBuyItem(0, 28612, 3, 1800),
            new SkybuildersBuyItem(0, 30111, 3, 1800),
            new SkybuildersBuyItem(0, 30112, 3, 1800),
            new SkybuildersBuyItem(0, 31401, 3, 1800),
            new SkybuildersBuyItem(0, 32826, 3, 1800),
            new SkybuildersBuyItem(0, 32829, 3, 1800),
            new SkybuildersBuyItem(0, 30110, 3, 900),
            new SkybuildersBuyItem(0, 28615, 3, 1800),
            new SkybuildersBuyItem(0, 30113, 3, 1800),
            new SkybuildersBuyItem(0, 31406, 3, 1800),
            new SkybuildersBuyItem(1, 28592, 1, 2200),  // Craftsmans Coverall Top
            new SkybuildersBuyItem(1, 28594, 1, 2200),  // Craftsmans Singlet
            new SkybuildersBuyItem(1, 30052, 1, 2200),  // Craftsmans Apron
            new SkybuildersBuyItem(1, 28593, 1, 2000),  // Craftsmans Coverall Bottoms
            new SkybuildersBuyItem(1, 30053, 1, 2000),  // Craftsmans Leather Trousers
            new SkybuildersBuyItem(1, 30054, 1, 1200),  // Craftsmans Leather Shoes
            new SkybuildersBuyItem(1, 32793, 1, 1200),  // Skyworkers Helmet
            new SkybuildersBuyItem(1, 32794, 1, 2200),  // Skyworkers Singlet
            new SkybuildersBuyItem(1, 32795, 1, 1200),  // Skyworkers Gloves
            new SkybuildersBuyItem(1, 32796, 1, 2000),  // Skyworkers Bottom
            new SkybuildersBuyItem(1, 32797, 1, 1200),  // Skyworkers Boots
        };
        public static ExpertCraftsSettings Instance => _settings ?? (_settings = new ExpertCraftsSettings());

        public ExpertCraftsSettings() : base(Path.Combine(CharacterSettingsDirectory, "ExpertCraftsSettings.json")) 
        {

        }
        
        public ExpertMode Mode
        {
            get => _expertMode;
            set
            {
                if (_expertMode != value)
                {
                    _expertMode = value;
                    Save();
                }
            }
        }
        
        public enum ExpertMode
        {
            Gatherer = 0,
            Crafter = 1,
        } 
        
        [Description("List of items to buy with Scrips.")]
        [Browsable(false)]
        public List<SkybuildersBuyItem> ItemsToBuy
        {
            get => _itemsToBuy ?? defaultList;
            set
            {
                if (_itemsToBuy != value)
                {
                    _itemsToBuy = value;
                    Save();
                }
            }
        }
    }


}