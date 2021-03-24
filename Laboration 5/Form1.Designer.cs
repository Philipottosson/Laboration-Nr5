
namespace Laboration_5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_URL = new System.Windows.Forms.Button();
            this.txt_URL = new System.Windows.Forms.TextBox();
            this.label_URL = new System.Windows.Forms.Label();
            this.txt_multibox = new System.Windows.Forms.TextBox();
            this.btn_saveAll = new System.Windows.Forms.Button();
            this.label_count = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_URL
            // 
            this.btn_URL.Location = new System.Drawing.Point(514, 33);
            this.btn_URL.Name = "btn_URL";
            this.btn_URL.Size = new System.Drawing.Size(75, 23);
            this.btn_URL.TabIndex = 0;
            this.btn_URL.Text = "Extract";
            this.btn_URL.UseVisualStyleBackColor = true;
            this.btn_URL.Click += new System.EventHandler(this.btn_URL_Click);
            // 
            // txt_URL
            // 
            this.txt_URL.Location = new System.Drawing.Point(86, 34);
            this.txt_URL.Name = "txt_URL";
            this.txt_URL.Size = new System.Drawing.Size(408, 23);
            this.txt_URL.TabIndex = 1;
            this.txt_URL.Text = "https://gp.se";
            // 
            // label_URL
            // 
            this.label_URL.AutoSize = true;
            this.label_URL.Location = new System.Drawing.Point(40, 37);
            this.label_URL.Name = "label_URL";
            this.label_URL.Size = new System.Drawing.Size(22, 15);
            this.label_URL.TabIndex = 2;
            this.label_URL.Text = "Url";
            // 
            // txt_multibox
            // 
            this.txt_multibox.Location = new System.Drawing.Point(86, 81);
            this.txt_multibox.Multiline = true;
            this.txt_multibox.Name = "txt_multibox";
            this.txt_multibox.Size = new System.Drawing.Size(408, 334);
            this.txt_multibox.TabIndex = 1;
            // 
            // btn_saveAll
            // 
            this.btn_saveAll.Location = new System.Drawing.Point(514, 81);
            this.btn_saveAll.Name = "btn_saveAll";
            this.btn_saveAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_saveAll.Size = new System.Drawing.Size(75, 23);
            this.btn_saveAll.TabIndex = 3;
            this.btn_saveAll.Text = "Save all";
            this.btn_saveAll.UseVisualStyleBackColor = true;
            this.btn_saveAll.Click += new System.EventHandler(this.btn_saveAll_Click);
            // 
            // label_count
            // 
            this.label_count.AutoSize = true;
            this.label_count.Location = new System.Drawing.Point(86, 432);
            this.label_count.Name = "label_count";
            this.label_count.Size = new System.Drawing.Size(84, 15);
            this.label_count.TabIndex = 4;
            this.label_count.Text = "Images saved: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 459);
            this.Controls.Add(this.label_count);
            this.Controls.Add(this.btn_saveAll);
            this.Controls.Add(this.label_URL);
            this.Controls.Add(this.txt_multibox);
            this.Controls.Add(this.txt_URL);
            this.Controls.Add(this.btn_URL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_URL;
        private System.Windows.Forms.TextBox txt_URL;
        private System.Windows.Forms.Label label_URL;
        private System.Windows.Forms.TextBox txt_multibox;
        private System.Windows.Forms.Button btn_saveAll;
        private System.Windows.Forms.Label label_count;
    }
}

