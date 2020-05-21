namespace OS_Lab_4
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Корневая директория");
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripAddDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripAddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView = new System.Windows.Forms.TreeView();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.ToolStripDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAddDirectory,
            this.toolStripAddFile,
            this.ToolStripDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(198, 70);
            // 
            // toolStripAddDirectory
            // 
            this.toolStripAddDirectory.Name = "toolStripAddDirectory";
            this.toolStripAddDirectory.Size = new System.Drawing.Size(197, 22);
            this.toolStripAddDirectory.Text = "Добавить директорию";
            this.toolStripAddDirectory.Click += new System.EventHandler(this.ToolStripAddDirectory_Click);
            // 
            // toolStripAddFile
            // 
            this.toolStripAddFile.Name = "toolStripAddFile";
            this.toolStripAddFile.Size = new System.Drawing.Size(197, 22);
            this.toolStripAddFile.Text = "Добавить файл";
            this.toolStripAddFile.Click += new System.EventHandler(this.ToolStripAddFile_Click);
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.Location = new System.Drawing.Point(839, 12);
            this.treeView.Name = "treeView";
            treeNode1.ContextMenuStrip = this.contextMenuStrip;
            treeNode1.Name = "Узел";
            treeNode1.Text = "Корневая директория";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView.Size = new System.Drawing.Size(289, 542);
            this.treeView.TabIndex = 0;
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(821, 542);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // ToolStripDelete
            // 
            this.ToolStripDelete.Name = "ToolStripDelete";
            this.ToolStripDelete.Size = new System.Drawing.Size(197, 22);
            this.ToolStripDelete.Text = "Удалить";
            this.ToolStripDelete.Click += new System.EventHandler(this.ToolStripDelete_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 566);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.treeView);
            this.Name = "FormMain";
            this.Text = "Файловая система";
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripAddDirectory;
        private System.Windows.Forms.ToolStripMenuItem toolStripAddFile;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripMenuItem ToolStripDelete;
    }
}

