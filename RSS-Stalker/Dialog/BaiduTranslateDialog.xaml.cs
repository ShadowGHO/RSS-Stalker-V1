﻿using CoreLib.Enums;
using CoreLib.Tools;
using RSS_Stalker.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace RSS_Stalker.Dialog
{
    public sealed partial class BaiduTranslateDialog : ContentDialog
    {
        public BaiduTranslateDialog()
        {
            this.InitializeComponent();
            string oldAppId = AppTools.GetLocalSetting(AppSettings.Translate_BaiduAppId, "");
            string oldKey = AppTools.GetLocalSetting(AppSettings.Translate_BaiduKey, "");
            AppIdTextBox.Text = oldAppId;
            KeyTextBox.Text = oldKey;
            Title = AppTools.GetReswLanguage("Tip_BaiduTranslateDialog");
            PrimaryButtonText = AppTools.GetReswLanguage("Tip_Confirm");
            SecondaryButtonText = AppTools.GetReswLanguage("Tip_Cancel");
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = true;
            IsPrimaryButtonEnabled = false;
            PrimaryButtonText = AppTools.GetReswLanguage("Tip_Waiting");
            string appId = AppIdTextBox.Text;
            string key = KeyTextBox.Text;
            if (!string.IsNullOrEmpty(appId) && !string.IsNullOrEmpty(key))
            {
                AppTools.WriteLocalSetting(AppSettings.Translate_BaiduAppId, appId);
                AppTools.WriteLocalSetting(AppSettings.Translate_BaiduKey, key);
                Hide();
            }
            else
            {
                IsPrimaryButtonEnabled = true;
                PrimaryButtonText = AppTools.GetReswLanguage("Tip_Confirm");
                new PopupToast(AppTools.GetReswLanguage("Tip_FieldEmpty")).ShowPopup();
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://api.fanyi.baidu.com/api/trans/product/index"));
        }
    }
}
