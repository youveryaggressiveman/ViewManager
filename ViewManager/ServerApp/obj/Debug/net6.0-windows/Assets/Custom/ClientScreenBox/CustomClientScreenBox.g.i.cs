﻿#pragma checksum "..\..\..\..\..\..\Assets\Custom\ClientScreenBox\CustomClientScreenBox.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0D14E0A9208A3D548E6F95EA012353A579AE3464"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ServerApp.Assets.Custom.ClientScreenBox;
using ServerApp.Core.Clients;
using ServerApp.Core.Screen;
using ServerApp.Properties.Lang;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ServerApp.Assets.Custom.ClientScreenBox {
    
    
    /// <summary>
    /// CustomClientScreenBox
    /// </summary>
    public partial class CustomClientScreenBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 50 "..\..\..\..\..\..\Assets\Custom\ClientScreenBox\CustomClientScreenBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitButton;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\..\..\Assets\Custom\ClientScreenBox\CustomClientScreenBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\..\..\Assets\Custom\ClientScreenBox\CustomClientScreenBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run pcNameRun;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ServerApp;V1.0.0.0;component/assets/custom/clientscreenbox/customclientscreenbox" +
                    ".xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Assets\Custom\ClientScreenBox\CustomClientScreenBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 46 "..\..\..\..\..\..\Assets\Custom\ClientScreenBox\CustomClientScreenBox.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DragWindow);
            
            #line default
            #line hidden
            return;
            case 2:
            this.exitButton = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\..\..\..\Assets\Custom\ClientScreenBox\CustomClientScreenBox.xaml"
            this.exitButton.Click += new System.Windows.RoutedEventHandler(this.exitButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.pcNameRun = ((System.Windows.Documents.Run)(target));
            return;
            case 5:
            
            #line 114 "..\..\..\..\..\..\Assets\Custom\ClientScreenBox\CustomClientScreenBox.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.exitButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

