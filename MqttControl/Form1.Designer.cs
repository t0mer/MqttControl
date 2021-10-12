namespace MqttControl
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.StatusIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.MQTTControlMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runAtStarupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableRunAnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.MQTTControlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusIcon
            // 
            this.StatusIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.StatusIcon.ContextMenuStrip = this.MQTTControlMenu;
            this.StatusIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("StatusIcon.Icon")));
            this.StatusIcon.Text = "Mqtt Control";
            this.StatusIcon.Visible = true;
            // 
            // MQTTControlMenu
            // 
            this.MQTTControlMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitServiceToolStripMenuItem,
            this.runAtStarupToolStripMenuItem,
            this.disableRunAnStartupToolStripMenuItem});
            this.MQTTControlMenu.Name = "MQTTControlMenu";
            this.MQTTControlMenu.Size = new System.Drawing.Size(280, 70);
            this.MQTTControlMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MQTTControlMenu_Opening);
            // 
            // exitServiceToolStripMenuItem
            // 
            this.exitServiceToolStripMenuItem.Name = "exitServiceToolStripMenuItem";
            this.exitServiceToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.exitServiceToolStripMenuItem.Text = "Exit MqttControl";
            this.exitServiceToolStripMenuItem.Click += new System.EventHandler(this.exitServiceToolStripMenuItem_Click);
            // 
            // runAtStarupToolStripMenuItem
            // 
            this.runAtStarupToolStripMenuItem.Name = "runAtStarupToolStripMenuItem";
            this.runAtStarupToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.runAtStarupToolStripMenuItem.Text = "Run MqttControl at startup";
            this.runAtStarupToolStripMenuItem.Click += new System.EventHandler(this.runAtStarupToolStripMenuItem_Click);
            // 
            // disableRunAnStartupToolStripMenuItem
            // 
            this.disableRunAnStartupToolStripMenuItem.Name = "disableRunAnStartupToolStripMenuItem";
            this.disableRunAnStartupToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.disableRunAnStartupToolStripMenuItem.Text = "Disable running MqttControl at startup";
            this.disableRunAnStartupToolStripMenuItem.Click += new System.EventHandler(this.disableRunAnStartupToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.Text = "MqttControl";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.MQTTControlMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon StatusIcon;
        private System.Windows.Forms.ContextMenuStrip MQTTControlMenu;
        private System.Windows.Forms.ToolStripMenuItem exitServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runAtStarupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableRunAnStartupToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}

