﻿using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Facebook;
using TweetSharp;
using VOTCClient.Core;
using VOTCClient.Core.Extensions;
using VOTCClient.Core.External.Faroo;
using VOTCClient.Core.Network;
using VOTCClient.Core.Speech;
using VOTCClient.Windows;

namespace VOTCClient.Pages
{
    public partial class Ui
    {
        internal KonamiSequence Sequence = new KonamiSequence();
        //internal readonly Timer TitleUpdater = new Timer();

        public Ui()
        {
            InitializeComponent();
            Kernel.UI = this;
            page_Loaded(null, null);
        }

        public void LoadImage()
        {
            try
            {
                var src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(Kernel.ProfilePicture, UriKind.Absolute);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                Image.Source = src;
            }
            catch { DisplayCmd("Couldn't get the Image from Facebook/Twitter. Check your Internet Connection!");}
        }

        internal async void UpdateMediaProgress(double percent) => await Dispatcher.BeginInvoke(new Action(() => MusicProgressBar.Value = percent), DispatcherPriority.Background);

        internal async void UpdateVolume(int audioLevel) => await Dispatcher.BeginInvoke(new Action(() => ProgressBar1.Value = ProgressBar1.Maximum > audioLevel ? audioLevel : ProgressBar1.Maximum), DispatcherPriority.ApplicationIdle);

        public async void DisplayCmd(string command, bool unknownCommand = true)
        {
            await Dispatcher.BeginInvoke(new Action(() =>
            {
                if (!unknownCommand && InternalSpeechRecognizer.DisplayUnknownCommands || unknownCommand)
                    ListBox.Items.Insert(0,command);
                //ListBox.ScrollIntoView(command);
            }), DispatcherPriority.Background);
            if(Kernel.Tracking)
                Tracking.Add("Command: "+command +" | Unknown: "+ unknownCommand);
        }

        #region Event Handlers
        private async void Faroo_Click(object sender, RoutedEventArgs e) => await Faroo.Test();

        private void Store_Click(object sender, RoutedEventArgs e)
        {
            var s = new Store();
            Kernel.StoreWindow = s;
            s.ShowDialog();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
            Kernel.Window.Content = new Settings();
        }
        private void LoadProfile_Click(object sender, RoutedEventArgs e) => new ProfileLoadWindow().ShowDialog();

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) => MessageBox.Show("Details: " + ListBox.SelectedItem);

        private void Minimize_Click(object sender, RoutedEventArgs e) => Kernel.Window.WindowState = WindowState.Minimized;

        private void About_Click(object sender, RoutedEventArgs e)
        {
            using (var box = new AboutBox())
                box.ShowDialog();
        }
        private void UploadScript_Click(object sender, RoutedEventArgs e)
        {
            var s = new ScriptUploader();
            Kernel.ScriptUploaderWindow = s;
            s.ShowDialog();
        }
        private void Facebook_Click(object sender, RoutedEventArgs e)
        {
            if (Kernel.FacebookClient != null)
                return;
            var box = new FacebookTwitterAuth();
            box.ShowDialog();
        }

        private async void page_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayCmd("Double click any item in here to read its content");
            DisplayCmd("Go to 'File -> Profiles' to load a profile!");
            DisplayCmd("If you're new, go to the store and download one!");
            try
            {
                if (sender != null && e != null)
                {
                    await Task.Run(async () =>
                    {
                        var builder = new StringBuilder();
                        foreach (var script in Directory.GetFiles("Scripts\\"))
                        {
                            var info = new FileInfo(script);
                            var localHash = info.CreationTimeUtc.ToString(CultureInfo.InvariantCulture);
                            var scriptName = Path.GetFileName(script);
                            string remoteHash;
                            try
                            {
                                using (var client = new WebClient())
                                {
                                    remoteHash = await client.DownloadStringTaskAsync(Kernel.RemoteHost + scriptName + ".gethash");
                                }
                            }
                            catch
                            {
                                continue;
                            }
                            var localTime = DateTime.Parse(localHash);
                            var remoteTime = DateTime.Parse(remoteHash);

                            if (localTime >= remoteTime)
                                continue;

                            builder.AppendLine("New version of '" + scriptName + "' available in the store!");
                            if (!Directory.Exists(@"Cache\Store\"))
                                continue;
                            if (File.Exists(@"Cache\Store\" + scriptName?.Replace(".cs", ".badge")))
                                File.Delete(@"Cache\Store\" + scriptName?.Replace(".cs", ".badge"));
                            if (File.Exists(@"Cache\Store\" + scriptName?.Replace(".cs", ".header")))
                                File.Delete(@"Cache\Store\" + scriptName?.Replace(".cs", ".header"));
                        }
                        if (!string.IsNullOrEmpty(builder.ToString()))
                            MessageBox.Show(builder.ToString());
                    });
                }
            }
            catch
            {
                MessageBox.Show("VOTC Master Server offline. No updates could be fetched :(");
            }
            InternalSpeechRecognizer.PrepareSpeech();
            if (string.IsNullOrEmpty(Kernel.FacebookAccessToken))
                return;
            try
            {
                Kernel.FacebookClient = new FacebookClient(Kernel.FacebookAccessToken);
                dynamic friendsTaskResult = await Kernel.FacebookClient.GetTaskAsync("/me");
                Kernel.FacebookName = friendsTaskResult.first_name + " " + friendsTaskResult.last_name;
                dynamic facebookImage = await Kernel.FacebookClient.GetTaskAsync("/me/picture?redirect=0&height=200&type=normal&width=200");
                Kernel.ProfilePicture = facebookImage["data"].url;
                Kernel.UI.LoadImage();
            }
            catch
            {
                //Ignored
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Kernel.TwitterClient != null)
                return;

            if (string.IsNullOrEmpty(Kernel.TwitterSecret) || string.IsNullOrEmpty(Kernel.TwitterToken))
            {
                var box = new FacebookTwitterAuth {Content = new TwitterAuthPage()};
                box.ShowDialog();
            }
            else
            {
                Kernel.TwitterClient = new TwitterService("bcPdqSrMo2zSoLRjvE20Z2B93", "2DPgdd0qqnpOxbglUMWs4okLjNL3IMgbuQezLCgwMr1XH4kYpw");
                Kernel.TwitterClient.AuthenticateWith(Kernel.TwitterToken, Kernel.TwitterSecret);
                Kernel.TwitterClient.UserAgent = "VOTC";
                var user = Kernel.TwitterClient.VerifyCredentials(new VerifyCredentialsOptions {IncludeEntities = true});
                Kernel.TwitterUsername = user.ScreenName;
                Kernel.ProfilePicture = user.ProfileImageUrl.Replace("_normal.", "_400x400.");
                Kernel.UI.LoadImage();
            }
        }

        #endregion

        private void Joystick_Click(object sender, RoutedEventArgs e)
        {
            var jc = new JoystickConfig();
            jc.ShowDialog();
        }

        private void builtin_click(object sender, RoutedEventArgs e)
        {
            var box = new BuiltInCommands();
            box.ShowDialog();
        }

        private void Changelog_Click(object sender, RoutedEventArgs e)
        {
            var box = new Changelog();
            box.ShowDialog();
        }

        private void ChatTest_Click(object sender, RoutedEventArgs e)
        {
            var box = new ChatWindow(Kernel.Window);
            Kernel.ChatWindow = box;
            box.Show();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (ChatBotBox.IsChecked != null) Kernel.ChatBotActive = ChatBotBox.IsChecked.Value;
        }

        private void WallOfFame_Click(object sender, RoutedEventArgs e) => new Contributors().Show();

        private void OverlayTest_Click(object sender, RoutedEventArgs e)
        {
            var box = new Form1();
            box.Show();
        }
    }
}