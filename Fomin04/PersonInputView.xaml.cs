﻿using System;
using System.Windows.Controls;

namespace Fomin04
{
    /// <summary>
    /// Interaction logic for PersonInputView.xaml
    /// </summary>
    internal partial class PersonInputView : UserControl
    {
        public PersonInputView(Action usersViewAction, Action<bool> loaderAction)
        {
            InitializeComponent();
            DataContext = new PersonInputViewModel(usersViewAction, loaderAction);
        }
    }
}
