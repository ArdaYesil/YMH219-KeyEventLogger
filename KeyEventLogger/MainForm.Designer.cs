namespace KeyEventLogger;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        btnStart = new Button();
        btnStop = new Button();
        btnClear = new Button();
        btnSave = new Button();
        lblStatus = new Label();
        lblFocus = new Label();
        grpEvents = new GroupBox();
        chkKeyDown = new CheckBox();
        chkKeyUp = new CheckBox();
        chkKeyPress = new CheckBox();
        lstLog = new ListBox();
        lblCount = new Label();
        lblTest = new Label();
        txtTest = new TextBox();
        grpStats = new GroupBox();
        lblTotal = new Label();
        lblTopTitle = new Label();
        lstTopKeys = new ListBox();
        grpFilter = new GroupBox();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        btnFilter = new Button();
        btnClearFilter = new Button();
        lblFilterState = new Label();
        grpEvents.SuspendLayout();
        grpStats.SuspendLayout();
        grpFilter.SuspendLayout();
        SuspendLayout();
        //
        // btnStart
        //
        btnStart.BackColor = Color.FromArgb(46, 125, 50);
        btnStart.FlatStyle = FlatStyle.Flat;
        btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnStart.ForeColor = Color.White;
        btnStart.Location = new Point(12, 12);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(110, 36);
        btnStart.Text = "▶ Başlat";
        btnStart.UseVisualStyleBackColor = false;
        btnStart.Click += BtnStart_Click;
        //
        // btnStop
        //
        btnStop.BackColor = Color.FromArgb(198, 40, 40);
        btnStop.FlatStyle = FlatStyle.Flat;
        btnStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnStop.ForeColor = Color.White;
        btnStop.Location = new Point(128, 12);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(110, 36);
        btnStop.Text = "■ Durdur";
        btnStop.UseVisualStyleBackColor = false;
        btnStop.Click += BtnStop_Click;
        //
        // btnClear
        //
        btnClear.Font = new Font("Segoe UI", 10F);
        btnClear.Location = new Point(244, 12);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(100, 36);
        btnClear.Text = "Temizle";
        btnClear.Click += BtnClear_Click;
        //
        // btnSave
        //
        btnSave.Font = new Font("Segoe UI", 10F);
        btnSave.Location = new Point(350, 12);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(110, 36);
        btnSave.Text = ".txt Kaydet";
        btnSave.Click += BtnSave_Click;
        //
        // lblStatus
        //
        lblStatus.BackColor = Color.DimGray;
        lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblStatus.ForeColor = Color.White;
        lblStatus.Location = new Point(470, 12);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(142, 36);
        lblStatus.Text = "■ KAYIT KAPALI";
        lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        //
        // lblFocus
        //
        lblFocus.ForeColor = Color.DimGray;
        lblFocus.Location = new Point(12, 54);
        lblFocus.Name = "lblFocus";
        lblFocus.Size = new Size(600, 20);
        lblFocus.Text = "ⓘ Yalnızca bu pencere aktifken basılan tuşlar kaydedilir.";
        //
        // grpEvents
        //
        grpEvents.Controls.Add(chkKeyDown);
        grpEvents.Controls.Add(chkKeyUp);
        grpEvents.Controls.Add(chkKeyPress);
        grpEvents.Location = new Point(12, 76);
        grpEvents.Name = "grpEvents";
        grpEvents.Size = new Size(600, 50);
        grpEvents.Text = "Kaydedilecek olaylar";
        //
        // chkKeyDown
        //
        chkKeyDown.AutoSize = true;
        chkKeyDown.Checked = true;
        chkKeyDown.Location = new Point(15, 21);
        chkKeyDown.Name = "chkKeyDown";
        chkKeyDown.Text = "KeyDown (tuşa basma)";
        //
        // chkKeyUp
        //
        chkKeyUp.AutoSize = true;
        chkKeyUp.Checked = true;
        chkKeyUp.Location = new Point(210, 21);
        chkKeyUp.Name = "chkKeyUp";
        chkKeyUp.Text = "KeyUp (tuşu bırakma)";
        //
        // chkKeyPress
        //
        chkKeyPress.AutoSize = true;
        chkKeyPress.Checked = true;
        chkKeyPress.Location = new Point(400, 21);
        chkKeyPress.Name = "chkKeyPress";
        chkKeyPress.Text = "KeyPress (karakter)";
        //
        // lstLog
        //
        lstLog.Font = new Font("Consolas", 9F);
        lstLog.HorizontalScrollbar = true;
        lstLog.IntegralHeight = false;
        lstLog.Location = new Point(12, 134);
        lstLog.Name = "lstLog";
        lstLog.Size = new Size(600, 300);
        //
        // lblCount
        //
        lblCount.Location = new Point(12, 438);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(600, 20);
        lblCount.Text = "Gösterilen: 0  /  Toplam kayıt: 0";
        lblCount.ForeColor = Color.DimGray;
        //
        // lblTest
        //
        lblTest.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTest.Location = new Point(12, 462);
        lblTest.Name = "lblTest";
        lblTest.Size = new Size(600, 20);
        lblTest.Text = "Deneme alanı (kayıt sırasında buraya yazabilirsiniz):";
        //
        // txtTest
        //
        txtTest.AcceptsReturn = true;
        txtTest.AcceptsTab = true;
        txtTest.Font = new Font("Segoe UI", 10F);
        txtTest.Location = new Point(12, 484);
        txtTest.Multiline = true;
        txtTest.Name = "txtTest";
        txtTest.ScrollBars = ScrollBars.Vertical;
        txtTest.Size = new Size(600, 104);
        //
        // grpStats
        //
        grpStats.Controls.Add(lblTotal);
        grpStats.Controls.Add(lblTopTitle);
        grpStats.Controls.Add(lstTopKeys);
        grpStats.Location = new Point(625, 12);
        grpStats.Name = "grpStats";
        grpStats.Size = new Size(263, 290);
        grpStats.Text = "İstatistik";
        //
        // lblTotal
        //
        lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotal.Location = new Point(12, 24);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(240, 22);
        lblTotal.Text = "Toplam tuş basımı: 0";
        //
        // lblTopTitle
        //
        lblTopTitle.Location = new Point(12, 52);
        lblTopTitle.Name = "lblTopTitle";
        lblTopTitle.Size = new Size(240, 20);
        lblTopTitle.Text = "En sık kullanılan 10 tuş:";
        //
        // lstTopKeys
        //
        lstTopKeys.Font = new Font("Consolas", 9.75F);
        lstTopKeys.IntegralHeight = false;
        lstTopKeys.Location = new Point(12, 74);
        lstTopKeys.Name = "lstTopKeys";
        lstTopKeys.SelectionMode = SelectionMode.None;
        lstTopKeys.Size = new Size(239, 204);
        //
        // grpFilter
        //
        grpFilter.Controls.Add(lblFrom);
        grpFilter.Controls.Add(dtpFrom);
        grpFilter.Controls.Add(lblTo);
        grpFilter.Controls.Add(dtpTo);
        grpFilter.Controls.Add(btnFilter);
        grpFilter.Controls.Add(btnClearFilter);
        grpFilter.Controls.Add(lblFilterState);
        grpFilter.Location = new Point(625, 310);
        grpFilter.Name = "grpFilter";
        grpFilter.Size = new Size(263, 278);
        grpFilter.Text = "Tarih / Saat Filtresi";
        //
        // lblFrom
        //
        lblFrom.Location = new Point(12, 26);
        lblFrom.Name = "lblFrom";
        lblFrom.Size = new Size(200, 20);
        lblFrom.Text = "Başlangıç:";
        //
        // dtpFrom
        //
        dtpFrom.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        dtpFrom.Format = DateTimePickerFormat.Custom;
        dtpFrom.Location = new Point(12, 46);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.ShowUpDown = true;
        dtpFrom.Size = new Size(239, 23);
        //
        // lblTo
        //
        lblTo.Location = new Point(12, 78);
        lblTo.Name = "lblTo";
        lblTo.Size = new Size(200, 20);
        lblTo.Text = "Bitiş:";
        //
        // dtpTo
        //
        dtpTo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        dtpTo.Format = DateTimePickerFormat.Custom;
        dtpTo.Location = new Point(12, 98);
        dtpTo.Name = "dtpTo";
        dtpTo.ShowUpDown = true;
        dtpTo.Size = new Size(239, 23);
        //
        // btnFilter
        //
        btnFilter.Location = new Point(12, 136);
        btnFilter.Name = "btnFilter";
        btnFilter.Size = new Size(115, 32);
        btnFilter.Text = "Filtrele";
        btnFilter.Click += BtnFilter_Click;
        //
        // btnClearFilter
        //
        btnClearFilter.Location = new Point(136, 136);
        btnClearFilter.Name = "btnClearFilter";
        btnClearFilter.Size = new Size(115, 32);
        btnClearFilter.Text = "Tümünü Göster";
        btnClearFilter.Click += BtnClearFilter_Click;
        //
        // lblFilterState
        //
        lblFilterState.ForeColor = Color.DimGray;
        lblFilterState.Location = new Point(12, 180);
        lblFilterState.Name = "lblFilterState";
        lblFilterState.Size = new Size(239, 80);
        lblFilterState.Text = "Filtre yok — tüm kayıtlar gösteriliyor.";
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 600);
        Controls.Add(btnStart);
        Controls.Add(btnStop);
        Controls.Add(btnClear);
        Controls.Add(btnSave);
        Controls.Add(lblStatus);
        Controls.Add(lblFocus);
        Controls.Add(grpEvents);
        Controls.Add(lstLog);
        Controls.Add(lblCount);
        Controls.Add(lblTest);
        Controls.Add(txtTest);
        Controls.Add(grpStats);
        Controls.Add(grpFilter);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        KeyPreview = true;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "YMH219 - Key Event Logger (Eğitsel)";
        Activated += MainForm_Activated;
        Deactivate += MainForm_Deactivate;
        KeyDown += MainForm_KeyDown;
        KeyUp += MainForm_KeyUp;
        KeyPress += MainForm_KeyPress;
        grpEvents.ResumeLayout(false);
        grpEvents.PerformLayout();
        grpStats.ResumeLayout(false);
        grpFilter.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btnStart;
    private Button btnStop;
    private Button btnClear;
    private Button btnSave;
    private Label lblStatus;
    private Label lblFocus;
    private GroupBox grpEvents;
    private CheckBox chkKeyDown;
    private CheckBox chkKeyUp;
    private CheckBox chkKeyPress;
    private ListBox lstLog;
    private Label lblCount;
    private Label lblTest;
    private TextBox txtTest;
    private GroupBox grpStats;
    private Label lblTotal;
    private Label lblTopTitle;
    private ListBox lstTopKeys;
    private GroupBox grpFilter;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Button btnFilter;
    private Button btnClearFilter;
    private Label lblFilterState;
}
