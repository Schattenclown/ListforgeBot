﻿#pragma checksum "..\..\..\View\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3AB5E7E8889CB5EDADA6CF88DE7853EB724D4EDDE2027E1AD15F776E631F0FFC"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Listforge_Control_panel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Listforge_Control_panel {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid lbServerAPI;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn Key;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btWeb;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btWebStat;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btWebStatQC;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btAddServer;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btDelServer;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btRefresh;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbHideKeys;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btDB_CreateTable;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Listforge - Control panel;component/view/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\View\MainWindow.xaml"
            ((Listforge_Control_panel.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbServerAPI = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.Key = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 4:
            this.btWeb = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\View\MainWindow.xaml"
            this.btWeb.Click += new System.Windows.RoutedEventHandler(this.btWeb_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btWebStat = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\View\MainWindow.xaml"
            this.btWebStat.Click += new System.Windows.RoutedEventHandler(this.btWebStat_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btWebStatQC = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\View\MainWindow.xaml"
            this.btWebStatQC.Click += new System.Windows.RoutedEventHandler(this.btWebStatQC_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btAddServer = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\View\MainWindow.xaml"
            this.btAddServer.Click += new System.Windows.RoutedEventHandler(this.btAddServer_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btDelServer = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\View\MainWindow.xaml"
            this.btDelServer.Click += new System.Windows.RoutedEventHandler(this.btDelServer_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btRefresh = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\View\MainWindow.xaml"
            this.btRefresh.Click += new System.Windows.RoutedEventHandler(this.btRefresh_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.cbHideKeys = ((System.Windows.Controls.CheckBox)(target));
            
            #line 54 "..\..\..\View\MainWindow.xaml"
            this.cbHideKeys.Checked += new System.Windows.RoutedEventHandler(this.cbHideKeys_Checked);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\View\MainWindow.xaml"
            this.cbHideKeys.Unchecked += new System.Windows.RoutedEventHandler(this.cbHideKeys_Unchecked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btDB_CreateTable = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\View\MainWindow.xaml"
            this.btDB_CreateTable.Click += new System.Windows.RoutedEventHandler(this.btDB_CreateTable_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

