﻿#pragma checksum "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "84B506B7AC3234A964926D3DEEFA31A3E330639F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ServerApp.Assets.Custom.MessageBox;
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


namespace ServerApp.Assets.Custom.MessageBox {
    
    
    /// <summary>
    /// CustomMessageBox
    /// </summary>
    public partial class CustomMessageBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock title;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitButton;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock description;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button secondButton;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button firstButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ServerApp;component/assets/custom/messagebox/custommessagebox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 43 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DragWindow);
            
            #line default
            #line hidden
            return;
            case 2:
            this.title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.exitButton = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
            this.exitButton.Click += new System.Windows.RoutedEventHandler(this.exitButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.description = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.secondButton = ((System.Windows.Controls.Button)(target));
            
            #line 105 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
            this.secondButton.Click += new System.Windows.RoutedEventHandler(this.secondButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.firstButton = ((System.Windows.Controls.Button)(target));
            
            #line 108 "..\..\..\..\..\..\Assets\Custom\MessageBox\CustomMessageBox.xaml"
            this.firstButton.Click += new System.Windows.RoutedEventHandler(this.firstButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

