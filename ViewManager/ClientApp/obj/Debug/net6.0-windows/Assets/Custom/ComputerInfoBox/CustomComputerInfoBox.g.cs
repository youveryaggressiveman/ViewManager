﻿#pragma checksum "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "42FE5752A2ECB9CEE0252FAD4C2E63495954A06C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ClientApp.Assets.Custom.ComputerInfoBox;
using ClientApp.Properties.Lang;
using LiveChartsCore.SkiaSharpView.WPF;
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


namespace ClientApp.Assets.Custom.ComputerInfoBox {
    
    
    /// <summary>
    /// CustomComputerInfoBox
    /// </summary>
    public partial class CustomComputerInfoBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock title;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitButton;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run pcNameRun;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollPcInfo;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock descriptionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveChartsCore.SkiaSharpView.WPF.CartesianChart chart;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
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
            System.Uri resourceLocater = new System.Uri("/ClientApp;component/assets/custom/computerinfobox/customcomputerinfobox.xaml", System.UriKind.Relative);
            
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
            
            #line 46 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DragWindow);
            
            #line default
            #line hidden
            return;
            case 2:
            this.title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.exitButton = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
            this.exitButton.Click += new System.Windows.RoutedEventHandler(this.exitButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.pcNameRun = ((System.Windows.Documents.Run)(target));
            return;
            case 5:
            this.scrollPcInfo = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 6:
            this.descriptionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.chart = ((LiveChartsCore.SkiaSharpView.WPF.CartesianChart)(target));
            return;
            case 8:
            this.pcInfoButton = ((System.Windows.Controls.Button)(target));
            
            #line 145 "..\..\..\..\..\..\Assets\Custom\ComputerInfoBox\CustomComputerInfoBox.xaml"
            this.pcInfoButton.Click += new System.Windows.RoutedEventHandler(this.pcInfoButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

