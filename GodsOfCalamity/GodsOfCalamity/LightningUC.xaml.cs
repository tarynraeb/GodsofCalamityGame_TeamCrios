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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GodsOfCalamity
{
    public sealed partial class LightningUC : UserControl
    {
        bool damaged = false;
        public bool destroyMe = false;

        public LightningUC()
        {
            this.InitializeComponent();
        }

        private void LightningDwelled(object sender, DwellInvokedRoutedEventArgs e)
        {
            destroyMe = true;
        }

        private void LightningLookHandler(object sender, StateChangedEventArgs e)
        {
            if (damaged)
            {
                FadeStoryboard.Resume();
                //damage the Entity
                //ALso, there is a timer on the XAML for the duration of the animation, might want to set the seconds (third row) to however long is needed for damage health(like 1 damage per second)
            }
            else
            {
                FadeStoryboard.Begin();
                //Damage the Entity at this Point
                //ALso, there is a timer on the XAML for the duration of the animation, might want to set the seconds (third row) to however long is needed for damage health(like 1 damage per second)
                //this is just so the animation can be paused and stopped for the fading
                damaged = true;
            }
        }

        private void OnInvokeProgress(object sender, DwellProgressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
