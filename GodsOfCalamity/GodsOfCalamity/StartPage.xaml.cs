using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GodsOfCalamity
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();      

            App.ScreenWidth = Window.Current.Bounds.Width;
            App.ScreenHeight = Window.Current.Bounds.Height;

            Windows.UI.Core.CoreWindow.GetForCurrentThread().KeyDown += new Windows.Foundation.TypedEventHandler<Windows.UI.Core.CoreWindow, Windows.UI.Core.KeyEventArgs>(delegate (Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args) {
                GazeInput.GetGazePointer(this).Click();
            });

            var sharedSettings = new ValueSet();
            GazeSettingsHelper.RetrieveSharedSettings(sharedSettings).Completed = new AsyncActionCompletedHandler((asyncInfo, asyncStatus) =>
            {
                GazeInput.LoadSettings(sharedSettings);
            });

        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage));
        }

        
    }
}
