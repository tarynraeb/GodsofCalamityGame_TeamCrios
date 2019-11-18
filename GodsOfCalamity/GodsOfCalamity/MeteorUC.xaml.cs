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
    public sealed partial class MeteorUC : UserControl
    {
        public bool destroyMe = false;

        public MeteorUC()
        {
            this.InitializeComponent();
        }

        //private void MeteorLookHandler(object sender, PointerRoutedEventArgs e)
        //{
        //    //damage The meteor
        //}

        //private void MeteorLookerLeft(object sender, PointerRoutedEventArgs e)
        //{
        //    //Does nothing, just here to confirm that event stopped
        //}

        private void GazeElement_StateChanged(object sender, StateChangedEventArgs e)
        {
            destroyMe = true;
        }

        private void OnInvokeProgress(object sender, DwellProgressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
