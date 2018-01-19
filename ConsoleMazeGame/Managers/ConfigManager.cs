using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DIY4UMazeGame.Managers
{
    class ConfigManager
    {
        public static int CLEAR = 0;
        public static int GOLD_ITEM = 1;
        public static int SILVER_ITEM = GOLD_ITEM << 1;
        public static int WOODEN_SWORD = GOLD_ITEM << 2;
        public static int IRON_SWORD = GOLD_ITEM << 3;
        public static int WOODEN_ARMOUR = GOLD_ITEM << 4;
        public static int IRON_ARMOUR = GOLD_ITEM << 5;
        public static int CHEST = GOLD_ITEM << 6;
        public static int CHEST_KEY = GOLD_ITEM << 7;
        public static int HEALTH_POTION = GOLD_ITEM << 8;
        public static int MONSTER = GOLD_ITEM << 9;
        public static int GLOBIN = GOLD_ITEM << 10;
        public static int PLAYER = GOLD_ITEM << 11;

        /* Public members */
        public static bool SoundOn = false;
        public static string DefaultSound = @"Resources\PowerUp.wav";
        public static string LogoFile = @"Resources\DIY4ULogo.txt";
        public static string GameOverFile = @"Resources\GameOverLogo.txt";

        public static string WorldMapFile = @"Resources\SampleWorldMap1.txt";
        public static string RoomInfoFile = @"Resources\WorldRoomInfo.txt";

        public static int NumOfMsg = 0;
        public static string[] MsgResouorce;

        public static char PlayerIcon = '■';
        public static char MonsterIcon = 'Ê';
        public static char GlobinIcon = 'Ê';
        public static char GoldIcon = 'G';
        public static char KeyIcon = 'K';
        public static char ChestIcon = 'C';
        public static char SilverIcon = 'C';
        public static char IronArmourIcon = 'K';
        public static char WoddenArmourIcon = 'C';
        public static char IronSwordIcon = 'K';
        public static char WoddenSwordIcon = 'C';
        public static char HealthPotionIcon = 'C';
        
        public static bool StartWithSword = false;
        public static bool StartWithArmour = false;

        public static char[] IconList = new char[(1 << 11) + 1]; //this of course is a waste of memory
        public static ConsoleColor[] IconColorList = new ConsoleColor[((1 << 11) + 1)]; ////this of course is a waste of memory


        public static int MaxEnemyPerRoom = 0;
        public static int MaxMonsterPerRoom = 0;
        public static int MaxGlobinPerRoom = 0;
        public static int MaxHealthPotionPerRoom = 0;

        public static int MaxGoldItemPerRoom = 0;
        public static int MaxSilverItemPerRoom = 0;
        public static int MaxKeyPerWorld = 0;
        public static int MaxChestPerWorld = 0;
        public static int MaxIronArmourPerWorld = 0;
        public static int MaxWoodenArmourPerWorld = 0;
        public static int MaxIronSwordPerWorld = 0;
        public static int MaxWoodenSwordPerWorld = 0;

        public static int ItemNotAvailableTime = 8000;

        public static string AppVersion = "";
        public static string AppTitle  = "";
        public static string Author = "";

        public static Random RandomGen= new Random();   //System Wide Generator 

        /* Private members */
        private static bool isInit = false;

        /*Key Constants */
        private const string SoundOnKey = "SoundOn";
        private const string DefaultSoundKey = "DefaultSound";

        private const string StartWithSwordKey = "StartWithSword";
        private const string StartWithArmourKey = "StartWithArmour";

        private const string LogoFileKey = "LogoFile";
        private const string GameOverFileKey = "GameOverFile";

        private const string WorldMapFileKey = "WorldMapFile";
        private const string RoomInfoFileKey = "RoomInfoFileKey";

        private const string NumOfMsgKey = "NumOfMsg";
        private const string MsgKey = "Msg";

        private const string PlayerIconKey = "PlayerIcon";
        private const string MonsterIconKey = "MonsterIcon";
        private const string GlobinIconKey = "GlobinIcon";
        private const string GoldIconKey = "GoldIcon";
        private const string SilverIconKey = "SilverIcon";
        private const string KeyIconKey = "KeyIcon";
        private const string ChestIconKey = "ChestIcon";
        private const string IronArmourIconKey = "IronArmourIcon";
        private const string WoodenArmourIconKey = "WoodenArmourIcon";
        private const string IronSwordIconKey = "IronSwordIcon";
        private const string WoodenSwordIconKey = "WoodenSwordIcon";
        private const string HealthPotionIconKey = "HealthPotionIcon";

        private const string PlayerColorKey = "PlayerColor";
        private const string MonsterColorKey = "MonsterColor";
        private const string GlobinColorKey = "GlobinColor";
        private const string GoldIColorKey = "GoldColor";
        private const string SilverColorKey = "SilverColor";
        private const string KeyColorKey = "KeyColor";
        private const string ChestColorKey = "ChestColor";
        private const string IronArmourColorKey = "IronArmourColor";
        private const string WoodenArmourColorKey = "WoodenArmourColor";
        private const string IronSwordColorKey = "IronSwordColor";
        private const string WoddenSwordColorKey = "WoodenSwordColor";
        private const string HealthPotionColorKey = "HealthPotionColor";

        private const string ItemNotAvailableTimeKey = "ItemNotAvailableTime";
        
        public static ConsoleColor PlayerColor = ConsoleColor.Red;
        public static ConsoleColor MonsterColor = ConsoleColor.Red;
        public static ConsoleColor GlobinColor = ConsoleColor.Red;
        public static ConsoleColor GoldColor = ConsoleColor.Red;
        public static ConsoleColor SilverColor = ConsoleColor.Red;
        public static ConsoleColor KeyColor = ConsoleColor.Red;
        public static ConsoleColor ChestColor = ConsoleColor.Red;
        public static ConsoleColor IronArmourColor = ConsoleColor.Red;
        public static ConsoleColor WoodenArmourColor = ConsoleColor.Red;
        public static ConsoleColor IronSwordColor = ConsoleColor.Red;
        public static ConsoleColor WoddenSwordColor = ConsoleColor.Red;
        public static ConsoleColor HealthPotionColor = ConsoleColor.Red;


        private const string MaxEnemyPerRoomKey = "MaxEnemyPerRoom";
        private const string MaxMonsterPerRoomKey = "MaxMonsterPerRoom";
        private const string MaxGlobinPerRoomKey = "MaxGlobinPerRoom";
        private const string MaxHealthPotionPerRoomKey = "MaxHealthPotionPerRoom";

        private const string MaxGoldItemPerRoomKey = "MaxGoldItemPerRoom";
        private const string MaxSilverItemPerRoomKey = "MaxSilverItemPerRoom";
        private const string MaxKeyPerWorldKey = "MaxKeyPerWorld";
        private const string MaxChestPerWorldKey = "MaxChestPerWorld";

        private const string MaxIronArmourPerWorldKey = "MaxIronArmourPerWorld";
        private const string MaxWoodenArmourPerWorldKey = "MaxWoodenArmourPerWorld";

        private const string MaxIronSwordrPerWorldKey = "MaxIronSwordrPerWorld";
        private const string MaxWoodenSwordPerWorldKey = "MaxWoodenSwordPerWorld";

        

        /* Class is privte and hence it cannot be instantiated */
        private ConfigManager()
        { }

        static ConfigManager()
        {
            AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AppTitle = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

        static string GetString(string key)
        {
            String tmpstr = null;
            String msg = null;
            try
            {
                tmpstr = ConfigurationManager.AppSettings[key].ToString();
                
            }
            catch (Exception ex)
            {
                msg = ex.Message;
               
            }

            return tmpstr;
        }

        static int GetIntVal(string key)
        {
            String tmpstr = null;
            int outintVal;
            String msg = null;
            try
            {
                tmpstr = ConfigurationManager.AppSettings[key].ToString();
                int.TryParse(tmpstr, out outintVal);
                return outintVal;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return 0;
        }

        public static void Init()
        {
            string tmpstr = null;
            

            if (!isInit)
            {
                   
                //SoundOnKey
                tmpstr = GetString(SoundOnKey);
                if (tmpstr != null && tmpstr.Contains("1"))
                    SoundOn = true;
                else
                    SoundOn = false;

                //DefaultSoundKey
                tmpstr = GetString(DefaultSoundKey);
                if (tmpstr != null)
                    DefaultSound = tmpstr;

                //LogoFileKey
                tmpstr = GetString(LogoFileKey);
                if (tmpstr != null)
                    LogoFile = tmpstr;

                tmpstr = GetString(WorldMapFileKey);
                if (tmpstr != null)
                    WorldMapFile = tmpstr;

                tmpstr = GetString(StartWithSwordKey);
                if (tmpstr != null && tmpstr.Contains("1"))
                    StartWithSword = true;

                tmpstr = GetString(StartWithArmourKey);
                if (tmpstr != null && tmpstr.Contains("1"))
                    StartWithArmour = true;


                tmpstr = GetString(GameOverFileKey);
                if (tmpstr != null)
                    GameOverFile = tmpstr;


                tmpstr = GetString(RoomInfoFileKey);
                if (tmpstr != null)
                    RoomInfoFile = tmpstr;


                tmpstr = GetString(PlayerIconKey);
                if (tmpstr != null)
                {
                    PlayerIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.PLAYER] = PlayerIcon;
                }

                tmpstr = GetString(MonsterIconKey);
                if (tmpstr != null)
                {
                    MonsterIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.MONSTER] = MonsterIcon;
                }

                tmpstr = GetString(GoldIconKey);
                if (tmpstr != null)
                {
                    GoldIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.GOLD_ITEM] = GoldIcon;
                }

                tmpstr = GetString(SilverIconKey);
                if (tmpstr != null)
                {
                    SilverIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.SILVER_ITEM] = SilverIcon;
                }

                tmpstr = GetString(GlobinIconKey);
                if (tmpstr != null)
                {
                    GlobinIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.GLOBIN] = GlobinIcon;
                }

                tmpstr = GetString(KeyIconKey);
                if (tmpstr != null)
                {
                    KeyIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.CHEST_KEY] = KeyIcon;
                }

                tmpstr = GetString(ChestIconKey);
                if (tmpstr != null)
                {
                    ChestIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.CHEST] = ChestIcon;
                }

                tmpstr = GetString(IronArmourIconKey);
                if (tmpstr != null)
                {
                    IronArmourIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.IRON_ARMOUR] = IronArmourIcon;
                }

                tmpstr = GetString(WoodenArmourIconKey);
                if (tmpstr != null)
                {
                    WoddenArmourIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.WOODEN_ARMOUR] = WoddenArmourIcon;
                }

                tmpstr = GetString(IronSwordIconKey);
                if (tmpstr != null)
                {
                    IronSwordIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.IRON_SWORD] = IronSwordIcon;
                }

                tmpstr = GetString(WoodenSwordIconKey);
                if (tmpstr != null)
                {
                    WoddenSwordIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.WOODEN_SWORD] = WoddenSwordIcon;
                }

                tmpstr = GetString(HealthPotionIconKey);
                if (tmpstr != null)
                {
                    HealthPotionIcon = Convert.ToChar(tmpstr.Substring(0));
                    IconList[ConfigManager.HEALTH_POTION] = HealthPotionIcon;
                }

                //Color
                tmpstr = GetString(PlayerColorKey);
                if (tmpstr != null)
                {
                    PlayerColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.PLAYER] = PlayerColor;
                }

                tmpstr = GetString(MonsterColorKey);
                if (tmpstr != null)
                {
                    MonsterColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.MONSTER] = MonsterColor;
                }

                tmpstr = GetString(GlobinColorKey);
                if (tmpstr != null)
                {
                    GlobinColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.GLOBIN] = GlobinColor;
                }

                tmpstr = GetString(GoldIColorKey);
                if (tmpstr != null)
                {
                    GoldColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.GOLD_ITEM] = GoldColor;
                }


                tmpstr = GetString(SilverColorKey);
                if (tmpstr != null)
                {
                    SilverColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.SILVER_ITEM] = SilverColor;
                }


                tmpstr = GetString(KeyColorKey);
                if (tmpstr != null)
                {
                    KeyColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.CHEST_KEY] = KeyColor;
                }
                
                tmpstr = GetString(ChestColorKey);
                if (tmpstr != null)
                {
                    ChestColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.CHEST] = ChestColor;
                }

                tmpstr = GetString(IronArmourColorKey);
                if (tmpstr != null)
                {
                    IronArmourColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.IRON_ARMOUR] = IronArmourColor;
                }

                tmpstr = GetString(WoodenArmourColorKey);
                if (tmpstr != null)
                {
                    WoodenArmourColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.WOODEN_ARMOUR] = WoodenArmourColor;

                }

                tmpstr = GetString(IronSwordColorKey);
                if (tmpstr != null)
                {
                    IronSwordColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.IRON_SWORD] = IronSwordColor;
                }

                tmpstr = GetString(WoddenSwordColorKey);
                if (tmpstr != null)
                {
                    WoddenSwordColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.WOODEN_SWORD] = WoddenSwordColor;
                }

                tmpstr = GetString(HealthPotionColorKey);
                if (tmpstr != null)
                {
                    HealthPotionColor = ScreenManager.GetConsoleColor(tmpstr);
                    IconColorList[ConfigManager.HEALTH_POTION] = HealthPotionColor;
                }


                MaxEnemyPerRoom = GetIntVal(MaxEnemyPerRoomKey);
                MaxMonsterPerRoom = GetIntVal(MaxMonsterPerRoomKey);
                MaxGlobinPerRoom = GetIntVal(MaxGlobinPerRoomKey);
                MaxHealthPotionPerRoom = GetIntVal(MaxHealthPotionPerRoomKey);

                MaxGoldItemPerRoom = GetIntVal(MaxGoldItemPerRoomKey);
                MaxSilverItemPerRoom = GetIntVal(MaxSilverItemPerRoomKey);

                MaxKeyPerWorld = GetIntVal(MaxKeyPerWorldKey);
                MaxChestPerWorld = GetIntVal(MaxChestPerWorldKey);
                MaxIronArmourPerWorld = GetIntVal(MaxIronArmourPerWorldKey);
                MaxWoodenArmourPerWorld = GetIntVal(MaxWoodenArmourPerWorldKey);
                MaxIronSwordPerWorld = GetIntVal(MaxWoodenSwordPerWorldKey);
                MaxWoodenSwordPerWorld = GetIntVal(MaxWoodenSwordPerWorldKey);

                ItemNotAvailableTime = GetIntVal(ItemNotAvailableTimeKey);

                //Load Text Resources 
               /* try
                {
                    tmpstr = ConfigurationManager.AppSettings[NumOfMsgKey].ToString();
                    int.TryParse(tmpstr, out outintVal);
                    NumOfMsg = outintVal;
                }
                catch (Exception e5)
                {
                    e5 = null;
                    String msg = e5.Message;
                    //LogoFile = null;
                }
                */

                NumOfMsg = GetIntVal(NumOfMsgKey);

                if (NumOfMsg > 0)
                {
                    MsgResouorce = new string[NumOfMsg];
                    for (int row = 0; row < NumOfMsg; row++)
                    {
                        /*
                        try
                        {
                            tmpstr = ConfigurationManager.AppSettings[MsgKey + row.ToString()].ToString();
                            MsgResouorce[row] = tmpstr;
                        }
                        catch (Exception e5)
                        {
                            e5 = null;
                            String msg = e5.Message;
                            //LogoFile = null;
                        }*/

                        tmpstr = GetString(MsgKey + row.ToString());
                        if (tmpstr != null)
                        {
                            MsgResouorce[row] = tmpstr;
                        }
                    }


                }

            }
        }

        public static string GetStringResource(int msgno)
        {
            if (msgno > 0 && msgno <= NumOfMsg)
                return MsgResouorce[msgno];
            else
                return "Not Fouud.";
        }

        public static void Shutdown()
        {

        }

    }
}
