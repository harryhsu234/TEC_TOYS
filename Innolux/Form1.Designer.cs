using System;
using System.Collections;
using System.Windows.Forms;

namespace INNOLUX_DB
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.ivdt_from = new System.Windows.Forms.DateTimePicker();
            this.ivdt_to = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbGUI_NO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAccountsCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbREV_NO = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.taProg = new System.Windows.Forms.TextBox();
            this.cbHideRCW = new System.Windows.Forms.CheckBox();
            this.cbHideNoGUI = new System.Windows.Forms.CheckBox();
            this.tbOldCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFastSelection = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.Font = new System.Drawing.Font("新細明體-ExtB", 28F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(282, 237);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(236, 86);
            this.button2.TabIndex = 1;
            this.button2.Text = "發射";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ivdt_from
            // 
            this.ivdt_from.CustomFormat = "yyyy/MM/dd";
            this.ivdt_from.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ivdt_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ivdt_from.Location = new System.Drawing.Point(117, 107);
            this.ivdt_from.Name = "ivdt_from";
            this.ivdt_from.Size = new System.Drawing.Size(183, 31);
            this.ivdt_from.TabIndex = 4;
            // 
            // ivdt_to
            // 
            this.ivdt_to.CustomFormat = "yyyy/MM/dd";
            this.ivdt_to.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ivdt_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ivdt_to.Location = new System.Drawing.Point(341, 107);
            this.ivdt_to.Name = "ivdt_to";
            this.ivdt_to.Size = new System.Drawing.Size(177, 31);
            this.ivdt_to.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "帳單日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(306, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "至";
            // 
            // tbGUI_NO
            // 
            this.tbGUI_NO.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbGUI_NO.Location = new System.Drawing.Point(117, 157);
            this.tbGUI_NO.Name = "tbGUI_NO";
            this.tbGUI_NO.Size = new System.Drawing.Size(135, 31);
            this.tbGUI_NO.TabIndex = 9;
            this.tbGUI_NO.TextChanged += new System.EventHandler(this.tbGUI_NO_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(12, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "發票號碼";
            // 
            // tbAccountsCode
            // 
            this.tbAccountsCode.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbAccountsCode.Location = new System.Drawing.Point(117, 59);
            this.tbAccountsCode.Name = "tbAccountsCode";
            this.tbAccountsCode.Size = new System.Drawing.Size(112, 31);
            this.tbAccountsCode.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(12, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "客戶代號";
            // 
            // tbREV_NO
            // 
            this.tbREV_NO.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbREV_NO.Location = new System.Drawing.Point(383, 157);
            this.tbREV_NO.Name = "tbREV_NO";
            this.tbREV_NO.Size = new System.Drawing.Size(135, 31);
            this.tbREV_NO.TabIndex = 13;
            this.tbREV_NO.TextChanged += new System.EventHandler(this.tbREV_NO_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(278, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "收據號碼";
            // 
            // taProg
            // 
            this.taProg.Location = new System.Drawing.Point(537, 13);
            this.taProg.Multiline = true;
            this.taProg.Name = "taProg";
            this.taProg.ReadOnly = true;
            this.taProg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.taProg.Size = new System.Drawing.Size(280, 310);
            this.taProg.TabIndex = 15;
            this.taProg.WordWrap = false;
            // 
            // cbHideRCW
            // 
            this.cbHideRCW.AutoSize = true;
            this.cbHideRCW.Checked = true;
            this.cbHideRCW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideRCW.Font = new System.Drawing.Font("新細明體", 12F);
            this.cbHideRCW.Location = new System.Drawing.Point(12, 299);
            this.cbHideRCW.Name = "cbHideRCW";
            this.cbHideRCW.Size = new System.Drawing.Size(111, 24);
            this.cbHideRCW.TabIndex = 16;
            this.cbHideRCW.Text = "過濾沖銷";
            this.cbHideRCW.UseVisualStyleBackColor = true;
            // 
            // cbHideNoGUI
            // 
            this.cbHideNoGUI.AutoSize = true;
            this.cbHideNoGUI.Checked = true;
            this.cbHideNoGUI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideNoGUI.Font = new System.Drawing.Font("新細明體", 12F);
            this.cbHideNoGUI.Location = new System.Drawing.Point(12, 269);
            this.cbHideNoGUI.Name = "cbHideNoGUI";
            this.cbHideNoGUI.Size = new System.Drawing.Size(151, 24);
            this.cbHideNoGUI.TabIndex = 17;
            this.cbHideNoGUI.Text = "過濾未開發票";
            this.cbHideNoGUI.UseVisualStyleBackColor = true;
            // 
            // tbOldCode
            // 
            this.tbOldCode.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbOldCode.Location = new System.Drawing.Point(383, 59);
            this.tbOldCode.Name = "tbOldCode";
            this.tbOldCode.Size = new System.Drawing.Size(135, 31);
            this.tbOldCode.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(258, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "客戶舊代碼";
            // 
            // cbFastSelection
            // 
            this.cbFastSelection.Font = new System.Drawing.Font("新細明體", 12F);
            this.cbFastSelection.FormattingEnabled = true;
            this.cbFastSelection.Location = new System.Drawing.Point(117, 13);
            this.cbFastSelection.Name = "cbFastSelection";
            this.cbFastSelection.Size = new System.Drawing.Size(401, 28);
            this.cbFastSelection.TabIndex = 18;
            this.cbFastSelection.SelectedIndexChanged += new System.EventHandler(this.cbFastSelection_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(12, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "快速選擇";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 337);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbFastSelection);
            this.Controls.Add(this.cbHideNoGUI);
            this.Controls.Add(this.cbHideRCW);
            this.Controls.Add(this.taProg);
            this.Controls.Add(this.tbREV_NO);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbAccountsCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbGUI_NO);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ivdt_to);
            this.Controls.Add(this.ivdt_from);
            this.Controls.Add(this.tbOldCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "群創費用轉出程式 v20170808";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker ivdt_from;
        private System.Windows.Forms.DateTimePicker ivdt_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbGUI_NO;
        private System.Windows.Forms.Label label4;
        private TextBox tbAccountsCode;
        private Label label5;
        private TextBox tbREV_NO;
        private Label label6;
        private TextBox taProg;
        private CheckBox cbHideRCW;
        private CheckBox cbHideNoGUI;
        private TextBox tbOldCode;
        private Label label1;
        private ComboBox cbFastSelection;
        private Label label7;
    }
}

