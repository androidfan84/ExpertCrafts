//!CompilerOption:optimize:On

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Buddy.Coroutines;
using Clio.Utilities;
using ff14bot;
using ff14bot.AClasses;
using ff14bot.Behavior;
using ff14bot.Enums;
using ff14bot.Managers;
using ff14bot.Navigation;
using ff14bot.NeoProfiles;
using ff14bot.Objects;
using ff14bot.Pathing.Service_Navigation;
using ff14bot.RemoteWindows;
using LlamaLibrary.Extensions;
using LlamaLibrary.Helpers;
using LlamaLibrary.Logging;
using LlamaLibrary.Memory;
using LlamaLibrary.RemoteAgents;
using LlamaLibrary.RemoteWindows;
using Newtonsoft.Json;
using TreeSharp;

namespace NavigationTest.ExpertCrafts
{
    public class SkybuildersBuyItem
    {
        [JsonIgnore]
        public string Name => DataManager.GetItem(item).CurrentLocaleName;
        public int selectString { get; set; }
        public uint item { get; set; }
        public int qty { get; set; }
        public int price { get; set; }
        
        public SkybuildersBuyItem(int selectString, uint item, int qty, int price)
        {
            this.selectString = selectString;
            this.item = item;
            this.qty = qty;
            this.price = price;
        }
    }
    
    public class ExpertCrafts : BotBase
    {
        private static readonly string _name = "ExpertCrafts";
        private static readonly LLogger Log = new LLogger(_name, Colors.Pink);
        private static DateTime startTime;
        private static long StartingScoreCombined;
        private static long CurrentScoreCombined;
        /*private static readonly List<(int selectString, uint item, int qty, int price)> thingsToBuy = new List<(int selectString, uint item, int qty, int price)>()
        {
            (0, 28612, 3, 1800),
            (0, 30111, 3, 1800),
            (0, 30112, 3, 1800),
            (0, 31401, 3, 1800),
            (0, 32826, 3, 1800),
            (0, 32829, 3, 1800),
            (0, 30110, 3, 900),
            (0, 28615, 3, 1800),
            (0, 30113, 3, 1800),
            (0, 31406, 3, 1800),
            (1, 28592, 1, 2200),
            (1, 28594, 1, 2200),
            (1, 32794, 1, 2200),
            (2, 25196, 50, 240),
            (2, 25194, 50, 240),
            (2, 25193, 50, 240),
            (2, 25192, 50, 240),
            (2, 25191, 50, 240)
        };*/
        
        private static Dictionary<string, ClassJobType> jobEnums = new Dictionary<string, ClassJobType>()
        {
            {"carpenter" , ClassJobType.Carpenter},
            {"blacksmith" , ClassJobType.Blacksmith},
            {"armorer" , ClassJobType.Armorer},
            {"goldsmith" , ClassJobType.Goldsmith},
            {"leatherworker" , ClassJobType.Leatherworker},
            {"weaver" , ClassJobType.Weaver},
            {"alchemist" , ClassJobType.Alchemist},
            {"culinarian" , ClassJobType.Culinarian},
            {"miner" , ClassJobType.Miner},
            {"botanist" , ClassJobType.Botanist},
            {"fisher" , ClassJobType.Fisher}
        };
        
        private static Dictionary<ClassJobType, int> startingScores = new Dictionary<ClassJobType, int>()
        {
            {ClassJobType.Carpenter,0},
            {ClassJobType.Blacksmith,0},
            {ClassJobType.Armorer,0},
            {ClassJobType.Goldsmith,0},
            {ClassJobType.Leatherworker,0},
            {ClassJobType.Weaver,0},
            {ClassJobType.Alchemist,0},
            {ClassJobType.Culinarian,0},
            {ClassJobType.Miner,0},
            {ClassJobType.Botanist,0},
            {ClassJobType.Fisher,0}
        };
        
        private static Dictionary<ClassJobType, int> scores = new Dictionary<ClassJobType, int>()
        {
            {ClassJobType.Carpenter,0},
            {ClassJobType.Blacksmith,0},
            {ClassJobType.Armorer,0},
            {ClassJobType.Goldsmith,0},
            {ClassJobType.Leatherworker,0},
            {ClassJobType.Weaver,0},
            {ClassJobType.Alchemist,0},
            {ClassJobType.Culinarian,0},
            {ClassJobType.Miner,0},
            {ClassJobType.Botanist,0},
            {ClassJobType.Fisher,0}
        };

        private static readonly uint[] items513 = {31953, 31954, 31955, 31956, 31957, 31958, 31959, 31960};
        
        
        public static string Crp = @"[{""Group"":1,""Item"":31953,""Amount"":20,""Enabled"":true,""Type"":""Carpenter""}]";
        public static string Bsm = @"[{""Group"":1,""Item"":31954,""Amount"":20,""Enabled"":true,""Type"":""Blacksmith""}]";
        public static string Arm = @"[{""Group"":1,""Item"":31955,""Amount"":20,""Enabled"":true,""Type"":""Armorer""}]";
        public static string Gsm = @"[{""Group"":1,""Item"":31956,""Amount"":20,""Enabled"":true,""Type"":""Goldsmith""}]";
        public static string Ltw = @"[{""Group"":1,""Item"":31957,""Amount"":20,""Enabled"":true,""Type"":""Leatherworker""}]";
        public static string Wvr = @"[{""Group"":1,""Item"":31958,""Amount"":20,""Enabled"":true,""Type"":""Weaver""}]";
        public static string Alc = @"[{""Group"":1,""Item"":31959,""Amount"":20,""Enabled"":true,""Type"":""Alchemist""}]";
        public static string Cul = @"[{""Group"":1,""Item"":31960,""Amount"":20,""Enabled"":true,""Type"":""Culinarian""}]";
        
        private static Dictionary<ClassJobType, string> orders = new Dictionary<ClassJobType, string>()
        {
            {ClassJobType.Carpenter,Crp},
            {ClassJobType.Blacksmith,Bsm},
            {ClassJobType.Armorer,Arm},
            {ClassJobType.Goldsmith,Gsm},
            {ClassJobType.Leatherworker,Ltw},
            {ClassJobType.Weaver,Wvr},
            {ClassJobType.Alchemist,Alc},
            {ClassJobType.Culinarian,Cul},
        };

        private static readonly string foodOrder =
            @"[{""Group"":1,""Item"":28724,""Amount"":200,""Enabled"":true,""Type"":""Exchange"",""AmountMode"":""Restock""},{""Id"":1,""Group"":1,""Item"":30482,""Amount"":90,""Enabled"":true,""Type"":""Culinarian"",""Hq"":true,""AmountMode"":""Restock""},{""Id"":2,""Group"":1,""Item"":27959,""Amount"":90,""Enabled"":true,""Type"":""Alchemist"",""Hq"":true,""AmountMode"":""Restock""},{""Id"":3,""Group"":1,""Item"":17837,""Amount"":999,""Enabled"":true,""Type"":""Purchase"",""AmountMode"":""Restock""}]";

        private Composite _root;

        public override string Name => _name;

        public override PulseFlags PulseFlags { get; } = PulseFlags.All;

        public override Composite Root => _root;
        
        private ExpertCraftsSettingsForm _form;
        public Version Version => new Version(0, 1);
        
        public override bool WantButton { get; } = true;
        
        public override void OnButtonPress()
        {
            if (_form == null)
            {
                _form = new ExpertCraftsSettingsForm()
                {
                    Text = "ExpertCraftsSettings" + Version,
                };
                _form.Closed += (o, e) => { _form = null; };
            }

            try
            {
                _form.Show();
            }
            catch (Exception e)
            {
                // ignored
            }  
        }        

        public override bool IsAutonomous => true;
        public override bool RequiresProfile => false;
        private static GameObject VendorNpc => GameObjectManager.GameObjects.FirstOrDefault(i => i.NpcId == 1031680 && i.IsVisible);
        
        internal void init()
        {
            OffsetManager.Init();
        }

        public override void Start()
        {
            GamelogManager.MessageRecevied += GamelogManagerOnMessageReceived;
            startTime = DateTime.Now;
            _root = new ActionRunCoroutine(r => Run());
        }

        public override void Stop()
        {
            GamelogManager.MessageRecevied -= GamelogManagerOnMessageReceived;
            _root = null;
        }

        private async Task<bool> Run()
        {
            switch (ExpertCraftsSettings.Instance.Mode)
            {
                case ExpertCraftsSettings.ExpertMode.Gatherer:
                    await RunGather();
                    break;
                case ExpertCraftsSettings.ExpertMode.Crafter:
                    await RunCrafting();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        private async Task<bool> RunCrafting()
        {
            
            Navigator.PlayerMover = new SlideMover();
            Navigator.NavigationProvider = new ServiceNavigationProvider();
            
            Log.Information("Running Crafting Task");
            
            await GetAllScores();
            foreach (var score in scores)
            {
                StartingScoreCombined += score.Value;
            }

            CurrentScoreCombined = StartingScoreCombined;
            await DiscardCrap();
            
            foreach (var order in orders)
            {
                while (scores[order.Key] < 500000)
                {
                    Log.Information($"Working on {order.Key} Current Score {scores[order.Key]}");
                    
                //    await CheckFood();
                    Log.Information($"Starting craft order");
                    await Lisbeth.ExecuteOrders(order.Value);
                    Log.Information($"Starting turn-in");
                    await TurnInCrafting();
                    await DiscardCrap();
                    foreach (var scorePair in scores)
                    {
                        Log.Information($"{scorePair.Key} : {scorePair.Value}");
                    }
                    Log.Information($"Score per hour: {(CurrentScoreCombined - StartingScoreCombined)/(DateTime.Now - startTime).TotalHours}");
                }
            }
            

            foreach (var scorePair in scores)
            {
                Log.Information($"{scorePair.Key} : {scorePair.Value}");
            }
            
            TreeRoot.Stop("Stop Requested");
            return true;
        }

        private static async Task CheckFood()
        {
            Log.Information($"Checking Food");
            if (!(ConditionParser.HasAtLeast(28724, 30) && ConditionParser.HqHasAtLeast(30482, 30) && ConditionParser.HqHasAtLeast(27959, 30) && ConditionParser.HasAtLeast(17837, 50)))
            {
                Log.Information($"Ordering Food");
                await Lisbeth.ExecuteOrders(foodOrder);
            }
        }

        private async Task<bool> RunGather()
        {
            Navigator.PlayerMover = new SlideMover();
            Navigator.NavigationProvider = new ServiceNavigationProvider();

            Log.Information("Running Gathering Task");
            
            

            await BuyItems();

            while (LlamaLibrary.ScriptConditions.Helpers.HasIshgardGatheringBotanist())
            {
                await IshgardHandin.HandInGatheringItem(1);
                await Coroutine.Sleep(200);
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            while (LlamaLibrary.ScriptConditions.Helpers.HasIshgardGatheringMining())
            {
                await IshgardHandin.HandInGatheringItem(0);
                await Coroutine.Sleep(200);
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            while (LlamaLibrary.ScriptConditions.Helpers.HasIshgardGatheringFisher())
            {
                await IshgardHandin.HandInGatheringItem(2);
                await Coroutine.Sleep(200);
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            TreeRoot.Stop("Stop Requested");
            return false;
        }

        private static async Task BuyItems()
        {
            if (HWDSupply.Instance.IsOpen)
            {
                HWDSupply.Instance.Close();
                await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
            }
            
            while (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= ExpertCraftsSettings.defaultList.Select(i => i.price).Min())
            {
                if (HWDGathereInspect.Instance.IsOpen)
                    HWDGathereInspect.Instance.Close();

                foreach (var item in ExpertCraftsSettings.defaultList.Where(i => i.price <= LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() && ConditionParser.ItemCount(i.item) < i.qty))
                {
                    if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() < ExpertCraftsSettings.defaultList.Select(i => i.price).Min())
                        break;

                    await IshgardHandin.BuyScripItem(item.item, item.qty - ConditionParser.ItemCount(item.item), item.selectString);
                    await Coroutine.Sleep(500);
                }
            }

            await CombineStacks();
        }

        private static async Task TurnInCrafting()
        {
            await GeneralFunctions.StopBusy(dismount: false);
            
            foreach (var item in InventoryManager.FilledSlots.Where(i => items513.Contains(i.RawItemId) && i.IsCollectable && i.Collectability < 1100))
            {
                item.Discard();
                Log.Information($"Discarding {item.Name} Collectability: {item.Collectability}");
                await Coroutine.Sleep(3000);
            }

            // Grade 4 Artisanal Skybuilders' Ice Box (Carpenter) 
            while (InventoryManager.FilledSlots.Any(i => i.RawItemId == 31953))
            {
                await IshgardHandin.HandInItem(31953, 0, 0, true);
                                if (HWDSupply.Instance.IsOpen)
                {
                    HWDSupply.Instance.Close();
                    await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
                }
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            // Grade 4 Artisanal Skybuilders' Chocobo Weathervane (Blacksmith) 
            while (InventoryManager.FilledSlots.Any(i => i.RawItemId == 31954))
            {
                await IshgardHandin.HandInItem(31954, 0, 1, true);
                                if (HWDSupply.Instance.IsOpen)
                {
                    HWDSupply.Instance.Close();
                    await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
                }
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            // Grade 4 Artisanal Skybuilders' Company Chest (Armorer) 
            while (InventoryManager.FilledSlots.Any(i => i.RawItemId == 31955))
            {
                await IshgardHandin.HandInItem(31955, 0, 2, true);
                                if (HWDSupply.Instance.IsOpen)
                {
                    HWDSupply.Instance.Close();
                    await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
                }
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            // Grade 4 Artisanal Skybuilders' Astroscope (Goldsmith) 
            while (InventoryManager.FilledSlots.Any(i => i.RawItemId == 31956))
            {
                await IshgardHandin.HandInItem(31956, 0, 3, true);
                                if (HWDSupply.Instance.IsOpen)
                {
                    HWDSupply.Instance.Close();
                    await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
                }
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            // Grade 4 Artisanal Skybuilders' Tool Belt (Leatherworker) 
            while (InventoryManager.FilledSlots.Any(i => i.RawItemId == 31957))
            {
                await IshgardHandin.HandInItem(31957, 0, 4, true);
                                if (HWDSupply.Instance.IsOpen)
                {
                    HWDSupply.Instance.Close();
                    await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
                }
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }
            
            // Grade 4 Artisanal Skybuilders' Vest (Weaver) 
            while (InventoryManager.FilledSlots.Any(i => i.RawItemId == 31958))
            {
                await IshgardHandin.HandInItem(31958, 0, 5, true);
                                if (HWDSupply.Instance.IsOpen)
                {
                    HWDSupply.Instance.Close();
                    await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
                }
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            // Grade 4 Artisanal Skybuilders' Tincture (Alchemist) 
            while (InventoryManager.FilledSlots.Any(i => i.RawItemId == 31959))
            {
                await IshgardHandin.HandInItem(31959, 0, 6, true);
                if (HWDSupply.Instance.IsOpen)
                {
                    HWDSupply.Instance.Close();
                    await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
                }
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            // Grade 4 Artisanal Skybuilders' Sorbet (Culinarian) 
            while (InventoryManager.FilledSlots.Any(i => i.RawItemId == 31960))
            {
                await IshgardHandin.HandInItem(31960, 0, 7, true);
                if (HWDSupply.Instance.IsOpen)
                {
                    HWDSupply.Instance.Close();
                    await Coroutine.Wait(2000, () => !HWDSupply.Instance.IsOpen);
                }
                if (LlamaLibrary.ScriptConditions.Helpers.GetSkybuilderScrips() >= 9000)
                    await BuyItems();
            }

            await DiscardCrap();
        }

        private static async Task DiscardCrap()
        {
            foreach (var item in InventoryManager.FilledSlots.Where(i=> i.EnglishName.ToLowerInvariant().Contains("magicked prism") || i.EnglishName.ToLowerInvariant().Contains("carbuncle")))
            {
                Log.Information($"Discarding {item.Name}");
                item.Discard();
                await Coroutine.Sleep(2000);
            }
        }

        private static async Task FindPrices()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var items in ExpertCraftsSettings.Instance.ItemsToBuy.GroupBy(i => i.selectString))
            {
                if (!ShopExchangeCurrency.Open && VendorNpc == null || VendorNpc.Location.Distance(Core.Me.Location) > 5f)
                    await Navigation.GetTo(886, new Vector3(36.33978f, -16f, 145.3877f));

                if (!ShopExchangeCurrency.Open && VendorNpc.Location.Distance(Core.Me.Location) > 4f)
                {
                    await Navigation.OffMeshMove(VendorNpc.Location);
                    await Coroutine.Sleep(500);
                }

                if (!ShopExchangeCurrency.Open)
                {
                    VendorNpc.Interact();
                    await Coroutine.Wait(5000, () => ShopExchangeCurrency.Open || Talk.DialogOpen || Conversation.IsOpen);
                    if (Conversation.IsOpen)
                    {
                        Conversation.SelectLine((uint) items.Key);
                        await Coroutine.Wait(5000, () => ShopExchangeCurrency.Open);
                    }
                }

                var shop = SpecialShopManager.Items;
                foreach (var item in items.Where(i => i.selectString == items.Key))
                {
                    try
                    {
                        var itemTemp = shop.FirstOrDefault(i => i.Item0.Id == item.item);
                        sb.AppendLine($"({item.selectString},{item.item},{item.qty},{itemTemp.CurrencyCosts.First()}),");
                    }
                    catch
                    {
                        Log.Information($"Shit {item.item}");
                    }
                }

                if (ShopExchangeCurrency.Open)
                {
                    ShopExchangeCurrency.Close();
                    await Coroutine.Sleep(2000);
                }

                await Coroutine.Sleep(2000);
            }

            Log.Information("\n" + sb.ToString());
        }

        public static async Task CombineStacks()
        {
            var itemIds = InventoryManager.FilledSlots.Where(slot => slot.Count < slot.Item.StackSize).GroupBy(i => i.TrueItemId).Select(j => j.Key);

            foreach (var itemIdTemp in itemIds)
            {
                var stacks = InventoryManager.FilledSlots.Where(i => i.TrueItemId == itemIdTemp && i.Count < i.Item.StackSize).OrderBy(j => j.Count);
                if (stacks.Any())
                    foreach (var slot in stacks.Skip(1))
                    {
                        if (stacks.Count() <= 1) break;

                        if (stacks.First().Count + slot.Count < stacks.First().Item.StackSize)
                        {
                            slot.Move(stacks.First());
                            await Coroutine.Sleep(500);
                            stacks = InventoryManager.FilledSlots.Where(i => i.TrueItemId == itemIdTemp && i.Count < i.Item.StackSize).OrderBy(j => j.Count);
                        }
                    }
            }
        }

        private static void GamelogManagerOnMessageReceived(object sender, ChatEventArgs e)
        {
            if (WorldManager.ZoneId == 886)
            {
                if (e.ChatLogEntry.MessageType == (MessageType) 2105)
                {
                    var strings = e.ChatLogEntry.Contents.Split(' ');
                    if (strings[1] == "earn")
                    {
                        //You earn 1266 weaver skyward points.
                        scores[jobEnums[strings[3]]] += int.Parse(strings[2]);
                        CurrentScoreCombined += int.Parse(strings[2]);
                    }
                }
            }
        }

        private static async Task GetAllScores()
        {
            if (await Navigation.GetTo(886, new Vector3(-30.35686f, -16f, 169.7765f)))
            {
                var ScoreNpc = GameObjectManager.GameObjects.FirstOrDefault(i => i.NpcId == 1031695 && i.IsVisible);
                if (ScoreNpc is null) return;
                
                if (!HWDScore.Instance.IsOpen && ScoreNpc.Location.Distance(Core.Me.Location) > 4f)
                {
                    await Navigation.OffMeshMove(ScoreNpc.Location);
                    await Coroutine.Sleep(500);
                }

                if (!HWDScore.Instance.IsOpen)
                {
                    ScoreNpc.Interact();
                    await Coroutine.Wait(5000, () => Conversation.IsOpen);
                    if (Conversation.IsOpen)
                    {
                        Conversation.SelectLine(0);
                    }
                    
                    await Coroutine.Wait(5000, () => HWDScore.Instance.IsOpen || Talk.DialogOpen);
                    await Coroutine.Sleep(100);

                    while (Talk.DialogOpen)
                    {
                        Talk.Next();
                        await Coroutine.Wait(5000, () => !Talk.DialogOpen);
                    }

                    await Coroutine.Wait(5000, () => HWDScore.Instance.IsOpen);
                }

                if (HWDScore.Instance.IsOpen)
                {
                    var scoresRead = AgentHWDScore.Instance.ReadTotalScores();
                    for (int i = 0; i < 11; i++)
                    {
                        scores[(ClassJobType) ((byte) ClassJobType.Carpenter + i)] = scoresRead[i];
                        startingScores[(ClassJobType) ((byte) ClassJobType.Carpenter + i)] = scoresRead[i];
                    }
                    HWDScore.Instance.Close();
                    await Coroutine.Wait(5000, () => !HWDScore.Instance.IsOpen);
                    await Coroutine.Sleep(500);
                }
            }
        }
    }
    
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "":   throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:   return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}