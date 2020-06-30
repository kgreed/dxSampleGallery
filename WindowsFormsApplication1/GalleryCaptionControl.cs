using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
namespace WindowsFormsApplication1
{
    class GalleryCaptionControl :XtraUserControl
    {
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        public string CaptionText { 
            get => label1.Text ;
            set => label1.Text = value;
        }

        public GalleryCaptionControl()
        {
            InitializeComponent();
           
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(159, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // GalleryCaptionControl
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "GalleryCaptionControl";
            this.Size = new System.Drawing.Size(436, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
