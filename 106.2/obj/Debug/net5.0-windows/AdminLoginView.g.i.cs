﻿#pragma checksum "..\..\..\AdminLoginView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A4423FAB6ACFD9E3D6FF5EA20F4C671F23E01A6D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using _106._2;


namespace _106._2 {
    
    
    /// <summary>
    /// AdminLoginView
    /// </summary>
    public partial class AdminLoginView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid membersdatagrid;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBOX;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MembernumberBOX;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SearchOptionBOX;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBOX;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PhonenumberBOX;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker JoinDataBOX;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddressBOX;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailBOX;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddMemberBUTTON;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\AdminLoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeInfoBUTTON;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/106.2;V1.0.0.0;component/adminloginview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AdminLoginView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.membersdatagrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 26 "..\..\..\AdminLoginView.xaml"
            this.membersdatagrid.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.membersdatagrid_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SearchBOX = ((System.Windows.Controls.TextBox)(target));
            
            #line 65 "..\..\..\AdminLoginView.xaml"
            this.SearchBOX.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchBOX_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MembernumberBOX = ((System.Windows.Controls.TextBox)(target));
            
            #line 68 "..\..\..\AdminLoginView.xaml"
            this.MembernumberBOX.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.MembernumberBOX_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SearchOptionBOX = ((System.Windows.Controls.ComboBox)(target));
            
            #line 71 "..\..\..\AdminLoginView.xaml"
            this.SearchOptionBOX.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SearchOptionBOX_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.NameBOX = ((System.Windows.Controls.TextBox)(target));
            
            #line 81 "..\..\..\AdminLoginView.xaml"
            this.NameBOX.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NameBOX_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PhonenumberBOX = ((System.Windows.Controls.TextBox)(target));
            
            #line 84 "..\..\..\AdminLoginView.xaml"
            this.PhonenumberBOX.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.PhonenumberBOX_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.JoinDataBOX = ((System.Windows.Controls.DatePicker)(target));
            
            #line 87 "..\..\..\AdminLoginView.xaml"
            this.JoinDataBOX.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.JoinDataBOX_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.AddressBOX = ((System.Windows.Controls.TextBox)(target));
            
            #line 90 "..\..\..\AdminLoginView.xaml"
            this.AddressBOX.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.AddressBOX_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.EmailBOX = ((System.Windows.Controls.TextBox)(target));
            
            #line 93 "..\..\..\AdminLoginView.xaml"
            this.EmailBOX.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.EmailBOX_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.AddMemberBUTTON = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\..\AdminLoginView.xaml"
            this.AddMemberBUTTON.Click += new System.Windows.RoutedEventHandler(this.AddMemberBUTTON_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ChangeInfoBUTTON = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\AdminLoginView.xaml"
            this.ChangeInfoBUTTON.Click += new System.Windows.RoutedEventHandler(this.ChangeInfoBUTTON_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

