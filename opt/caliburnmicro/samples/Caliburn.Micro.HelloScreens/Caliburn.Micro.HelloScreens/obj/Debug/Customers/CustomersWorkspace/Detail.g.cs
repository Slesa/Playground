﻿#pragma checksum "D:\sources\Playground\opt\caliburnmicro_45e393aa9c1e\samples\Caliburn.Micro.HelloScreens\Caliburn.Micro.HelloScreens\Customers\CustomersWorkspace\Detail.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "075062A46E1D7ADF1E62CAB72784C088"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Caliburn.Micro.HelloScreens.Customers.CustomersWorkspace {
    
    
    public partial class Detail : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.TransitioningContentControl ActiveItem;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Caliburn.Micro.HelloScreens;component/Customers/CustomersWorkspace/Detail.xaml", System.UriKind.Relative));
            this.ActiveItem = ((System.Windows.Controls.TransitioningContentControl)(this.FindName("ActiveItem")));
        }
    }
}

