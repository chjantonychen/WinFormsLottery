using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsLottery;

public partial class MainForm : Form
{
    private List<string> participants = new();
    private List<Winner> winners = new();
    private Random random = new();
    
    // 奖项配置
    private const int FirstPrizeCount = 1;
    private const int SecondPrizeCount = 2;
    private const int ThirdPrizeCount = 3;

    public MainForm()
    {
        InitializeComponent();
        InitializeData();
    }

    private void InitializeData()
    {
        // 初始化示例数据
        var sampleNames = new[] { "张三", "李四", "王五", "赵六", "钱七", "孙八", "周九", "吴十" };
        participants.AddRange(sampleNames);
        UpdateParticipantsList();
    }

    private void UpdateParticipantsList()
    {
        listBoxParticipants.Items.Clear();
        foreach (var participant in participants)
        {
            listBoxParticipants.Items.Add(participant);
        }
        lblParticipantCount.Text = $"共 {participants.Count} 人";
    }

    private void UpdateWinnersList()
    {
        listBoxWinners.Items.Clear();
        foreach (var winner in winners.OrderBy(w => w.PrizeLevel))
        {
            string prizeName = winner.PrizeLevel switch
            {
                1 => "🏆 一等奖",
                2 => "🥈 二等奖",
                3 => "🥉 三等奖",
                _ => "参与奖"
            };
            listBoxWinners.Items.Add($"{prizeName}: {winner.Name}");
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("请输入姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (participants.Contains(name))
        {
            MessageBox.Show("该参与者已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        participants.Add(name);
        txtName.Clear();
        txtName.Focus();
        UpdateParticipantsList();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        if (listBoxParticipants.SelectedItem == null)
        {
            MessageBox.Show("请先选择要移除的参与者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string name = listBoxParticipants.SelectedItem.ToString()!;
        participants.Remove(name);
        UpdateParticipantsList();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        if (participants.Count == 0) return;
        
        var result = MessageBox.Show("确定要清空所有参与者吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            participants.Clear();
            UpdateParticipantsList();
        }
    }

    private void btnDrawFirst_Click(object sender, EventArgs e)
    {
        DrawPrize(1, FirstPrizeCount);
    }

    private void btnDrawSecond_Click(object sender, EventArgs e)
    {
        DrawPrize(2, SecondPrizeCount);
    }

    private void btnDrawThird_Click(object sender, EventArgs e)
    {
        DrawPrize(3, ThirdPrizeCount);
    }

    private void DrawPrize(int prizeLevel, int count)
    {
        var availableParticipants = participants.Where(p => !winners.Any(w => w.Name == p)).ToList();
        
        if (availableParticipants.Count < count)
        {
            MessageBox.Show($"参与者不足！还需要 {count - availableParticipants.Count} 人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // 移除该级别的现有获奖者
        winners.RemoveAll(w => w.PrizeLevel == prizeLevel);

        // 随机抽取
        for (int i = 0; i < count; i++)
        {
            int index = random.Next(availableParticipants.Count);
            string winner = availableParticipants[index];
            winners.Add(new Winner { Name = winner, PrizeLevel = prizeLevel });
            availableParticipants.RemoveAt(index);
        }

        UpdateWinnersList();
        
        string prizeName = prizeLevel switch
        {
            1 => "一等奖",
            2 => "二等奖",
            3 => "三等奖",
            _ => "奖项"
        };
        
        MessageBox.Show($"{prizeName}抽取完成！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnClearWinners_Click(object sender, EventArgs e)
    {
        if (winners.Count == 0) return;
        
        var result = MessageBox.Show("确定要清空所有获奖者吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            winners.Clear();
            UpdateWinnersList();
        }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show("确定要重置所有数据吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            participants.Clear();
            winners.Clear();
            UpdateParticipantsList();
            UpdateWinnersList();
        }
    }

    private void txtName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            btnAdd_Click(sender, e);
        }
    }
}

public class Winner
{
    public string Name { get; set; } = "";
    public int PrizeLevel { get; set; }
}
