﻿#pragma checksum "..\..\..\UpdateToolWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4D4EC7BC769D09B2CED369ED27C632680245CC3C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using RENT_A_TOOL;
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


namespace RENT_A_TOOL {
    
    
    /// <summary>
    /// UpdateToolWindow
    /// </summary>
    public partial class UpdateToolWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\UpdateToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NazwaTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\UpdateToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OpisTextBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\UpdateToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StanMagazynowyTextBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\UpdateToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button WybierzZdjecieButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\UpdateToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PreviewImage;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\UpdateToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EdytujButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RENT_A_TOOL;component/updatetoolwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UpdateToolWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NazwaTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.OpisTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.StanMagazynowyTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.WybierzZdjecieButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\UpdateToolWindow.xaml"
            this.WybierzZdjecieButton.Click += new System.Windows.RoutedEventHandler(this.WybierzZdjecieButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PreviewImage = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.EdytujButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\UpdateToolWindow.xaml"
            this.EdytujButton.Click += new System.Windows.RoutedEventHandler(this.EdytujButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

