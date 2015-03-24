{"Category":5,"Added":"0001-01-01T00:00:00","Updated":"2015-02-09T15:26:41.3517753Z","Harmful":false,"Name":"ARMA3 V2","Author":"BitFlash, LLC.","App":"StarCitizen","Foreground":"Yes","Description":"Description here","Commands":["One command per line"],"Contents":"{\"Category\":2,\"Added\":\"2015-02-09T15:24:46.5374153Z\",\"Updated\":\"0001-01-01T00:00:00\",\"Harmful\":false,\"Name\":\"ARMA3 V2\",\"Author\":\"BitFlash, LLC.\",\"App\":\"StarCitizen\",\"Foreground\":\"Yes\",\"Description\":\"Description here\",\"Commands\":[\"One command per line\"],\"Contents\":\"{\\\"Category\\\":2,\\\"Added\\\":\\\"2015-01-13T17:24:08.2821171Z\\\",\\\"Updated\\\":\\\"0001-01-01T00:00:00\\\",\\\"Harmful\\\":false,\\\"Name\\\":\\\"ARMA3 V2\\\",\\\"Author\\\":\\\"BitFlash, LLC.\\\",\\\"App\\\":\\\"arma3\\\",\\\"Foreground\\\":\\\"Yes\\\",\\\"Description\\\":\\\"This is a Sample Script\\\",\\\"Commands\\\":[\\\"test - echo test using TTS\\\"],\\\"Contents\\\":\\\"using System.Collections.Generic;\\\\r\\\\nusing System.Linq;\\\\r\\\\nusing System.Speech.Recognition;\\\\r\\\\nusing System.Windows.Forms;\\\\r\\\\nusing System.Windows.Input;\\\\r\\\\nusing VOTCClient.Core;\\\\r\\\\nusing VOTCClient.Core.Extensions;\\\\r\\\\nusing VOTCClient.Core.Scripts;\\\\r\\\\nusing VOTCClient.Core.Speech;\\\\r\\\\nusing Keyboard = InputManager.Keyboard;\\\\r\\\\nusing Mouse = InputManager.Mouse;\\\\r\\\\nnamespace ScriptTemplate\\\\r\\\\n{\\\\r\\\\n    public class ARMA3\\\\r\\\\n    {\\\\r\\\\n        public static ScriptInfo SetUp()\\\\r\\\\n        {\\\\r\\\\n            Kernel.UI.DisplayCmd(\\\\\\\"ARMA 3 Script loaded!\\\\\\\", false);\\\\r\\\\n            var Info = new ScriptInfo\\\\r\\\\n            {\\\\r\\\\n                ProcessName = \\\\\\\"arma3\\\\\\\",\\\\r\\\\n                RequireProcessInForeground = true,\\\\r\\\\n            };\\\\r\\\\n            LoadGrammar();\\\\r\\\\n            return Info;\\\\r\\\\n        }\\\\r\\\\n        public static bool IncommingVoicePacket(string voicePacket)\\\\r\\\\n        {\\\\r\\\\n            string[] Cmd = voicePacket.Split(' ');\\\\r\\\\n            return Process(Cmd);\\\\r\\\\n        }\\\\r\\\\n        static bool Process(IEnumerable<string> cmd)\\\\r\\\\n        {\\\\r\\\\n            var Command = cmd.Select(part => part.ToLower()).ToList();\\\\r\\\\n            \\\\r\\\\n            if (Command.ContainsMany(\\\\\\\"toggle\\\\\\\", \\\\\\\"auto hover\\\\\\\") || Command.Contains(\\\\\\\"surface\\\\\\\") || Command.ContainsMany(\\\\\\\"hand\\\\\\\",\\\\\\\"brake\\\\\\\"))\\\\r\\\\n                Keyboard.KeyPress(Key.X.ToKey(), 20);\\\\r\\\\n            else if (Command.Contains(\\\\\\\"dive\\\\\\\"))\\\\r\\\\n                Keyboard.KeyPress(Key.Z.ToKey(),10);\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"toggle\\\\\\\", \\\\\\\"gear\\\\\\\"))\\\\r\\\\n                Keyboard.KeyPress(Key.G.ToKey(), 20);\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"rope\\\\\\\"))\\\\r\\\\n                Keyboard.KeyPress(Key.B.ToKey(), 20);\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"load\\\\\\\", \\\\\\\"assistant\\\\\\\"))\\\\r\\\\n            {\\\\r\\\\n                Key[] Keys = {Key.RightCtrl, Key.B};\\\\r\\\\n                Keyboard.ShortcutKeys(Keys.ToKey(), 100);\\\\r\\\\n            }\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"flaps\\\\\\\", \\\\\\\"up\\\\\\\"))\\\\r\\\\n            {\\\\r\\\\n                Keys[] kk = { Keys.RControlKey, Keys.L };\\\\r\\\\n                Keyboard.ShortcutKeys(kk, 100);\\\\r\\\\n            }\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"flaps\\\\\\\", \\\\\\\"down\\\\\\\"))\\\\r\\\\n            {\\\\r\\\\n                Keys[] kk = { Keys.RControlKey, Keys.K };\\\\r\\\\n                Keyboard.ShortcutKeys(kk, 100);\\\\r\\\\n            }\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"zoom\\\\\\\", \\\\\\\"in\\\\\\\"))\\\\r\\\\n            {\\\\r\\\\n                Mouse.PressButton(Mouse.MouseKeys.Right);\\\\r\\\\n            }\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"zoom\\\\\\\", \\\\\\\"out\\\\\\\"))\\\\r\\\\n            {\\\\r\\\\n                Mouse.PressButton(Mouse.MouseKeys.Right);\\\\r\\\\n            }\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"toggle\\\\\\\", \\\\\\\"gps\\\\\\\"))\\\\r\\\\n            {\\\\r\\\\n                Key[] Keys = { Key.RightCtrl, Key.M };\\\\r\\\\n                Keyboard.ShortcutKeys(Keys.ToKey(), 100);\\\\r\\\\n            }\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"compass\\\\\\\"))\\\\r\\\\n            {\\\\r\\\\n                Keyboard.KeyPress(Key.K.ToKey(), 200);\\\\r\\\\n                Keyboard.KeyPress(Key.K.ToKey(), 200);\\\\r\\\\n            }\\\\r\\\\n            else if (Command.ContainsMany(\\\\\\\"watch\\\\\\\"))\\\\r\\\\n            {\\\\r\\\\n                Keyboard.KeyPress(Key.O.ToKey(), 200);\\\\r\\\\n                Keyboard.KeyPress(Key.O.ToKey(), 200);\\\\r\\\\n            }\\\\r\\\\n            return true;\\\\r\\\\n        }\\\\r\\\\n        static void LoadGrammar()\\\\r\\\\n        {\\\\r\\\\n            foreach (var G in CreateGrammar())\\\\r\\\\n                InternalSpeechRecognizer.LoadGrammar(G);\\\\r\\\\n        }\\\\r\\\\n        static IEnumerable<Grammar> CreateGrammar()\\\\r\\\\n        {\\\\r\\\\n            var GrammarBuilder = new GrammarBuilder(\\\\\\\"toggle\\\\\\\");\\\\r\\\\n            var CommandChoices = new Choices(\\\\\\\"auto hover\\\\\\\", \\\\\\\"gear\\\\\\\", \\\\\\\"gps\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"compass\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"compass\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"watch\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"watch\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"zoom\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"in\\\\\\\", \\\\\\\"out\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"flaps\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"up\\\\\\\", \\\\\\\"down\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"load\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"assistant\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"rope\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"rope\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"dive\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"dive\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"hand\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"break\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n            GrammarBuilder = new GrammarBuilder(\\\\\\\"surface\\\\\\\");\\\\r\\\\n            CommandChoices = new Choices(\\\\\\\"surface\\\\\\\");\\\\r\\\\n            GrammarBuilder.Append(CommandChoices);\\\\r\\\\n            yield return new Grammar(GrammarBuilder);\\\\r\\\\n        }\\\\r\\\\n    }\\\\r\\\\n}\\\\r\\\\n\\\",\\\"HeaderImage\\\":\\\"http://i.epvpimg.com/TBMKe.png\\\",\\\"StoreBadge\\\":\\\"http://i.epvpimg.com/pKUtg.png\\\",\\\"Password\\\":\\\"v3rrys3cr3tp455w0rd\\\",\\\"Downloads\\\":0,\\\"RatingsCount\\\":0,\\\"Rating\\\":0.0}\\r\\n\",\"HeaderImage\":\"http://i.epvpimg.com/TBMKe.png\",\"StoreBadge\":\"http://i.epvpimg.com/pKUtg.png\",\"Password\":\"v3rrys3cr3tp455w0rd\",\"Downloads\":0,\"RatingsCount\":0,\"Rating\":0.0}\r\n","HeaderImage":"http://i.epvpimg.com/TBMKe.png","StoreBadge":"http://i.epvpimg.com/pKUtg.png","Password":"v3rrys3cr3tp455w0rd","Downloads":0,"RatingsCount":0,"Rating":0.0}