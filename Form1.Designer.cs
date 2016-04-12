namespace apprentissageSUP
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.pBoxOut = new System.Windows.Forms.PictureBox();
            this.openD = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnLearn = new System.Windows.Forms.Button();
            this.btnCLearn = new System.Windows.Forms.Button();
            this.lClearn = new System.Windows.Forms.Label();
            this.nNbIt = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lC1 = new System.Windows.Forms.Label();
            this.nNbNeu = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nAlpha = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lState = new System.Windows.Forms.Label();
            this.cbScreen = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nNbIt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nNbNeu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // pBoxOut
            // 
            this.pBoxOut.Location = new System.Drawing.Point(12, 102);
            this.pBoxOut.Name = "pBoxOut";
            this.pBoxOut.Size = new System.Drawing.Size(500, 500);
            this.pBoxOut.TabIndex = 0;
            this.pBoxOut.TabStop = false;
            // 
            // openD
            // 
            this.openD.Location = new System.Drawing.Point(12, 12);
            this.openD.Name = "openD";
            this.openD.Size = new System.Drawing.Size(75, 23);
            this.openD.TabIndex = 2;
            this.openD.Text = "open";
            this.openD.UseVisualStyleBackColor = true;
            this.openD.Click += new System.EventHandler(this.openD_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // btnLearn
            // 
            this.btnLearn.Location = new System.Drawing.Point(187, 12);
            this.btnLearn.Name = "btnLearn";
            this.btnLearn.Size = new System.Drawing.Size(75, 23);
            this.btnLearn.TabIndex = 3;
            this.btnLearn.Text = "learn";
            this.btnLearn.UseVisualStyleBackColor = true;
            this.btnLearn.Click += new System.EventHandler(this.btnLearn_Click);
            // 
            // btnCLearn
            // 
            this.btnCLearn.Location = new System.Drawing.Point(552, 12);
            this.btnCLearn.Name = "btnCLearn";
            this.btnCLearn.Size = new System.Drawing.Size(75, 23);
            this.btnCLearn.TabIndex = 4;
            this.btnCLearn.Text = "cLearn";
            this.btnCLearn.UseVisualStyleBackColor = true;
            this.btnCLearn.Click += new System.EventHandler(this.btnCLearn_Click);
            // 
            // lClearn
            // 
            this.lClearn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lClearn.Location = new System.Drawing.Point(552, 38);
            this.lClearn.Name = "lClearn";
            this.lClearn.Size = new System.Drawing.Size(75, 23);
            this.lClearn.TabIndex = 5;
            // 
            // nNbIt
            // 
            this.nNbIt.Location = new System.Drawing.Point(13, 40);
            this.nNbIt.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nNbIt.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nNbIt.Name = "nNbIt";
            this.nNbIt.Size = new System.Drawing.Size(74, 20);
            this.nNbIt.TabIndex = 6;
            this.nNbIt.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(93, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "nb iterations";
            // 
            // lC1
            // 
            this.lC1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lC1.Location = new System.Drawing.Point(552, 102);
            this.lC1.Name = "lC1";
            this.lC1.Size = new System.Drawing.Size(194, 201);
            this.lC1.TabIndex = 8;
            // 
            // nNbNeu
            // 
            this.nNbNeu.Location = new System.Drawing.Point(12, 66);
            this.nNbNeu.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nNbNeu.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nNbNeu.Name = "nNbNeu";
            this.nNbNeu.Size = new System.Drawing.Size(74, 20);
            this.nNbNeu.TabIndex = 9;
            this.nNbNeu.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(92, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "nb de neuronnes par couche cachée";
            // 
            // nAlpha
            // 
            this.nAlpha.DecimalPlaces = 3;
            this.nAlpha.Location = new System.Drawing.Point(302, 39);
            this.nAlpha.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nAlpha.Name = "nAlpha";
            this.nAlpha.Size = new System.Drawing.Size(74, 20);
            this.nAlpha.TabIndex = 11;
            this.nAlpha.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(382, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "paramètre d\'apprentissage";
            // 
            // lState
            // 
            this.lState.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lState.ForeColor = System.Drawing.Color.DarkRed;
            this.lState.Location = new System.Drawing.Point(548, 339);
            this.lState.Name = "lState";
            this.lState.Size = new System.Drawing.Size(198, 23);
            this.lState.TabIndex = 13;
            // 
            // cbScreen
            // 
            this.cbScreen.AutoSize = true;
            this.cbScreen.Location = new System.Drawing.Point(633, 41);
            this.cbScreen.Name = "cbScreen";
            this.cbScreen.Size = new System.Drawing.Size(85, 17);
            this.cbScreen.TabIndex = 14;
            this.cbScreen.Text = "Screenshots";
            this.cbScreen.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 614);
            this.Controls.Add(this.cbScreen);
            this.Controls.Add(this.lState);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nAlpha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nNbNeu);
            this.Controls.Add(this.lC1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nNbIt);
            this.Controls.Add(this.lClearn);
            this.Controls.Add(this.btnCLearn);
            this.Controls.Add(this.btnLearn);
            this.Controls.Add(this.openD);
            this.Controls.Add(this.pBoxOut);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pBoxOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nNbIt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nNbNeu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pBoxOut;
        private System.Windows.Forms.Button openD;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnLearn;
        private System.Windows.Forms.Button btnCLearn;
        private System.Windows.Forms.Label lClearn;
        private System.Windows.Forms.NumericUpDown nNbIt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lC1;
        private System.Windows.Forms.NumericUpDown nNbNeu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nAlpha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lState;
        private System.Windows.Forms.CheckBox cbScreen;
    }
}

