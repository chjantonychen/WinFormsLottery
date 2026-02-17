namespace WinFormsLottery;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        
        // 设置窗体
        this.Text = "🎲 WinForms 抽奖程序";
        this.Size = new System.Drawing.Size(900, 650);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
        this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        
        // 创建主容器
        var mainPanel = new System.Windows.Forms.TableLayoutPanel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new System.Windows.Forms.Padding(20)
        };
        mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
        mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
        
        // ===== 左侧面板：参与者管理 =====
        var leftPanel = CreateParticipantsPanel();
        mainPanel.Controls.Add(leftPanel, 0, 0);
        
        // ===== 右侧面板：抽奖和获奖者 =====
        var rightPanel = CreateLotteryPanel();
        mainPanel.Controls.Add(rightPanel, 1, 0);
        
        this.Controls.Add(mainPanel);
    }

    private System.Windows.Forms.Panel CreateParticipantsPanel()
    {
        var panel = new System.Windows.Forms.Panel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            BackColor = System.Drawing.Color.White,
            Padding = new System.Windows.Forms.Padding(15)
        };
        panel.Paint += (s, e) => DrawRoundedBorder(e.Graphics, panel.ClientRectangle);

        var layout = new System.Windows.Forms.TableLayoutPanel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1
        };
        layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
        layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
        layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));

        // 标题
        var lblTitle = new System.Windows.Forms.Label
        {
            Text = "👥 参与者管理",
            Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(51, 51, 51),
            Dock = System.Windows.Forms.DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        };
        layout.Controls.Add(lblTitle, 0, 0);

        // 输入区域
        var inputPanel = new System.Windows.Forms.FlowLayoutPanel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight
        };
        
        txtName = new System.Windows.Forms.TextBox
        {
            Width = 180,
            Height = 35,
            Font = new System.Drawing.Font("微软雅黑", 11F),
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
            Margin = new System.Windows.Forms.Padding(0, 5, 10, 5)
        };
        txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
        
        btnAdd = CreateStyledButton("➕ 添加", System.Drawing.Color.FromArgb(76, 175, 80));
        btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
        
        inputPanel.Controls.Add(txtName);
        inputPanel.Controls.Add(btnAdd);
        layout.Controls.Add(inputPanel, 0, 1);

        // 参与者列表
        listBoxParticipants = new System.Windows.Forms.ListBox
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            Font = new System.Drawing.Font("微软雅黑", 11F),
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
            SelectionMode = System.Windows.Forms.SelectionMode.One,
            IntegralHeight = false
        };
        layout.Controls.Add(listBoxParticipants, 0, 2);

        // 底部按钮
        var bottomPanel = new System.Windows.Forms.FlowLayoutPanel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight
        };
        
        lblParticipantCount = new System.Windows.Forms.Label
        {
            Text = "共 0 人",
            AutoSize = true,
            Font = new System.Drawing.Font("微软雅黑", 10F),
            ForeColor = System.Drawing.Color.Gray,
            Margin = new System.Windows.Forms.Padding(0, 10, 20, 0)
        };
        
        btnRemove = CreateStyledButton("➖ 移除", System.Drawing.Color.FromArgb(255, 152, 0));
        btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
        
        btnClear = CreateStyledButton("🗑️ 清空", System.Drawing.Color.FromArgb(244, 67, 54));
        btnClear.Click += new System.EventHandler(this.btnClear_Click);
        
        bottomPanel.Controls.Add(lblParticipantCount);
        bottomPanel.Controls.Add(btnRemove);
        bottomPanel.Controls.Add(btnClear);
        layout.Controls.Add(bottomPanel, 0, 3);

        panel.Controls.Add(layout);
        return panel;
    }

    private System.Windows.Forms.Panel CreateLotteryPanel()
    {
        var panel = new System.Windows.Forms.Panel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            BackColor = System.Drawing.Color.White,
            Padding = new System.Windows.Forms.Padding(15),
            Margin = new System.Windows.Forms.Padding(20, 0, 0, 0)
        };
        panel.Paint += (s, e) => DrawRoundedBorder(e.Graphics, panel.ClientRectangle);

        var layout = new System.Windows.Forms.TableLayoutPanel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 1
        };
        layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
        layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
        layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

        // 标题
        var lblTitle = new System.Windows.Forms.Label
        {
            Text = "🎰 抽奖区域",
            Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(51, 51, 51),
            Dock = System.Windows.Forms.DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        };
        layout.Controls.Add(lblTitle, 0, 0);

        // 抽奖按钮区域
        var drawPanel = new System.Windows.Forms.TableLayoutPanel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 2,
            Padding = new System.Windows.Forms.Padding(0, 10, 0, 10)
        };
        for (int i = 0; i < 3; i++)
            drawPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
        drawPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        drawPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));

        btnDrawFirst = CreatePrizeButton("🏆 抽取一等奖", System.Drawing.Color.FromArgb(255, 215, 0), 1);
        btnDrawFirst.Click += new System.EventHandler(this.btnDrawFirst_Click);
        
        btnDrawSecond = CreatePrizeButton("🥈 抽取二等奖", System.Drawing.Color.FromArgb(192, 192, 192), 2);
        btnDrawSecond.Click += new System.EventHandler(this.btnDrawSecond_Click);
        
        btnDrawThird = CreatePrizeButton("🥉 抽取三等奖", System.Drawing.Color.FromArgb(205, 127, 50), 3);
        btnDrawThird.Click += new System.EventHandler(this.btnDrawThird_Click);

        drawPanel.Controls.Add(btnDrawFirst, 0, 0);
        drawPanel.Controls.Add(btnDrawSecond, 1, 0);
        drawPanel.Controls.Add(btnDrawThird, 2, 0);
        
        // 重置按钮
        btnReset = CreateStyledButton("🔄 重置所有数据", System.Drawing.Color.FromArgb(96, 125, 139));
        btnReset.Dock = System.Windows.Forms.DockStyle.Fill;
        btnReset.Click += new System.EventHandler(this.btnReset_Click);
        drawPanel.SetColumnSpan(btnReset, 3);
        drawPanel.Controls.Add(btnReset, 0, 1);

        layout.Controls.Add(drawPanel, 0, 1);

        // 获奖者列表
        var winnerPanel = new System.Windows.Forms.Panel
        {
            Dock = System.Windows.Forms.DockStyle.Fill
        };
        
        var winnerLayout = new System.Windows.Forms.TableLayoutPanel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1
        };
        winnerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
        winnerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

        var winnerHeader = new System.Windows.Forms.Panel
        {
            Dock = System.Windows.Forms.DockStyle.Fill
        };
        
        var lblWinnerTitle = new System.Windows.Forms.Label
        {
            Text = "🏆 获奖者名单",
            Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(51, 51, 51),
            Dock = System.Windows.Forms.DockStyle.Left,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            Width = 150
        };
        
        btnClearWinners = CreateStyledButton("清空获奖名单", System.Drawing.Color.FromArgb(158, 158, 158));
        btnClearWinners.Dock = System.Windows.Forms.DockStyle.Right;
        btnClearWinners.Width = 120;
        btnClearWinners.Click += new System.EventHandler(this.btnClearWinners_Click);
        
        winnerHeader.Controls.Add(lblWinnerTitle);
        winnerHeader.Controls.Add(btnClearWinners);
        
        winnerLayout.Controls.Add(winnerHeader, 0, 0);

        listBoxWinners = new System.Windows.Forms.ListBox
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            Font = new System.Drawing.Font("微软雅黑", 11F),
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
            IntegralHeight = false,
            BackColor = System.Drawing.Color.FromArgb(255, 250, 240)
        };
        winnerLayout.Controls.Add(listBoxWinners, 0, 1);

        winnerPanel.Controls.Add(winnerLayout);
        layout.Controls.Add(winnerPanel, 0, 2);

        panel.Controls.Add(layout);
        return panel;
    }

    private System.Windows.Forms.Button CreateStyledButton(string text, System.Drawing.Color backColor)
    {
        return new System.Windows.Forms.Button
        {
            Text = text,
            BackColor = backColor,
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold),
            FlatStyle = System.Windows.Forms.FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Size = new System.Drawing.Size(90, 35),
            Cursor = System.Windows.Forms.Cursors.Hand,
            Margin = new System.Windows.Forms.Padding(5)
        };
    }

    private System.Windows.Forms.Button CreatePrizeButton(string text, System.Drawing.Color baseColor, int level)
    {
        var button = new System.Windows.Forms.Button
        {
            Text = text,
            Dock = System.Windows.Forms.DockStyle.Fill,
            Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold),
            FlatStyle = System.Windows.Forms.FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Margin = new System.Windows.Forms.Padding(8),
            Cursor = System.Windows.Forms.Cursors.Hand
        };

        // 渐变效果
        button.Paint += (s, e) =>
        {
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                button.ClientRectangle,
                baseColor,
                System.Drawing.Color.FromArgb(
                    Math.Max(baseColor.R - 30, 0),
                    Math.Max(baseColor.G - 30, 0),
                    Math.Max(baseColor.B - 30, 0)),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, button.ClientRectangle);
            }
            
            // 绘制文字
            var textSize = e.Graphics.MeasureString(button.Text, button.Font);
            using (var brush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
            {
                e.Graphics.DrawString(button.Text, button.Font, brush,
                    (button.Width - textSize.Width) / 2,
                    (button.Height - textSize.Height) / 2);
            }
        };

        return button;
    }

    private void DrawRoundedBorder(System.Drawing.Graphics g, System.Drawing.Rectangle rect)
    {
        using (var path = new System.Drawing.Drawing2D.GraphicsPath())
        {
            int radius = 10;
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            
            using (var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(224, 224, 224), 1))
            {
                g.DrawPath(pen, path);
            }
        }
    }

    // 控件声明
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.ListBox listBoxParticipants;
    private System.Windows.Forms.ListBox listBoxWinners;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnDrawFirst;
    private System.Windows.Forms.Button btnDrawSecond;
    private System.Windows.Forms.Button btnDrawThird;
    private System.Windows.Forms.Button btnClearWinners;
    private System.Windows.Forms.Button btnReset;
    private System.Windows.Forms.Label lblParticipantCount;
}
