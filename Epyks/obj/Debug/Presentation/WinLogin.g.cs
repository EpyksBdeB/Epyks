﻿#pragma checksum "..\..\..\Presentation\WinLogin.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5E3F0FCC531C9AE4D4C0A351096EBFB0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Epyks.Presentation {
    
    
    /// <summary>
    /// WinLogin
    /// </summary>
    public partial class WinLogin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\Presentation\WinLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Epyks.Presentation.WinLogin Window;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\Presentation\WinLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtUsername;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Presentation\WinLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox TxtPassword;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Presentation\WinLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnLogin;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Presentation\WinLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnGoRegister;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Presentation\WinLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgLogo;
        
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
            System.Uri resourceLocater = new System.Uri("/Epyks;component/presentation/winlogin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Presentation\WinLogin.xaml"
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
            this.Window = ((Epyks.Presentation.WinLogin)(target));
            return;
            case 2:
            this.TxtUsername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TxtPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 33 "..\..\..\Presentation\WinLogin.xaml"
            this.TxtPassword.GotFocus += new System.Windows.RoutedEventHandler(this.TxtPassword_GotFocus);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\Presentation\WinLogin.xaml"
            this.TxtPassword.LostFocus += new System.Windows.RoutedEventHandler(this.TxtPassword_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnLogin = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.BtnGoRegister = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Presentation\WinLogin.xaml"
            this.BtnGoRegister.Click += new System.Windows.RoutedEventHandler(this.BtnGoRegister_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ImgLogo = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

