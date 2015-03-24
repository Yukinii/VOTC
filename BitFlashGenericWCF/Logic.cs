﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using BitFlashGenericWCF.Core;
using BitFlashGenericWCF.Scripts;

namespace BitFlashGenericWCF
{
    public class Logic : ILogic
    {
        public string TestRemoteExecution(int value,string apiKey) => !API.Validate(apiKey) ? "Access Denied. API Key banned." : string.Format("You entered: {0}", value);

        public string DownloadScript(string scriptName, string apiKey)
        {
            return File.ReadAllText(Path.Combine(Kernel.ScriptLocation, scriptName));
        }

        public string UploadScript(string scriptName, string apiKey)
        {
            var S = new Script();
            return S.ScriptSubmit(scriptName.SplitByLine(), scriptName.Contains("Socket") || scriptName.Contains(".Listen"));
        }

        public string StoreBadge(string scriptName, string apiKey) => Kernel.AllScripts[scriptName].StoreBadge;

        public string StoreHeader(string scriptName, string apiKey) => Kernel.AllScripts[scriptName].HeaderImage;

        public string GetScriptHash(string scriptName, string apiKey) => Kernel.AllScripts[scriptName].Updated.ToUniversalTime().ToString(CultureInfo.InvariantCulture);

        public List<string> LiveStats(string apiKey) => Kernel.CommandsReceived;

        public List<string> GetMostDownloadedScripts(string apiKey) => Kernel.AllScripts.Where(o => o.Value.Category == Category.MostDownloaded).Select(script => script.Key).ToList();

        public List<string> GetNewestScripts(string apiKey) => Kernel.AllScripts.Where(o => o.Value.Category == Category.New).Select(script => script.Key).ToList();

        public List<string> GetHarmfulScripts(string apiKey) => Kernel.AllScripts.Where(o => o.Value.Category == Category.Harmful).Select(script => script.Key).ToList();

        public List<string> GetMostPopularScripts(string apiKey) => Kernel.AllScripts.Where(o => o.Value.Category == Category.Popular).Select(script => script.Key).ToList();

        public List<string> GetFeaturedScripts(string apiKey) => Kernel.AllScripts.Where(o => o.Value.Category == Category.Featured).Select(script => script.Key).ToList();

        public List<string> GetUpdatedScripts(string apiKey) => Kernel.AllScripts.Where(o => o.Value.Category == Category.Updated).Select(script => script.Key).ToList();
    }
}