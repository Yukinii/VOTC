﻿using System;
using System.Speech.Recognition;
using System.Threading.Tasks;
using System.Windows.Threading;
using VOTCClient.Core.External.Chatbot;

namespace VOTCClient.Core.Speech
{
    internal static class SpeechPacketHandler
    {
        internal static async Task<bool> Process(string input)
        {
            try
            {
                if ((input.ToLower() != "unlock" || !Kernel.Locked) && Kernel.Locked)
                    return false;

                if (Kernel.Locked)
                {
                    Kernel.Locked = false;
                    TextToSpeech.Speak("Unlocked.");
                    return true;
                }
                if (input.ToLower().Contains("set volume to"))
                {

                }
                if (ExecuteInternalCommand(input))
                    return true;

                if (Kernel.ChatBotActive)
                    TextToSpeech.Speak(await Bot.TalkTo(input));

                if (Kernel.ScriptInfos.RequireProcessInForeground && Kernel.IsInGame(Kernel.ScriptInfos.ProcessName) || !Kernel.ScriptInfos.RequireProcessInForeground)
                    return await Kernel.UserScriptEngine.Execute(input);

                Kernel.UI.DisplayCmd("Command ignored. Application not in foreground! (" + Kernel.ScriptInfos.ProcessName + ")", false);

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        private static bool ExecuteInternalCommand(string input)
        {

            switch (input.ToLower())
            {

                case "lock":
                {
                    Kernel.Locked = true;
                    TextToSpeech.Speak("Locked");
                    return true;
                }
                case "clear":
                {
                    Kernel.UI.Dispatcher.BeginInvoke(new Action(() => Kernel.UI.ListBox.Items.Clear()), DispatcherPriority.Background);
                    break;
                }
                case "activate push to talk":
                {
                    Kernel.UsePtt = true;
                    Kernel.KeyboardHook.Hook();
                    InternalSpeechRecognizer.Engine.RecognizeAsyncStop();
                    return true;
                }
                case "deactivate push to talk":
                {
                    InternalSpeechRecognizer.Engine.RecognizeAsyncStop();
                    Kernel.UsePtt = false;
                    Kernel.KeyboardHook.Unhook();
                    InternalSpeechRecognizer.Engine.RecognizeAsync(RecognizeMode.Multiple);
                    return true;
                }
                case "play music":
                {
                    TextToSpeech.Speak("Loading your playlist, this could take a few seconds.");
                    Kernel.Playlist.SongsInPlaylist = Kernel.Music;
                    if (Kernel.Playlist.SongsInPlaylist.Count > 1)
                        Kernel.Playlist.Play(Kernel.Random.Next(1, Kernel.Playlist.SongsInPlaylist.Count));
                    else
                        Kernel.UI.DisplayCmd("You need more songs in your My Music directory.");
                    return true;
                }
                case "delete track":
                {
                    if (Kernel.EnableDeleteMusicVoiceCommand)
                    {
                        Kernel.Playlist.Pause();
                        Kernel.Playlist.DeleteSong();
                        Kernel.Playlist.NextSong();
                    }
                    break;
                }
                case "pause music":
                    {
                        Kernel.Playlist.Pause();
                        break;
                    }
                case "resume music":
                    {
                        Kernel.Playlist.Resume();
                        break;
                    }
                case "turn down volume":
                {
                    TextToSpeech.Speak("Turn down for what?");
                    if (Kernel.Playlist.Volume - 10 > 0)
                        Kernel.Playlist.Volume -= 10;
                    else
                    {
                        Kernel.Playlist.Volume = 0;
                    }
                    return true;
                }
                case "turn up volume":
                {
                    if (Kernel.Playlist.Volume + 10 < 100)
                        Kernel.Playlist.Volume += 10;
                    else
                    {
                        Kernel.Playlist.Volume = 100;
                    }
                    return true;
                }
                case "next track":
                {
                    if (Kernel.Playlist.SongsInPlaylist.Count > 1)
                        Kernel.Playlist.Play(Kernel.Random.Next(1, Kernel.Playlist.SongsInPlaylist.Count));
                    else
                        Kernel.UI.DisplayCmd("You need more songs in your My Music directory.");
                    return true;
                }
                case "stop music":
                {
                    Kernel.Playlist.Stop();
                    return true;
                }
                case "i accept lord gaben into my wallet":
                {
                    System.Diagnostics.Process.Start("http://gaben.tv/");
                    return true;
                }
            }
            return false;
        }
    }
}