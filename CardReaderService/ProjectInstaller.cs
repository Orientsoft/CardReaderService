﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace CardReaderService
{
    [RunInstaller(true)]
    public partial class CardReaderServiceProjectInstaller : System.Configuration.Install.Installer
    {
        public CardReaderServiceProjectInstaller()
        {
            InitializeComponent();
        }

        private void cardReaderServiceProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void cardReaderServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
