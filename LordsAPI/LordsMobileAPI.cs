﻿using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LordsAPI
{
    public class LordsMobileAPI
    {
        public class Settings
        {
            public static Process GetProcess()
            {
                return Process.GetProcessesByName("Lords Mobile").FirstOrDefault(); // Game process
            }
            public class Resolution
            {
                public static bool Change(Size size)
                {
                    MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                    Binarysharp.MemoryManagement.Windows.RemoteWindow window = sharp.Windows.MainWindow;
                    window.Width = size.Width;
                    window.Height = size.Height;
                    sharp.Dispose();
                    return true;
                }
                public static async Task<bool> ChangeAsync(Size size)
                {
                    return await Task.Run(() => Change(size));
                }
                public static Size Get()
                {
                    MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                    Binarysharp.MemoryManagement.Windows.RemoteWindow window = sharp.Windows.MainWindow;
                    sharp.Dispose();
                    return new Size(window.Width, window.Height);
                }
                public static async Task<Size> GetAsync()
                {
                    return await Task.Run(() => Get());
                }
            }
        }
        public class Actions
        {
            public class MysteryBox
            {
                public static bool ClickMysteryBox(LordsMobileAPI.UserInfo.Location location)
                {
                    if (location.type == UserInfo.Location.Locations.Castle)
                    {
                        Utils utils = new Utils();
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                        Binarysharp.MemoryManagement.Windows.RemoteWindow window = sharp.Windows.MainWindow;
                        window.Mouse.MoveTo(game.Width - 175, game.Height - 225);
                        window.Mouse.ClickLeft();
                        sharp.Dispose();
                        return true;
                    }
                    else return false;
                }
                public static bool Colect()
                {
                    Utils utils = new Utils();
                    Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                    MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                    Binarysharp.MemoryManagement.Windows.RemoteWindow window = sharp.Windows.MainWindow;
                    window.Mouse.MoveTo(game.Width - 830, game.Height - 225);
                    window.Mouse.ClickLeft();
                    sharp.Dispose();
                    return true;
                }
                public static async Task<bool> ClickMysteryBoxAsync(LordsMobileAPI.UserInfo.Location location)
                {
                    return await Task.Run(() => ClickMysteryBox(location));
                }
                public static async Task<bool> ColectAsync()
                {
                    return await Task.Run(() => Colect());
                }
            }
            public class MerchantShip
            {
                public enum BuySlots
                {
                    Slot1,
                    Slot2,
                    Slot3,
                    Slot4,
                }
                public static bool Buy(LordsMobileAPI.UserInfo.Location location, BuySlots slot)
                {
                    if (location.type == UserInfo.Location.Locations.MerchantShip)
                    {
                        Utils utils = new Utils();
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                        Binarysharp.MemoryManagement.Windows.RemoteWindow window = sharp.Windows.MainWindow;
                        if (slot == BuySlots.Slot1)
                            window.Mouse.MoveTo(game.Width - (450 + 713), game.Height - 365);
                        else if (slot == BuySlots.Slot2)
                            window.Mouse.MoveTo(game.Width - 450, game.Height - 365);
                        else if (slot == BuySlots.Slot3)
                            window.Mouse.MoveTo(game.Width - (450 + 713), game.Height - (365 - 250));
                        else if (slot == BuySlots.Slot4)
                            window.Mouse.MoveTo(game.Width - 450, game.Height - (365 - 250));
                        window.Mouse.ClickLeft();
                        sharp.Dispose();
                        return true;
                    }
                    else return false;
                }
                public static async Task<bool> BuyAsync(LordsMobileAPI.UserInfo.Location location, BuySlots slot)
                {
                    return await Task.Run(() => Buy(location, slot));
                }
            }
        }

        public class UserInfo
        {
            public class Location
            {
                public string name;
                public Locations type;
                public enum Locations
                {
                    Unknown,

                    Castle,
                    Map,
                    MerchantShip,
                    Coliseum,
                    Menagerie,
                    SacredTower,
                    Aviary,
                    Academy,
                    Barracks,
                    Quests,
                    Guild,
                    GuildShop,
                }
                public static Location GetLocation()
                {
                    Utils utils = new Utils();
                    Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                    game.Save("game.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    Bitmap find = utils.ConvertImagePixelType((Bitmap)Bitmap.FromFile("game\\gomap.png"));
                    List<Rectangle> done = utils.Find(game, find);
                    if (done.Count >= 1)
                    {
                        return new Location { name = Locations.Castle.ToString(), type = Locations.Castle };
                    }
                    else
                    {
                        Bitmap game2 = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        game2.Save("game.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Bitmap find2 = utils.ConvertImagePixelType((Bitmap)Bitmap.FromFile("game\\gocastle.png"));
                        List<Rectangle> done2 = utils.Find(game2, find2);
                        if (done2.Count >= 1)
                        {
                            return new Location { name = Locations.Map.ToString(), type = Locations.Map };
                        }
                        else
                        {
                            Bitmap game3 = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                            game3.Save("game.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            Bitmap find3 = utils.ConvertImagePixelType((Bitmap)Bitmap.FromFile("game\\BarracksMenuDetect.png"));
                            List<Rectangle> done3 = utils.Find(game3, find3);
                            if (done3.Count >= 1)
                            {
                                return new Location { name = Locations.Barracks.ToString(), type = Locations.Barracks };
                            }
                            else
                            {
                                Bitmap game4 = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                                game4.Save("game.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                Bitmap find4 = utils.ConvertImagePixelType((Bitmap)Bitmap.FromFile("game\\SacredTowerMenuDetect.png"));
                                List<Rectangle> done4 = utils.Find(game4, find4);
                                if (done4.Count >= 1)
                                {
                                    return new Location { name = Locations.SacredTower.ToString(), type = Locations.SacredTower };
                                }
                                else
                                {
                                    Bitmap game5 = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                                    game5.Save("game.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                    Bitmap find5 = utils.ConvertImagePixelType((Bitmap)Bitmap.FromFile("game\\QuestsMenuDetect.png"));
                                    List<Rectangle> done5 = utils.Find(game5, find5);
                                    if (done5.Count >= 1)
                                    {
                                        return new Location { name = Locations.Quests.ToString(), type = Locations.Quests };
                                    }
                                    else
                                    {
                                        Bitmap game6 = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                                        game6.Save("game.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                        Bitmap find6 = utils.ConvertImagePixelType((Bitmap)Bitmap.FromFile("game\\MerchantShipMenuDetect.png"));
                                        List<Rectangle> done6 = utils.Find(game6, find6);
                                        if (done6.Count >= 1)
                                        {
                                            return new Location { name = Locations.MerchantShip.ToString(), type = Locations.MerchantShip };
                                        }
                                        else
                                        {
                                            return new Location { name = Locations.Unknown.ToString(), type = Locations.Unknown };
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                public static async Task<Location> GetLocationAsync()
                {
                    return await Task.Run(() => GetLocation());
                }
            }

            public class Resources
            {
                private static readonly int Width = 160;
                private static readonly int X = 99;
                private static readonly Tesseract.EngineMode mode = Tesseract.EngineMode.Default;
                private static string Replacer(string text)
                {
                    return text.Replace("|", "").Replace(" ", "").Replace(Environment.NewLine, "").Trim();
                }
                public class Food
                {
                    public static string GetCount()
                    {
                        MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                        Utils utils = new Utils();
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        var window = sharp.Windows.MainWindow;
                        //window.Mouse.MoveTo(game.Width - 170, game.Height - 893);
                        Bitmap crop = utils.Crop(game, new Rectangle(game.Width - Width, game.Height - 893, X, 30));
                        crop.ImageToGray();
                        crop.Save("Food.png", System.Drawing.Imaging.ImageFormat.Png);
                        string power = utils.GetTextFromImage(crop, mode);
                        crop.Dispose();
                        sharp.Dispose();
                        game.Dispose();
                        return Replacer(power);
                    }
                    public static async Task<string> GetCountAsync()
                    {
                        return await Task.Run(() => GetCount());
                    }
                }
                public class Stone
                {
                    public static string GetCount()
                    {
                        MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                        Utils utils = new Utils();
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        var window = sharp.Windows.MainWindow;
                        //window.Mouse.MoveTo(game.Width - 170, game.Height - 893 + 50);
                        Bitmap crop = utils.Crop(game, new Rectangle(game.Width - Width, game.Height - 893 + 50, X, 30));
                        crop.ImageToGray();
                        crop.Save("Stone.png", System.Drawing.Imaging.ImageFormat.Png);
                        string power = utils.GetTextFromImage(crop, mode);
                        crop.Dispose();
                        sharp.Dispose();
                        game.Dispose();
                        return Replacer(power);
                    }
                    public static async Task<string> GetCountAsync()
                    {
                        return await Task.Run(() => GetCount());
                    }
                }
                public class Wood
                {
                    public static string GetCount()
                    {
                        MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                        Utils utils = new Utils();
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        var window = sharp.Windows.MainWindow;
                        //window.Mouse.MoveTo(game.Width - 170, game.Height - 893 + 50 + 50);
                        Bitmap crop = utils.Crop(game, new Rectangle(game.Width - Width, game.Height - 893 + 50 + 50, X, 30));
                        crop.ImageToGray();
                        crop.Save("Wood.png", System.Drawing.Imaging.ImageFormat.Png);
                        string power = utils.GetTextFromImage(crop, mode);
                        crop.Dispose();
                        sharp.Dispose();
                        game.Dispose();
                        return Replacer(power);
                    }
                    public static async Task<string> GetCountAsync()
                    {
                        return await Task.Run(() => GetCount());
                    }

                }
                public class Ore
                {
                    public static string GetCount()
                    {
                        MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                        Utils utils = new Utils();
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        var window = sharp.Windows.MainWindow;
                        //window.Mouse.MoveTo(game.Width - 170, game.Height - 893 + 50 + 50 + 50);
                        Bitmap crop = utils.Crop(game, new Rectangle(game.Width - Width, game.Height - 893 + 50 + 50 + 50, X, 30));
                        crop.ImageToGray();
                        crop.Save("Ore.png", System.Drawing.Imaging.ImageFormat.Png);
                        string power = utils.GetTextFromImage(crop, mode);
                        crop.Dispose();
                        sharp.Dispose();
                        game.Dispose();
                        return Replacer(power);
                    }
                    public static async Task<string> GetCountAsync()
                    {
                        return await Task.Run(() => GetCount());
                    }
                }
                public class Gold
                {
                    public static string GetCount()
                    {
                        MemorySharp sharp = new MemorySharp(LordsMobileAPI.Settings.GetProcess());
                        Utils utils = new Utils();
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        var window = sharp.Windows.MainWindow;
                        //window.Mouse.MoveTo(game.Width - 170, game.Height - 893 + 50 + 50 + 50 + 50);
                        Bitmap crop = utils.Crop(game, new Rectangle(game.Width - Width, game.Height - 893 + 50 + 50 + 50 + 50, X, 30));
                        crop.ImageToGray();
                        crop.Save("Gold.png", System.Drawing.Imaging.ImageFormat.Png);
                        string power = utils.GetTextFromImage(crop, mode);
                        crop.Dispose();
                        sharp.Dispose();
                        game.Dispose();
                        return Replacer(power);
                    }
                    public static async Task<string> GetCountAsync()
                    {
                        return await Task.Run(() => GetCount());
                    }
                }
            }
            public class Statistic
            {
                private static readonly Tesseract.EngineMode mode = Tesseract.EngineMode.Default;
                private static string Replacer(string text)
                {
                    if (text.StartsWith(".") || text.StartsWith(",") || text.StartsWith("\\"))
                        return text.Remove(0, 1).Replace("Mt ", "").Replace("‘", "").Replace("(", "").Replace(" ", "").Replace(Environment.NewLine, "").Trim();
                    else return text.Replace("Mt ", "").Replace("‘", "").Replace("(", "").Replace(" ", "").Replace(Environment.NewLine, "").Trim();
                }
                public static async Task<string> GetInfomationAsync(Statistics stat)
                {
                    return await Task.Run(() => GetInfomation(stat));
                }
                public static string GetInfomation(Statistics stat)
                {
                    Utils utils = new Utils();
                    if (stat == Statistics.Power)
                    {
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        Bitmap crop = utils.Crop(game, new Rectangle(game.Width - 1480, game.Height - 892, 200, 30));
                        string power = utils.GetTextFromImage(crop, mode);
                        return Replacer(power);
                    }
                    else if (stat == Statistics.Gems)
                    {
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        // Get the window
                        Bitmap find = (Bitmap)Bitmap.FromFile(@"game\\power.png");
                        find.Save("find.png", System.Drawing.Imaging.ImageFormat.Png);
                        List<Rectangle> done = utils.Find(game, find, 0.7);
                        if (done.Count >= 1)
                        {
                            Bitmap crop = utils.Crop(game, new Rectangle(done[0].X + 155, done[0].Y + 50, 150, 30));
                            crop.Save("Gems.png", System.Drawing.Imaging.ImageFormat.Png);
                            string power = utils.GetTextFromImage(crop, mode);
                            return Replacer(power);
                        }
                        else return string.Empty;
                    }
                    else if (stat == Statistics.Level)
                    {
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));
                        // Get the window
                        Bitmap find = (Bitmap)Bitmap.FromFile(@"game\\power.png");
                        find.Save("find.png", System.Drawing.Imaging.ImageFormat.Png);
                        List<Rectangle> done = utils.Find(game, find, 0.7);
                        if (done.Count >= 1)
                        {
                            //window.Mouse.MoveTo(done[0].X + 10, done[0].Y + 156);
                            Bitmap crop = utils.Crop(game, new Rectangle(done[0].X + 10, done[0].Y + 156, 33, 25));
                            crop.Save("Level.png", System.Drawing.Imaging.ImageFormat.Png);
                            string power = utils.GetTextFromImage(crop, mode);
                            return utils.TruncateLongString(power, 2, "");
                        }
                        else return string.Empty;
                    }
                    else if (stat == Statistics.MysteryBox)
                    {
                        Bitmap game = utils.ConvertImagePixelType(utils.GetProgrammImage(LordsMobileAPI.Settings.GetProcess()));

                        Bitmap crop = utils.Crop(game, new Rectangle(game.Width - 235, game.Height - 185, 100, 30));
                        crop.Save("Crop.png", System.Drawing.Imaging.ImageFormat.Png);
                        string power = utils.GetTextFromImage(crop, mode);
                        power = power.Replace("_", "");
                        string power2 = utils.TruncateLongString(power, 7, "");
                        power = power.Replace("L", "");
                        if (power2 == "3a6paTs" && power.Contains("3a6paTs"))
                        {
                            return "Done";
                        }
                        else if (power.Contains(":"))
                        {
                            power2 = utils.TruncateLongString(power, 5, "");
                            return power2;
                        }
                        else return string.Empty;
                    }
                    else return string.Empty;
                }
                public enum Statistics
                {
                    Power,
                    Gems,
                    Level,
                    MysteryBox,
                }
            }
        }
    }
}
