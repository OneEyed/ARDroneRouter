namespace ARDroneRouter
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
            this.lstRouter = new System.Windows.Forms.ListBox();
            this.lstDrone = new System.Windows.Forms.ListBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.lblRouter = new System.Windows.Forms.Label();
            this.lblDrone = new System.Windows.Forms.Label();
            this.txtDroneAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnBind = new System.Windows.Forms.Button();
            this.lblRouterAddress = new System.Windows.Forms.Label();
            this.txtRouterAddress = new System.Windows.Forms.TextBox();
            this.rtxtStatus = new System.Windows.Forms.RichTextBox();
            this.lblDroneName = new System.Windows.Forms.Label();
            this.txtDroneName = new System.Windows.Forms.TextBox();
            this.btnDroneProxy = new System.Windows.Forms.Button();
            this.lblProxyIP = new System.Windows.Forms.Label();
            this.txtProxyIP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstRouter
            // 
            this.lstRouter.FormattingEnabled = true;
            this.lstRouter.Location = new System.Drawing.Point(21, 34);
            this.lstRouter.Name = "lstRouter";
            this.lstRouter.Size = new System.Drawing.Size(156, 134);
            this.lstRouter.TabIndex = 0;
            // 
            // lstDrone
            // 
            this.lstDrone.FormattingEnabled = true;
            this.lstDrone.Location = new System.Drawing.Point(183, 34);
            this.lstDrone.Name = "lstDrone";
            this.lstDrone.Size = new System.Drawing.Size(156, 134);
            this.lstDrone.TabIndex = 1;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(110, 174);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(150, 23);
            this.btnScan.TabIndex = 2;
            this.btnScan.Text = "Scan WiFi Networks";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblRouter
            // 
            this.lblRouter.AutoSize = true;
            this.lblRouter.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRouter.Location = new System.Drawing.Point(16, 14);
            this.lblRouter.Name = "lblRouter";
            this.lblRouter.Size = new System.Drawing.Size(118, 19);
            this.lblRouter.TabIndex = 3;
            this.lblRouter.Text = "Select Router:";
            // 
            // lblDrone
            // 
            this.lblDrone.AutoSize = true;
            this.lblDrone.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrone.Location = new System.Drawing.Point(178, 14);
            this.lblDrone.Name = "lblDrone";
            this.lblDrone.Size = new System.Drawing.Size(139, 19);
            this.lblDrone.TabIndex = 4;
            this.lblDrone.Text = "Select AR Drone:";
            // 
            // txtDroneAddress
            // 
            this.txtDroneAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDroneAddress.Location = new System.Drawing.Point(150, 224);
            this.txtDroneAddress.Name = "txtDroneAddress";
            this.txtDroneAddress.Size = new System.Drawing.Size(156, 20);
            this.txtDroneAddress.TabIndex = 5;
            this.txtDroneAddress.Text = "192.168.1.4";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(51, 226);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(93, 13);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Drone IP Address:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(88, 301);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(150, 298);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(156, 20);
            this.txtPassword.TabIndex = 7;
            // 
            // btnBind
            // 
            this.btnBind.Location = new System.Drawing.Point(21, 323);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(150, 23);
            this.btnBind.TabIndex = 9;
            this.btnBind.Text = "Bind Drone to Router";
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.btnBind_Click);
            // 
            // lblRouterAddress
            // 
            this.lblRouterAddress.AutoSize = true;
            this.lblRouterAddress.Location = new System.Drawing.Point(51, 249);
            this.lblRouterAddress.Name = "lblRouterAddress";
            this.lblRouterAddress.Size = new System.Drawing.Size(96, 13);
            this.lblRouterAddress.TabIndex = 11;
            this.lblRouterAddress.Text = "Router IP Address:";
            // 
            // txtRouterAddress
            // 
            this.txtRouterAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRouterAddress.Location = new System.Drawing.Point(150, 247);
            this.txtRouterAddress.Name = "txtRouterAddress";
            this.txtRouterAddress.Size = new System.Drawing.Size(156, 20);
            this.txtRouterAddress.TabIndex = 10;
            this.txtRouterAddress.Text = "192.168.1.254";
            // 
            // rtxtStatus
            // 
            this.rtxtStatus.Location = new System.Drawing.Point(6, 352);
            this.rtxtStatus.Name = "rtxtStatus";
            this.rtxtStatus.Size = new System.Drawing.Size(359, 157);
            this.rtxtStatus.TabIndex = 12;
            this.rtxtStatus.Text = "";
            // 
            // lblDroneName
            // 
            this.lblDroneName.AutoSize = true;
            this.lblDroneName.Location = new System.Drawing.Point(74, 202);
            this.lblDroneName.Name = "lblDroneName";
            this.lblDroneName.Size = new System.Drawing.Size(70, 13);
            this.lblDroneName.TabIndex = 13;
            this.lblDroneName.Text = "Drone Name:";
            // 
            // txtDroneName
            // 
            this.txtDroneName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDroneName.Location = new System.Drawing.Point(150, 201);
            this.txtDroneName.Name = "txtDroneName";
            this.txtDroneName.Size = new System.Drawing.Size(156, 20);
            this.txtDroneName.TabIndex = 14;
            // 
            // btnDroneProxy
            // 
            this.btnDroneProxy.Location = new System.Drawing.Point(189, 323);
            this.btnDroneProxy.Name = "btnDroneProxy";
            this.btnDroneProxy.Size = new System.Drawing.Size(150, 23);
            this.btnDroneProxy.TabIndex = 15;
            this.btnDroneProxy.Text = "Send Info to DroneProxy";
            this.btnDroneProxy.UseVisualStyleBackColor = true;
            this.btnDroneProxy.Click += new System.EventHandler(this.btnDroneProxy_Click);
            // 
            // lblProxyIP
            // 
            this.lblProxyIP.AutoSize = true;
            this.lblProxyIP.Location = new System.Drawing.Point(51, 273);
            this.lblProxyIP.Name = "lblProxyIP";
            this.lblProxyIP.Size = new System.Drawing.Size(90, 13);
            this.lblProxyIP.TabIndex = 17;
            this.lblProxyIP.Text = "Proxy IP Address:";
            // 
            // txtProxyIP
            // 
            this.txtProxyIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProxyIP.Location = new System.Drawing.Point(150, 271);
            this.txtProxyIP.Name = "txtProxyIP";
            this.txtProxyIP.Size = new System.Drawing.Size(156, 20);
            this.txtProxyIP.TabIndex = 16;
            this.txtProxyIP.Text = "192.168.1.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 514);
            this.Controls.Add(this.lblProxyIP);
            this.Controls.Add(this.txtProxyIP);
            this.Controls.Add(this.btnDroneProxy);
            this.Controls.Add(this.txtDroneName);
            this.Controls.Add(this.lblDroneName);
            this.Controls.Add(this.rtxtStatus);
            this.Controls.Add(this.lblRouterAddress);
            this.Controls.Add(this.txtRouterAddress);
            this.Controls.Add(this.btnBind);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtDroneAddress);
            this.Controls.Add(this.lblDrone);
            this.Controls.Add(this.lblRouter);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.lstDrone);
            this.Controls.Add(this.lstRouter);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstRouter;
        private System.Windows.Forms.ListBox lstDrone;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label lblRouter;
        private System.Windows.Forms.Label lblDrone;
        private System.Windows.Forms.TextBox txtDroneAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.Label lblRouterAddress;
        private System.Windows.Forms.TextBox txtRouterAddress;
        private System.Windows.Forms.RichTextBox rtxtStatus;
        private System.Windows.Forms.Label lblDroneName;
        private System.Windows.Forms.TextBox txtDroneName;
        private System.Windows.Forms.Button btnDroneProxy;
        private System.Windows.Forms.Label lblProxyIP;
        private System.Windows.Forms.TextBox txtProxyIP;
    }
}

