namespace CardReaderService
{
    partial class CardReaderServiceProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cardReaderServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.cardReaderServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // cardReaderServiceProcessInstaller
            // 
            this.cardReaderServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.cardReaderServiceProcessInstaller.Password = null;
            this.cardReaderServiceProcessInstaller.Username = null;
            // 
            // cardReaderServiceInstaller
            // 
            this.cardReaderServiceInstaller.Description = "Browser HTTP Request - CardReader Serial Port Bridge";
            this.cardReaderServiceInstaller.DisplayName = "CardReader Service";
            this.cardReaderServiceInstaller.ServiceName = "CardReaderService";
            // 
            // CardReaderServiceProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.cardReaderServiceInstaller,
            this.cardReaderServiceProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller cardReaderServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller cardReaderServiceInstaller;
    }
}