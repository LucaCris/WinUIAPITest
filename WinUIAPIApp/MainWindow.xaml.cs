// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using APIModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace WinUIAPIApp
{
    public sealed partial class MainWindow : Window
    {
        string token;

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void OnLogin(object sender, RoutedEventArgs e)
        {
            var req = new LoginRequest() { UserName = "Pluto", Password = "Pollo" };
            var res = await req.PostAPI<LoginResponse>("Login");

            if (!res.Error) {
                token = res.Token;
                myButton.Content = "Logged";
            }
        }

        private async void OnContent(object sender, RoutedEventArgs e)
        {
            Msg.Text = "...";
            var req=new ContentRequest() { Token = token };
            var res = await req.PostAPI<ContentResponse>("GetContent");
            if (res.Error) {
                Msg.Text = $"Error: {res.Message}";
            } else
                Msg.Text = res.Content;
        }

        private async void OnContentLock(object sender, RoutedEventArgs e)
        {
            Msg.Text = "...";
            var req = new LockContentRequest() { Token = token, RequireLock = Lock.IsChecked ?? false };
            var res = await req.PostAPI<LockContentResponse>("LockContent");
            if (res.Error) {
                Msg.Text = $"Error: {res.Message}";
            } else
                Msg.Text = $"{res.Content} / Can Modify: {res.CanModifyContent}";
        }
    }
}
