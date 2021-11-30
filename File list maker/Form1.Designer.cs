
namespace File_list_maker
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.colName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colAttributes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colCreationDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colModificationDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colLastAccessDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnOpenList = new System.Windows.Forms.Button();
            this.btnSaveList = new System.Windows.Forms.Button();
            this.btnMakeDriveList = new System.Windows.Forms.Button();
            this.lblDateCreated = new System.Windows.Forms.Label();
            this.btnClearList = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxDrives1 = new File_list_maker.ComboBoxDrives();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.colName);
            this.treeListView1.AllColumns.Add(this.colSize);
            this.treeListView1.AllColumns.Add(this.colType);
            this.treeListView1.AllColumns.Add(this.colAttributes);
            this.treeListView1.AllColumns.Add(this.colCreationDate);
            this.treeListView1.AllColumns.Add(this.colModificationDate);
            this.treeListView1.AllColumns.Add(this.colLastAccessDate);
            this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView1.CellEditUseWholeCell = false;
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colSize,
            this.colType,
            this.colAttributes,
            this.colCreationDate,
            this.colModificationDate,
            this.colLastAccessDate});
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeListView1.FullRowSelect = true;
            this.treeListView1.HideSelection = false;
            this.treeListView1.Location = new System.Drawing.Point(8, 50);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(780, 359);
            this.treeListView1.TabIndex = 0;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            this.treeListView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeListView1_MouseDoubleClick);
            // 
            // colName
            // 
            this.colName.AspectName = "DisplayName";
            this.colName.IsEditable = false;
            this.colName.Text = "Список файлов";
            this.colName.Width = 525;
            // 
            // colSize
            // 
            this.colSize.AspectName = "Size";
            this.colSize.IsEditable = false;
            this.colSize.Text = "Размер";
            this.colSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSize.Width = 130;
            // 
            // colType
            // 
            this.colType.AspectName = "ItemType";
            this.colType.Text = "Тип";
            this.colType.Width = 70;
            // 
            // colAttributes
            // 
            this.colAttributes.AspectName = "AttributesString";
            this.colAttributes.Text = "Аттрибуты";
            this.colAttributes.Width = 100;
            // 
            // colCreationDate
            // 
            this.colCreationDate.AspectName = "DateCreated";
            this.colCreationDate.Text = "Создан";
            this.colCreationDate.Width = 130;
            // 
            // colModificationDate
            // 
            this.colModificationDate.AspectName = "DateModified";
            this.colModificationDate.Text = "Изменён";
            this.colModificationDate.Width = 130;
            // 
            // colLastAccessDate
            // 
            this.colLastAccessDate.AspectName = "DateLastAccess";
            this.colLastAccessDate.Text = "Последний доступ";
            this.colLastAccessDate.Width = 140;
            // 
            // btnOpenList
            // 
            this.btnOpenList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenList.Location = new System.Drawing.Point(8, 415);
            this.btnOpenList.Name = "btnOpenList";
            this.btnOpenList.Size = new System.Drawing.Size(75, 23);
            this.btnOpenList.TabIndex = 1;
            this.btnOpenList.Text = "Открыть";
            this.toolTip1.SetToolTip(this.btnOpenList, "Загрузить список из файла");
            this.btnOpenList.UseVisualStyleBackColor = true;
            this.btnOpenList.Click += new System.EventHandler(this.btnOpenList_Click);
            // 
            // btnSaveList
            // 
            this.btnSaveList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveList.Location = new System.Drawing.Point(89, 415);
            this.btnSaveList.Name = "btnSaveList";
            this.btnSaveList.Size = new System.Drawing.Size(75, 23);
            this.btnSaveList.TabIndex = 3;
            this.btnSaveList.Text = "Сохранить";
            this.toolTip1.SetToolTip(this.btnSaveList, "Сохранить список в файл");
            this.btnSaveList.UseVisualStyleBackColor = true;
            this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
            // 
            // btnMakeDriveList
            // 
            this.btnMakeDriveList.Location = new System.Drawing.Point(8, 23);
            this.btnMakeDriveList.Name = "btnMakeDriveList";
            this.btnMakeDriveList.Size = new System.Drawing.Size(135, 21);
            this.btnMakeDriveList.TabIndex = 5;
            this.btnMakeDriveList.Text = "Создать список диска";
            this.toolTip1.SetToolTip(this.btnMakeDriveList, "Создать список файлов выбранного диска");
            this.btnMakeDriveList.UseVisualStyleBackColor = true;
            this.btnMakeDriveList.Click += new System.EventHandler(this.btnMakeDriveList_Click);
            // 
            // lblDateCreated
            // 
            this.lblDateCreated.AutoSize = true;
            this.lblDateCreated.Location = new System.Drawing.Point(8, 7);
            this.lblDateCreated.Name = "lblDateCreated";
            this.lblDateCreated.Size = new System.Drawing.Size(76, 13);
            this.lblDateCreated.TabIndex = 6;
            this.lblDateCreated.Text = "Создан: N / A";
            // 
            // btnClearList
            // 
            this.btnClearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearList.Location = new System.Drawing.Point(713, 415);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(75, 23);
            this.btnClearList.TabIndex = 7;
            this.btnClearList.Text = "Очистить";
            this.toolTip1.SetToolTip(this.btnClearList, "Опустошить список");
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // comboBoxDrives1
            // 
            this.comboBoxDrives1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDrives1.FormattingEnabled = true;
            this.comboBoxDrives1.Location = new System.Drawing.Point(152, 23);
            this.comboBoxDrives1.Name = "comboBoxDrives1";
            this.comboBoxDrives1.Size = new System.Drawing.Size(212, 21);
            this.comboBoxDrives1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.lblDateCreated);
            this.Controls.Add(this.btnMakeDriveList);
            this.Controls.Add(this.comboBoxDrives1);
            this.Controls.Add(this.btnSaveList);
            this.Controls.Add(this.btnOpenList);
            this.Controls.Add(this.treeListView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "Form1";
            this.Text = "File list maker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.TreeListView treeListView1;
        private System.Windows.Forms.Button btnOpenList;
        private BrightIdeasSoftware.OLVColumn colName;
        private System.Windows.Forms.Button btnSaveList;
        private ComboBoxDrives comboBoxDrives1;
        private System.Windows.Forms.Button btnMakeDriveList;
        private System.Windows.Forms.Label lblDateCreated;
        private BrightIdeasSoftware.OLVColumn colSize;
        private BrightIdeasSoftware.OLVColumn colAttributes;
        private BrightIdeasSoftware.OLVColumn colCreationDate;
        private BrightIdeasSoftware.OLVColumn colModificationDate;
        private System.Windows.Forms.Button btnClearList;
        private BrightIdeasSoftware.OLVColumn colType;
        private BrightIdeasSoftware.OLVColumn colLastAccessDate;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

