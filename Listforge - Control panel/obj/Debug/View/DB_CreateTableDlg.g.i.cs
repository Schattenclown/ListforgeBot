﻿#pragma checksum "..\..\..\View\DB_CreateTableDlg.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ED6C83F1F87BFDFF3E821C17FCB049E8E1D8D4BBD9DB652EEBDBC8BC44902FCC"
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
    /// DB_CreateTableDlg
    /// </summary>
    public partial class DB_CreateTableDlg : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\View\DB_CreateTableDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LF_ServerInfoLive;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\View\DB_CreateTableDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LF_API_Uri;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\View\DB_CreateTableDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TL_USerdata;
        
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
            System.Uri resourceLocater = new System.Uri("/Listforge - Control panel;component/view/db_createtabledlg.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\DB_CreateTableDlg.xaml"
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
            this.LF_ServerInfoLive = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\View\DB_CreateTableDlg.xaml"
            this.LF_ServerInfoLive.Click += new System.Windows.RoutedEventHandler(this.LF_ServerInfoLive_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LF_API_Uri = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\View\DB_CreateTableDlg.xaml"
            this.LF_API_Uri.Click += new System.Windows.RoutedEventHandler(this.LF_API_Uri_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TL_USerdata = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\View\DB_CreateTableDlg.xaml"
            this.TL_USerdata.Click += new System.Windows.RoutedEventHandler(this.TL_USerdata_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

