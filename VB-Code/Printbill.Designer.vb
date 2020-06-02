<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Printbill
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.table = New System.Windows.Forms.TableLayoutPanel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.grid = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.table)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.grid)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(450, 262)
        Me.Panel1.TabIndex = 0
        '
        'table
        '
        Me.table.ColumnCount = 1
        Me.table.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.table.Location = New System.Drawing.Point(361, 61)
        Me.table.Name = "table"
        Me.table.RowCount = 6
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.table.Size = New System.Drawing.Size(86, 192)
        Me.table.TabIndex = 5
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(361, 32)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Print Preview"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(361, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Print"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'grid
        '
        Me.grid.AllowUserToAddRows = False
        Me.grid.AllowUserToDeleteRows = False
        Me.grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grid.Location = New System.Drawing.Point(3, 3)
        Me.grid.Name = "grid"
        Me.grid.ReadOnly = True
        Me.grid.Size = New System.Drawing.Size(352, 250)
        Me.grid.TabIndex = 2
        '
        'Printbill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 262)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Printbill"
        Me.Text = "Printbill"
        Me.Panel1.ResumeLayout(False)
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents grid As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Private WithEvents table As System.Windows.Forms.TableLayoutPanel








End Class
