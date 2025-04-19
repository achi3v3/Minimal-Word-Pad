using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
namespace MiniWordPad
{
    public partial class Form1: Form
    {
        private string currentFilePath = null;
        private int savedSelectionStart = 0;
        private int savedSelectionLength = 0;


        public Form1()
        {
            InitializeComponent();
            Text = "Amiri WordPad";
            UpdateStatus("Готов");


            foreach (var size in new float[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 })
            {
                fontSizeComboBox.Items.Add(size);
            }


            foreach (FontFamily font in FontFamily.Families)
            {
                fontComboBox.Items.Add(font.Name);
            }

            fontComboBox.SelectedItem = "Times New Roman";
            fontSizeComboBox.SelectedItem = 12f;

            this.Activated += (s, e) =>
            {
                if (foundPositions.Count > 0)
                    HighlightFoundText(); 
            };

            boldToolStripButton.CheckOnClick = true;
            italicToolStripButton.CheckOnClick = true;
            underlineToolStripButton.CheckOnClick = true;
            alignCenterToolStripButton.CheckOnClick = true;
            alignLeftToolStripButton.CheckOnClick = true;
            alignRightToolStripButton.CheckOnClick = true;

            mainToolStrip.Visible = false;
            panelSearch.Width = 0;
            panelReplace.Width = 0;

        }
        private void fontDefaultToolStripButton_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;

            if (contentRichTextBox.SelectionFont != null)
            {
                fontDialog.Font = contentRichTextBox.SelectionFont;
                fontDialog.Color = contentRichTextBox.SelectionColor;
            }

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                contentRichTextBox.SelectionFont = fontDialog.Font;
                contentRichTextBox.SelectionColor = fontDialog.Color;
            }
        }
        private void contentRichTextBox_TextChanged(object sender, EventArgs e)
        {
            searchText = "";
            foundPositions.Clear();
            currentPosition = 0;
            UpdateStatus();
        }
        private void contentRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }
        private void UpdateStatus(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
                statusLabel.Text = message;

            int index = contentRichTextBox.SelectionStart;
            int line = contentRichTextBox.GetLineFromCharIndex(index);
            int column = index - contentRichTextBox.GetFirstCharIndexOfCurrentLine();
            int charCount = contentRichTextBox.TextLength;

            cursorPoisitionLabel.Text = $"Строка: {line + 1}, Столбец: {column + 1}";
            charCountLabel.Text = $"Символов: {charCount}";
        }
        private void SaveCursorPosition()
        {
            savedSelectionStart = contentRichTextBox.SelectionStart;
            savedSelectionLength = contentRichTextBox.SelectionLength;
        }
        private void RestoreCursorPosition()
        {
            contentRichTextBox.SelectionStart = savedSelectionStart;
            contentRichTextBox.SelectionLength = savedSelectionLength;
            contentRichTextBox.ScrollToCaret();
        }

        // ===== Функции ФАЙЛ =====
        private void ResetFormatting()
        {
            contentRichTextBox.SelectionFont = new Font("Times New Roman", 12f);
            contentRichTextBox.SelectionColor = Color.Black;

            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Left;

            fontComboBox.SelectedItem = "Times New Roman";
            fontSizeComboBox.SelectedItem = 12f;
            searchBox.Text = "";
            textBoxOn.Text = "";
            textBoxWhat.Text = "";
        }
        private void NewFile()
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения?", "Amiri WordPad",
                                          MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    return;
            }
            contentRichTextBox.Clear();
            currentFilePath = null;
            Text = "Amiri WordPad - Новый документ";
            ResetFormatting();
        }
        private void OpenFile()
        {
            if (contentRichTextBox.TextLength > 0 && contentRichTextBox.Modified)
            {
                var result = MessageBox.Show("Сохранить изменения в текущем документе?", "Amiri WordPad",
                                          MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    return;
            }

            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|RTF файлы (*.rtf)|*.rtf";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ResetFormatting();
                     
                        if (openDialog.FileName.EndsWith(".rtf"))
                            contentRichTextBox.LoadFile(openDialog.FileName);
                        else
                            contentRichTextBox.LoadFile(openDialog.FileName, RichTextBoxStreamType.PlainText);

                        currentFilePath = openDialog.FileName;
                        Text = $"Amiri WordPad - {Path.GetFileName(currentFilePath)}";
                        UpdateStatus("Файл открыт");
                        contentRichTextBox.Modified = false; // cбрасываем флаг изменений
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
                SaveFileAs();
            else
            {
                try
                {
                    if (currentFilePath.EndsWith(".rtf"))
                        contentRichTextBox.SaveFile(currentFilePath);
                    else
                        contentRichTextBox.SaveFile(currentFilePath, RichTextBoxStreamType.PlainText);

                    UpdateStatus("Файл сохранен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
            }
        }

        private void SaveFileAs()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|RTF файлы (*.rtf)|*.rtf";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveDialog.FileName;
                    SaveFile();
                    Text = $"Amiri WordPad - {Path.GetFileName(currentFilePath)}";
                }
            }
        }

        // ===== Обработчики меню =====
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
            searchBox.Text = "";
            textBoxOn.Text = "";
            textBoxWhat.Text = "";
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
            searchBox.Text = "";
            textBoxOn.Text = "";
            textBoxWhat.Text = "";
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения перед выходом?", "Simple WordPad",
                                            MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    return;
            }
            Application.Exit();
        }

        // ===== Панель инструментов =====

        private void alignLeftToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
            UpdateStatus("Выравнивание по левому краю");

        }

        private void alignCenterToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            UpdateStatus("Выравнивание по центру");
        }

        private void alignRightToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
            UpdateStatus("Выравнивание по правому краю");
        }

        private string currentSearchText = "";
        private int currentSearchIndex = 0;
        private List<int> searchOccurrences = new List<int>();

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            if (contentRichTextBox.SelectionLength > 0)
            {
                try
                {
                    DataObject data = new DataObject();

                    data.SetText(contentRichTextBox.SelectedText); // простой текст
                    if (contentRichTextBox.SelectedRtf != null)
                    {
                        data.SetData(DataFormats.Rtf, contentRichTextBox.SelectedRtf); // форматирование с РТФ копирования
                    }

                    Clipboard.Clear();
                    Clipboard.SetDataObject(data, true); //access
                    UpdateStatus("Текст скопирован");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка копирования: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выделите текст для копирования", "Уведомление",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText() || Clipboard.ContainsText(TextDataFormat.Rtf))
                {
                    if (Clipboard.ContainsText(TextDataFormat.Rtf))
                    {
                        contentRichTextBox.Paste(DataFormats.GetFormat(DataFormats.Rtf));
                    }
                    else
                    {
                        contentRichTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
                    }
                    UpdateStatus("Текст вставлен");
                }
                else
                {
                    MessageBox.Show("Буфер обмена не содержит текста", "Уведомление",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка вставки: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            if (contentRichTextBox.SelectionLength > 0)
            {
                try
                {
                    DataObject data = new DataObject();

                    data.SetText(contentRichTextBox.SelectedText);
                    if (contentRichTextBox.SelectedRtf != null)
                    {
                        data.SetData(DataFormats.Rtf, contentRichTextBox.SelectedRtf);
                    }

                    Clipboard.Clear();
                    Clipboard.SetDataObject(data, true);

                    contentRichTextBox.SelectedText = "";

                    UpdateStatus("Текст вырезан");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при вырезании: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выделите текст для вырезания", "Инфо",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private string searchText = "";
        private int currentPosition = 0;
        private List<int> foundPositions = new List<int>();
        private Form searchForm;

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            if (panelSearch.Width < 100)
            {
                panelSearch.Width = 150;
                panelReplace.Width = 0;

            }
            else
            {
                panelSearch.Width = 0;
            }
        }
        private void ReplaceToolStripButton_Click(object sender, EventArgs e)
        {
            if (panelReplace.Width < 100)
            {
                panelReplace.Width = 150;
                panelSearch.Width = 0;
            }
            else
            {
                panelReplace.Width = 0;
            }
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            FindNext(searchBox.Text);
        }
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            FindPrevious(searchBox.Text);
        }
        private void buttonNextReplace_Click(object sender, EventArgs e)
        {
            FindNext(textBoxWhat.Text);
        }
        private void buttonPrevReplace_Click(object sender, EventArgs e)
        {
            FindPrevious(textBoxWhat.Text);
        }

        private void FindNext(string text)
        {
            if (text != searchText)
            {
                searchText = text;
                currentPosition = 0;
                FindAllOccurrences();
            }

            if (foundPositions.Count == 0)
            {
                MessageBox.Show("Текст не найден!");
                return;
            }

            currentPosition++;
            if (currentPosition >= foundPositions.Count)
            {
                currentPosition = 0;
            }

            HighlightFoundText();
            contentRichTextBox.Focus();
        }

        private void FindPrevious(string text)
        {
            if (text != searchText)
            {
                searchText = text;
                currentPosition = 0;
                FindAllOccurrences();
            }

            if (foundPositions.Count == 0)
            {
                MessageBox.Show("Текст не найден!");
                return;
            }

            currentPosition -= 1;
            if (currentPosition < 0)
            {
                currentPosition = foundPositions.Count - 1;
            }

            HighlightFoundText();
            contentRichTextBox.Focus();

        }

        private void FindAllOccurrences()
        {
            foundPositions.Clear();
            int index = 0;

            while (index < contentRichTextBox.Text.Length)
            {
                index = contentRichTextBox.Find(
                    searchText,
                    index,
                    RichTextBoxFinds.None
                );

                if (index == -1) break;

                foundPositions.Add(index);
                index += searchText.Length;
            }
        }

        private void HighlightFoundText()
        {
            contentRichTextBox.SelectionLength = 0; // Restart SELECTION

            if (foundPositions.Count > 0 && currentPosition < foundPositions.Count)
            {
                contentRichTextBox.SelectionStart = foundPositions[currentPosition]; // [ XX : XX] .. дефолт выделение
                contentRichTextBox.SelectionLength = searchText.Length;


                contentRichTextBox.ScrollToCaret();
                contentRichTextBox.Refresh();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            contentRichTextBox.Redo();
            UpdateStatus("Действие вперёд");
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            contentRichTextBox.Undo();
            UpdateStatus("Вернуть действие");
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            ReplaceCurrent();
        }
        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            ReplaceAll();
        }
        private void ReplaceCurrent()
        {
            string search = textBoxWhat.Text;
            string replace = textBoxOn.Text;

            if (string.IsNullOrEmpty(search)) return;

            // первое вхождение или текст изменился--- пересчитываем все вхождения
            if (search != searchText || foundPositions.Count == 0)
            {
                searchText = search; 
                currentPosition = 0; 
                foundPositions.Clear(); 
                FindAllOccurrences(); 
            }

            if (foundPositions.Count == 0) return; 

            // get current позицию для замены
            int position = foundPositions[currentPosition];

            contentRichTextBox.SelectionStart = position;
            contentRichTextBox.SelectionLength = search.Length;
            contentRichTextBox.SelectedText = replace;

            UpdateStatus($"Заменено {search} - {replace}");


            foundPositions.Clear();
            FindAllOccurrences(); // peres4et

            currentPosition++;
            if (currentPosition >= foundPositions.Count) currentPosition = 0;

            HighlightFoundText();
        }

        private void ReplaceAll()
        {
            if (!string.IsNullOrEmpty(textBoxWhat.Text))
            {
                ReplaceAllOccurrences(textBoxWhat.Text, textBoxOn.Text);
            }
        }

        private void ReplaceAllOccurrences(string searchText, string replaceText)
        {
            if (string.IsNullOrEmpty(searchText)) return;

            contentRichTextBox.SuspendLayout(); // pause
            int replaceCount = 0;
            int index = 0;

            while (index < contentRichTextBox.TextLength)
            {
                index = contentRichTextBox.Find(
                    searchText,
                    index,
                    RichTextBoxFinds.None
                );

                if (index == -1) break;

                contentRichTextBox.SelectionStart = index;
                contentRichTextBox.SelectionLength = searchText.Length;
                contentRichTextBox.SelectedText = replaceText;

                replaceCount++;
                index += replaceText.Length;
            }

            contentRichTextBox.ResumeLayout();
            UpdateStatus($"Заменено {replaceCount} вхождений");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения перед выходом?", "Amiri WordPad",
                                            MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
    }
}