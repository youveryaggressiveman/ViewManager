﻿#pragma checksum "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "38F5D2E744D341D1C9706590BDDC58E70812222A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ServerApp.Assets.Custom.ComputerInfoBox;
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


namespace ServerApp.Assets.Custom.ComputerInfoBox {
    
    
    /// <summary>
    /// CustomComputerInfoBox
    /// </summary>
    public partial class CustomComputerInfoBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 51 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock title;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitButton;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run pcNameRun;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock descriptionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pcInfoButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ServerApp;V1.0.0.0;component/assets/custom/computerinfobox/customcomputerinfobox" +
                    ".xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 45 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DragWindow);
            
            #line default
            #line hidden
            return;
            case 2:
            this.title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.exitButton = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
            this.exitButton.Click += new System.Windows.RoutedEventHandler(this.exitButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.pcNameRun = ((System.Windows.Documents.Run)(target));
            return;
            case 5:
            this.descriptionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.pcInfoButton = ((System.Windows.Controls.Button)(target));
            
            #line 134 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
            this.pcInfoButton.Click += new System.Windows.RoutedEventHandler(this.pcInfoButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

