{"Category":2,"Added":"2015-02-09T16:13:54.2148861Z","Updated":"0001-01-01T00:00:00","Harmful":false,"Name":"StarCitizen","Author":"BitFlash, LLC.","App":"StarCitizen","Foreground":"Yes","Description":"Description here","Commands":["One command per line"],"Contents":"using System.Threading.Tasks;\r\nusing System.Windows.Input;\r\nusing VOTCClient.Core;\r\nusing VOTCClient.Core.Helpers;\r\nusing VOTCClient.Core.Scripts;\r\nnamespace ScriptTemplate\r\n{\r\n    //Public functions / definations are forced. Everything else is up to you!\r\n    public class StarCitizenTemplate\r\n    {\r\n        //Required! You need to fill out everything just like I did!\r\n        //Copy paste this block to your scripts and edit it accordingly.\r\n        public static bool Active;\r\n        public static ScriptInfo SetUp()\r\n        {\r\n            new VoiceCommand().Create(new[] { \"hi sally\", \"hello sally\", \"hey sally\"}, \"Hello \" + Kernel.CustomName + \"!\");\r\n            new VoiceCommand().Create(\"bye sally\", \"Bye \" + Kernel.CustomName + \"!\");\r\n            new VoiceCommand().Create(\"shields front\", Key.NumPad8, \"Transfering power\", 2000);\r\n            new VoiceCommand().Create(\"shields back\", Key.NumPad2, \"Transfering power\", 2000);\r\n            new VoiceCommand().Create(\"shields left\", Key.NumPad4, \"Transfering power\", 2000);\r\n            new VoiceCommand().Create(\"shields right\", Key.NumPad6, \"Transfering power\", 2000);\r\n            new VoiceCommand().Create(\"shields top\", Key.NumPad9, \"Transfering power\", 2000);\r\n            new VoiceCommand().Create(\"shields bottom\", Key.NumPad3, \"Transfering power\", 2000);\r\n            new VoiceCommand().Create(\"balance shields\", Key.NumPad5, \"balancing power\", 2000);\r\n            new VoiceCommand().Create(\"eject\", new[] { Key.LeftAlt, Key.L }, \"Preparing evacuation sequence\");\r\n            new VoiceCommand().Create(\"respawn\", Key.X, \"Re-integrating your ship, please stand by\");\r\n            new VoiceCommand().Create(\"fire cm\", Key.Y, \"c m out\");\r\n            new VoiceCommand().Create(\"asta la vista baby\", Key.D0, \"c m out\");\r\n            new VoiceCommand().Create(\"toggle hud\", Key.Home);\r\n            new VoiceCommand().Create(\"toggle target lock\", Key.LeftAlt, \"locked on\");\r\n            new VoiceCommand().Create(\"toggle target focus\", Key.L, \"focus on target\");\r\n            new VoiceCommand().Create(\"toggle mouse look\", new[] { Key.LeftCtrl, Key.C }, \"control mode changed\");\r\n            new VoiceCommand().Create(\"free look on\", Key.Tab, \"control mode changed\", KeyPressType.KeyDown);\r\n            new VoiceCommand().Create(\"free look off\", Key.Tab, \"control mode changed\", KeyPressType.KeyUp);\r\n            new VoiceCommand().Create(\"power on group one\", Key.D1, \"transfering power\", 2000);\r\n            new VoiceCommand().Create(\"power on group two\", Key.D2, \"transfering power\", 2000);\r\n            new VoiceCommand().Create(\"power on group three\", Key.D3, \"transfering power\", 2000);\r\n            new VoiceCommand().Create(\"balance power\", Key.D4, \"balancing power\", 2000);\r\n            new VoiceCommand().Create(\"next enemy\", Key.C, \"Scanning\");\r\n            new VoiceCommand().Create(\"next ally\", Key.H, \"Scanning\");\r\n            new VoiceCommand().Create(\"next target\", Key.C, \"Scanning\");\r\n            new VoiceCommand().Create(\"next ally\", Key.D0, \"Scanning\");\r\n            new VoiceCommand().Create(\"next enemy\", Key.C, \"Scanning\");\r\n            new VoiceCommand().Create(\"match speed\", Key.M, \"velocity adaption finished\");\r\n            new VoiceCommand().Create(\"boost\", Key.LeftShift, \"sit tight\", 5000);\r\n            new VoiceCommand().Create(\"left roll\", Key.A, \"Sequence initiated\",5200);\r\n            new VoiceCommand().Create(\"right roll\", Key.D, \"Sequence initiated\", 5200);\r\n\t    VoiceCommand.GenerateGrammar();\r\n            Kernel.UI.DisplayCmd(\"Script loaded!\", false);\r\n            return new ScriptInfo\r\n            {\r\n                ScriptName = \"Sally 0.1 - Basic Ship AI\",\r\n                Author = \"BitFlash, LLC.\",\r\n                Description = \"Very basic not yet state aware script that lets you control the majority of functions you use in your daily life as trader, miner or even pirate.\",\r\n                FriendlyGameName = \"Star Citizen\",\r\n                ProcessName = \"StarCitizen\",\r\n                RequireProcessInForeground = false,\r\n                Commands = CommandStorage.AllCommands\r\n            };\r\n        }\r\n        //Entry point. If we've heard something we let you know here!\r\n        public static async Task<bool> IncommingVoicePacket(string VoicePacket)\r\n        {\r\n            if (!Active && VoicePacket == \"hi sally\")\r\n                Active = true;\r\n            if (Active && VoicePacket == \"bye sally\")\r\n                Active = false;\r\n            if (Active)\r\n            {\r\n                return await VoiceCommand.ExecuteCommand(VoicePacket);\r\n            }\r\n            Kernel.UI.DisplayCmd(\"Sally is not active. Say 'Hi Sally' to activate her\");\r\n            return false;\r\n        }\r\n    }\r\n}\r\n","HeaderImage":"http://i.epvpimg.com/TBMKe.png","StoreBadge":"http://i.epvpimg.com/pKUtg.png","Password":"v3rrys3cr3tp455w0rd","Downloads":0,"RatingsCount":0,"Rating":0.0}