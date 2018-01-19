using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine
{
    using DIY4UMazeGame.Managers;
    using DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject;
    using DIY4UMazeGame.GameEngine.GameInfo;
    using DIY4UMazeGame.GameScreenLib.Screens;
    using DIY4UMazeGame.GameEngine.GameInfo.Items;
    using DIY4UMazeGame.GameEngine.GameInfo.Roles;
    using DIY4UMazeGame.Utilities;

    public class GameItemHandler
    {
        public static bool HandleKey(Object item)
        {
            ConsoleKeyInfo keyInfo;
            bool toProcessing = true;
            bool toPickup = false;

            GameRoom gm;
         
            ScreenManager.WaitForBufferClear();  //Clear Buffer

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);

            //************************************
            PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(21) + ((Item)item).descriptions + "\r\n" +
                ConfigManager.GetStringResource(22) + "\r\n" + ConfigManager.GetStringResource(23));
            PlayUIBottomItem.UpdateInstruction(
            PlayUIBottomItem.YES_INSTRUNCTION | PlayUIBottomItem.NO_INSTRUNCTION);
            PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
            //************************************

            while (toProcessing)
            {
                keyInfo = Console.ReadKey(true);

                

                switch (Char.ToUpper(keyInfo.KeyChar))
                {
                    case 'N':
                        toProcessing = false;
                        //****************************************
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(
                            PlayUIBottomItem.UP_INSTRUNCTION ^
                            PlayUIBottomItem.DOWN_INSTRUNCTION ^
                            PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                            PlayUIBottomItem.LEFT_INSTRUNCTION
                        );
                        //****************************************
                        ((IAvailable)item).SetNotAvailable(ConfigManager.ItemNotAvailableTime);
                        break;

                    case 'Y':
                        toProcessing = false;
                        toPickup = true;
                        gm.ItemsHashTable.Remove(((Item)item).InstanceKey); //Remove from room
                        GameState.inventory.Add((Item)item);
                        ((ITakable)item).Take();

                        //****************************************************
                        PlayUILeftPanel.UpdateKeyItem(GameState.CounKeyInInventory());
                        PlayUILeftPanel.UpdateTotalItem(GameState.TotalItemInInventory());                      
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(
                           PlayUIBottomItem.UP_INSTRUNCTION ^
                           PlayUIBottomItem.DOWN_INSTRUNCTION ^
                           PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                           PlayUIBottomItem.LEFT_INSTRUNCTION);

                        PlayUIRightPanel.PaintLegendOption(GameState.currentRoom);
                        //****************************************************
                        break;
                }

            }

            return toPickup;
        }


        public static bool HandleSword(Object item)
        {
            ConsoleKeyInfo keyInfo;
            bool toProcessing = true;
            bool toPickup = false;
            GameRoom gm;

            ScreenManager.WaitForBufferClear();  //Clear Buffer

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);

            if (GameState.CountSwordInInventory() > 0)
            {
                //************************************
                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(21) + ((Item)item).descriptions + "\r\n" +
                ConfigManager.GetStringResource(26) + GameState.GetFirstSwordInInventory().descriptions + "?\r\n" +
                ConfigManager.GetStringResource(28) + ((IWeapon)item).capableHit + "%" + "\r\n" +
                ConfigManager.GetStringResource(23));
                
                PlayUIBottomItem.UpdateInstruction(
                PlayUIBottomItem.YES_INSTRUNCTION | PlayUIBottomItem.NO_INSTRUNCTION);
                PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
                //***********************************
            }
            else
            {
                //************************************
                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(21) + ((Item)item).descriptions + "\r\n" +
                ConfigManager.GetStringResource(28) + ((IWeapon)item).capableHit + "%" + "\r\n" +
                ConfigManager.GetStringResource(22) + "\r\n" + ConfigManager.GetStringResource(23));

                PlayUIBottomItem.UpdateInstruction(
                PlayUIBottomItem.YES_INSTRUNCTION | PlayUIBottomItem.NO_INSTRUNCTION);
                PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
                //***********************************
            }

            while (toProcessing)
            {
                keyInfo = Console.ReadKey(true);

                switch (Char.ToUpper(keyInfo.KeyChar))
                {
                    case 'N':
                        toProcessing = false;
                        //***************************
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(
                            PlayUIBottomItem.UP_INSTRUNCTION ^
                            PlayUIBottomItem.DOWN_INSTRUNCTION ^
                            PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                            PlayUIBottomItem.LEFT_INSTRUNCTION
                        );
                        //******************************
                        ((IAvailable)item).SetNotAvailable(ConfigManager.ItemNotAvailableTime);
                        break;

                    case 'Y':
                        toProcessing = false;
                        toPickup = true;
                        gm.ItemsHashTable.Remove(((Item)item).InstanceKey); //Remove from room
                        GameState.RemoveSwordFromInventory();
                        GameState.inventory.Add((Item)item);
                        ((ITakable)item).Take();
                        //*************************************
                        PlayUITopItem.UpdateWeaponType(((Item)item).descriptions);
                        PlayUILeftPanel.UpdateSwordItem(GameState.CountSwordInInventory());
                        PlayUILeftPanel.UpdateWeaponTypeVal(((Item)item).descriptions);
                        PlayUILeftPanel.UpdateWeaponrPowerLevel(
                              Utility.getPercentageVal(((IWeapon)item).capableHit,400));
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(
                           PlayUIBottomItem.UP_INSTRUNCTION ^
                           PlayUIBottomItem.DOWN_INSTRUNCTION ^
                           PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                           PlayUIBottomItem.LEFT_INSTRUNCTION
                        );

                        PlayUIRightPanel.PaintLegendOption(GameState.currentRoom);
                        //***************************************
                        break;
                }
            }

            return toPickup;
        }

        public static bool HandleArmour(Object item)
        {
            ConsoleKeyInfo keyInfo;
            bool toProcessing = true;
            bool toPickup = false;

            GameRoom gm;

            ScreenManager.WaitForBufferClear();  //Clear Buffer

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);


            if (GameState.CountSwordInInventory() > 0)
            {
                //************************************
                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(21) + ((Item)item).descriptions + "\r\n" +
                ConfigManager.GetStringResource(26) + GameState.GetFirstSwordInInventory().descriptions + "?\r\n" +
                ConfigManager.GetStringResource(27) + ((IShield)item).shieldPower +"%" + "\r\n" +
                ConfigManager.GetStringResource(23));

                PlayUIBottomItem.UpdateInstruction(
                    PlayUIBottomItem.YES_INSTRUNCTION | PlayUIBottomItem.NO_INSTRUNCTION);
                PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
                //***********************************
            }
            else
            {
                //************************************
                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(21) + ((Item)item).descriptions + "\r\n" +
                ConfigManager.GetStringResource(22) + "\r\n" + ConfigManager.GetStringResource(27) + ((IShield)item).shieldPower + "%" + "\r\n" +
                ConfigManager.GetStringResource(23));

                PlayUIBottomItem.UpdateInstruction(
                   PlayUIBottomItem.YES_INSTRUNCTION | PlayUIBottomItem.NO_INSTRUNCTION);
                PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
                //***********************************
            }

            while (toProcessing)
            {
                keyInfo = Console.ReadKey(true);

                switch (Char.ToUpper(keyInfo.KeyChar))
                {
                    case 'N':
                        toProcessing = false;
                        //***************************
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(
                            PlayUIBottomItem.UP_INSTRUNCTION ^
                            PlayUIBottomItem.DOWN_INSTRUNCTION ^
                            PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                            PlayUIBottomItem.LEFT_INSTRUNCTION
                        );
                        //******************************
                        ((IAvailable)item).SetNotAvailable(ConfigManager.ItemNotAvailableTime);
                        break;

                    case 'Y':
                        toProcessing = false;
                        toPickup = true;
                        gm.ItemsHashTable.Remove(((Item)item).InstanceKey); //Remove from room
                        GameState.RemoveArmourFromInventory();
                        GameState.inventory.Add((Item)item);
                        ((ITakable)item).Take();
                        //*************************************
                        PlayUITopItem.UpdateArmourType(((Item)item).descriptions);
                        PlayUILeftPanel.UpdateArmourItem(GameState.CountArmourInInventory());
                        PlayUILeftPanel.UpdateArmourTypeVal(((Item)item).descriptions);
                        PlayUILeftPanel.UpdateArmourPowerLevel(((IShield)item).shieldPower);
                       
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(
                           PlayUIBottomItem.UP_INSTRUNCTION ^
                           PlayUIBottomItem.DOWN_INSTRUNCTION ^
                           PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                           PlayUIBottomItem.LEFT_INSTRUNCTION
                        );

                        PlayUIRightPanel.PaintLegendOption(GameState.currentRoom);
                        //***************************************
                        break;
                }
            }

            return toPickup;
        }

        public static bool HandleChest(Object item)
        {
            ConsoleKeyInfo keyInfo;
            bool toProcessing = true;
            bool toProcessing1 = true;
            bool toPickup = false;
            GameWorldInfo gwinfo;
            GameRoom gm;

            ScreenManager.WaitForBufferClear();  //Clear Buffer

            gm = GameDataFactory.curr_gwinfo.GetGameRoombyNumber(GameState.currentRoom);
            gwinfo = GameDataFactory.curr_gwinfo;


            if (GameState.CounKeyInInventory() <= 0)
            {
                //*********************************************************
                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(21) + ((Item)item).descriptions + "\r\n" +
                    ConfigManager.GetStringResource(32));
                PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.CONTINUE_INSTRUNCTION);
                PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
                //********************************************************

                while (toProcessing)
                {
                    keyInfo = Console.ReadKey(true);

                    switch (Char.ToUpper(keyInfo.KeyChar))
                    {
                        case 'C':
                            toProcessing = false;
                            PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                            PlayUIBottomItem.UpdateMenuOption(
                               PlayUIBottomItem.UP_INSTRUNCTION ^
                               PlayUIBottomItem.DOWN_INSTRUNCTION ^
                               PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                               PlayUIBottomItem.LEFT_INSTRUNCTION
                           );
                            ((IAvailable)item).SetNotAvailable(ConfigManager.ItemNotAvailableTime);
                            return toPickup;
                           
                    }
                }
            }
            else
            {
                PlayUIBottomItem.updateMessageBox(ConfigManager.GetStringResource(21) + ((Item)item).descriptions + "\r\n" +
                    ConfigManager.GetStringResource(35));
                PlayUIBottomItem.UpdateInstruction(
                 PlayUIBottomItem.YES_INSTRUNCTION | PlayUIBottomItem.NO_INSTRUNCTION);
                PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);
            }


            while (toProcessing)
            {
                keyInfo = Console.ReadKey(true);

                switch (Char.ToUpper(keyInfo.KeyChar))
                {
                    case 'N':
                        toProcessing = false;
                        //***************************
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(
                            PlayUIBottomItem.UP_INSTRUNCTION ^
                            PlayUIBottomItem.DOWN_INSTRUNCTION ^
                            PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                            PlayUIBottomItem.LEFT_INSTRUNCTION
                        );
                        //******************************
                        ((IAvailable)item).SetNotAvailable(ConfigManager.ItemNotAvailableTime);
                        break;

                    case 'Y':
                        toProcessing = false;
                        toPickup = true;
                        gm.ItemsHashTable.Remove(((Item)item).InstanceKey); //Remove from room                                          
                        ((ITakable)item).Take();

                        //Pocess item inside treasure chest
                        foreach(Item itemobj in ((Chest)item).Goldlist.itemList)
                        {
                            gwinfo.GetGameRoombyNumber(GameState.currentRoom).TotalGoldItem--;
                            gwinfo.GetGameRoombyNumber(GameState.currentRoom).TotalItem--;
                            GameState.inventory.Add(itemobj);

                        }

                        GameState.UpdateScore();

                        foreach (Item itemobj in ((Chest)item).Silverlist.itemList)
                        {
                            gwinfo.GetGameRoombyNumber(GameState.currentRoom).TotalSilverItem--;
                            gwinfo.GetGameRoombyNumber(GameState.currentRoom).TotalItem--;
                            GameState.inventory.Add(itemobj);
                        }

                        GameState.UpdateScore();

                        foreach (Item itemobj in ((Chest)item).HealthPotionlist.itemList)
                        {
                            gwinfo.GetGameRoombyNumber(GameState.currentRoom).TotalHealthPotionItem--;
                            gwinfo.GetGameRoombyNumber(GameState.currentRoom).TotalItem--;
                            gwinfo.UpdatePlyerHealth(((HealthPotion)itemobj).lifeLine);
                            GameState.inventory.Add(itemobj);
                        }

                        //Remove 1 Key 
                        GameState.RemoveKeyFromInventory();

                        //************************************************************
                        PlayUIBottomItem.updateMessageBox(
                                   ConfigManager.GetStringResource(37) + ((Item)item).descriptions + "\r\n" +
                                   ConfigManager.GetStringResource(29) + ((Chest)item).Goldlist.GetCount() + "\n" +
                                   ConfigManager.GetStringResource(30) + ((Chest)item).Silverlist.GetCount() + "\n" +
                                   ConfigManager.GetStringResource(31) + ((Chest)item).HealthPotionlist.GetCount());
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.CONTINUE_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(PlayUIBottomItem.CLEAR);

                        PlayUILeftPanel.UpdateSilverItem(GameState.CountSilverInInventory());
                        PlayUILeftPanel.UpdateGoldItem(GameState.CountGoldInInventory());
                        PlayUILeftPanel.UpdateHealthPotiontem(GameState.CountHealthPotionInInventory());
                        PlayUILeftPanel.UpdateKeyItem(GameState.CounKeyInInventory());
                        PlayUILeftPanel.UpdateTotalItem(GameState.TotalItemInInventory());

                        PlayUITopItem.UpdateHealthPotion(GameState.CountHealthPotionInInventory());                       
                        PlayUITopItem.UpdateHealthLevel(gwinfo.The_Player.GetHealthLevel());
                        PlayUITopItem.UpdateSilverLevel(GameState.CountSilverInInventory());
                        PlayUITopItem.UpdateGoldLevel(GameState.CountGoldInInventory());
                      
                        PlayUIRightPanel.UpdateHealthLevel(gwinfo.The_Player.GetHealthLevel());

                        PlayUIRightPanel.PaintLegendOption(GameState.currentRoom);
                        //*************************************************************

                        while (toProcessing1)
                        {
                            keyInfo = Console.ReadKey(true);

                            switch (Char.ToUpper(keyInfo.KeyChar))
                            {
                                case 'C':
                                    toProcessing1 = false;
                                    break;

                            }
                        }

                        //*********************************************************
                        PlayUIBottomItem.UpdateInstruction(PlayUIBottomItem.QUIT_INSTRUNCTION);
                        PlayUIBottomItem.UpdateMenuOption(
                           PlayUIBottomItem.UP_INSTRUNCTION ^
                           PlayUIBottomItem.DOWN_INSTRUNCTION ^
                           PlayUIBottomItem.RIGHT_INSTRUNCTION ^
                           PlayUIBottomItem.LEFT_INSTRUNCTION
                        );
                        //****************************************************************


                        break;
                }
            }

            return toPickup;
        }

     }
}
