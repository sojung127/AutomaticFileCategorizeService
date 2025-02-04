﻿#pragma checksum "..\..\SearchPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "06E148CE55556FD4D9D5E14372A1186C7A8C545E1D693E4F0FF2130959D4115C"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using TagDocx;


namespace TagDocx {
    
    
    /// <summary>
    /// SearchPage
    /// </summary>
    public partial class SearchPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Search;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid SearchResult;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridBarTitle;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TabMenu;
        
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
            System.Uri resourceLocater = new System.Uri("/TagDocx;component/searchpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SearchPage.xaml"
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
            this.Search = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\SearchPage.xaml"
            this.Search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 29 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowTagList);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SearchResult = ((System.Windows.Controls.DataGrid)(target));
            
            #line 35 "..\..\SearchPage.xaml"
            this.SearchResult.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Listbox1_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 35 "..\..\SearchPage.xaml"
            this.SearchResult.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 39 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.menuitem_click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 40 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.filePathCopy);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 41 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.fileDelete);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 42 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.fileMove);
            
            #line default
            #line hidden
            return;
            case 8:
            this.GridBarTitle = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.TabMenu = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            
            #line 70 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BackToMainPage);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 71 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectAll_Exectued);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 72 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeselectAll_Executed);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 73 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.fileDelete);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 74 "..\..\SearchPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.fileCopy);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

